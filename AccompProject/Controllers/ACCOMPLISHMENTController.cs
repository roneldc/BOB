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
using System.Data.Entity.Validation;
using AccompProject.Services;
using AccompProject.Helpers;
using AccompProject.Models.Others;

namespace AccompProject.Controllers
{
    //  [SessionExpire]
    public class ACCOMPLISHMENTController : Controller
    {
        private AccomplishmentEntities db = new AccomplishmentEntities();
        //  sa
        private AccomplishmentEntities db1 = new AccomplishmentEntities();
        //  sa
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




            if (User.IsInRole("IMTSS Physical") || User.IsInRole("IMTSS Financial"))
            {

                //all projects
                var accomplishments = db.ACCOMPLISHMENTs
                     .OrderByDescending(r => r.year)
                     .Where(r => r.subproject.Contains(term) && r.region == strauto)
                     .Select(r => new
                     {
                         label = r.subproject
                     })
                     .Take(5);



                //var accomplishments = db.ACCOMPLISHMENTs
                //   .OrderByDescending(r => r.year)
                //   .Where(r => r.subproject.Contains(term) && r.region == strauto && r.year <= 2018)
                //   .Select(r => new
                //   {
                //       label = r.subproject
                //   })
                //   .Take(5);





                return Json(accomplishments, JsonRequestBehavior.AllowGet);
            }
            else if (User.IsInRole("Financial") || User.IsInRole("Central"))
            {


                //all projects
                var accomplishments = db.ACCOMPLISHMENTs
                      .OrderByDescending(r => r.year)
                      .Where(r => r.subproject.StartsWith(term))
                      .Select(r => new
                      {
                          label = r.subproject
                      })
                      .Take(5);

                //var accomplishments = db.ACCOMPLISHMENTs
                //     .OrderByDescending(r => r.year)
                //     .Where(r => r.subproject.StartsWith(term) && r.year <=2018)
                //     .Select(r => new
                //     {
                //         label = r.subproject
                //     })
                //     .Take(5);

                return Json(accomplishments, JsonRequestBehavior.AllowGet);

            }

            else if (User.IsInRole("NISRIP") || User.IsInRole("NISRIP Physical"))
            {
                // all projects
                var accomplishments = db.ACCOMPLISHMENTs
                      .OrderByDescending(r => r.year)
                      .Where(r => r.subproject.StartsWith(term) && r.mainproject == "NISRIP")
                      .Select(r => new
                      {
                          label = r.subproject
                      })
                      .Take(5);


                //var accomplishments = db.ACCOMPLISHMENTs
                //     .OrderByDescending(r => r.year)
                //     .Where(r => r.subproject.StartsWith(term) && r.mainproject == "NISRIP" && r.year <= 2018)
                //     .Select(r => new
                //     {
                //         label = r.subproject
                //     })
                //     .Take(5);


                return Json(accomplishments, JsonRequestBehavior.AllowGet);

            }
            else if (User.IsInRole("FSDE") || User.IsInRole("FSDE Physical"))
            {

                //all projects
                var accomplishments = db.ACCOMPLISHMENTs
                      .OrderByDescending(r => r.year)
                      .Where(r => r.subproject.StartsWith(term) && r.mainproject == "FSDE")
                      .Select(r => new
                      {
                          label = r.subproject
                      })
                      .Take(5);



                //var accomplishments = db.ACCOMPLISHMENTs
                //      .OrderByDescending(r => r.year)
                //      .Where(r => r.subproject.StartsWith(term) && r.mainproject == "FSDE" && r.year <= 2018)
                //      .Select(r => new
                //      {
                //          label = r.subproject
                //      })
                //      .Take(5);

                return Json(accomplishments, JsonRequestBehavior.AllowGet);

            }
            else if (User.IsInRole("SRIP") || User.IsInRole("SRIP Physical"))
            {

                //all projects
                var accomplishments = db.ACCOMPLISHMENTs
                      .OrderByDescending(r => r.year)
                      .Where(r => r.subproject.StartsWith(term) && r.mainproject == "SRIP")
                      .Select(r => new
                      {
                          label = r.subproject
                      })
                      .Take(5);

                //var accomplishments = db.ACCOMPLISHMENTs
                //   .OrderByDescending(r => r.year)
                //   .Where(r => r.subproject.StartsWith(term) && r.mainproject == "SRIP" && r.year <= 2018)
                //   .Select(r => new
                //   {
                //       label = r.subproject
                //   })
                //   .Take(5);

                return Json(accomplishments, JsonRequestBehavior.AllowGet);

            }
            else if (User.IsInRole("CLIMATE") || User.IsInRole("Climate Physical"))
            {

                var accomplishments = db.ACCOMPLISHMENTs
                      .OrderByDescending(r => r.year)
                      .Where(r => r.subproject.StartsWith(term) && r.mainproject.Contains("Climate"))
                      .Select(r => new
                      {
                          label = r.subproject
                      })
                      .Take(5);

                //var accomplishments = db.ACCOMPLISHMENTs
                //     .OrderByDescending(r => r.year)
                //     .Where(r => r.subproject.StartsWith(term) && r.mainproject.Contains("Climate") && r.year <= 2018 )
                //     .Select(r => new
                //     {
                //         label = r.subproject
                //     })
                //     .Take(5);

                return Json(accomplishments, JsonRequestBehavior.AllowGet);

            }
            else if (User.IsInRole("CARP") || User.IsInRole("CARP Physical"))
            {

                var accomplishments = db.ACCOMPLISHMENTs
                      .OrderByDescending(r => r.year)
                      .Where(r => r.subproject.StartsWith(term) && r.mainproject == "CARP-IC")
                      .Select(r => new
                      {
                          label = r.subproject
                      })
                      .Take(5);


                //          var accomplishments = db.ACCOMPLISHMENTs
                //.OrderByDescending(r => r.year)
                //.Where(r => r.subproject.StartsWith(term) && r.mainproject == "CARP-IC" && r.year <= 2018)
                //.Select(r => new
                //{
                //    label = r.subproject
                //})
                //.Take(5);


                return Json(accomplishments, JsonRequestBehavior.AllowGet);

            }
            else if (User.IsInRole("PIDP") || User.IsInRole("PIDP Physical"))
            {

                var accomplishments = db.ACCOMPLISHMENTs
                      .OrderByDescending(r => r.year)
                      .Where(r => r.subproject.StartsWith(term) && r.mainproject == "PIDP")
                      .Select(r => new
                      {
                          label = r.subproject
                      })
                      .Take(5);


                //var accomplishments = db.ACCOMPLISHMENTs
                //     .OrderByDescending(r => r.year)
                //     .Where(r => r.subproject.StartsWith(term) && r.mainproject == "PIDP" && r.year <= 2018)
                //     .Select(r => new
                //     {
                //         label = r.subproject
                //     })
                //     .Take(5);

                return Json(accomplishments, JsonRequestBehavior.AllowGet);

            }


