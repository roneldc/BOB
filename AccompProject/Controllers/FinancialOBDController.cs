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
using AccompProject.Services;

namespace AccompProject.Controllers
{
    [SessionExpire]
    [Authorize]
    public class FinancialOBDController : Controller
    {
        // GET: FinancialOBD
        // GET: ACCOMPLISHMENT
        private AccomplishmentEntities db = new AccomplishmentEntities();

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

            if (User.IsInRole("Financial"))
            {

                var accomplishments = db.FinancialViews
                      .OrderByDescending(r => r.asadate)
                      .Where(r => r.subproject.Contains(term))
                      .Select(r => new
                      {
                          label = r.subproject
                      })
                      .Take(5);

                return Json(accomplishments, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (Session["asabag"].ToString() == null)
                {
                    var accomplishments = db.FinancialViews
                              .OrderByDescending(r => r.asadate)
                              .Where(r => r.subproject.Contains(term) && r.region == strauto)
                              .Select(r => new
                              {
                                  label = r.subproject
                              })
                              .Take(5);

                    return Json(accomplishments, JsonRequestBehavior.AllowGet);


                }

                else
                {

                    int asabag = Convert.ToInt32(Session["asabag"].ToString());
                    string ids = Session["ids"].ToString();
                    string asano = Session["asano"].ToString();

                    var accomplishments = db.RegionalFinancialViewForODs
                                    .OrderByDescending(r => r.maindescription)
                                  .Where(r => r.subproject.Contains(term) && r.region == strauto && r.year == asabag && r.maindescription == ids && r.asano == asano)
                                    .Select(r => new
                                    {
                                        label = r.subproject
                                    })
                              .Take(5);


                    return Json(accomplishments, JsonRequestBehavior.AllowGet);


                }
            }



        }



        public ActionResult MyIndex(string searchTerm = null, int page = 1)
        {



            if (Session["regiontolog"] == null)
            {

                return RedirectToAction("LogIn", "Account", new { area = "" });

            }
            string str = Session["regiontolog"].ToString();

            //int mnt = DateTime.Now.Month;
            //int yr = DateTime.Now.Year;
            //Session["mnt"] = mnt;
            //Session["yr"] = yr;
            //var now = DateTime.Now;
            //var DaysInMonth = DateTime.DaysInMonth(now.Year, now.Month);
            //var lastDay = new DateTime(now.Year, now.Month, DaysInMonth);
            //Session["asof"] = lastDay;
            //ViewBag.DTETME = DateTime.Now.ToString("MMMM yyyy");

            if (User.IsInRole("Financial"))
            {
                var accomplishments = db.FinancialNCAADAViews
                     .OrderByDescending(r => r.asadate)
                     .Where(r => (searchTerm == null || r.subproject.StartsWith(searchTerm)))
                     .ToPagedList(page, 30);


                if (Request.IsAjaxRequest())
                {
                    return PartialView("_ListOfASA", accomplishments);
                }
                return View(accomplishments);
            }
            else
            {
                var accomplishments = db.FinancialNCAADAViews
                         .OrderByDescending(r => r.asadate)
                         .Where(r => (searchTerm == null || r.subproject.StartsWith(searchTerm)) && r.region == str)
                         .ToPagedList(page, 30);


                if (Request.IsAjaxRequest())
                {
                    return PartialView("_ListOfASA", accomplishments);
                }
                return View(accomplishments);

            }


        }



        public ActionResult MyIndexNCA(string searchTerm = null, int page = 1)
        {



            if (Session["regiontolog"] == null)
            {

                return RedirectToAction("LogIn", "Account", new { area = "" });

            }
            string str = Session["regiontolog"].ToString();

            //int mnt = DateTime.Now.Month;
            //int yr = DateTime.Now.Year;
            //Session["mnt"] = mnt;
            //Session["yr"] = yr;
            //var now = DateTime.Now;
            //var DaysInMonth = DateTime.DaysInMonth(now.Year, now.Month);
            //var lastDay = new DateTime(now.Year, now.Month, DaysInMonth);
            //Session["asof"] = lastDay;
            //ViewBag.DTETME = DateTime.Now.ToString("MMMM yyyy");

            if (User.IsInRole("Financial"))
            {
                var accomplishments = db.FinancialViews
                     .OrderByDescending(r => r.asadate)
                     .Where(r => (searchTerm == null || r.subproject.StartsWith(searchTerm)) && r.asano != null && r.asano != "")
                     .ToPagedList(page, 30);


                if (Request.IsAjaxRequest())
                {
                    return PartialView("_ListOfASANCA", accomplishments);
                }
                return View(accomplishments);
            }
            else
            {
                var accomplishments = db.FinancialViews
                         .OrderByDescending(r => r.asadate)
                         .Where(r => (searchTerm == null || r.subproject.StartsWith(searchTerm)) && r.region == str)
                         .ToPagedList(page, 30);


                if (Request.IsAjaxRequest())
                {
                    return PartialView("_ListOfASANCA", accomplishments);
                }
                return View(accomplishments);

            }


        }

        public ActionResult Index(int id, string idaccomp, int idfinance)
        {

            ViewBag.idncaada = id;
            Session["idncaada"] = id;
            ViewBag.IDFinance = idfinance;
            Session["idfinance"] = idfinance;
            ViewBag.IDAccomp = idaccomp;
            Session["idaccomp"] = idaccomp;

            var financials = db.FinancialOBDViews
                .OrderByDescending(a => a.yr).ThenByDescending(a => a.mnt)
                .Where(a => a.IDNCAADA == id);



            if (financials == null)
            {
                return HttpNotFound();
            }
            // return View(asa);
            return PartialView("_Index", financials);



        }

