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
    public class ProductTypesController : Controller
    {
        private DmQT06Entities1 db = new DmQT06Entities1();

        // GET: Customer/ProductTypes
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

        // GET: Customer/ProductTypes/Details/5
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
            return View(product);
        }

        // GET: Customer/ProductTypes/Create
        public ActionResult Create()
        {
            ViewBag.ProductTypeID = new SelectList(db.ProductType, "ID", "ProductTypeCode");
            return View();
        }

        // POST: Customer/ProductTypes/Create
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

        // GET: Customer/ProductTypes/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.ProductTypeID = new SelectList(db.ProductType, "ID", "ProductTypeCode", product.ProductTypeID);
            return View(product);
        }

        // POST: Customer/ProductTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProductCode,ProductName,ProductTypeID,SalePrice,OriginPrice,InstallmentPrice,Quantity,Avatar,Status")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductTypeID = new SelectList(db.ProductType, "ID", "ProductTypeCode", product.ProductTypeID);
            return View(product);
        }

        // GET: Customer/ProductTypes/Delete/5
        public ActionResult Delete(int? id)
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
            return View(product);
        }

        // POST: Customer/ProductTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
