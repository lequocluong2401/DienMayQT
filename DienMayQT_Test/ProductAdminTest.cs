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

namespace DienMayQT_Test
{
    [TestClass]
    public class ProductAdminTest
    {
        [TestMethod]
        public void TestIndex()
        {
            var controller = new ProductAdminController();
            var context = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(c => c.Session).Returns(session.Object);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            session.Setup(s => s["Username"]).Returns("abc");

            var result = controller.Index() as ViewResult;
            var db = new DmQT06Entities1();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(List<Product>));
            Assert.AreEqual(db.Product.Count(), ((List<Product>)result.Model).Count);
            session.Setup(s => s["Username"]).Returns("null");
        }

        [TestMethod]
        public void CreateTest()
        {
            var controller = new ProductAdminController();
            var db = new DmQT06Entities1();
            var result = controller.Create() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.ViewData["product.ID"], typeof(SelectList));
        }
    }
}