        public ActionResult Edit(int? id)
        {


            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FinancialNCAADAView financ = db.FinancialNCAADAViews.Find(id);
            Session["subproject"] = financ.subproject;
            if (financ == null)
            {
                return HttpNotFound();
            }
            return View(financ);
        }

        public ActionResult Create(int? Id)
        {

            FinancialOBD Fview = new FinancialOBD();
            Fview.IDNCAADA = Id;


            return PartialView("_Create", Fview);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDAccomp,IDFinance,IDNCAADA,Obligationno,obligationamount,burno,buramount,disbursement,asof,mnt,yr,status")]FinancialOBD FD)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    db.FinancialOBDs.Add(FD);
                    db.SaveChanges();

                    string url = Url.Action("Index", "FinancialOBD", new { id = FD.IDNCAADA, idaccomp = FD.IDAccomp, idfinance = FD.IDFinance });

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


            FinancialOBDView FD = db.FinancialOBDViews.Find(id);


            if (FD == null)
            {
                return HttpNotFound();
            }




            return PartialView("_MyEdit", FD);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyEdit(FinancialOBDView FODB)
        {

            if (ModelState.IsValid)
            {

                var FinOBD = new FinancialOBD()
                {
                    IDFinance = FODB.IDFinance,
                    IDAccomp = FODB.IDAccomp,
                    IDObd = FODB.IDObd,
                    Obligationno = FODB.Obligationno,
                    Obligationamount = FODB.Obligationamount,
                    Burno = FODB.Burno,
                    Buramount = FODB.Buramount,
                    Disbursement = FODB.Disbursement,
                    mnt = FODB.mnt,
                    yr = FODB.yr,
                    asof = FODB.asof,
                    IDNCAADA = FODB.IDNCAADA,
                    status = FODB.status



                };

                db.Entry(FinOBD).State = EntityState.Modified;
                db.SaveChanges();

                string url = Url.Action("Index", "FinancialOBD", new { id = FODB.IDNCAADA, idaccomp = FODB.IDAccomp, idfinance = FODB.IDFinance });

                return Json(new { success = true, url = url });
                //return Json(new { suceess = true });
            }

            return PartialView("_MyEdit", FODB);

        }

        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FinancialOBDView FD = db.FinancialOBDViews.Find(id);

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

            FinancialOBD FD = db.FinancialOBDs.Find(id);
            db.FinancialOBDs.Remove(FD);
            db.SaveChanges();

            string url = Url.Action("Index", "FinancialOBD", new { id = FD.IDNCAADA, idaccomp = FD.IDAccomp, idfinance = FD.IDFinance });

            return Json(new { success = true, url = url });

        }



        //NCA REGION


        public ActionResult IndexNCA(int id, string idaccomp)
        {

            ViewBag.IDFinance = id;
            Session["idfinance"] = id;
            ViewBag.IDAccomp = idaccomp;
            Session["idaccomp"] = idaccomp;

            var financials = db.FinancialNCAADAViews.Where(a => a.IDFinance == id && a.blnk != "blank");



            if (financials == null)
            {
                return HttpNotFound();
            }
            // return View(asa);
            return PartialView("_IndexNCA", financials);



        }


        public ActionResult EditNCA(int? id)
        {


            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FinancialView financ = db.FinancialViews.Find(id);
            if (financ == null)
            {
                return HttpNotFound();
            }
            return View(financ);
        }

        public ActionResult CreateNCA(int? Id)
        {

            FinancialNCAADA Fview = new FinancialNCAADA();
            Fview.IDFinance = Id;


            return PartialView("_CreateNCA", Fview);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNCA([Bind(Include = "IDAccomp,IDFinance,NCADate,NCANO,NCAAmount,NTADate,NTANO,NTAAmount,Validity,Covering,Accountno,ADADate,ADANO,ADAAmount")]FinancialNCAADA FD)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    db.FinancialNCAADAs.Add(FD);
                    db.SaveChanges();

                    string url = Url.Action("IndexNCA", "FinancialOBD", new { id = FD.IDFinance, idaccomp = FD.IDAccomp });

                    return Json(new { success = true, url = url });
                    //return Json(new { suceess = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }


            return PartialView("_CreateNCA", FD);

        }

