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
    public class ProductAdminController : Controller
    {
        DmQT06Entities1 db = new DmQT06Entities1();
        //
        // GET: /Admin/ProductAdmin/
        public ActionResult Index()
        {
        
            var product = db.Product.OrderByDescending(x => x.ID).ToList();
            if (Session["Username"] != null)
            {
                return View(product);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
            // GET: /BangSanPham/Details/5
            public FileResult Details(int id)
        {
            var path = Server.MapPath("~/App_Data/" + id);
            return File(path, "images");
        }
       
        public ActionResult Create()
        {
            ViewBag.ProductTypeID = new SelectList(db.ProductType.OrderByDescending(x => x.ID).ToList(), "ID", "ProductTypeName");
            if (Session["Username"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // POST: /BangSanPham/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product model)
        {
            CheckBangSanPham(model);
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {

                    db.Product.Add(model);
                    db.SaveChanges();

                    var path = Server.MapPath("~/App_Data");
                    path = path + '/' + model.ID;
                    if (Request.Files["HinhAnh"] != null && Request.Files["HinhAnh"].ContentLength > 0)
                    {
                        Request.Files["HinhAnh"].SaveAs(path);

                    }
                    else
                    {
                        ModelState.AddModelError("HinhAnh", "Chưa có hình sản phẩm");
                    }
                    scope.Complete(); // approve for transaction
                    return RedirectToAction("Index");
                }
            }
            ViewBag.ProductType = db.ProductType.OrderByDescending(x => x.ID).ToList();
            return View(model);

        }
        private void CheckBangSanPham(Product model)
        {
            if (model.OriginPrice < 0)
                ModelState.AddModelError("OriginPrice", "Giá gốc phải lớn hơn 0!");
            if (model.SalePrice < model.OriginPrice)
                ModelState.AddModelError("SalePrice", "Giá bán phải lớn hơn giá gốc!");
            if (model.InstallmentPrice < model.OriginPrice)
                ModelState.AddModelError("InstallmentPrice", "Giá góp phải lớn hơn giá gốc!");
            if (model.Quantity < 0)
                ModelState.AddModelError("Quantity", "Số lượng phải lớn hơn 0");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product bangsanpham = db.Product.Find(id);
            if (bangsanpham == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductType = db.ProductType.OrderByDescending(x => x.ID).ToList();
            //ViewBag.Loai_id = new SelectList(db.ProductTypes, "id", "TenLoai", bangsanpham.ProductType);
            return View(bangsanpham);
        }

        // POST: /BangSanPham/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(Product model)
        {
            CheckBangSanPham(model);
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    var path = Server.MapPath("~/App_Data");
                    path = path + '/' + model.ID;
                    if (Request.Files["HinhAnh"] != null && Request.Files["HinhAnh"].ContentLength > 0)
                    {
                        Request.Files["HinhAnh"].SaveAs(path);

                    }
                    else
                    {
                        ModelState.AddModelError("HinhAnh", "Chưa có hình sản phẩm");
                    }

                    scope.Complete(); // approve for transaction
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product pro = db.Product.Find(id);
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
            Product pro = db.Product.Find(id);
            db.Product.Remove(pro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        

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