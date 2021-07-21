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
    public class PositionListsController : Controller
    {
        private AccomplishmentEntities db = new AccomplishmentEntities();

        // GET: PositionLists
        public ActionResult Index()
        {
            return View(db.PositionLists.ToList());
        }

        // GET: PositionLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PositionList positionList = db.PositionLists.Find(id);
            if (positionList == null)
            {
                return HttpNotFound();
            }
            return View(positionList);
        }

        // GET: PositionLists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PositionLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PositionID,Position")] PositionList positionList)
        {
            if (ModelState.IsValid)
            {
                db.PositionLists.Add(positionList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(positionList);
        }

        // GET: PositionLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PositionList positionList = db.PositionLists.Find(id);
            if (positionList == null)
            {
                return HttpNotFound();
            }
            return View(positionList);
        }

        // POST: PositionLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PositionID,Position")] PositionList positionList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(positionList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(positionList);
        }

        // GET: PositionLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PositionList positionList = db.PositionLists.Find(id);
            if (positionList == null)
            {
                return HttpNotFound();
            }
            return View(positionList);
        }

        // POST: PositionLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PositionList positionList = db.PositionLists.Find(id);
            db.PositionLists.Remove(positionList);
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