        public ActionResult MyEditNCA(int? id)
        {


            FinancialNCAADAView FD = db.FinancialNCAADAViews.Find(id);


            if (FD == null)
            {
                return HttpNotFound();
            }




            return PartialView("_MyEditNCA", FD);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyEditNCA(FinancialNCAADAView FNCA)
        {

            if (ModelState.IsValid)
            {

                var FinNCA = new FinancialNCAADA()
                {
                    IDFinance = FNCA.IDFinance,
                    IDAccomp = FNCA.IDAccomp,
                    IDNCAADA = FNCA.IDNCAADA,
                    NCADate = FNCA.NCADate,
                    NCANO = FNCA.NCANO,
                    NCAAmount = FNCA.NCAAmount,
                    NTADate = FNCA.NTADate,
                    NTANO = FNCA.NTANO,
                    NTAAmount = FNCA.NTAAmount,
                    Validity = FNCA.Validity,
                    Covering = FNCA.Covering,
                    AccountNo = FNCA.AccountNo,
                    ADADate = FNCA.ADADate,
                    ADANO = FNCA.ADANO,
                    ADAAmount = FNCA.ADAAmount



                };

                db.Entry(FinNCA).State = EntityState.Modified;
                db.SaveChanges();

                string url = Url.Action("IndexNCA", "FinancialOBD", new { id = FNCA.IDFinance, idaccomp = FNCA.IDAccomp });

                return Json(new { success = true, url = url });
                //return Json(new { suceess = true });
            }

            return PartialView("_MyEditNCA", FNCA);

        }

        public ActionResult DeleteNCA(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FinancialNCAADAView FD = db.FinancialNCAADAViews.Find(id);

            if (FD == null)
            {
                return HttpNotFound();

            }
            return PartialView("_DeleteNCA", FD);

        }

        [HttpPost, ActionName("DeleteNCA")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteNCA(int id)
        {

            FinancialNCAADA FD = db.FinancialNCAADAs.Find(id);
            db.FinancialNCAADAs.Remove(FD);
            db.SaveChanges();

            string url = Url.Action("IndexNCA", "FinancialOBD", new { id = FD.IDFinance, idaccomp = FD.IDAccomp });

            return Json(new { success = true, url = url });

        }




        //OD REGION

        //public ActionResult MyIndexODBUR(string searchTerm = null, int asabag = 0, int page = 1)
        //{



        //    if (Session["regiontolog"] == null)
        //    {

        //        return RedirectToAction("LogIn", "Account", new { area = "" });

        //    }
        //    string str = Session["regiontolog"].ToString();
        //    Session["asabag"] = asabag;

        //    //var asalst = new List<string>();
        //    var asaqry = from d in db.ACCOMPLISHMENTs
        //                 orderby d.year
        //                 where d.region == str
        //                 select d.year;




        //    //asalst.AddRange(asaqry.Distinct());

        //    var asalst = asaqry.Distinct();
        //    ViewBag.asabag = new SelectList(asalst);


        //    //var accomplishments = db.RegionalFinancialViews
        //    //                .GroupBy(s => new { s.year, s.region, s.maindescription })
        //    //                .OrderBy(g => g.Key.maindescription)
        //    //                .Select(g =>
        //    //                        new
        //    //                        {
        //    //                            maindescription = g.Key.maindescription,
        //    //                            saroamount = g.Sum(x => x.saroamount),
        //    //                            asaamount = g.Sum(x => x.asaamount),
        //    //                            Obligationamount = g.Sum(x => x.Obligationamount),
        //    //                            Disbursement = g.Sum(x => x.Disbursement),
        //    //                            region = g.Key.region,
        //    //                            year = g.Key.year

        //    //                        }
        //    //                    )
        //    //            .ToPagedList(page, 30);

        //    //var accomplishments = db.RegionalFinancialViewSummaryProjects
        //    //                    .OrderByDescending(r => r.maindescription)
        //    //                  .Where(r => r.region == str && r.year == asabag)
        //    //                  .ToPagedList(page, 30);



        //    var accomplishments = db.RegionalFinancialViewSummaryProjects
        //                    .OrderByDescending(r => r.maindescription)
        //                  .Where(r => r.region == str && r.year == asabag)
        //                  .ToPagedList(page, 30);


        //    if (Request.IsAjaxRequest())
        //    {
        //        return PartialView("_ListOfOD", accomplishments);
        //    }

        //    return View(accomplishments);



        //}


        public ActionResult MyIndexOD(string searchTerm = null, int asabag = 0, int page = 1)
        {



            if (Session["regiontolog"] == null)
            {

                return RedirectToAction("LogIn", "Account", new { area = "" });

            }
            string str = Session["regiontolog"].ToString();
            Session["asabag"] = asabag;

            //var asalst = new List<string>();
            var asaqry = from d in db.ACCOMPLISHMENTs
                         orderby d.year descending
                         where d.region == str
                         select d.year;


            // && d.year == 2018

            //asalst.AddRange(asaqry.Distinct());

            var asalst = asaqry.Distinct();
            ViewBag.asabag = new SelectList(asalst.OrderByDescending(z => z));


            //var accomplishments = db.RegionalFinancialViews
            //                .GroupBy(s => new { s.year, s.region, s.maindescription })
            //                .OrderBy(g => g.Key.maindescription)
            //                .Select(g =>
            //                        new
            //                        {
            //                            maindescription = g.Key.maindescription,
            //                            saroamount = g.Sum(x => x.saroamount),
            //                            asaamount = g.Sum(x => x.asaamount),
            //                            Obligationamount = g.Sum(x => x.Obligationamount),
            //                            Disbursement = g.Sum(x => x.Disbursement),
            //                            region = g.Key.region,
            //                            year = g.Key.year

            //                        }
            //                    )
            //            .ToPagedList(page, 30);

            //var accomplishments = db.RegionalFinancialViewSummaryProjects
            //                    .OrderByDescending(r => r.maindescription)
            //                  .Where(r => r.region == str && r.year == asabag)
            //                  .ToPagedList(page, 30);


            //asa


            var accomplishments = db.RegionalFinancialViewSummaryProjects
                            .OrderByDescending(r => r.maindescription)
                          .Where(r => r.region == str && r.year == asabag)
                          .ToPagedList(page, 30);


            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListOfOD", accomplishments);
            }

            return View(accomplishments);



        }


        public ActionResult MyIndexODASA(string searchTerm = null, string ids = null, int asabag = 0, int page = 1)
        {



            if (Session["regiontolog"] == null)
            {

                return RedirectToAction("LogIn", "Account", new { area = "" });

            }
            string str = Session["regiontolog"].ToString();
            Session["ids"] = ids;

            ////var asalst = new List<string>();
            //var asaqry = from d in db.ACCOMPLISHMENTs
            //             orderby d.year
            //             where d.region == str
            //             select d.year;




            //asalst.AddRange(asaqry.Distinct());

            //var asalst = asaqry.Distinct();
            //ViewBag.asabag = new SelectList(asalst);


            //var accomplishments = db.RegionalFinancialViews
            //                .GroupBy(s => new { s.year, s.region, s.maindescription })
            //                .OrderBy(g => g.Key.maindescription)
            //                .Select(g =>
            //                        new
            //                        {
            //                            maindescription = g.Key.maindescription,
            //                            saroamount = g.Sum(x => x.saroamount),
            //                            asaamount = g.Sum(x => x.asaamount),
            //                            Obligationamount = g.Sum(x => x.Obligationamount),
            //                            Disbursement = g.Sum(x => x.Disbursement),
            //                            region = g.Key.region,
            //                            year = g.Key.year

            //                        }
            //                    )
            //            .ToPagedList(page, 30);

            //var accomplishments = db.RegionalFinancialViewSummaryProjects
            //                    .OrderByDescending(r => r.maindescription)
            //                  .Where(r => r.region == str && r.year == asabag)
            //                  .ToPagedList(page, 30);



            var accomplishments = db.RegionalFinancialViewSummaryProjectASAs
                            .OrderByDescending(r => r.maindescription)
                          .Where(r => r.region == str && r.year == asabag && r.maindescription == ids)
                          .ToPagedList(page, 60);


            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListOfODASA", accomplishments);
            }

            return View(accomplishments);



        }


