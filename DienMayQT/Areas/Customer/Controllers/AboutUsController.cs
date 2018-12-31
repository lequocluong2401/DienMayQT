using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DienMayQT.Models;

namespace DienMayQT.Areas.Customer.Controllers
{
    public class AboutUsController : Controller
    {
        private DmQT06Entities1 db = new DmQT06Entities1();
        // GET: Customer/AboutUs
        public ActionResult Index()
        {
            var model = new KhuModels();
            model.Product = db.Product.ToList();
            model.ProductType = db.ProductType.ToList();
            return View(model);
        }

        // GET: Customer/AboutUs/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Category(int id)
        {
            return RedirectToAction("Category", "ProductsCustomer", new { area = "Customer", id = id });
        }

        // GET: Customer/AboutUs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/AboutUs/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/AboutUs/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customer/AboutUs/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/AboutUs/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/AboutUs/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
