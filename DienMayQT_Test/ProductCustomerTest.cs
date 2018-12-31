using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DienMayQT;
using DienMayQT.Areas.Admin.Controllers;
using DienMayQT.Models;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using Moq;
using System.Net;
using System.Web;
using System.Web.Routing;
using DienMayQT.Areas.Customer.Controllers;

namespace DienMayQT_Test
{
    [TestClass]
    public class ProductCustomerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
             ProductsCustomerController controller = new ProductsCustomerController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            ProductsCustomerController controller = new ProductsCustomerController();

            // Act
            //ViewResult result = controller.AboutUs() as ViewResult;

            // Assert
            //Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            ProductsCustomerController controller = new ProductsCustomerController();

            // Act
            //ViewResult result = controller.Contact() as ViewResult;

            // Assert
            //Assert.IsNotNull(result);
        }
    }
}
