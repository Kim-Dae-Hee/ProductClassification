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
            
            Assert.IsTrue(products.Count > 0);
        }

        [TestMethod]
        public void Get()
        {
            Product product = DataRepository.Product.Get(2);

            Assert.AreEqual(529, product.QRCodeData);
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