            else if (User.IsInRole("ED"))
            {

                var accomplishments = db.ACCOMPLISHMENTs
                      .OrderByDescending(r => r.year)
                      .Where(r => r.subproject.StartsWith(term) && r.projectmonitor == "ED")
                      .Select(r => new
                      {
                          label = r.subproject
                      })
                      .Take(5);


                //var accomplishments = db.ACCOMPLISHMENTs
                //   .OrderByDescending(r => r.year)
                //   .Where(r => r.subproject.StartsWith(term) && r.projectmonitor == "ED" && r.year <= 2018)
                //   .Select(r => new
                //   {
                //       label = r.subproject
                //   })
                //   .Take(5);

                return Json(accomplishments, JsonRequestBehavior.AllowGet);

            }
            else
            {

                var accomplishments = db.ACCOMPLISHMENTs
                      .OrderByDescending(r => r.year)
                      .Where(r => r.subproject.Contains(term) && r.region == strauto)
                      .Select(r => new
                      {
                          label = r.subproject
                      })
                      .Take(5);


                //var accomplishments = db.ACCOMPLISHMENTs
                //     .OrderByDescending(r => r.year)
                //     .Where(r => r.subproject.Contains(term) && r.region == strauto && r.year <= 2018)
                //     .Select(r => new
                //     {
                //         label = r.subproject
                //     })
                //     .Take(5);

                return Json(accomplishments, JsonRequestBehavior.AllowGet);
            }
        }




        public ActionResult Cluster(string id)
        {
            //try
            //{
            string Constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection CON = new SqlConnection(Constring))
            {
                CON.Open();
                SqlCommand cmd = new SqlCommand("select * from tocluster where idaccomp = '" + id + "'", CON);

                string str = Convert.ToString(cmd.ExecuteScalar());
                if (string.IsNullOrEmpty(str))
                {
                    str = "Yes";
                }
                else
                {
                    str = "No";

                }


                if (str == "Yes")
                {

                    cmd.CommandText = "insert into tocluster (idaccomp,dateselected,timeselected) values (@idaccomp,@dateselected,@timeselected)";

                }
                else
                {

                    cmd.CommandText = "update tocluster set idaccomp = @idaccomp, dateselected = @dateselected, timeselected = @timeselected  where idaccomp = @idaccomp";
                }

                cmd.Connection = CON;


                cmd.Parameters.AddWithValue("@idaccomp", id);
                cmd.Parameters.AddWithValue("@dateselected", DateTime.Now);
                cmd.Parameters.AddWithValue("@timeselected", DateTime.Now.TimeOfDay);
                cmd.ExecuteNonQuery();
                TempData["msg"] += "  <div id=\"noti\" class=\"alert alert-success fde\" role=\"alert\">Project is now subject for Clustering!</div>";
                return RedirectToAction("Index");
                //    return new EmptyResult();
            }

