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
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;

namespace AccompProject.Controllers
{
    public class ClimateChangeController : Controller
    {
        private AccomplishmentEntities db = new AccomplishmentEntities();

        //
        // GET: /ClimateChange/
        public ActionResult Index(int id)
        {

            ViewBag.IDAccomp = id;
            Session["idaccomp"] = id;

            var physicalaccomp = db.ClimateChangeAccompViews
                .OrderByDescending(a => a.yr).ThenByDescending(a => a.mnt)
                .Where(a => a.IDClimate == id);



            if (physicalaccomp == null)
            {
                return HttpNotFound();
            }
            // return View(asa);
            return PartialView("_Index", physicalaccomp);



        }


        public ActionResult Edit(string id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClimateChangeProfileView financ = db.ClimateChangeProfileViews.Find(id);
            Session["subproject"] = financ.subproject;
            if (financ == null)
            {
                return HttpNotFound();
            }
            return View(financ);
        }


        public ActionResult MyEdit(int? id)
        {


            ClimateChangeAccompView FD = db.ClimateChangeAccompViews.Find(id);


            if (FD == null)
            {
                return HttpNotFound();
            }




            return PartialView("_MyEdit", FD);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyEdit(ClimateChangeAccompView FODB)
        {

            if (ModelState.IsValid)
            {


                var FinOBD = new ClimateChangeAccomp()
                {
                    IDClimateAccomp = FODB.IDClimateAccomp,
                    IDClimate = FODB.IDClimate,
                    Actual = FODB.Actual,
                    Phy = FODB.Phy,
                    Fin = FODB.Fin,
                    remarks = FODB.remarks,
                    mnt = FODB.mnt,
                    yr = FODB.yr,
                    asof = FODB.asof
                  


                };

                db.Entry(FinOBD).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                string url = Url.Action("Index", "ClimateChange", new { id = FODB.IDClimate });

                return Json(new { success = true, url = url });
                //return Json(new { suceess = true });
            }

            return PartialView("_MyEdit", FODB);

        }


        public ActionResult EditProfile(string id)
        {

            ClimateChangeProfileView FD = db.ClimateChangeProfileViews.Find(id);


            if (FD == null)
            {
                return HttpNotFound();
            }




            return View(FD);
        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(ClimateChangeProfileView FODB)
        {

            if (ModelState.IsValid)
            {


                var FinOBD = new ClimateChangeProfile()
                {
                    ProjectActivity = FODB.ProjectActivity,
                    IDClimate = FODB.IDClimate,
                    Target = FODB.Target,
                    ImplementationPeriod = FODB.ImplementationPeriod,
                    IDAccomp = FODB.IDAccomp


                };

                db.Entry(FinOBD).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index","ACCOMPLISHMENT", new { id = FODB.IDAccomp });
            }

            return View(FODB);

        }
   
    }
}
