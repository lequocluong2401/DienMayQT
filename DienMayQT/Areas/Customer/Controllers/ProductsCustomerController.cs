using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DienMayQT.Models;

namespace DienMayQT.Areas.Customer.Controllers
{
    public class ProductsCustomerController : Controller
    {
        private DmQT06Entities1 db = new DmQT06Entities1();

        // GET: Customer/ProductsCustomer
        public ActionResult Index()
        {
            var model = new KhuModels();
            model.Product = db.Product.ToList();
            model.ProductType = db.ProductType.ToList();
            return View(model);
        }

        // GET: /BangSanPham/Details/5
        public FileResult GetImgSrc(int id)
        {
            var path = Server.MapPath("~/App_Data/" + id);
            return File(path, "images");
        }

        public ActionResult Category(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductType type = db.ProductType.Find(id);
            if (type == null)
            {
                return HttpNotFound();
            }

            var model = new KhuModels();
            model.Product = db.Product.ToList();
            model.ProductType = db.ProductType.ToList();
            model.Category = type;
            return View(model);
        }

        // GET: Customer/ProductsCustomer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            var model = new KhuModels();
            model.Product = db.Product.ToList();
            model.ProductType = db.ProductType.ToList();
            model.Product1 = product;
            return View(model);
        }
    }
}
