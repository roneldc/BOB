using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AccompProject.Models;
using AccompProject.Models.EntityModel;

namespace AccompProject.Controllers
{
    public class ConsultantListsController : Controller
    {
        private AccomplishmentEntities db = new AccomplishmentEntities();

        // GET: ConsultantLists
        public ActionResult Index()
        {
            return View(db.ConsultantLists.ToList());
        }

        // GET: ConsultantLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsultantList consultantList = db.ConsultantLists.Find(id);
            if (consultantList == null)
            {
                return HttpNotFound();
            }
            return View(consultantList);
        }

        // GET: ConsultantLists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ConsultantLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdConsultant,Consultant")] ConsultantList consultantList)
        {
            if (ModelState.IsValid)
            {
                db.ConsultantLists.Add(consultantList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(consultantList);
        }

        // GET: ConsultantLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsultantList consultantList = db.ConsultantLists.Find(id);
            if (consultantList == null)
            {
                return HttpNotFound();
            }
            return View(consultantList);
        }

        // POST: ConsultantLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdConsultant,Consultant")] ConsultantList consultantList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consultantList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(consultantList);
        }

        // GET: ConsultantLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsultantList consultantList = db.ConsultantLists.Find(id);
            if (consultantList == null)
            {
                return HttpNotFound();
            }
            return View(consultantList);
        }

        // POST: ConsultantLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ConsultantList consultantList = db.ConsultantLists.Find(id);
            db.ConsultantLists.Remove(consultantList);
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
