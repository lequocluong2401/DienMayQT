using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DienMayQT.Models;

namespace DienMayQT.Areas.Admin.Controllers
{
    public class CashBillsAdminController : Controller
    {
        private DIENMAYQUYETTIENEntities db = new DIENMAYQUYETTIENEntities();

        private void Check(CashBill model)
        {
            //if (model.BillCode)
            //    ModelState.AddModelError("GiaGoc", "");
            if (model.CustomerName.Length > 30)
                ModelState.AddModelError("CustomerName", "Tên khách hàng phải ít hơn 30 kí tự!");
            if (model.PhoneNumber.Length < 10)
                ModelState.AddModelError("PhoneNumber", "Số điện thoại phải nhiều hơn 10 kí tự!");
            if (model.Address.Length < 10)
                ModelState.AddModelError("Address", "Địa chỉ phải nhiều hơn 10 kí tự!");
            //if (model.Date)
            //    ModelState.AddModelError("SoLuongTon", "Số lượng tồn phải lớn hơn 0!");
            if (model.Shipper.Length > 30)
                ModelState.AddModelError("Shipper", "Tên Shipper phải ít hơn 30 kí tự!");
            if (model.Note.Length > 100)
                ModelState.AddModelError("Note", "Ghi chú phải ít hơn 100 kí tự!");
            if (model.GrandTotal < 0)
                ModelState.AddModelError("GrandTotal", "Tổng giá tiền phải lớn hơn 0!");
        }

        // GET: Admin/CashBillsAdmin
        public ActionResult Index()
        {
            return View(db.CashBills.ToList());
        }

        // GET: Admin/CashBillsAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashBill cashBill = db.CashBills.Find(id);
            if (cashBill == null)
            {
                return HttpNotFound();
            }
            return View(cashBill);
        }

        // GET: Admin/CashBillsAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CashBillsAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,BillCode,CustomerName,PhoneNumber,Address,Date,Shipper,Note,GrandTotal")] CashBill cashBill)
        {
            Check(cashBill);
            if (ModelState.IsValid)
            {
                db.CashBills.Add(cashBill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cashBill);
        }

        // GET: Admin/CashBillsAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashBill cashBill = db.CashBills.Find(id);
            if (cashBill == null)
            {
                return HttpNotFound();
            }
            return View(cashBill);
        }

        // POST: Admin/CashBillsAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BillCode,CustomerName,PhoneNumber,Address,Date,Shipper,Note,GrandTotal")] CashBill cashBill)
        {
            Check(cashBill);
            if (ModelState.IsValid)
            {
                db.Entry(cashBill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cashBill);
        }

        // GET: Admin/CashBillsAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashBill cashBill = db.CashBills.Find(id);
            if (cashBill == null)
            {
                return HttpNotFound();
            }
            return View(cashBill);
        }

        // POST: Admin/CashBillsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CashBill cashBill = db.CashBills.Find(id);
            db.CashBills.Remove(cashBill);
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
