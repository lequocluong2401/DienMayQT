using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DienMayQT.Models;
using EntityState = System.Data.Entity.EntityState;

namespace DienMayQT.Areas.Admin.Controllers
{
    public class InstallmentBillsController : Controller
    {
        private DmQT06Entities1 db = new DmQT06Entities1();

        private void Check(InstallmentBill model)
        {
            //if (model.BillCode.Length < 1)
            //    ModelState.AddModelError("BillCode", "Mã hóa đơn phải dài hơn 1 kí từ");
            if (model.CustomerID > 30)
                ModelState.AddModelError("CustomerID", "Mã khách hàng phải ít hơn 30 kí tự!");
            //if (model.Date)
            //    ModelState.AddModelError("SoLuongTon", "Số lượng tồn phải lớn hơn 0!");
            if (model.Shipper.Length > 30)
                ModelState.AddModelError("Shipper", "Tên Shipper phải ít hơn 30 kí tự!");
            if (model.Note.Length > 100)
                ModelState.AddModelError("Note", "Ghi chú phải ít hơn 100 kí tự!");
            if (model.Method.Length < 5)
                ModelState.AddModelError("Method", "Hình thức thanh toán phải nhiều hơn 10 kí tự!");
            //if (model.Period < 1)
            //    ModelState.AddModelError("Period", "Chu kì phải nhiều hơn 1!");
            if (model.GrandTotal < 0)
                ModelState.AddModelError("GrandTotal", "Tổng giá tiền phải lớn hơn 0!");
            //if (model.Taken < 0)
            //    ModelState.AddModelError("Taken", "Tổng giá tiền phải lớn hơn 0!");
            //if (model.Remain < 0)
            //    ModelState.AddModelError("Remain", "Tổng giá tiền phải lớn hơn 0!");
        }

        // GET: Admin/InstallmentBills
        public ActionResult Index()
        {
            var installmentBills = db.InstallmentBill.Include(i => i.Customer);
            return View(installmentBills.ToList());
        }

        
        // GET: Admin/InstallmentBills/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstallmentBill installmentBill = db.InstallmentBill.Find(id);
            if (installmentBill == null)
            {
                return HttpNotFound();
            }
            return View(installmentBill);
        }

        // GET: Admin/InstallmentBills/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customer, "ID", "CustomerCode");
            return View();
        }

        // POST: Admin/InstallmentBills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,BillCode,CustomerID,Date,Shipper,Note,Method,Period,GrandTotal,Taken,Remain")] InstallmentBill installmentBill)
        {
            Check(installmentBill);
            if (ModelState.IsValid)
            {
                db.InstallmentBill.Add(installmentBill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customer, "ID", "CustomerCode", installmentBill.CustomerID);
            return View(installmentBill);
        }

        // GET: Admin/InstallmentBills/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstallmentBill installmentBill = db.InstallmentBill.Find(id);
            if (installmentBill == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customer, "ID", "CustomerCode", installmentBill.CustomerID);
            return View(installmentBill);
        }

        // POST: Admin/InstallmentBills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BillCode,CustomerID,Date,Shipper,Note,Method,Period,GrandTotal,Taken,Remain")] InstallmentBill installmentBill)
        {
            Check(installmentBill);
            if (ModelState.IsValid)
            {
                db.Entry(installmentBill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customer, "ID", "CustomerCode", installmentBill.CustomerID);
            return View(installmentBill);
        }

        // GET: Admin/InstallmentBills/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstallmentBill installmentBill = db.InstallmentBill.Find(id);
            if (installmentBill == null)
            {
                return HttpNotFound();
            }
            return View(installmentBill);
        }

        // POST: Admin/InstallmentBills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InstallmentBill installmentBill = db.InstallmentBill.Find(id);
            db.InstallmentBill.Remove(installmentBill);
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
