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
    public class ASATablesController : Controller
    {
        private AccomplishmentEntities db = new AccomplishmentEntities();

        // GET: ASATables
        //public ActionResult Index()
        //{
        //    return View(db.ASATables.ToList());
        //}
        public ActionResult loaddata()
        {
            using (AccomplishmentEntities dc = new AccomplishmentEntities())
            {
                // dc.Configuration.LazyLoadingEnabled = false; // if your table is relational, contain foreign key
                var data = dc.ASATables
                    .Where(a => a.IDAccomp == "04B756D0-6B46-4DC6-A6B0-A6D59E515007")
                    .OrderBy(a => a.IDAccomp).ToList();
                return Json(new { data = data }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Index(string id, string asabag)
        {


            var asalst = new List<string>();
            var asaqry = from d in db.ASATables
                         orderby d.ASANo
                         select d.ASANo;


            asalst.AddRange(asaqry.Distinct());
            ViewBag.asabag = new SelectList(asalst);





            Session["idaccomp"] = id;
            var asas = db.ASATables
                    .Where(r => r.IDAccomp == id);

            var asa = from a in db.ASATables
                      select a;


            if (!string.IsNullOrEmpty(asabag))
            {

                asa = asa.Where(a => a.IDAccomp == id && a.ASANo == asabag);

            }
            else
            {
                asa = asa.Where(a => a.IDAccomp == id);
            }
         

            return View(asa);
        }

        // GET: ASATables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ASATable aSATable = db.ASATables.Find(id);
            if (aSATable == null)
            {
                return HttpNotFound();
            }
            return View(aSATable);
        }

        // GET: ASATables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ASATables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDASA,IDAccomp,ASANo,ASAAmount,As_Of")] ASATable aSATable)
        {
            if (ModelState.IsValid)
            {
                db.ASATables.Add(aSATable);
                db.SaveChanges();
              //  return RedirectToAction("Index");
                return RedirectToAction("Index", new { id = Session["idaccomp"].ToString() });
            }

            return View(aSATable);
        }

        // GET: ASATables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ASATable aSATable = db.ASATables.Find(id);
            if (aSATable == null)
            {
                return HttpNotFound();
            }
            return View(aSATable);
        }

        // POST: ASATables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDASA,IDAccomp,ASANo,ASAAmount,As_Of")] ASATable aSATable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aSATable).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = Session["idaccomp"].ToString() });
            }
            return View(aSATable);
        }

        // GET: ASATables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ASATable aSATable = db.ASATables.Find(id);
            if (aSATable == null)
            {
                return HttpNotFound();
            }
            return View(aSATable);
        }

        // POST: ASATables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ASATable aSATable = db.ASATables.Find(id);
            db.ASATables.Remove(aSATable);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = Session["idaccomp"].ToString() });
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
