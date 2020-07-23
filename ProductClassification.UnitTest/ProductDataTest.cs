using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductClassification.Data;

namespace ProductClassification.UnitTest
{
    [TestClass]
    public class ProductDataTest
    {
        [TestMethod]
        public void GetAll()
        {
            List<Product> products = DataRepository.Product.GetAll();
            // Assert.AreEqual(true, albums.Count > 0);
            Assert.IsTrue(products.Count > 0);
            // Assert 조건을 확인합니다. 조건이 false이면 메시지를 출력하고 호출 스택을 보여주는 메시지 상자를 표시합니다
        }

        [TestMethod]
        public void Get()
        {
            Product product = DataRepository.Product.Get(2);
            Assert.AreEqual(529, product.QRCodeData);
            // AreEqual 지정된 객체가 같은지 테스트하고 두 객체가 같지 않은 경우 예외를 발생시킵니다
        }

        [TestMethod]
        public void GetCount()
        {
            int count = DataRepository.Product.GetCount();
            Assert.IsTrue(count > 0);
        }

        [TestMethod]
        public void Insert()
        {
            int oldCount = DataRepository.Product.GetCount();

            Product product = new Product();
            product.QRCodeData = 5;
            product.CheckedDate = DateTime.Now;
            product.IsDefective = false;

            DataRepository.Product.Insert(product);

            int newCount = DataRepository.Product.GetCount();

            Assert.AreEqual(oldCount + 1, newCount);
        }

        [TestMethod]
        public void Delete()
        {
            Product product = new Product();

            Random num = new Random();

            product.QRCodeData = num.Next();
            product.CheckedDate = DateTime.Now;
            product.IsDefective = false;

            DataRepository.Product.Insert(product);

            int oldCount = DataRepository.Product.GetCount();

            int maxProductId = DataRepository.Product.GetMaxId();
            DataRepository.Product.Delete(maxProductId);

            int newCount = DataRepository.Product.GetCount();

            Assert.AreEqual(oldCount - 1, newCount);
        }

        [TestMethod]
        public void Update()
        {
            Product product = DataRepository.Product.Get(1);
            int oldQRCodeData = product.QRCodeData;
            
            Random num = new Random();

            product.QRCodeData = num.Next();

            DataRepository.Product.Update(product);

            product = DataRepository.Product.Get(1);

            Assert.AreNotEqual(oldQRCodeData, product.QRCodeData);
        }

    }
}
