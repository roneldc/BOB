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
using PagedList;

namespace AccompProject.Controllers
{
    public class FinancialController : Controller
    {
        private AccomplishmentEntities db = new AccomplishmentEntities();



        // GET: ACCOMPLISHMENT
        public ActionResult Autocomplete(string term)
        {
            //Session["regiontolog"] = "UPRIIS";
            if (Session["regiontolog"] == null)
            {

                return RedirectToAction("LogIn", "Account", new { area = "" });

            }
            string strauto = Session["regiontolog"].ToString();
            int mnt = DateTime.Now.Month;
            int yr = DateTime.Now.Year;

   
                var accomplishments = db.ACCOMPLISHMENTs
                      .OrderByDescending(r => r.year)
                      .Where(r => r.subproject.Contains(term))
                      .Select(r => new
                      {
                          label = r.subproject
                      })
                      .Take(5);

                return Json(accomplishments, JsonRequestBehavior.AllowGet);

           
          
        }



        public ActionResult MyIndex(string searchTerm = null, int page = 1)
        {


          
            if (Session["regiontolog"] == null)
            {

                return RedirectToAction("LogIn", "Account", new { area = "" });

            }
            string str = Session["regiontolog"].ToString();

            int mnt = DateTime.Now.Month;
            int yr = DateTime.Now.Year;
            Session["mnt"] = mnt;
            Session["yr"] = yr;
            var now = DateTime.Now;
            var DaysInMonth = DateTime.DaysInMonth(now.Year, now.Month);
            var lastDay = new DateTime(now.Year, now.Month, DaysInMonth);
            Session["asof"] = lastDay;
            ViewBag.DTETME = DateTime.Now.ToString("MMMM yyyy");

           
                var accomplishments = db.ACCOMPLISHMENTs
                     .OrderByDescending(r => r.year)
                     .Where(r => (searchTerm == null || r.subproject.StartsWith(searchTerm)))
                     .ToPagedList(page, 30);


                if (Request.IsAjaxRequest())
                {
                    return PartialView("_ListOfProject", accomplishments);
                }

                return View(accomplishments);
            
        }

        // GET: ACCOMPLISHMENT/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACCOMPLISHMENT aCCOMPLISHMENT = db.ACCOMPLISHMENTs.Find(id);
            if (aCCOMPLISHMENT == null)
            {
                return HttpNotFound();
            }
            return View(aCCOMPLISHMENT);
        }

        public ActionResult Index(string id)
        {

            ViewBag.IDAccomp = id;
            Session["idaccomp"] = id;
            
            //var financials = db.FinancialViews
            //    .OrderByDescending(a => a.yr).ThenByDescending(a => a.mnt)
            //    .Where(a => a.IDAccomp == id);

            var financials = db.FinancialViews
                .OrderByDescending(a => a.REFERENCENO)
                .Where(a => a.IDAccomp == id);


            if (financials == null)
            {
                return HttpNotFound();
            }

            var regreg  = financials.Select(r => r.region).FirstOrDefault();

            if (regreg == null) {

                regreg = "NOT";
            }

            Session["regreg"] = regreg;
            
            
            
            // return View(asa);
            return PartialView("_Index", financials);

           
        
        }

        public ActionResult Edit(string id)
        {

        
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACCOMPLISHMENT financ = db.ACCOMPLISHMENTs.Find(id);
            Session["subproject"] = financ.subproject;


       

            if (financ == null)
            {
                return HttpNotFound();
            }
            return View(financ);
        }

        public ActionResult Create(string Id)
        {
                                  
            FinancialDetail Fview = new FinancialDetail();
            Fview.IDAccomp = Id;



            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "PC", Value = "PC" });
            items.Add(new SelectListItem { Text = "LP", Value = "LP" });
            ViewBag.tf = new SelectList(items, "Value", "Text");


            List<SelectListItem> pitems = new List<SelectListItem>();

            pitems.Add(new SelectListItem { Text = "PS", Value = "PS" });
            pitems.Add(new SelectListItem { Text = "MOOE", Value = "MOOE" });
            pitems.Add(new SelectListItem { Text = "FinEx", Value = "FinEx" });
            pitems.Add(new SelectListItem { Text = "CO", Value = "CO" });


            ViewBag.p = new SelectList(pitems, "Value", "Text");

            return PartialView("_Create", Fview);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDAccomp,as_of,sarono,saroamount,asano,asaamount,disbursement_co_no,disbursement_co,disbursement_region_no,disbursement_region,obligation_co_no,obligation_co,obligation_region_no,obligation_region,ncano,ncaamount,ddno,ddamount,ntano,ntaamount,jevdate,transactiondate,remarksfinancial,mnt,yr,asadate,fundCode,adano,adaamount,burno,buramount,typefinance,particulars")]FinancialDetail FD)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    string usr = User.Identity.Name.ToString();

                    var a = db.AspNetUsers.First(e => e.Email == usr);


                    FD.mnt = Convert.ToInt32(a.mnt);
                    FD.yr = Convert.ToInt32(a.yr);

                    db.FinancialDetails.Add(FD);
                    db.SaveChanges();

                    string url = Url.Action("Index", "Financial", new { id = FD.IDAccomp });

                    return Json(new { success = true, url = url });
                    //return Json(new { suceess = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }


            return PartialView("_Create", FD);

        }

        public ActionResult MyEdit(int? id)
        {


            FinancialDetail FD = db.FinancialDetails.Find(id);
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "PC", Value = "PC" });
            items.Add(new SelectListItem { Text = "LP", Value = "LP" });
            ViewBag.tf = new SelectList(items, "Value", "Text",FD.typefinance);

            List<SelectListItem> pitems = new List<SelectListItem>();

            pitems.Add(new SelectListItem { Text = "PS", Value = "PS" });
            pitems.Add(new SelectListItem { Text = "MOOE", Value = "MOOE" });
            pitems.Add(new SelectListItem { Text = "FinEx", Value = "FinEx" });
            pitems.Add(new SelectListItem { Text = "CO", Value = "CO" });


            ViewBag.p = new SelectList(pitems, "Value", "Text",FD.particulars);

            if (FD == null)
            {
                return HttpNotFound();
            }


          

            return PartialView("_MyEdit", FD);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyEdit([Bind(Include = "IDFinance,IDAccomp,as_of,sarono,saroamount,asano,asaamount,disbursement_co_no,disbursement_co,disbursement_region_no,disbursement_region,obligation_co_no,obligation_co,obligation_region_no,obligation_region,ncano,ncaamount,ddno,ddamount,ntano,ntaamount,jevdate,transactiondate,remarksfinancial,mnt,yr,asadate,fundCode,adano,adaamount,burno,buramount,typefinance,particulars")]FinancialDetail FD)
        {

            if (ModelState.IsValid)
            {
                db.Entry(FD).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                string url = Url.Action("Index", "Financial", new { id = FD.IDAccomp });

                return Json(new { success = true, url = url });
                //return Json(new { suceess = true });
            }
          
            return PartialView("_MyEdit", FD);

        }

        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FinancialDetail FD = db.FinancialDetails.Find(id);

            if (FD == null)
            {
                return HttpNotFound();

            }
            return PartialView("_Delete", FD);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {

            FinancialDetail FD = db.FinancialDetails.Find(id);
            db.FinancialDetails.Remove(FD);
            db.SaveChanges();

            string url = Url.Action("Index", "Financial", new { id = FD.IDAccomp });

            return Json(new { success = true, url = url });

        }


    }
}