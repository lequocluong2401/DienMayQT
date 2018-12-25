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

namespace DienMayQT.Areas.Admin.Controllers
{
    public class ProductAdminController : Controller
    {
        DIENMAYQUYETTIENEntities db = new DIENMAYQUYETTIENEntities();
        //
        // GET: /Admin/ProductAdmin/
        public ActionResult Index()
        {
        
            var product = db.Products.OrderByDescending(x => x.ID).ToList();
            if (Session["Username"] != null)
            {
                return View(product);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ProductType = db.ProductTypes.OrderByDescending(x => x.ID).ToList();
            return View();
        }

        // POST: /QuanliBSP/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product p)
        {
            var pro = new Product();
            pro.ProductCode = p.ProductCode;
            pro.ProductName = p.ProductName;
            pro.SalePrice = p.SalePrice;
            pro.Quantity = p.Quantity;
            pro.Status = p.Status;
            pro.ProductTypeID = p.ProductTypeID;
            db.Products.Add(pro);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Product model = db.Products.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductType = db.ProductTypes.OrderByDescending(x => x.ID).ToList();
            return View(model);
        }

        // POST: /QuanliBSP/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product p, int id)
        {
            var pro = db.Products.FirstOrDefault(x => x.ID == id);
            pro.ProductCode = p.ProductCode;
            pro.ProductName = p.ProductName;
            pro.SalePrice = p.SalePrice;
            pro.Quantity = p.Quantity;
            pro.Status = p.Status;
            pro.ProductTypeID = p.ProductTypeID;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product pro = db.Products.Find(id);
            if (pro == null)
            {
                return HttpNotFound();
            }
            return View(pro);
        }

        // POST: /BangSanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product pro = db.Products.Find(id);
            db.Products.Remove(pro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product pro = db.Products.Find(id);
            if (pro == null)
            {
                return HttpNotFound();
            }
            return View(pro);
        }

        public ActionResult Login(Account acc)
        {
            if (ModelState.IsValid)
            {
                using (DIENMAYQUYETTIENEntities db = new DIENMAYQUYETTIENEntities())
                {
                    var obj = db.Accounts.Where(a => a.Usename.Equals(acc.Usename) && a.Password.Equals(acc.Password)).FirstOrDefault();

                    if (obj != null)
                    {
                        Session["Username"] = obj.Usename.ToString();
                        Session["FullName"] = obj.Fullname.ToString();
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(acc);
        }
        [HttpPost]
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon(); // it will clear the session at the end of request
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}