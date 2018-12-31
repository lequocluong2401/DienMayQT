using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DienMayQT.Models;
using System.Data.Entity;
using System.Web.Security;
using System.Data;
using System.Transactions;
using System.Net;
using EntityState = System.Data.Entity.EntityState;

namespace DienMayQT.Areas.Admin.Controllers
{
    public class CashbillDetailAdminController : Controller
    {
        // GET: Admin/CashbillDetailAdmin
        DmQT06Entities1 db = new DmQT06Entities1();
        // Get Sale Price
        public int SalePrice(int ProductID)
        {
            return db.Product.Find(ProductID).SalePrice;
        }
        // GET: Admin/CashbillDetailAdmin
        public ActionResult Index()
        {
            if (Session["Username"] != null)
            {
                if (Session["ctcashBill"] == null)
                {
                    Session["ctcashBill"] = new List<CashBillDetail>();
                }
                return PartialView(Session["ctcashBill"]);
            }
            else
            {
                return RedirectToAction("Login");
            }

        }

        // GET: /Admin/CashBillDetails/Details/5
        public int DonGiaBan(int ProductID)
        {
            return db.Product.Find(ProductID).SalePrice;
        }

        // GET: /Admin/CashBillDetails/Create
        public PartialViewResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Product, "ID", "ProductName");
            var model = new CashBillDetail();
            model.BillID = 0;
            model.Quantity = 1;
            return PartialView(model);
        }

        // POST: Admin/CashBillDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create2(CashBillDetail model)
        {
            if (ModelState.IsValid)
            {
                model.ID = Environment.TickCount;
                model.Product = db.Product.Find(model.ProductID);
                var ctcashBill = Session["ctcashBill"] as List<CashBillDetail>;
                if (ctcashBill == null)
                    ctcashBill = new List<CashBillDetail>();
                ctcashBill.Add(model);
                Session["ctcashBill"] = ctcashBill;
                return RedirectToAction("Create", "CashbillAdmin");
            }

            ViewBag.ProductID = new SelectList(db.Product, "ID", "ProductName", model.ProductID);
            return View("Create", model);
        }

        public PartialViewResult Edit3()
        {
            ViewBag.ProductID = new SelectList(db.Product, "ID", "ProductName");
            var model = new CashBillDetail();
            model.BillID = 0;
            model.Quantity = 1;
            return PartialView(model);
        }
        // edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit2(CashBillDetail model)
        {
            if (ModelState.IsValid)
            {
                model.ID = Environment.TickCount;
                model.Product = db.Product.Find(model.ProductID);
                var ctcashBill = Session["ctcashBill"] as List<CashBillDetail>;
                if (ctcashBill == null)
                    ctcashBill = new List<CashBillDetail>();
                ctcashBill.Add(model);
                Session["ctcashBill"] = ctcashBill;
                return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
            }

            ViewBag.ProductID = new SelectList(db.Product, "ID", "ProductName", model.ProductID);
            return View("Create", model);
        }

        // GET: Admin/CashBillDetails/Edit/5
        public PartialViewResult Edit(int id)
        {

            List<CashBillDetail> cbDetails = db.CashBillDetail.Where(c => c.BillID == id).ToList();
            if (Session["ctcashBill"] == null)
                Session["ctcashBill"] = new List<CashBillDetail>();
            ViewBag.cbDetails = cbDetails;
            ViewBag.ctcashBill = Session["ctcashBill"];
            return PartialView();
        }

        // POST: Admin/CashBillDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CashBillDetail cashBillDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cashBillDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BillID = new SelectList(db.CashBill, "ID", "BillCode", cashBillDetail.BillID);
            ViewBag.ProductID = new SelectList(db.Product, "ID", "ProductCode", cashBillDetail.ProductID);
            return View(cashBillDetail);
        }

        // GET: Admin/CashBillDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashBillDetail cashBillDetail = db.CashBillDetail.Find(id);
            if (cashBillDetail == null)
            {
                return HttpNotFound();
            }
            return View(cashBillDetail);
        }

        // POST: Admin/CashBillDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CashBillDetail cashBillDetail = db.CashBillDetail.Find(id);
            db.CashBillDetail.Remove(cashBillDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Account acc)
        {
            if (ModelState.IsValid)
            {
                using (DmQT06Entities1 db = new DmQT06Entities1())
                {
                    var obj = db.Account.Where(a => a.UserName.Equals(acc.UserName) && a.PassWord.Equals(acc.PassWord)).FirstOrDefault();

                    if (obj != null)
                    {
                        Session["Username"] = obj.UserName.ToString();
                        Session["FullName"] = obj.FullName.ToString();
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(acc);
        }
        //Logout
        [HttpPost]
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon(); // it will clear the session at the end of request
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
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