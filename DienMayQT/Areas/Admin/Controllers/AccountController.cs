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
    public class AccountController : Controller
    {

        DmQT06Entities1 db = new DmQT06Entities1();
        public ActionResult Index()
        {
            var acc = db.Account.OrderByDescending(x => x.ID).ToList();
           
                return View(acc);
       
        }
        public ActionResult Create()
        {
   
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserName,PassWord,FullName")] Account model)
        {
            if (ModelState.IsValid)
            {
                db.Account.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);

        }

        public ActionResult Edit(int id)
        {
            Account model = db.Account.SingleOrDefault(m => m.ID == id);
            if (model == null)
            {
                return HttpNotFound();
            }
          
            return View(model);
        }

        // POST: /DanhSachSanPham/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Account model)
        {
   
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(model);
        }
        // GET: /Account/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Account.SingleOrDefault(m => m.ID == id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: /Link/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = db.Account.SingleOrDefault(m => m.ID == id);
            db.Account.Remove(account);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }

}
