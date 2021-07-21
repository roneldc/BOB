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
using PagedList;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core;
using System.Configuration;
using System.Data.SqlClient;


namespace AccompProject.Controllers
{
    public class NCARequestController : Controller
    {
        private AccomplishmentEntities db = new AccomplishmentEntities();

        
        // GET: NCARequest
        public ActionResult Index()
        {
            return View(db.NCARequests.ToList());
        }

        // GET: NCARequest/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NCARequest nCARequest = db.NCARequests.Find(id);
            if (nCARequest == null)
            {
                return HttpNotFound();
            }
            return View(nCARequest);
        }

        // GET: NCARequest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NCARequest/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDAccomp,IDNCARequest,ASANo,ASAAmount,RequestAmount,dteRequest,timeRequest")] NCARequest nCARequest)
        {
            if (ModelState.IsValid)
            {
                db.NCARequests.Add(nCARequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nCARequest);
        }

        // GET: NCARequest/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NCARequest nCARequest = db.NCARequests.Find(id);
            if (nCARequest == null)
            {
                return HttpNotFound();
            }
            return View(nCARequest);
        }

        // POST: NCARequest/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDAccomp,IDNCARequest,ASANo,ASAAmount,RequestAmount,dteRequest,timeRequest")] NCARequest nCARequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nCARequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nCARequest);
        }

        // GET: NCARequest/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NCARequest nCARequest = db.NCARequests.Find(id);
            if (nCARequest == null)
            {
                return HttpNotFound();
            }
            return View(nCARequest);
        }

        // POST: NCARequest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NCARequest nCARequest = db.NCARequests.Find(id);
            db.NCARequests.Remove(nCARequest);
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
