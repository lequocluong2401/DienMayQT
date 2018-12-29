using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Transactions;
using DienMayQT.Models;
using System.Web.Security;
using System.Data.Entity.Validation;

namespace DienMayQT.Areas.Admin.Controllers
{
    public class ProductTypeController : Controller
    {
        DmQT06Entities1 db = new DmQT06Entities1();
        // GET: Admin/ProductType
        public ActionResult Index()
        {
            var model = db.ProductType.OrderByDescending(x => x.ID).ToList();
            return View(model);
        }

        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductType model)
        {
            checkProductType(model);
            if (ModelState.IsValid)
            {
                db.ProductType.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }
        private void checkProductType(ProductType model) {
            if (model.ProductTypeCode.Length > 3||model.ProductTypeCode.Length <1) {
                ModelState.AddModelError("ProductCode", "Mã sản phẩm phải nhiều hơn 3 ");
            }
        }

        public ActionResult Edit(int id)
        {
            ProductType model = db.ProductType.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductType model)
        {
            checkProductType(model);
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductType model = db.ProductType.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: /Link/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductType model = db.ProductType.Find(id);
            db.ProductType.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}