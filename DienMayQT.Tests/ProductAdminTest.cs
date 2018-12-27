using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DienMayQT.Controllers;
using DienMayQT.Areas.Admin.Controllers;
using DienMayQT.Models;
using System.Collections.Generic;
using System.Web;

namespace DienMayQT.Tests
{
    [TestClass]
    public class ProductAdminTest
    {
        [TestMethod]
        public void TestIndex()
        {
            var controller = new ProductAdminController();
            var result = controller.Index() as ViewResult;
            var db = new DIENMAYQUYETTIENEntities();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(List<Product>));
            Assert.AreEqual(db.Products.Count(), ((List<Product>)result.Model).Count);
        }

        [TestMethod]
        public void TestCreate1()
        {
            var controller = new ProductAdminController();
            var result = controller.Create() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.ViewData["ProductTypeID"], typeof(SelectList));
        }

        [TestMethod]
        public void TestDetails()
        {
            var controller = new ProductAdminController();
            var context = new Mock<HttpContextBase>();
            context.Setup(c => c.Server.MapPath("~/App_Data/0")).Returns("~/App_Data/0");
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var result = controller.Details("0") as FilePathResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("images", result.ContentType);
            Assert.AreEqual("~/App_Data/0", result.FileName);
        }

        [TestMethod]
        public void TestCreate2()
        {
            var controller = new ProductAdminController();
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var files = new Mock<HttpFileCollectionBase>();
            var file = new Mock<HttpPostedFileBase>();
            context.Setup(c => c.Request).Returns(request.Object);
            request.Setup(r => r.Files).Returns(files.Object);
            files.Setup(f => f["HinhAnh"]).Returns(file.Object);
            file.Setup(f => f.ContentLength).Returns(1);
            context.Setup(c => c.Server.MapPath("~/App_Data")).Returns("~/App_Data");
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var db = new DIENMAYQUYETTIENEntities();
            var model = new Product();
            model.ProductTypeID = db.ProductTypes.First().id;
            model.ProductName = "TenSP";
            model.ProductCode = "MaSP";
            model.OriginPrice = 123;
            model.SalePrice = 456;
            model.InstallmentPrice = 789;
            model.Quantity = 10;

            using (var scope = new TransactionScope())
            {
                var result0 = controller.Create(model) as RedirectToRouteResult;
                Assert.IsNotNull(result0);
                file.Verify(f => f.SaveAs(It.Is<string>(s => s.StartsWith("~/App_Data/"))));
                Assert.AreEqual("Index", result0.RouteValues["action"]);

                file.Setup(f => f.ContentLength).Returns(0);
                var result1 = controller.Create(model) as ViewResult;
                Assert.IsNotNull(result1);
                Assert.IsInstanceOfType(result1.ViewData["Loai_id"], typeof(SelectList));
            }
        }
    }
}
