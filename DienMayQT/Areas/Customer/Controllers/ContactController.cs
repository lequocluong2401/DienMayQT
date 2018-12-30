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
    public class ContactController : Controller
    {
        private DmQT06Entities1 db = new DmQT06Entities1();

        // GET: Customer/Contact
        public ActionResult Index()
        {
            var model = new KhuModels();
            model.Product = db.Product.ToList();
            model.ProductType = db.ProductType.ToList();
            return View(model);
        }

        

        // GET: Customer/Contact/Create
        public ActionResult Create()
        {
            ViewBag.ProductTypeID = new SelectList(db.ProductType, "ID", "ProductTypeCode");
            return View();
        }

        // POST: Customer/Contact/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProductCode,ProductName,ProductTypeID,SalePrice,OriginPrice,InstallmentPrice,Quantity,Avatar,Status")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductTypeID = new SelectList(db.ProductType, "ID", "ProductTypeCode", product.ProductTypeID);
            return View(product);
        }
        
    }
}
