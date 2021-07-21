using AccompProject.Models;
using AccompProject.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AccompProject.Controllers
{
    public class ProjectCoordinatesController : Controller
    {
        //
        private AccomplishmentEntities db = new AccomplishmentEntities();

        // GET: /ProjectCoordinates/
        public ActionResult Index(string id, string sub)
        {
            if (Session["regiontolog"] == null)
            {

                return RedirectToAction("LogIn", "Account", new { area = "" });

            }
            Session["subproject"] = sub;
            Session["idaccomp"] = id;
            var dam = db.ProjectCoordinatesViews
                       .Where(r => r.IDAccomp == id);
                 

            //if (Request.IsAjaxRequest())
            //{
            //    return PartialView("_ListOfDam", dam);
            //}


            return View(dam);
        }

        //
        // GET: /ProjectCoordinates/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /ProjectCoordinates/Create
        public ActionResult Create()
        {
            
            return View();
        }

        //
        // POST: /ProjectCoordinates/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectCoordinatesView PCV)
        {
            try
            {
                var PC = new ProjectCoordinate()
                {

                    idaccomp =   Session["idaccomp"].ToString(),
                    lati = PCV.lati,
                    longi = PCV.longi,
                    Description = PCV.Description,
                    status = PCV.status
                    


                };

                    db.ProjectCoordinates.Add(PC);
                    db.SaveChanges();


                    return RedirectToAction("Index", new { id = Session["idaccomp"].ToString(), sub = Session["subproject"].ToString() });
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ProjectCoordinates/Edit/5
        public ActionResult Edit(int id)
        {

            ProjectCoordinatesView DAM = db.ProjectCoordinatesViews.Find(id);
            Session["subproject"] = DAM.subproject;
            if (DAM == null)
            {
                return HttpNotFound();
            }

            return View(DAM);
        }

        //
        // POST: /ProjectCoordinates/Edit/5
        [HttpPost]
        public ActionResult Edit(ProjectCoordinatesView pcv)
        {
            try
            {
              
                var pc = new ProjectCoordinate { 
                
                    idaccomp = pcv.IDAccomp,
                    idcoordinates = pcv.idcoordinates,
                    Description = pcv.Description,
                    lati = pcv.lati,
                    longi= pcv.longi,
                    status = pcv.status

                
                };

                db.Entry(pc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = pcv.IDAccomp, sub = pcv.subproject});
            }
            catch
            {
                return View(pcv);
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectCoordinate aSATable = db.ProjectCoordinates.Find(id);
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
            ProjectCoordinate aSATable = db.ProjectCoordinates.Find(id);
            db.ProjectCoordinates.Remove(aSATable);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = Session["idaccomp"].ToString(), sub = Session["subproject"].ToString() });
        }
    }
}