        public ActionResult MyIndexODASASubproject(string searchTerm = null, string ids = null, string asano = null, int asabag = 0, int page = 1, string mainp = null)
        {

            if (asano != null)
            {

                Session["asano"] = asano;


            }

            if (Session["regiontolog"] == null)
            {

                return RedirectToAction("LogIn", "Account", new { area = "" });

            }
            string str = Session["regiontolog"].ToString();


            ////var asalst = new List<string>();
            //var asaqry = from d in db.ACCOMPLISHMENTs
            //             orderby d.year
            //             where d.region == str
            //             select d.year;




            //asalst.AddRange(asaqry.Distinct());

            //var asalst = asaqry.Distinct();
            //ViewBag.asabag = new SelectList(asalst);


            //var accomplishments = db.RegionalFinancialViews
            //                .GroupBy(s => new { s.year, s.region, s.maindescription })
            //                .OrderBy(g => g.Key.maindescription)
            //                .Select(g =>
            //                        new
            //                        {
            //                            maindescription = g.Key.maindescription,
            //                            saroamount = g.Sum(x => x.saroamount),
            //                            asaamount = g.Sum(x => x.asaamount),
            //                            Obligationamount = g.Sum(x => x.Obligationamount),
            //                            Disbursement = g.Sum(x => x.Disbursement),
            //                            region = g.Key.region,
            //                            year = g.Key.year

            //                        }
            //                    )
            //            .ToPagedList(page, 30);

            //var accomplishments = db.RegionalFinancialViewSummaryProjects
            //                    .OrderByDescending(r => r.maindescription)
            //                  .Where(r => r.region == str && r.year == asabag)
            //                  .ToPagedList(page, 30);

            asabag = Convert.ToInt32(Session["asabag"].ToString());
            ids = Session["ids"].ToString();
            asano = Session["asano"].ToString();
            //            var accomplishments = db.RegionalFinancialViewForODs
            //  /                        .OrderByDescending(r => r.maindescription)
            //                   .Where(r => (searchTerm == null || r.subproject.StartsWith(searchTerm)) && r.region == str && r.year == asabag && r.maindescription == ids && r.asano == asano)
            //                      .ToPagedList(page, 30);

            if (asano == null)
            {

                var accomplishments = db.RegionalFinancialViewForODs
                   .OrderByDescending(r => r.maindescription)
                 .Where(r => r.region == str && r.year == asabag && r.maindescription == ids)
                 .ToPagedList(page, 30);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_ListOfODASASubproject", accomplishments);
                }
                return View(accomplishments);
            }
            else
            {
                asano = Session["asano"].ToString();

                var accomplishments = db.RegionalFinancialViewForODs
                    .OrderByDescending(r => r.maindescription)
                  .Where(r => r.region == str && r.year == asabag && r.maindescription == ids && r.asano == asano)
                  .ToPagedList(page, 30);


                Session["regionOD"] = str;
                Session["mainOD"] = ids;
                Session["asanoOD"] = asano;

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_ListOfODASASubproject", accomplishments);

                }

                return View(accomplishments);
            }

            //if (Request.IsAjaxRequest())
            //{
            //    return PartialView("_ListOfODASASubproject", accomplishments);
            //}

