# 개요
![전체 흐름도](Images/전체_흐름도.png)
QR 코드가 부착된 물품이 오류인지 판별하는 프로그램입니다.
이 프로그램을 통해서 사용자는 날짜별로 정상과 오류 물품의 개수를 확인할 수 있습니다.
(시연 동영상 : https://www.youtube.com/watch?v=1WmiCw99rDA)

# 사용기술

### 언어
* C# 3.0+, Python 3.7

### 프레임워크
* ASP.Net Web API
* Entity Framework
* Winform

### 데이터베이스
* MSSQL Server 2019

### Third Party Control
* DevExpress Winform

# 데이터베이스 테이블
![데이터베이스 테이블](Images/데이터베이스_테이블.png)
isDefective가 true이면 오류 false면 정상 제품입니다.

# 설명

## 1 구현 내용

### 1-1 아두이노 -> 라즈베리 파이
![Serial통신](Images/Serial통신.png)

초음파 센서가 물품을 탐지하면 아두이이노는 라즈베리 파이에 물품의 오류를 판별하라는 신호를 Serial 통신으로 보냅니다.

### 1-2 OpenCv를 이용해서 오류 판별
![오류 판별](Images/오류_판별.png)

라즈베리 파이 카메라 5mp 모듈은 제품에 부착된 QR 코드로 오류를 판별합니다.
QR 코드 데이터는 Python zbar 라이브러리를 이용했습니다.
(저희는 임의로 QR 코드가 4자리가 아니면 오류로 판별했습니다.)

### 1-3 라즈베리 파이 -> Web API
![라즈베리파이 Web A Pi](Images/라즈베리파이_WebAPi.png)

QR 코드 데이터를 Python requests 라이브러리를 이용해 JSON 형식으로 Web API에 전송합니다.

### 1-4 Web API -> DB
![Web Api D B](Images/WebApi_DB.png)

ASP.Net Web API 응용프로그램을 IIS(Internet Information Sevices)에 배포해서 서버를 구축했습니다.
서버에 도착한 QR 코드 데이터는 요청에 따라 DB에 삽입, 삭제, 수정, 읽기를 수행합니다.

### 1-5 DB 데이터 모니터링

![모니터링](Images/모니터링.png)

사용자는 실시간으로 날짜별 정상, 오류 제품의 개수를 모니터링할 수 있습니다.
실시간으로 DB를 모니터링하기 위해 Timer 클래스를 이용해 10초마다 DB에 저장된 물품의 개수를 확인했습니다.


 


