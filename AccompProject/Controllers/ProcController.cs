using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccompProject.Controllers
{
    public class ProcController : Controller
    {
        //
        // GET: /Proc/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Proc/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Proc/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Proc/Create
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

        //
        // GET: /Proc/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Proc/Edit/5
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

        //
        // GET: /Proc/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Proc/Delete/5
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