            //    return View(accomplishments);



        }

        public ActionResult IndexOD(int id, string idaccomp, int idfinance)
        {

            ViewBag.idncaada = id;
            Session["idncaada"] = id;
            ViewBag.IDFinance = idfinance;
            Session["idfinance"] = idfinance;
            ViewBag.IDAccomp = idaccomp;
            Session["idaccomp"] = idaccomp;

            var financials = db.FinancialOBDViews
                     .OrderByDescending(a => a.yr).ThenByDescending(a => a.mnt)
                .Where(a => a.IDNCAADA == id && a.status == "OD" && a.IDFinance == idfinance);




            if (financials == null)
            {
                return HttpNotFound();
            }
            // return View(asa);
            return PartialView("_IndexOD", financials);




        }


        public ActionResult EditOD(int? id)
        {


            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            FinancialView financ = db.FinancialViews.Find(id);
            Session["subproject"] = financ.subproject;
            Session["year"] = financ.year;

            if (financ == null)
            {
                return HttpNotFound();
            }




            return View(financ);
        }

        public ActionResult CreateOD(int? Id)
        {




            FinancialOBD Fview = new FinancialOBD();
            Fview.IDFinance = Id;

            string eml = User.Identity.Name.ToString();

            var d = db.AspNetUsers.First(a => a.Email == eml);




            Fview.mnt = d.mnt;
            Fview.yr = d.yr;

            Fview.asof = ((d.mnt.ToString()).Length < 2 ? "0" + d.mnt.ToString() + "/" + d.yr.ToString() : "" + d.mnt.ToString() + "/" + d.yr.ToString());


            TempData["msg"] = "";
            return PartialView("_CreateOD", Fview);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOD([Bind(Include = "IDAccomp,IDFinance,IDNCAADA,Obligationno,obligationamount,burno,buramount,disbursement,asof,mnt,yr,status,nca,dateOD,dateDis,cash,cashdate")]FinancialOBD FD)
        {
            var valids = true;
            double obligation = 0.00;
            var disbursement = 0.00;
            var asa = 0.00;

            TempData["msg"] = "<h4 class = \"alert alert-danger fde\"> Error list </h4>";

            var strhoney = "select isnull(sum(obligationamount),0) as obligation, isnull(sum(disbursement),0) as disbursement from financialobd where idfinance = " + FD.IDFinance;

            string Constring1 = ConfigurationManager.ConnectionStrings["AccomplishmentEntitiesSecurity"].ConnectionString;
            using (SqlConnection CON = new SqlConnection(Constring1))
            {
                CON.Open();
                SqlCommand cmd = new SqlCommand(strhoney, CON);
                SqlDataReader reader;


                reader = cmd.ExecuteReader();

                if (reader.Read())
                {

                    obligation = reader.GetDouble(0);
                    disbursement = reader.GetDouble(1);

                }

                reader.Close();


            }

            var strhoney1 = "select isnull(sum(asaamount),0) as asa from financialdetail where idfinance = " + FD.IDFinance;

            string Constring11 = ConfigurationManager.ConnectionStrings["AccomplishmentEntitiesSecurity"].ConnectionString;
            using (SqlConnection CON = new SqlConnection(Constring11))
            {
                CON.Open();
                SqlCommand cmd = new SqlCommand(strhoney1, CON);
                SqlDataReader reader;


                reader = cmd.ExecuteReader();

                if (reader.Read())
                {

                    asa = reader.GetDouble(0);



                }

                reader.Close();

            }

            if (FD.Obligationamount.GetValueOrDefault(0) == 0)
            {
                FD.Obligationamount = 0;
            }

            if (FD.Disbursement.GetValueOrDefault(0) == 0)
            {
                FD.Disbursement = 0;
            }

            obligation = Convert.ToDouble((obligation).ToString("0.00"));
            disbursement = Convert.ToDouble((disbursement).ToString("0.00"));


            decimal d = Convert.ToDecimal(obligation + FD.Obligationamount);



            if ((d) > Convert.ToDecimal(asa))
            {

                TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\"> Obligation Amount is greater than Asa Amount! </div>";
                valids = false;

            }


            if ((disbursement + FD.Disbursement) > (obligation + FD.Obligationamount))
            {

                TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\"> Disbursement Amount is greater than Obligation Amount! </div>";
                valids = false;

            }

            if ((FD.Disbursement < 0) || (FD.Obligationamount < 0))
            {
                if (String.IsNullOrWhiteSpace(FD.Burno))
                {
                    TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\"> Remarks is Requirement for negative values </div>";
                    valids = false;
                }

            }



            if ((ModelState.IsValid) && (valids == true))
            {
                try
                {



                    //audit trail

                    string Constring = ConfigurationManager.ConnectionStrings["AccomplishmentEntitiesSecurity"].ConnectionString;
                    using (SqlConnection CON = new SqlConnection(Constring))
                    {
                        CON.Open();
                        SqlCommand cmd = new SqlCommand("insert into FinancialOBDLog (userid,dateedit,timeedit,idaccomp,action) values (@userid,@dateedit,@timeedit,@idaccomp,@action)", CON);
                        cmd.Parameters.AddWithValue("@userid", Session["userid"].ToString());
                        cmd.Parameters.AddWithValue("@idaccomp", FD.IDAccomp);
                        cmd.Parameters.AddWithValue("@dateedit", DateTime.Now);
                        cmd.Parameters.AddWithValue("@timeedit", DateTime.Now.TimeOfDay);
                        cmd.Parameters.AddWithValue("@Action", "ADD");
                        cmd.ExecuteNonQuery();


                    }






                    FD.DateEntered = DateTime.Now;
                    db.FinancialOBDs.Add(FD);
                    db.SaveChanges();



                    var msg = "";
                    msg = msg + Session["regionOD"].ToString() + " updated the Mainproject " + Session["mainOD"].ToString();
                    msg = msg + ", ASA No. : " + Session["asanoOD"].ToString();
                    msg = msg + ", Obligation :  " + FD.Obligationamount;
                    msg = msg + ", Disbursement :  " + FD.Disbursement;
                    msg = msg + " Please Don't reply this is Auto Generated by the System for details refer to Reportserver";

                    SMS sSMS = new SMS();

                    var smsnum = db.SMS
                  .Where(a => a.projectmonitor == "FD").ToList();

                    if (smsnum.Count != 0)
                    {
                        for (int i = 0; i < smsnum.Count; i++)
                        {
                            if (smsnum[i].status.ToString() == "YES")
                            {


                                sSMS.sendSMS(smsnum[i].SMSnumber.ToString(), msg);
                                //  System.Threading.Thread.Sleep(3000);
                            }


                        }

                    }


                    string url = Url.Action("IndexOD", "FinancialOBD", new { id = 0, idaccomp = FD.IDAccomp, idfinance = FD.IDFinance });

                    return Json(new { success = true, url = url });
                    //return Json(new { suceess = true });





                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }


            return PartialView("_CreateOD", FD);

        }






        public ActionResult MyEditOD(int? id)
        {


            FinancialOBDView FD = db.FinancialOBDViews.Find(id);


            if (FD == null)
            {
                return HttpNotFound();
            }




            return PartialView("_MyEditOD", FD);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyEditOD(FinancialOBDView FODB)
        {

            var valids = true;
            var obligation = 0.00;
            var disbursement = 0.00;
            var asa = 0.00;

            TempData["msg"] = "<h4 class = \"alert alert-danger fde\"> Error list </h4>";

            var strhoney = "select isnull(sum(obligationamount),0) as obligation, isnull(sum(disbursement),0) as disbursement from financialobd where idfinance = " + FODB.IDFinance
                            + " AND IDOBD <>  " + FODB.IDObd;

            string Constring1 = ConfigurationManager.ConnectionStrings["AccomplishmentEntitiesSecurity"].ConnectionString;
            using (SqlConnection CON = new SqlConnection(Constring1))
            {
                CON.Open();
                SqlCommand cmd = new SqlCommand(strhoney, CON);
                SqlDataReader reader;


                reader = cmd.ExecuteReader();

                if (reader.Read())
                {

                    obligation = reader.GetDouble(0);
                    disbursement = reader.GetDouble(1);

                }

                reader.Close();


            }

            var strhoney1 = "select isnull(sum(asaamount),0) as asa from financialdetail where idfinance = " + FODB.IDFinance;

            string Constring11 = ConfigurationManager.ConnectionStrings["AccomplishmentEntitiesSecurity"].ConnectionString;
            using (SqlConnection CON = new SqlConnection(Constring11))
            {
                CON.Open();
                SqlCommand cmd = new SqlCommand(strhoney1, CON);
                SqlDataReader reader;


                reader = cmd.ExecuteReader();

                if (reader.Read())
                {

                    asa = reader.GetDouble(0);



                }

                reader.Close();

            }

            if (FODB.Obligationamount.GetValueOrDefault(0) == 0)
            {
                FODB.Obligationamount = 0;
            }

            if (FODB.Disbursement.GetValueOrDefault(0) == 0)
            {
                FODB.Disbursement = 0;
            }


            //obligation = Convert.ToDouble((obligation).ToString("F"));
            //disbursement = Convert.ToDouble((disbursement).ToString("F"));

            obligation = Convert.ToDouble((obligation).ToString("0.00"));
            disbursement = Convert.ToDouble((disbursement).ToString("0.00"));


            decimal d = Convert.ToDecimal(obligation + FODB.Obligationamount);


            if ((d) > Convert.ToDecimal(asa))
            {

                TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\"> Obligation Amount is greater than Asa Amount! </div>";
                valids = false;

            }


            if ((disbursement + FODB.Disbursement) > (obligation + FODB.Obligationamount))
            {

                TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\"> Disbursement Amount is greater than Obligation Amount! </div>";
                valids = false;

            }

            if ((FODB.Disbursement < 0) || (FODB.Obligationamount < 0))
            {
                if (String.IsNullOrWhiteSpace(FODB.Burno))
                {
                    TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\"> Remarks is Requirement for negative values </div>";
                    valids = false;
                }

            }



            if ((ModelState.IsValid) && (valids == true))
            {

                string Constring = ConfigurationManager.ConnectionStrings["AccomplishmentEntitiesSecurity"].ConnectionString;
                using (SqlConnection CON = new SqlConnection(Constring))
                {
                    CON.Open();
                    SqlCommand cmd = new SqlCommand("insert into FinancialOBDLog (userid,dateedit,timeedit,idaccomp,action) values (@userid,@dateedit,@timeedit,@idaccomp,@action)", CON);
                    cmd.Parameters.AddWithValue("@userid", Session["userid"].ToString());
                    cmd.Parameters.AddWithValue("@idaccomp", FODB.IDAccomp);
                    cmd.Parameters.AddWithValue("@dateedit", DateTime.Now);
                    cmd.Parameters.AddWithValue("@timeedit", DateTime.Now.TimeOfDay);
                    cmd.Parameters.AddWithValue("@Action", "EDIT");
                    cmd.ExecuteNonQuery();


                }







                var FinOBD = new FinancialOBD()
                {
                    IDFinance = FODB.IDFinance,
                    IDAccomp = FODB.IDAccomp,
                    IDObd = FODB.IDObd,
                    Obligationno = FODB.Obligationno,
                    Obligationamount = FODB.Obligationamount,
                    Burno = FODB.Burno,
                    Buramount = FODB.Buramount,
                    Disbursement = FODB.Disbursement,
                    mnt = FODB.mnt,
                    yr = FODB.yr,
                    asof = FODB.asof,
                    IDNCAADA = FODB.IDNCAADA,
                    status = FODB.status,
                    nca = FODB.nca,
                    DateEntered = FODB.DateEntered,
                    dateOD = FODB.dateOD,
                    dateDis = FODB.dateDis,
                    cash = FODB.cash,
                    cashdate = FODB.cashdate



                };

                db.Entry(FinOBD).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();



                var msg = "";
                msg = msg + Session["regionOD"].ToString() + " updated the Mainproject " + Session["mainOD"].ToString();
                msg = msg + ", ASA No. : " + Session["asanoOD"].ToString();
                msg = msg + ", Obligation :  " + FODB.Obligationamount;
                msg = msg + ", Disbursement :  " + FODB.Disbursement;
                msg = msg + " Please Don't reply this is Auto Generated by the System for details refer to Reportserver";

                SMS sSMS = new SMS();

                var smsnum = db.SMS
              .Where(a => a.projectmonitor == "FD").ToList();

                if (smsnum.Count != 0)
                {
                    for (int i = 0; i < smsnum.Count; i++)
                    {
                        if (smsnum[i].status.ToString() == "YES")
                        {


                            sSMS.sendSMS(smsnum[i].SMSnumber.ToString(), msg);
                            //  System.Threading.Thread.Sleep(3000);
                        }


                    }

                }


                string url = Url.Action("IndexOD", "FinancialOBD", new { id = 0, idaccomp = FODB.IDAccomp, idfinance = FODB.IDFinance });

                return Json(new { success = true, url = url });
                //return Json(new { suceess = true });
            }

            return PartialView("_MyEditOD", FODB);

        }

        public ActionResult DeleteOD(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FinancialOBDView FD = db.FinancialOBDViews.Find(id);

            if (FD == null)
            {
                return HttpNotFound();

            }
            return PartialView("_DeleteOD", FD);

        }

        [HttpPost, ActionName("DeleteOD")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteOD(int id)
        {




            FinancialOBD FD = db.FinancialOBDs.Find(id);

            string Constring = ConfigurationManager.ConnectionStrings["AccomplishmentEntitiesSecurity"].ConnectionString;
            using (SqlConnection CON = new SqlConnection(Constring))
            {
                CON.Open();
                SqlCommand cmd = new SqlCommand("insert into FinancialOBDLog (userid,dateedit,timeedit,idaccomp,action) values (@userid,@dateedit,@timeedit,@idaccomp,@action)", CON);
                cmd.Parameters.AddWithValue("@userid", Session["userid"].ToString());
                cmd.Parameters.AddWithValue("@idaccomp", FD.IDAccomp);
                cmd.Parameters.AddWithValue("@dateedit", DateTime.Now);
                cmd.Parameters.AddWithValue("@timeedit", DateTime.Now.TimeOfDay);
                cmd.Parameters.AddWithValue("@Action", "DELETE");
                cmd.ExecuteNonQuery();


            }
            db.FinancialOBDs.Remove(FD);
            db.SaveChanges();

            string url = Url.Action("IndexOD", "FinancialOBD", new { id = 0, idaccomp = FD.IDAccomp, idfinance = FD.IDFinance });

            return Json(new { success = true, url = url });

        }


        //NCA REQUEST



        public ActionResult NCARequest(string ids = null, string asano = null, int asabag = 0)
        {


            return RedirectToAction("MyIndexODASASubproject", "FinancialOBD", new { asabag = asabag, ids = ids, asano = asano });

        }




        //OD BUR REGION


        public ActionResult IndexODBURFinancial()
        {



            if (Session["regiontolog"] == null)
            {

                return RedirectToAction("LogIn", "Account", new { area = "" });

            }
            string str = Session["regiontolog"].ToString();




            var accomplishments = db.FinancialViews
                  .OrderByDescending(r => r.asadate)
                  .Where(r => r.TYPERELEASE == "ASA" || r.TYPERELEASE == "BUR")
                .ToList();



            return View(accomplishments);

        }











        public ActionResult MyIndexODBUR(string searchTerm = null, int page = 1)
        {



            if (Session["regiontolog"] == null)
            {

                return RedirectToAction("LogIn", "Account", new { area = "" });

            }
            string str = Session["regiontolog"].ToString();



            if (User.IsInRole("Financial"))
            {

                var accomplishments = db.FinancialViews
                     .OrderByDescending(r => r.asadate)
                     .Where(r => (searchTerm == null || r.subproject.StartsWith(searchTerm)) && r.TYPERELEASE != "ASA")
                     .ToPagedList(page, 30);

                if (Session["regiontolog"].ToString() == "NATIONWIDE")
                {

                    //accomplishments = db.FinancialViews
                    //    .OrderByDescending(r => r.asadate)
                    //    .Where(r => (searchTerm == null || r.subproject.StartsWith(searchTerm)) && (r.TYPERELEASE == "ASA" || r.TYPERELEASE == "BUR"))
                    //    .ToPagedList(page, 30);


                    accomplishments = db.FinancialViews
                      .OrderByDescending(r => r.asadate)
                      .Where(r => (searchTerm == null || r.subproject.StartsWith(searchTerm)) && (r.TYPERELEASE == "ASA" || r.TYPERELEASE == "BUR"))
                      .ToPagedList(page, 30);

                }
                else
                {

                    accomplishments = db.FinancialViews
                        .OrderByDescending(r => r.asadate)
                        .Where(r => (searchTerm == null || r.subproject.StartsWith(searchTerm)) && r.TYPERELEASE != "ASA")
                        .ToPagedList(page, 30);

                }

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_ListOfODBUR", accomplishments);
                }


                return View(accomplishments);
            }
            else
            {
                var accomplishments = db.FinancialViews
                         .OrderByDescending(r => r.asadate)
                         .Where(r => (searchTerm == null || r.subproject.StartsWith(searchTerm)) && r.region == str && r.TYPERELEASE == "ASA")
                         .ToPagedList(page, 30);


                if (Request.IsAjaxRequest())
                {
                    return PartialView("_ListOfODBUR", accomplishments);
                }
                return View(accomplishments);

            }


        }

        public ActionResult IndexODBUR(int id, string idaccomp, int idfinance)
        {

            ViewBag.idncaada = id;
            Session["idncaada"] = id;
            ViewBag.IDFinance = idfinance;
            Session["idfinance"] = idfinance;
            ViewBag.IDAccomp = idaccomp;
            Session["idaccomp"] = idaccomp;

            var financials = db.FinancialOBDViews
                     .OrderByDescending(a => a.yr).ThenByDescending(a => a.mnt)
                .Where(a => a.IDNCAADA == id && a.status == "OD" && a.IDFinance == idfinance);



            if (financials == null)
            {
                return HttpNotFound();
            }
            // return View(asa);
            return PartialView("_IndexODBUR", financials);




        }


        public ActionResult EditODBUR(int? id)
        {


            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FinancialView financ = db.FinancialViews.Find(id);
            Session["subproject"] = financ.subproject;

            if (financ == null)
            {
                return HttpNotFound();
            }
            return View(financ);
        }

        public ActionResult CreateODBUR(int? Id)
        {

            FinancialOBD Fview = new FinancialOBD();
            Fview.IDFinance = Id;


            string eml = User.Identity.Name.ToString();

            var d = db.AspNetUsers.First(a => a.Email == eml);




            Fview.mnt = d.mnt;
            Fview.yr = d.yr;

            Fview.asof = ((d.mnt.ToString()).Length < 2 ? "0" + d.mnt.ToString() + "/" + d.yr.ToString() : "" + d.mnt.ToString() + "/" + d.yr.ToString());





            return PartialView("_CreateODBUR", Fview);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateODBUR([Bind(Include = "IDAccomp,IDFinance,IDNCAADA,Obligationno,obligationamount,burno,buramount,disbursement,asof,mnt,yr,status,nca,dateOD,dateDis,cash,cashdate")]FinancialOBD FD)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    FD.DateEntered = DateTime.Now;
                    db.FinancialOBDs.Add(FD);
                    db.SaveChanges();

                    string url = Url.Action("IndexODBUR", "FinancialOBD", new { id = 0, idaccomp = FD.IDAccomp, idfinance = FD.IDFinance });

                    return Json(new { success = true, url = url });
                    //return Json(new { suceess = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }


            return PartialView("_CreateODBUR", FD);

        }


        public ActionResult MyEditODBUR(int? id)
        {


            FinancialOBDView FD = db.FinancialOBDViews.Find(id);


            if (FD == null)
            {
                return HttpNotFound();
            }




            return PartialView("_MyEditODBUR", FD);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyEditODBUR(FinancialOBDView FODB)
        {

            if (ModelState.IsValid)
            {

                var FinOBD = new FinancialOBD()
                {
                    IDFinance = FODB.IDFinance,
                    IDAccomp = FODB.IDAccomp,
                    IDObd = FODB.IDObd,
                    Obligationno = FODB.Obligationno,
                    Obligationamount = FODB.Obligationamount,
                    Burno = FODB.Burno,
                    Buramount = FODB.Buramount,
                    Disbursement = FODB.Disbursement,
                    mnt = FODB.mnt,
                    yr = FODB.yr,
                    asof = FODB.asof,
                    IDNCAADA = FODB.IDNCAADA,
                    status = FODB.status,
                    nca = FODB.nca,
                    DateEntered = FODB.DateEntered,
                    dateOD = FODB.dateOD,
                    dateDis = FODB.dateDis,
                    cash = FODB.cash,
                    cashdate = FODB.cashdate


                };

                db.Entry(FinOBD).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                string url = Url.Action("IndexODBUR", "FinancialOBD", new { id = 0, idaccomp = FODB.IDAccomp, idfinance = FODB.IDFinance });

                return Json(new { success = true, url = url });
                //return Json(new { suceess = true });
            }

            return PartialView("_MyEditODBUR", FODB);

        }

        public ActionResult DeleteODBUR(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FinancialOBDView FD = db.FinancialOBDViews.Find(id);

            if (FD == null)
            {
                return HttpNotFound();

            }
            return PartialView("_DeleteODBUR", FD);

        }

        [HttpPost, ActionName("DeleteODBUR")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteODBUR(int id)
        {

            FinancialOBD FD = db.FinancialOBDs.Find(id);
            db.FinancialOBDs.Remove(FD);
            db.SaveChanges();

            string url = Url.Action("IndexODBUR", "FinancialOBD", new { id = 0, idaccomp = FD.IDAccomp, idfinance = FD.IDFinance });

            return Json(new { success = true, url = url });

        }







    }
}