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
    public class InstallmentbillAdminTest
    {
        [TestMethod]
        public void TestIndexInstallmentBill()
        {
            var controller = new InstallmentBillsController();
            var context = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(c => c.Session).Returns(session.Object);
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            session.Setup(s => s["Username"]).Returns("abc");

            var result = controller.Index() as ViewResult;
            var db = new DmQT06Entities1();


            //Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(List<InstallmentBill>));
            Assert.AreEqual(db.InstallmentBill.Count(), ((List<InstallmentBill>)result.Model).Count);

            session.Setup(s => s["Username"]).Returns(null);
            var redirect = controller.Index() as RedirectToRouteResult;
            //Assert.AreEqual("Login", redirect.RouteValues["controller"]);
            Assert.AreEqual("Login", redirect.RouteValues["action"]);

        }
    }
}
