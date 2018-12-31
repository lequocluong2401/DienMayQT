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
<<<<<<< HEAD
            //if (model.BillCode.Length < 1)
            //    ModelState.AddModelError("BillCode", "Mã hóa đơn phải dài hơn 1 kí từ");
            if (model.CustomerID > 30)
                ModelState.AddModelError("CustomerID", "Mã khách hàng phải ít hơn 30 kí tự!");
=======
            
            if (model.CustomerID > 20)
                ModelState.AddModelError("CustomerID", "Mã khách hàng phải ít hơn 20 kí tự!");
            if (model.Customer.CustomerName.Length > 50)

                ModelState.AddModelError("CustomerName", "Tên khách hàng phải ít hơn 50 kí tự!");
            if (model.Customer.YearOfBirth <= 0)
                ModelState.AddModelError("YearOfBirth", "Năm sinh của khách hàng phải lớn hơn 0!");
            if (model.Customer.PhoneNumber.Length <= 0)
                ModelState.AddModelError("PhoneNumber", "Số điện thoại phải nhiều hơn 0 kí tự!");
>>>>>>> 966e81826e24a555c6839cf21a0bc69b10353c5a
            //if (model.Date)
            //    ModelState.AddModelError("Shipper", "Tên Shipper phải ít hơn 30 kí tự!");
            if (model.Customer.Address.Length > 100)
                ModelState.AddModelError("Address", "Địa chỉ phải ít hơn 100 kí tự!");

            if (model.Shipper.Length > 50)
                ModelState.AddModelError("Shipper", "Tên Shipper phải ít hơn 50 kí tự!");
            if (model.Note.Length > 100)
                ModelState.AddModelError("Note", "Ghi chú phải ít hơn 100 kí tự!");
            //if (model.Method < 5)
            //    ModelState.AddModelError("Method", "Hình thức thanh toán phải nhiều hơn 10 kí tự!");
            //if (model.Period < 1)
            //    ModelState.AddModelError("Period", "Chu kì phải nhiều hơn 1!");
            //if (model.ProductCode > 20)

            //    ModelState.AddModelError("ProductCode", "Mã sản phẩm phải ngắn hơn 20 kí tự!");
            //if (model.ProductName > 50)
            //    ModelState.AddModelError("ProductName", "Tên sản phẩm phải ngắn hơn 50 kí tự!");
            //if (model.Quantity <= 0)
            //    ModelState.AddModelError("Quantity", "Số lượng phải lớn hơn 0!");
            //if (model.InstallmentPrice <= 0)
            //    ModelState.AddModelError("InstallmentPrice", "Giá bán phải lớn hơn 0!");

            if (model.GrandTotal <= 0)
                ModelState.AddModelError("GrandTotal", "Tổng số tiền thanh toán phải lớn hơn 0!");
            if (model.Taken <= 0)
                ModelState.AddModelError("Taken", "Tổng số tiền đã đưa phải lớn hơn 0!");
            if (model.Remain <= 0)
                ModelState.AddModelError("Remain", "Tổng số tiền còn lại phải lớn hơn 0!");
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
