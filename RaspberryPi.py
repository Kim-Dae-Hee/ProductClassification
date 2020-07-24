from pyzbar.pyzbar import decode
from ftplib import FTP
import os
import numpy as np
import cv2
import time
from picamera.array import PiRGBArray
from picamera import PiCamera
import requests
import json
import serial

fourcc = cv2.VideoWriter_fourcc(*'X264')

camera=PiCamera()
camera.resolution=(240,240)
camera.framerate=20
rawCapture=PiRGBArray(camera)
time.sleep(0.1)
cv2.namedWindow("Image",cv2.WINDOW_NORMAL)

def getQRcodeInformations(image):
     QRcodes = decode(image)
     for QRcode in QRcodes:
        (x, y, w, h) = QRcode.rect
        cv2.rectangle(image,(x, y),(x + w, y + h),(0, 0, 255),2)
        QRcodeData = QRcode.data.decode("utf-8")
        QRcodeType = QRcode.type
        return(QRcodeType,QRcodeData,True)
     return('','',False)

def insertQRcodeData(QRcodeData, isDefective):
    address = "http://10.10.16.145/ProductClassification.Api/api/Product"
    headers = {"content-type":'application/json'}
    user_data = {'IsDefective':isDefective, 'QRCodeData':QRcodeData}
    response = requests.post(address, data = json.dumps(user_data), headers = headers)
    print(response.text)

def runCamera():
    for frame in camera.capture_continuous(rawCapture,format="bgr",use_video_port=True):
        image=frame.array
        cv2.imshow("Image",image)
        QRcodeType,QRcodeData,isGetQRcodeData = getQRcodeInformations(image)
        print(QRcodeData)
        if isGetQRcodeData == True:
            if len(QRcodeData) == 4:
                insertQRcodeData(QRcodeData, 0)
            else:
                insertQRcodeData(QRcodeData, 1)
            rawCapture.truncate(0)
            cv2.destroyAllWindows()
            return
        
        #print(QRcodeType)
        #print(QRcodeData)
        rawCapture.truncate(0)
        
        if cv2.waitKey(2) & 0xFF == ord('q'):
                    break
        
    #cap.release()
    #cv2.destroyAllWindows()

try:
    serialFromArduino = serial.Serial('/dev/ttyUSB0',9600)
    print ('Arduino Connected')
    serialFromArduino.flushInput()
    while True:
        value = int(serialFromArduino.readline())
        print(type(value), value)
        if value == 1:
            runCamera()
            value = 0
        
except:
    print ('Arduino not, connected')