            // }
            //catch (Exception ex)
            //{
            //    return Json(new { success = false, ExceptionMessage = "Some error here" });
            //}
        }




        public ActionResult Index()
        {


            //Session["regiontolog"] = "UPRIIS";

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


            if (User.IsInRole("IMTSS Physical") || User.IsInRole("IMTSS Financial"))
            {
                var accomplishments = db.ACCOMPLISHMENTs
                       .OrderByDescending(r => r.year)
                       .Where(r => r.region == str).ToList();



                //var accomplishments = db.ACCOMPLISHMENTs
                //      .OrderByDescending(r => r.year)
                //      .Where(r => (searchTerm == null || r.subproject.Contains(searchTerm)) && r.region == str && r.year <= 2018)
                //      .ToPagedList(page, 30);

                //if (Request.IsAjaxRequest())
                //{
                //    return PartialView("_ListOfProject", accomplishments);
                //}

                return View(accomplishments);
            }
            else if (User.IsInRole("NISRIP"))
            {
                var accomplishments = db.ACCOMPLISHMENTs
                       .OrderByDescending(r => r.year)
                       .Where(r => r.region == str && r.mainproject == "NISRIP").ToList();


                //var accomplishments = db.ACCOMPLISHMENTs
                //      .OrderByDescending(r => r.year)
                //      .Where(r => (searchTerm == null || r.subproject.Contains(searchTerm)) && r.region == str && r.mainproject == "NISRIP" && r.year <= 2018)
                //      .ToPagedList(page, 30);


                return View(accomplishments);
            }
            else if (User.IsInRole("NISRIP Physical"))
            {
                var accomplishments = db.ACCOMPLISHMENTs
                       .OrderByDescending(r => r.year)
                       .Where(r => r.mainproject == "NISRIP").ToList();




                return View(accomplishments);
            }
            else if (User.IsInRole("FSDE Physical"))
            {
                var accomplishments = db.ACCOMPLISHMENTs
                       .OrderByDescending(r => r.year)
                       .Where(r => r.mainproject == "FSDE").ToList();

                return View(accomplishments);
            }

            else if (User.IsInRole("Climate Physical"))
            {
                var accomplishments = db.ACCOMPLISHMENTs
                       .OrderByDescending(r => r.year)
                       .Where(r => r.mainproject.Contains("Climate")).ToList();



                return View(accomplishments);
            }
            else if (User.IsInRole("SRIP Physical"))
            {
                var accomplishments = db.ACCOMPLISHMENTs
                       .OrderByDescending(r => r.year)
                       .Where(r => r.mainproject == "SRIP")
                       .ToList();


                return View(accomplishments);

            }


            //else if (User.IsInRole("PIDP Physical"))
            //{
            //    var accomplishments = db.ACCOMPLISHMENTs
            //           .OrderByDescending(r => r.year)
            //           .Where(r => (searchTerm == null || r.subproject.Contains(searchTerm)) && r.mainproject == "PIDP")
            //           .ToPagedList(page, 60);

            //    if (Request.IsAjaxRequest())
            //    {
            //        return PartialView("_ListOfProject", accomplishments);
            //    }

            //    return View(accomplishments);
            //}


            else if (User.IsInRole("ED"))
            {
                var accomplishments = db.ACCOMPLISHMENTs
                       .OrderByDescending(r => r.year)
                       .Where(r => r.projectmonitor == "ED")
                       .ToList();


                return View(accomplishments);
            }
            else if (User.IsInRole("CARP Physical"))
            {
                var accomplishments = db.ACCOMPLISHMENTs
                       .OrderByDescending(r => r.year)
                       .Where(r => r.mainproject == "CARP-IC")
                       .ToList();



                return View(accomplishments);
            }
            else if (User.IsInRole("Financial") || User.IsInRole("Central"))
            {

                var accomplishments = db.ACCOMPLISHMENTs
                     .OrderByDescending(r => r.year)
                    .ToList();


                return View(accomplishments);

            }
            else
            {
                var accomplishments = db.ACCOMPLISHMENTs
                       .OrderByDescending(r => r.year)
                       .Where(r => r.region == str)
                      .ToList();


                if (Request.IsAjaxRequest())
                {
                    return PartialView("_ListOfProject", accomplishments);
                }

                return View(accomplishments);

            }

            //string date = "2014-09-30 00:00:00.000";
            //DateTime dt = Convert.ToDateTime(date);

            //var accomplishments = db.ACCOMPLISHMENTs
            //  .OrderByDescending(r => r.year)
            //  .Where(r => r.as_of == dt && r.year == 2014)
            //  .Take(100);

            //if (Request.IsAjaxRequest())
            //{
            //    return PartialView("_ListOfProject", accomplishments);
            //}

            //return View(accomplishments);
            //return View(db.ACCOMPLISHMENTs.ToList());
        }


        //  index 9-15-2020 old code

        //public ActionResult Index(string searchTerm = null, int page = 1)
        //   {


        //       //Session["regiontolog"] = "UPRIIS";

        //       if (Session["regiontolog"] == null)
        //       {

        //           return RedirectToAction("LogIn", "Account", new { area = "" });

        //       }
        //       string str = Session["regiontolog"].ToString();

        //       int mnt = DateTime.Now.Month;
        //       int yr = DateTime.Now.Year;
        //       Session["mnt"] = mnt;
        //       Session["yr"] = yr;
        //       var now = DateTime.Now;
        //       var DaysInMonth = DateTime.DaysInMonth(now.Year, now.Month);
        //       var lastDay = new DateTime(now.Year, now.Month, DaysInMonth);
        //       Session["asof"] = lastDay;
        //       ViewBag.DTETME = DateTime.Now.ToString("MMMM yyyy");


        //       if (User.IsInRole("IMTSS Physical") || User.IsInRole("IMTSS Financial"))
        //       {
        //           var accomplishments = db.ACCOMPLISHMENTs
        //                  .OrderByDescending(r => r.year)
        //                  .Where(r => (searchTerm == null || r.subproject.Contains(searchTerm)) && r.region == str)
        //                  .ToPagedList(page, 60);



        //           //var accomplishments = db.ACCOMPLISHMENTs
        //           //      .OrderByDescending(r => r.year)
        //           //      .Where(r => (searchTerm == null || r.subproject.Contains(searchTerm)) && r.region == str && r.year <= 2018)
        //           //      .ToPagedList(page, 30);

        //           if (Request.IsAjaxRequest())
        //           {
        //               return PartialView("_ListOfProject", accomplishments);
        //           }

        //           return View(accomplishments);
        //       }
        //       else if (User.IsInRole("NISRIP") )
        //       {
        //           var accomplishments = db.ACCOMPLISHMENTs
        //                  .OrderByDescending(r => r.year)
        //                  .Where(r => (searchTerm == null || r.subproject.Contains(searchTerm)) && r.region == str && r.mainproject == "NISRIP")
        //                  .ToPagedList(page, 60);


        //           //var accomplishments = db.ACCOMPLISHMENTs
        //           //      .OrderByDescending(r => r.year)
        //           //      .Where(r => (searchTerm == null || r.subproject.Contains(searchTerm)) && r.region == str && r.mainproject == "NISRIP" && r.year <= 2018)
        //           //      .ToPagedList(page, 30);

        //           if (Request.IsAjaxRequest())
        //           {
        //               return PartialView("_ListOfProject", accomplishments);
        //           }

        //           return View(accomplishments);
        //       }
        //       else if ( User.IsInRole("NISRIP Physical"))
        //       {
        //           var accomplishments = db.ACCOMPLISHMENTs
        //                  .OrderByDescending(r => r.year)
        //                  .Where(r => (searchTerm == null || r.subproject.Contains(searchTerm)) && r.mainproject == "NISRIP")
        //                  .ToPagedList(page, 60);


        //           //var accomplishments = db.ACCOMPLISHMENTs
        //           //      .OrderByDescending(r => r.year)
        //           //      .Where(r => (searchTerm == null || r.subproject.Contains(searchTerm)) && r.mainproject == "NISRIP" && r.year <= 2018)
        //           //      .ToPagedList(page, 30);


        //           if (Request.IsAjaxRequest())
        //           {
        //               return PartialView("_ListOfProject", accomplishments);
        //           }

        //           return View(accomplishments);
        //       }
        //       else if (User.IsInRole("FSDE Physical"))
        //       {
        //           var accomplishments = db.ACCOMPLISHMENTs
        //                  .OrderByDescending(r => r.year)
        //                  .Where(r => (searchTerm == null || r.subproject.Contains(searchTerm)) && r.mainproject == "FSDE" )
        //                  .ToPagedList(page, 60);

        //           if (Request.IsAjaxRequest())
        //           {
        //               return PartialView("_ListOfProject", accomplishments);
        //           }

        //           return View(accomplishments);
        //       }

        //       else if (User.IsInRole("Climate Physical"))
        //       {
        //           var accomplishments = db.ACCOMPLISHMENTs
        //                  .OrderByDescending(r => r.year)
        //                  .Where(r => (searchTerm == null || r.subproject.Contains(searchTerm)) && r.mainproject.Contains("Climate"))
        //                  .ToPagedList(page, 60);

        //           if (Request.IsAjaxRequest())
        //           {
        //               return PartialView("_ListOfProject", accomplishments);
        //           }

        //           return View(accomplishments);
        //       }
        //       else if (User.IsInRole("SRIP Physical"))
        //       {
        //           var accomplishments = db.ACCOMPLISHMENTs
        //                  .OrderByDescending(r => r.year)
        //                  .Where(r => (searchTerm == null || r.subproject.Contains(searchTerm)) && r.mainproject == "SRIP")
        //                  .ToPagedList(page, 60);

        //           if (Request.IsAjaxRequest())
        //           {
        //               return PartialView("_ListOfProject", accomplishments);
        //           }

        //           return View(accomplishments);

        //       }


        //       else if (User.IsInRole("PIDP Physical"))
        //       {
        //           var accomplishments = db.ACCOMPLISHMENTs
        //                  .OrderByDescending(r => r.year)
        //                  .Where(r => (searchTerm == null || r.subproject.Contains(searchTerm)) && r.mainproject == "PIDP")
        //                  .ToPagedList(page, 60);

        //           if (Request.IsAjaxRequest())
        //           {
        //               return PartialView("_ListOfProject", accomplishments);
        //           }

        //           return View(accomplishments);
        //       }


        //       else if (User.IsInRole("ED"))
        //       {
        //           var accomplishments = db.ACCOMPLISHMENTs
        //                  .OrderByDescending(r => r.year)
        //                  .Where(r => (searchTerm == null || r.subproject.Contains(searchTerm)) && r.projectmonitor == "ED")
        //                  .ToPagedList(page, 60);

        //           if (Request.IsAjaxRequest())
        //           {
        //               return PartialView("_ListOfProject", accomplishments);
        //           }

        //           return View(accomplishments);
        //       }
        //       else if (User.IsInRole("CARP Physical"))
        //       {
        //           var accomplishments = db.ACCOMPLISHMENTs
        //                  .OrderByDescending(r => r.year)
        //                  .Where(r => (searchTerm == null || r.subproject.Contains(searchTerm)) && r.mainproject == "CARP-IC")
        //                  .ToPagedList(page, 60);

        //           if (Request.IsAjaxRequest())
        //           {
        //               return PartialView("_ListOfProject", accomplishments);
        //           }

        //           return View(accomplishments);
        //       }
        //       else if (User.IsInRole("Financial") || User.IsInRole("Central"))
        //       {

        //           var accomplishments = db.ACCOMPLISHMENTs
        //                .OrderByDescending(r => r.year)
        //                .Where(r => (searchTerm == null || r.subproject.Contains(searchTerm)))
        //                .ToPagedList(page, 60);


        //           if (Request.IsAjaxRequest())
        //           {
        //               return PartialView("_ListOfProject", accomplishments);
        //           }

        //           return View(accomplishments);

        //       }
        //       else
        //       {
        //           var accomplishments = db.ACCOMPLISHMENTs
        //                  .OrderByDescending(r => r.year)
        //                  .Where(r => (searchTerm == null || r.subproject.Contains(searchTerm)) && r.region == str )
        //                  .ToPagedList(page, 60);


        //           if (Request.IsAjaxRequest())
        //           {
        //               return PartialView("_ListOfProject", accomplishments);
        //           }

        //           return View(accomplishments);

        //       }

        //       //string date = "2014-09-30 00:00:00.000";
        //       //DateTime dt = Convert.ToDateTime(date);

        //       //var accomplishments = db.ACCOMPLISHMENTs
        //       //  .OrderByDescending(r => r.year)
        //       //  .Where(r => r.as_of == dt && r.year == 2014)
        //       //  .Take(100);

        //       //if (Request.IsAjaxRequest())
        //       //{
        //       //    return PartialView("_ListOfProject", accomplishments);
        //       //}

        //       //return View(accomplishments);
        //       //return View(db.ACCOMPLISHMENTs.ToList());
        //   }




        //  index 9-15-2020 old code

        public ActionResult IndexOld(string searchTerm = null, int page = 1)
        {


            //Session["regiontolog"] = "UPRIIS";

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


            if (User.IsInRole("IMTSS Physical") || User.IsInRole("IMTSS Financial"))
            {
                var accomplishments = db.ACCOMPLISHMENTs
                       .OrderByDescending(r => r.year)
                       .Where(r => (searchTerm == null || r.subproject.Contains(searchTerm)) && r.region == str)
                       .ToPagedList(page, 60);



                //var accomplishments = db.ACCOMPLISHMENTs
                //      .OrderByDescending(r => r.year)
                //      .Where(r => (searchTerm == null || r.subproject.Contains(searchTerm)) && r.region == str && r.year <= 2018)
                //      .ToPagedList(page, 30);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_ListOfProject", accomplishments);
                }

                return View(accomplishments);
            }
            else if (User.IsInRole("NISRIP"))
            {
                var accomplishments = db.ACCOMPLISHMENTs
                       .OrderByDescending(r => r.year)
                       .Where(r => (searchTerm == null || r.subproject.Contains(searchTerm)) && r.region == str && r.mainproject == "NISRIP")
                       .ToPagedList(page, 60);


                //var accomplishments = db.ACCOMPLISHMENTs
                //      .OrderByDescending(r => r.year)
                //      .Where(r => (searchTerm == null || r.subproject.Contains(searchTerm)) && r.region == str && r.mainproject == "NISRIP" && r.year <= 2018)
                //      .ToPagedList(page, 30);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_ListOfProject", accomplishments);
                }

                return View(accomplishments);
            }
            else if (User.IsInRole("NISRIP Physical"))
            {
                var accomplishments = db.ACCOMPLISHMENTs
                       .OrderByDescending(r => r.year)
                       .Where(r => (searchTerm == null || r.subproject.Contains(searchTerm)) && r.mainproject == "NISRIP")
                       .ToPagedList(page, 60);


                //var accomplishments = db.ACCOMPLISHMENTs
                //      .OrderByDescending(r => r.year)
                //      .Where(r => (searchTerm == null || r.subproject.Contains(searchTerm)) && r.mainproject == "NISRIP" && r.year <= 2018)
                //      .ToPagedList(page, 30);


                if (Request.IsAjaxRequest())
                {
                    return PartialView("_ListOfProject", accomplishments);
                }

                return View(accomplishments);
            }
            else if (User.IsInRole("FSDE Physical"))
            {
                var accomplishments = db.ACCOMPLISHMENTs
                       .OrderByDescending(r => r.year)
                       .Where(r => (searchTerm == null || r.subproject.Contains(searchTerm)) && r.mainproject == "FSDE")
                       .ToPagedList(page, 60);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_ListOfProject", accomplishments);
                }

                return View(accomplishments);
            }

            else if (User.IsInRole("Climate Physical"))
            {
                var accomplishments = db.ACCOMPLISHMENTs
                       .OrderByDescending(r => r.year)
                       .Where(r => (searchTerm == null || r.subproject.Contains(searchTerm)) && r.mainproject.Contains("Climate"))
                       .ToPagedList(page, 60);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_ListOfProject", accomplishments);
                }

                return View(accomplishments);
            }
            else if (User.IsInRole("SRIP Physical"))
            {
                var accomplishments = db.ACCOMPLISHMENTs
                       .OrderByDescending(r => r.year)
                       .Where(r => (searchTerm == null || r.subproject.Contains(searchTerm)) && r.mainproject == "SRIP")
                       .ToPagedList(page, 60);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_ListOfProject", accomplishments);
                }

                return View(accomplishments);

            }


            else if (User.IsInRole("PIDP Physical"))
            {
                var accomplishments = db.ACCOMPLISHMENTs
                       .OrderByDescending(r => r.year)
                       .Where(r => (searchTerm == null || r.subproject.Contains(searchTerm)) && r.mainproject == "PIDP")
                       .ToPagedList(page, 60);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_ListOfProject", accomplishments);
                }

                return View(accomplishments);
            }


            else if (User.IsInRole("ED"))
            {
                var accomplishments = db.ACCOMPLISHMENTs
                       .OrderByDescending(r => r.year)
                       .Where(r => (searchTerm == null || r.subproject.Contains(searchTerm)) && r.projectmonitor == "ED")
                       .ToPagedList(page, 60);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_ListOfProject", accomplishments);
                }

                return View(accomplishments);
            }
            else if (User.IsInRole("CARP Physical"))
            {
                var accomplishments = db.ACCOMPLISHMENTs
                       .OrderByDescending(r => r.year)
                       .Where(r => (searchTerm == null || r.subproject.Contains(searchTerm)) && r.mainproject == "CARP-IC")
                       .ToPagedList(page, 60);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_ListOfProject", accomplishments);
                }

                return View(accomplishments);
            }
            else if (User.IsInRole("Financial") || User.IsInRole("Central"))
            {

                var accomplishments = db.ACCOMPLISHMENTs
                     .OrderByDescending(r => r.year)
                     .Where(r => (searchTerm == null || r.subproject.Contains(searchTerm)))
                     .ToPagedList(page, 60);


                if (Request.IsAjaxRequest())
                {
                    return PartialView("_ListOfProjectOld", accomplishments);
                }

                return View(accomplishments);

            }
            else
            {
                var accomplishments = db.ACCOMPLISHMENTs
                       .OrderByDescending(r => r.year)
                       .Where(r => (searchTerm == null || r.subproject.Contains(searchTerm)) && r.region == str)
                       .ToPagedList(page, 60);


                if (Request.IsAjaxRequest())
                {
                    return PartialView("_ListOfProject", accomplishments);
                }

                return View(accomplishments);

            }

            //string date = "2014-09-30 00:00:00.000";
            //DateTime dt = Convert.ToDateTime(date);

            //var accomplishments = db.ACCOMPLISHMENTs
            //  .OrderByDescending(r => r.year)
            //  .Where(r => r.as_of == dt && r.year == 2014)
            //  .Take(100);

            //if (Request.IsAjaxRequest())
            //{
            //    return PartialView("_ListOfProject", accomplishments);
            //}

            //return View(accomplishments);
            //return View(db.ACCOMPLISHMENTs.ToList());
        }



        public ActionResult IndexCluster(string searchTerm = null, int page = 1)
        {


            //Session["regiontolog"] = "UPRIIS";

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



            var accomplishments = db.ToClusterViews
                   .OrderByDescending(r => r.year)
                   .Where(r => r.region == str)
                   .ToPagedList(page, 30);


            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListOfProjectCluster", accomplishments);
            }

            return View(accomplishments);



            //string date = "2014-09-30 00:00:00.000";
            //DateTime dt = Convert.ToDateTime(date);

            //var accomplishments = db.ACCOMPLISHMENTs
            //  .OrderByDescending(r => r.year)
            //  .Where(r => r.as_of == dt && r.year == 2014)
            //  .Take(100);

            //if (Request.IsAjaxRequest())
            //{
            //    return PartialView("_ListOfProject", accomplishments);
            //}

            //return View(accomplishments);
            //return View(db.ACCOMPLISHMENTs.ToList());
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

        // GET: ACCOMPLISHMENT/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ACCOMPLISHMENT/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDAccomp,year,region,province,district,Category1,Category2,Category3,Category4,mainproject,subproject,subsubproject,municipality,amount,Allotment,Obligation,newed,restored,rehab,canals,canal_lining,structures,roads,farmer_beneficiaries,jobs,remarks,new_accomp,resto_accomp,rehab_accomp,canals_accomp,canal_lining_accomp,structures_accomp,roads_accomp,Beneficiary_accomp,JobGen,Physical,Financial,Remarks_accomp,status,Value_accomp,Expenditures,Phy,Fin,FUSA,OK,MONITORING1,MONITORING2,PC,VAL,EXP,as_of,SAMPLE,saro,asa,p_new,p_resto,p_rehab,p_canal,p_canal_lining,p_structure,p_road,p_job,p_fb,disbursement")] ACCOMPLISHMENT aCCOMPLISHMENT)
        {
            if (ModelState.IsValid)
            {
                db.ACCOMPLISHMENTs.Add(aCCOMPLISHMENT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aCCOMPLISHMENT);
        }

        // GET: ACCOMPLISHMENT/Edit/5
        public ActionResult Edit(string id)
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

        // POST: ACCOMPLISHMENT/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDAccomp,year,region,province,district,Category1,Category2,Category3,Category4,mainproject,subproject,subsubproject,municipality,amount,Allotment,Obligation,newed,restored,rehab,canals,canal_lining,structures,roads,farmer_beneficiaries,jobs,remarks,new_accomp,resto_accomp,rehab_accomp,canals_accomp,canal_lining_accomp,structures_accomp,roads_accomp,Beneficiary_accomp,JobGen,Physical,Financial,Remarks_accomp,status,Value_accomp,Expenditures,Phy,Fin,FUSA,OK,MONITORING1,MONITORING2,PC,VAL,EXP,as_of,SAMPLE,saro,asa,p_new,p_resto,p_rehab,p_canal,p_canal_lining,p_structure,p_road,p_job,p_fb,disbursement,RowVersion,mnt,year_covered,cash,saro_region,asa_region,cash_region,remarks_financial,asano")] ACCOMPLISHMENT aCCOMPLISHMENT)
        {
            try
            {


                if (ModelState.IsValid)
                {

                    db.Entry(aCCOMPLISHMENT).State = System.Data.Entity.EntityState.Modified;


                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var clientValues = (ACCOMPLISHMENT)entry.Entity;
                var databaseEntry = (ACCOMPLISHMENT)entry.GetDatabaseValues().ToObject();

                ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                + "was modified by another user after you got the original value. The "
                + "edit operation was canceled and the current values in the database "
                + "have been displayed. If you still want to edit this record, click "
                + "the Save button again. Otherwise click the Back to List hyperlink.");
                aCCOMPLISHMENT.RowVersion = databaseEntry.RowVersion;

            }

            return View(aCCOMPLISHMENT);
        }




        // GET: ACCOMPLISHMENT/Delete/5
        public ActionResult Remove(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToClusterView aCCOMPLISHMENT = db.ToClusterViews.Find(id);
            if (aCCOMPLISHMENT == null)
            {
                return HttpNotFound();
            }
            return View(aCCOMPLISHMENT);
        }

        // POST: ACCOMPLISHMENT/Delete/5
        [HttpPost, ActionName("Remove")]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveCluster(int id)
        {
            ToCluster aCCOMPLISHMENT = db.ToClusters.Find(id);
            db.ToClusters.Remove(aCCOMPLISHMENT);
            db.SaveChanges();
            return RedirectToAction("IndexCluster");
        }









        // GET: ACCOMPLISHMENT/Delete/5
        public ActionResult Delete(string id)
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

        // POST: ACCOMPLISHMENT/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ACCOMPLISHMENT aCCOMPLISHMENT = db.ACCOMPLISHMENTs.Find(id);
            db.ACCOMPLISHMENTs.Remove(aCCOMPLISHMENT);
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








        #region TARGET EDIT



        public ActionResult EditAccomp(string IDAccomp = null)
        {

            var accomp = db.ACCOMPLISHMENTs.Where(x => x.IDAccomp == IDAccomp).FirstOrDefault();

            if (accomp == null)
            {

                HttpNotFound();

            }




            return View(accomp);
        }




        public ActionResult EditAccompCoord(string IDAccomp = null)
        {

            var accomp = db.ACCOMPLISHMENTs.Where(x => x.IDAccomp == IDAccomp).FirstOrDefault();

            if (accomp == null)
            {

                HttpNotFound();

            }




            return View(accomp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccomp(ACCOMPLISHMENT accomp, HttpPostedFileBase upload)
        {



            #region checking

            var ac = new AccompChanges();


            var ACHANGES = new AChanges
            {
                Idphysical = 0,
                idaccomp = accomp.IDAccomp

            };

            var strhoney = "select isnull(newed,0), isnull(restored,0) , isnull(rehab,0), isnull(canals,0), isnull(canal_lining,0) " +
                " , isnull(roads,0), isnull(structures,0), isnull(HDPE,0), isnull(COCONET,0), isnull(GRAVEL,0)" +
                " from ACCOMPLISHMENTBaseline where idaccomp = '" + accomp.IDAccomp + "'";



            string Constring1 = ConfigurationManager.ConnectionStrings["AccomplishmentEntitiesSecurity"].ConnectionString;
            using (SqlConnection CON = new SqlConnection(Constring1))
            {
                CON.Open();
                SqlCommand cmd = new SqlCommand(strhoney, CON);
                SqlDataReader reader;


                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    //update 7-17-2019
                    var GRAVELACCOMP = accomp.GRAVEL;
                    if (GRAVELACCOMP == null)
                    {
                        GRAVELACCOMP = 0;
                    }
                    if (reader.GetDouble(9) > GRAVELACCOMP)
                    {

                        ACHANGES.fname = "GRAVEL";
                        ACHANGES.comp = "GREATER THAN";

                        ac.SaveChangesTarget(ACHANGES);



                    }

                    if (reader.GetDouble(9) < GRAVELACCOMP)
                    {

                        ACHANGES.fname = "GRAVEL";
                        ACHANGES.comp = "LESS THAN";

                        ac.SaveChangesTarget(ACHANGES);



                    }

                    if (reader.GetDouble(9) == GRAVELACCOMP)
                    {

                        ACHANGES.fname = "GRAVEL";
                        ACHANGES.comp = "EQUAL";

                        ac.SaveChangesTarget(ACHANGES);



                    }







                    var COCONETACCOMP = accomp.COCONET;
                    if (COCONETACCOMP == null)
                    {
                        COCONETACCOMP = 0;
                    }


                    if (reader.GetDouble(8) > COCONETACCOMP)
                    {

                        ACHANGES.fname = "COCONET";
                        ACHANGES.comp = "GREATER THAN";

                        ac.SaveChangesTarget(ACHANGES);


                    }


                    if (reader.GetDouble(8) < COCONETACCOMP)
                    {

                        ACHANGES.fname = "COCONET";
                        ACHANGES.comp = "LESS THAN";

                        ac.SaveChangesTarget(ACHANGES);


                    }


                    if (reader.GetDouble(8) == COCONETACCOMP)
                    {

                        ACHANGES.fname = "COCONET";
                        ACHANGES.comp = "EQUAL";

                        ac.SaveChangesTarget(ACHANGES);


                    }



                    var HDPEACCOMP = accomp.HDPE;
                    if (HDPEACCOMP == null)
                    {
                        HDPEACCOMP = 0;
                    }

                    if (reader.GetDouble(7) > HDPEACCOMP)
                    {
                        ACHANGES.fname = "HDPE";
                        ACHANGES.comp = "GREATER THAN";

                        ac.SaveChangesTarget(ACHANGES);



                    }

                    if (reader.GetDouble(7) < HDPEACCOMP)
                    {
                        ACHANGES.fname = "HDPE";
                        ACHANGES.comp = "LESS THAN";

                        ac.SaveChangesTarget(ACHANGES);



                    }


                    if (reader.GetDouble(7) == HDPEACCOMP)
                    {
                        ACHANGES.fname = "HDPE";
                        ACHANGES.comp = "EQUAL";

                        ac.SaveChangesTarget(ACHANGES);



                    }





                    //2-10-2020
                    var structuresaccomp = accomp.structures;
                    if (structuresaccomp == null)
                    {
                        structuresaccomp = 0;
                    }

                    if (reader.GetDouble(6) > structuresaccomp)
                    {

                        ACHANGES.fname = "STRUCTURES";
                        ACHANGES.comp = "GREATER THAN";


                        ac.SaveChangesTarget(ACHANGES);

                    }



                    if (reader.GetDouble(6) < structuresaccomp)
                    {

                        ACHANGES.fname = "STRUCTURES";
                        ACHANGES.comp = "LESS THAN";

                        ac.SaveChangesTarget(ACHANGES);


                    }
                    if (reader.GetDouble(6) == structuresaccomp)
                    {

                        ACHANGES.fname = "STRUCTURES";
                        ACHANGES.comp = "EQUAL";

                        ac.SaveChangesTarget(ACHANGES);


                    }









                    var roadsaccomp = accomp.roads;
                    if (roadsaccomp == null)
                    {
                        roadsaccomp = 0;
                    }

                    if (reader.GetDouble(5) > roadsaccomp)
                    {

                        ACHANGES.fname = "ROADS";
                        ACHANGES.comp = "GREATER THAN";

                        ac.SaveChangesTarget(ACHANGES);


                    }


                    if (reader.GetDouble(5) < roadsaccomp)
                    {

                        ACHANGES.fname = "ROADS";
                        ACHANGES.comp = "LESS THAN";

                        ac.SaveChangesTarget(ACHANGES);

                    }


                    if (reader.GetDouble(5) == roadsaccomp)
                    {

                        ACHANGES.fname = "ROADS";
                        ACHANGES.comp = "EQUAL";

                        ac.SaveChangesTarget(ACHANGES);

                    }





                    var canal_liningaccomp = accomp.canal_lining;
                    if (canal_liningaccomp == null)
                    {
                        canal_liningaccomp = 0;
                    }

                    if (reader.GetDouble(4) > canal_liningaccomp)
                    {

                        ACHANGES.fname = "CANAL LINING";
                        ACHANGES.comp = "GREATER THAN";

                        ac.SaveChangesTarget(ACHANGES);


                    }

                    if (reader.GetDouble(4) < canal_liningaccomp)
                    {

                        ACHANGES.fname = "CANAL LINING";
                        ACHANGES.comp = "LESS THAN";

                        ac.SaveChangesTarget(ACHANGES);

                    }



                    if (reader.GetDouble(4) == canal_liningaccomp)
                    {

                        ACHANGES.fname = "CANAL LINING";
                        ACHANGES.comp = "EQUAL";

                        ac.SaveChangesTarget(ACHANGES);

                    }












                    //2-10-2020
                    var canalsaccomp = accomp.canals;
                    if (canalsaccomp == null)
                    {
                        canalsaccomp = 0;
                    }

                    if (reader.GetDouble(3) > canalsaccomp)
                    {

                        ACHANGES.fname = "CANAL";
                        ACHANGES.comp = "GREATER THAN";

                        ac.SaveChangesTarget(ACHANGES);


                    }



                    if (reader.GetDouble(3) < canalsaccomp)
                    {

                        ACHANGES.fname = "CANAL";
                        ACHANGES.comp = "LESS THAN";

                        ac.SaveChangesTarget(ACHANGES);


                    }



                    if (reader.GetDouble(3) == canalsaccomp)
                    {

                        ACHANGES.fname = "CANAL";
                        ACHANGES.comp = "EQUAL";

                        ac.SaveChangesTarget(ACHANGES);


                    }





                    var rehabarea = accomp.rehab;
                    if (rehabarea == null)
                    {
                        rehabarea = 0;
                    }


                    if (reader.GetInt32(2) > rehabarea)
                    {

                        ACHANGES.fname = "REHAB";
                        ACHANGES.comp = "GREATER THAN";

                        ac.SaveChangesTarget(ACHANGES);


                    }


                    if (reader.GetInt32(2) < rehabarea)
                    {

                        ACHANGES.fname = "REHAB";
                        ACHANGES.comp = "LESS THAN";

                        ac.SaveChangesTarget(ACHANGES);


                    }


                    if (reader.GetInt32(2) == rehabarea)
                    {

                        ACHANGES.fname = "REHAB";
                        ACHANGES.comp = "EQUAL";

                        ac.SaveChangesTarget(ACHANGES);


                    }



                    //2-10-2020
                    var newarea = accomp.newed;
                    if (newarea == null)
                    {
                        newarea = 0;
                    }

                    if (reader.GetInt32(0) > newarea)
                    {

                        ACHANGES.fname = "NEW";
                        ACHANGES.comp = "GREATER THAN";

                        ac.SaveChangesTarget(ACHANGES);


                    }


                    if (reader.GetInt32(0) < newarea)
                    {

                        ACHANGES.fname = "NEW";
                        ACHANGES.comp = "LESS THAN";

                        ac.SaveChangesTarget(ACHANGES);


                    }


                    if (reader.GetInt32(0) == newarea)
                    {

                        ACHANGES.fname = "NEW";
                        ACHANGES.comp = "EQUAL";

                        ac.SaveChangesTarget(ACHANGES);


                    }




                    var restorearea = accomp.restored;
                    if (restorearea == null)
                    {
                        restorearea = 0;
                    }

                    if (reader.GetInt32(1) > restorearea)
                    {

                        ACHANGES.fname = "RESTORE";
                        ACHANGES.comp = "GREATER THAN";

                        ac.SaveChangesTarget(ACHANGES);


                    }

                    if (reader.GetInt32(1) < restorearea)
                    {

                        ACHANGES.fname = "RESTORE";
                        ACHANGES.comp = "LESS THAN";

                        ac.SaveChangesTarget(ACHANGES);


                    }


                    if (reader.GetInt32(1) == restorearea)
                    {

                        ACHANGES.fname = "RESTORE";
                        ACHANGES.comp = "EQUAL";

                        ac.SaveChangesTarget(ACHANGES);


                    }







                }

                // Data is accessible through the


            }




            #endregion
















            if (ModelState.IsValid)
            {
                if (accomp.File != null && accomp.File.ContentLength > 0)
                {
                    if (accomp.Obligation > 0)
                    {


                        using (var context = new AccomplishmentEntities())
                        {
                            FileAccompTarget aCCOMPLISHMENT = context.FileAccompTargets.AsNoTracking().Where(c => c.IDAccomp == accomp.IDAccomp).FirstOrDefault();

                            if (aCCOMPLISHMENT != null)
                            {
                                context.FileAccompTargets.Attach(aCCOMPLISHMENT);
                                context.FileAccompTargets.Remove(aCCOMPLISHMENT);

                                context.SaveChanges();
                            }
                        }
                    }



                    accomp.Obligation = 1;

                    db.Entry(accomp).State = System.Data.Entity.EntityState.Modified;


                    db.SaveChanges();


                    FileUploadServiceDam service = new FileUploadServiceDam();
                    service.SaveFileDetailsTargetAccomp(accomp.File, accomp.IDAccomp, "TARGET");




                    //12-26-2020

                    var PA = new FileAccompTarget()
                    {

                        IDAccomp = accomp.IDAccomp,
                        dateupload = DateTime.Now,
                        timeupload = DateTime.Now.TimeOfDay,
                        remarks = accomp.mainproject,
                        FileName = System.IO.Path.GetFileName(accomp.File.FileName),
                        FileType = 1,
                        ContentType = ""

                    };
                    //using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    //{
                    //    PA.Content = reader.ReadBytes(upload.ContentLength);
                    //}

                    db.FileAccompTargets.Add(PA);
                    db.SaveChanges();






                    //sms
                    //var msg = "";
                    //msg = msg + " " + accomp.region + " updated the target " + accomp.subproject + " under " + accomp.mainproject;
                    //msg = msg + " under GAA " + accomp.year;
                    //msg = msg + "  for details refer to Reportserver";

                    //SMS sSMS = new SMS();

                    //if (accomp.projectmonitor == "OD")
                    //{
                    //    var smsnum = db.SMS
                    //   .Where(a => a.monitoredarea == accomp.region && a.projectmonitor == accomp.projectmonitor && a.position == "AREAMONITOR").ToList();

                    //    if (smsnum.Count != 0)
                    //    {
                    //        for (int i = 0; i < smsnum.Count; i++)
                    //        {
                    //            if (smsnum[i].status.ToString() == "YES")
                    //            {


                    //                sSMS.sendSMS(smsnum[i].SMSnumber.ToString(), msg);
                    //                //  System.Threading.Thread.Sleep(3000);
                    //            }


                    //        }
                    //    }

                    //}

                    //if (accomp.projectmonitor == "ED")
                    //{
                    //    var smsnum = db.SMS
                    //   .Where(a => a.projectmonitor == accomp.projectmonitor && a.position == "AREAMONITOR").ToList();

                    //    if (smsnum.Count != 0)
                    //    {
                    //        for (int i = 0; i < smsnum.Count; i++)
                    //        {
                    //            if (smsnum[i].status.ToString() == "YES")
                    //            {


                    //                sSMS.sendSMS(smsnum[i].SMSnumber.ToString(), msg);
                    //                //  System.Threading.Thread.Sleep(3000);
                    //            }


                    //        }
                    //    }

                    //}














                    return RedirectToAction("Index");

                }
                else
                {

                    TempData["msg"] = "<h4 class = \"alert alert-danger fde\"> Upload justification File to Edit the Target </h4>";

                }



















                //   }


                //catch (DbEntityValidationException e)
                //{
                //    foreach (var eve in e.EntityValidationErrors)
                //    {
                //        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                //        foreach (var ve in eve.ValidationErrors)
                //        {
                //            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                //                ve.PropertyName, ve.ErrorMessage);
                //        }
                //    }
                //    throw;
                //}





            }


            return View(accomp);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccompCoord(ACCOMPLISHMENT accomp)
        {



            if (ModelState.IsValid)
            {


                db.Entry(accomp).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");


            }

            return View(accomp);



        }

        #endregion




        #region Map

        public ActionResult IndexMap()
        {



            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //var ip = db.ACCOMPLISHMENTs.Where(x => x.p_new > 0).ToList();


           return View();


        }



        public ActionResult IndexDelineate()
        {
            //0212D24B-8B9F-4544-89E3-0369ECF9B213
            //   string strauto = "0212D24B-8B9F-4544-89E3-0369ECF9B213";


            ViewBag.Coordinates = "[]";




            ViewBag.MarkersPICpoly = "[]";
            ViewBag.Markers = "[]";




            //new 

            var ip = db.ACCOMPLISHMENTs.Where(x => x.p_structure > 0).ToList();



            return PartialView("_IndexDelineate", ip);
            //    return View();
        }


        #endregion





    }


    public class SessionExpireAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (HttpContext.Current.Session["regiontolog"] == null)
            {
                AuthenticationManager.SignOut();
                filterContext.Result = new RedirectResult("~/Account/Login");
                return;
            }


            base.OnActionExecuting(filterContext);
        }

        public IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }

    }















}
