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
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Net.Mail;
using AccompProject.Resources.Constants;
using AccompProject.Hubs;
using AccompProject.Services;
using System.IO.Ports;
using System.Globalization;
using AccompProject.Helpers;
using AccompProject.Models.Others;





namespace AccompProject.Controllers
{




    public class PhysicalAccompController : Controller
    {
        private AccomplishmentEntities db = new AccomplishmentEntities();
        SerialPort sp = new SerialPort();
        // GET: PhysicalAccomp

        public ActionResult SMSREAD()
        {

            SMS sSMS = new SMS();

            string val1 = sSMS.ReceiveSMS();
            ViewBag.smsread = val1;

            string val2 = "";


            string[] stringSeparators = new string[] { "+CMGL:", "OK", "REC READ" };
            string[] authorsList = val1.Split(stringSeparators, StringSplitOptions.None);

            foreach (string author in authorsList)
            {

                val2 = val2 + " " + author + Environment.NewLine;

            }

            ViewBag.one = authorsList[0] + " vone";
            ViewBag.two = authorsList[1] + " v2";

            ViewBag.three = authorsList[2] + " v3";

            ViewBag.four = authorsList[3] + " v4";
            ViewBag.five = authorsList[4] + " v5";

            ViewBag.six = authorsList[5] + " v6";
            ViewBag.seven = authorsList[6] + " v7";
            ViewBag.eight = authorsList[7] + " v8";

            ViewBag.nine = authorsList[8] + " v9";



            ViewBag.smsread1 = val2;

            return View();
        }


        public ActionResult Index(string id)
        {

            ViewBag.IDAccomp = id;
            Session["idaccomp"] = id;

            var physicalaccomp = db.PhysicalAccompViews
                .OrderByDescending(a => a.yr).ThenByDescending(a => a.mnt)
                .Where(a => a.IDAccomp == id);



            if (physicalaccomp == null)
            {
                return HttpNotFound();
            }
            // return View(asa);
            return PartialView("_Index", physicalaccomp);



        }


        public ActionResult IndexIssues(string id)
        {

            ViewBag.IDAccomp = id;
            Session["idaccomp"] = id;

            var physicalaccomp = db.ProjectIssuesViews
                .OrderByDescending(a => a.yr).ThenByDescending(a => a.mnt)
                .Where(a => a.IDAccomp == id);



            if (physicalaccomp == null)
            {
                return HttpNotFound();
            }
            // return View(asa);
            return PartialView("_IndexIssues", physicalaccomp);



        }

        public ActionResult IndexStatus(string id)
        {

            ViewBag.IDAccomp = id;
            Session["idaccomp"] = id;

            var physicalaccomp = db.ProjectStatusImplementationViews
                .OrderByDescending(a => a.yr).ThenByDescending(a => a.mnt)
                .Where(a => a.IDAccomp == id);



            if (physicalaccomp == null)
            {
                return HttpNotFound();
            }
            // return View(asa);
            return PartialView("_IndexStatus", physicalaccomp);



        }


        public ActionResult IndexFSDE(string id)
        {

            ViewBag.IDAccomp = id;
            Session["idaccomp"] = id;

            var physicalaccomp = db.FSDEPhysicalViews
                .OrderByDescending(a => a.yr).ThenByDescending(a => a.mnt)
                .Where(a => a.IDAccomp == id);



            if (physicalaccomp == null)
            {
                return HttpNotFound();
            }
            // return View(asa);
            return PartialView("_IndexFSDE", physicalaccomp);



        }


        public ActionResult IndexFSDEStudy(string id)
        {

            ViewBag.IDAccomp = id;
            Session["idaccomp"] = id;

            var physicalaccomp = db.FSDEContractStudyViews
                .OrderByDescending(a => a.TypeofStudy)
                .Where(a => a.IDAccomp == id);



            if (physicalaccomp == null)
            {
                return HttpNotFound();
            }
            // return View(asa);
            return PartialView("_IndexFSDEStudy", physicalaccomp);



        }


        public ActionResult IndexFSDEStudyPersonel(int id)
        {

            ViewBag.IDStudy = id;
            Session["idstudy"] = id;

            var physicalaccomp = db.FSDEConsultantListViews
                .OrderByDescending(a => a.TypeofStudy)
                .Where(a => a.IDStudy == id);



            if (physicalaccomp == null)
            {
                return HttpNotFound();
            }
            // return View(asa);
            return PartialView("_IndexFSDEStudyPersonel", physicalaccomp);



        }


        public ActionResult UpdateCoordinates(string id)
        {
            ACCOMPLISHMENT FD = db.ACCOMPLISHMENTs.Find(id);


            if (FD == null)
            {
                return HttpNotFound();
            }




            return View(FD);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCoordinates(ACCOMPLISHMENT acc)
        {
            string Constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection CON = new SqlConnection(Constring))
            {
                CON.Open();
                SqlCommand cmd = new SqlCommand("update accomplishment set lat = '" + acc.lat + "'" + ", longi = '" + acc.longi + "'" + " where idaccomp = '" + acc.IDAccomp + "'", CON);


                cmd.ExecuteNonQuery();
                return RedirectToAction("Index", "Accomplishment", "");
                // return Json(new { success = true });

            }



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


        public ActionResult EditIssues(string id)
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

        public ActionResult EditStatus(string id)
        {




            //List<SelectListItem> items = new List<SelectListItem>();

            //items.Add(new SelectListItem { Text = "Not Yet Started", Value = "Not Yet Started" });

            //items.Add(new SelectListItem { Text = "Pre-Procurement", Value = "Pre-Procurement" });

            //items.Add(new SelectListItem { Text = "Procurement", Value = "Procurement" });

            //items.Add(new SelectListItem { Text = "Awarded", Value = "Awarded" });

            //items.Add(new SelectListItem { Text = "On-Going", Value = "On-Going" });
            //items.Add(new SelectListItem { Text = "Completed", Value = "Completed" });
            ////            ViewBag.TypeOfDam = items;

            //ViewBag.StatusProj = new SelectList(items, "Value", "Text");


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


        public ActionResult EditFSDE(string id)
        {




            //List<SelectListItem> items = new List<SelectListItem>();

            //items.Add(new SelectListItem { Text = "Not Yet Started", Value = "Not Yet Started" });

            //items.Add(new SelectListItem { Text = "Pre-Procurement", Value = "Pre-Procurement" });

            //items.Add(new SelectListItem { Text = "Procurement", Value = "Procurement" });

            //items.Add(new SelectListItem { Text = "Awarded", Value = "Awarded" });

            //items.Add(new SelectListItem { Text = "On-Going", Value = "On-Going" });
            //items.Add(new SelectListItem { Text = "Completed", Value = "Completed" });
            ////            ViewBag.TypeOfDam = items;

            //ViewBag.StatusProj = new SelectList(items, "Value", "Text");


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

        public ActionResult EditFSDEStudy(string id)
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

        public ActionResult EditFSDEStudyPersonel(int id)
        {



            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FSDEContractStudyView financ = db.FSDEContractStudyViews.Find(id);
            Session["subproject"] = financ.subproject;
            if (financ == null)
            {
                return HttpNotFound();
            }


            return View(financ);
        }

        public ActionResult Create(string Id)
        {

            //ACCOMPLISHMENT Fview = new ACCOMPLISHMENT();
            //Fview.IDAccomp = Id;
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACCOMPLISHMENT financ = db.ACCOMPLISHMENTs.Find(Id);
            //Session["subproject"] = financ.subproject;
            if (financ == null)
            {
                return HttpNotFound();
            }
            // return View(financ);
            financ.IDAccomp = Id;
            return PartialView("_Create", financ);
        }


        public ActionResult CreateIssues(string Id)
        {

            //ACCOMPLISHMENT Fview = new ACCOMPLISHMENT();
            //Fview.IDAccomp = Id;
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.problema = new SelectList(db.ListofIssues, "code", "description");


            ProjectIssue pi = new ProjectIssue();

            //Session["subproject"] = financ.subproject;

            // return View(financ);
            pi.Idaccomp = Id;
            return PartialView("_CreateIssues", pi);
        }



        public ActionResult CreateFSDEStudy(string Id)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "", Value = "" });
            items.Add(new SelectListItem { Text = "FS", Value = "FS" });
            items.Add(new SelectListItem { Text = "DE", Value = "DE" });
            items.Add(new SelectListItem { Text = "FSDE", Value = "FSDE" });


            ViewBag.StatusProj = new SelectList(items, "Value", "Text", "");

            ViewBag.ConsultantList = new SelectList(db.ConsultantLists, "IdConsultant", "Consultant");
            FSDEContractStudy Fview = new FSDEContractStudy();
            Fview.IDAccomp = Id;


            return PartialView("_CreateFSDEStudy", Fview);
        }


        public ActionResult CreateFSDEStudyPersonel(int Id)
        {

            ViewBag.PositionLists = new SelectList(db.PositionLists, "PositionID", "Position");
            StudyConsultant Fview = new StudyConsultant();
            Fview.IDStudy = Id;


            return PartialView("_CreateFSDEStudyPersonel", Fview);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFSDEStudy([Bind(Include = "IDAccomp,IDStudy,typeofStudy,Consultant")] FSDEContractStudy FD)
        {



            if ((ModelState.IsValid))
            {
                try
                {



                    //audit trail



                    db.FSDEContractStudies.Add(FD);
                    db.SaveChanges();






                    string url = Url.Action("IndexFSDEStudy", "PhysicalAccomp", new { id = FD.IDAccomp });

                    return Json(new { success = true, url = url });
                    //return Json(new { suceess = true });





                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }


            return PartialView("_CreateFSDEStudy", FD);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFSDEStudyPersonel([Bind(Include = "IDStudy,lname,fname,midname,sufix, position")] StudyConsultant FD)
        {



            if ((ModelState.IsValid))
            {
                try
                {



                    //audit trail



                    db.StudyConsultants.Add(FD);
                    db.SaveChanges();






                    string url = Url.Action("IndexFSDEStudyPersonel", "PhysicalAccomp", new { id = FD.IDStudy });

                    return Json(new { success = true, url = url });
                    //return Json(new { suceess = true });





                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }


            return PartialView("_CreateFSDEStudyPersonel", FD);

        }





        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "IDAccomp,newarea,restorearea,rehabarea,canalsaccomp,canal_liningaccomp,structuresaccomp,roadsaccomp,fbaccomp,jobsaccomp,asof,mnt,yr")]PhysicalAccomp FD)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            db.PhysicalAccomps.Add(FD);
        //            db.SaveChanges();

        //            string url = Url.Action("Index", "PhysicalAccomp", new { id = FD.IDAccomp });

        //            return Json(new { success = true, url = url });
        //            //return Json(new { suceess = true });
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex);
        //        }
        //    }


        //    return PartialView("_Create", FD);

        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateIssues(ProjectIssue FD)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    int mntsesssion = Convert.ToInt32(Session["mnttoedit"].ToString());

                    int yrsesssion = Convert.ToInt32(Session["yrtoedit"].ToString());
                    string asof1 = "";

                    if (mntsesssion >= 10)
                    {

                        asof1 = mntsesssion.ToString() + "/" + yrsesssion.ToString();


                    }
                    else
                    {

                        asof1 = "0" + mntsesssion.ToString() + "/" + yrsesssion.ToString();

                    }


                    var PA = new ProjectIssue()
                    {



                        asof = asof1,
                        mnt = mntsesssion,
                        yr = yrsesssion,
                        Idaccomp = FD.Idaccomp,
                        description = FD.description,
                        dateencoded = DateTime.Now,
                        problem = FD.problem,
                        username = User.Identity.Name

                    };

                    db.ProjectIssues.Add(PA);
                    db.SaveChanges();




                    string url = Url.Action("IndexIssues", "PhysicalAccomp", new { id = FD.Idaccomp });

                    return Json(new { success = true, url = url });
                    //return Json(new { suceess = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            ViewBag.problema = new SelectList(db.ListofIssues, "code", "description", FD.problem);


            return PartialView("_CreateIssues", FD);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyEditIssues(ProjectIssuesView FD)
        {

            //if (ModelState.IsValid)
            //{
            try
            {



                var PA = new ProjectIssue()
                {



                    asof = FD.asof,
                    mnt = FD.mnt,
                    yr = FD.yr,
                    Idaccomp = FD.IDAccomp,
                    description = FD.description,
                    dateencoded = DateTime.Now,
                    problem = FD.problem,
                    username = User.Identity.Name,
                    IDProblem = FD.IDProblem
                };

                db.Entry(PA).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();




                string url = Url.Action("IndexIssues", "PhysicalAccomp", new { id = FD.IDAccomp });

                return Json(new { success = true, url = url });
                //return Json(new { suceess = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            //  }
            ViewBag.problema = new SelectList(db.ListofIssues, "code", "description", FD.problem);


            return PartialView("_MyEditIssues", FD);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ACCOMPLISHMENT FD, HttpPostedFileBase uploadfile)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var PA = new PhysicalAccomp()
                    {

                        IDAccomp = FD.IDAccomp,
                        newarea = FD.new_accomp,
                        restorearea = FD.resto_accomp,
                        rehabarea = FD.rehab_accomp,
                        canalsaccomp = FD.canals_accomp,
                        canal_liningaccomp = FD.canal_lining_accomp,
                        structuresaccomp = FD.structures_accomp,
                        roadsaccomp = FD.roads_accomp,
                        fbaccomp = FD.Beneficiary_accomp,
                        jobsaccomp = FD.JobGen,
                        asof = FD.asof,
                        mnt = FD.mnt,
                        yr = FD.year_covered,
                        remarksAccomp = FD.Remarks_accomp,
                        Physical = FD.Physical,
                        Financial = FD.Financial,
                        ValueAccomp = FD.Value_accomp,
                        Expenditures = FD.Expenditures,
                        HDPEACCOMP = FD.HDPEACCOMP,
                        COCONETACCOMP = FD.COCONETACCOMP,
                        GRAVELACCOMP = FD.GRAVELACCOMP


                    };

                    db.PhysicalAccomps.Add(PA);
                    db.SaveChanges();

                    if (uploadfile != null && uploadfile.ContentLength > 0)
                    {
                        var avatar = new FileAccomp()
                        {
                            FileName = System.IO.Path.GetFileName(uploadfile.FileName),
                            //FileType = FileType.Avatar,
                            ContentType = uploadfile.ContentType,
                            IDAccomp = FD.IDAccomp
                        };
                        using (var reader = new System.IO.BinaryReader(uploadfile.InputStream))
                        {
                            avatar.Content = reader.ReadBytes(uploadfile.ContentLength);
                        }

                        db.FileAccomps.Add(avatar);
                        db.SaveChanges();
                    }


                    string url = Url.Action("Index", "PhysicalAccomp", new { id = FD.IDAccomp });

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


            PhysicalAccompView FD = db.PhysicalAccompViews.Find(id);


            if (FD == null)
            {
                return HttpNotFound();
            }




            return PartialView("_MyEdit", FD);
        }
        public ActionResult MyEditTarget(int? id)
        {


            PhysicalAccompView FD = db.PhysicalAccompViews.Find(id);


            if (FD == null)
            {
                return HttpNotFound();
            }




            return PartialView("_MyEditTarget", FD);
        }
        public ActionResult MyEditIssues(int? id)
        {


            ProjectIssuesView FD = db.ProjectIssuesViews.Find(id);


            if (FD == null)
            {
                return HttpNotFound();
            }


            ViewBag.problema = new SelectList(db.ListofIssues, "code", "description", FD.problem);


            return PartialView("_MyEditIssues", FD);
        }

        //free for all
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult MyEdit(PhysicalAccompView FODB, HttpPostedFileBase upload)
        //{

        //    #region Checking

        //    var ac = new AccompChanges();
        //    var valids = true;
        //    TempData["msg"] = "<h4 class = \"alert alert-danger fde\"> Error list </h4>";
        //    var mntcheck = (FODB.mnt);
        //    var myyr = FODB.yr;


        //    var ACHANGES = new AChanges
        //    {
        //        Idphysical = FODB.IDPhysical,
        //        idaccomp = ""

        //    };




        //    if (mntcheck < 1)
        //    {

        //        mntcheck = 12;
        //        myyr = (myyr) - 1;
        //    }

        //    var strhoney = "select isnull(physical,0), mnt, yr, isnull(financial,0), isnull(newarea,0), isnull(restorearea,0) , isnull(rehabarea,0), isnull(canalsaccomp,0), isnull(canal_liningaccomp,0) " +
        //        " , isnull(roadsaccomp,0), isnull(structuresaccomp,0), isnull(HDPEACCOMP,0), isnull(COCONETACCOMP,0), isnull(GRAVELACCOMP,0)" +
        //        " from physicalaccompbaseline where idaccomp = '" + FODB.IDAccomp + "'" +
        //        " and mnt = " + mntcheck +
        //        " and yr = " + myyr;


        //    string Constring1 = ConfigurationManager.ConnectionStrings["AccomplishmentEntitiesSecurity"].ConnectionString;
        //    using (SqlConnection CON = new SqlConnection(Constring1))
        //    {
        //        CON.Open();
        //        SqlCommand cmd = new SqlCommand(strhoney, CON);
        //        SqlDataReader reader;


        //        reader = cmd.ExecuteReader();

        //        if (reader.Read())
        //        {
        //            //update 7-17-2019
        //            var GRAVELACCOMP = FODB.GRAVELACCOMP;
        //            if (GRAVELACCOMP == null)
        //            {
        //                GRAVELACCOMP = 0;
        //            }
        //            if (reader.GetDouble(13) > GRAVELACCOMP)
        //            {

        //                ACHANGES.fname = "GRAVEL";
        //                ACHANGES.comp = "GREATER THAN";

        //                ac.SaveChanges(ACHANGES);



        //            }

        //            if (reader.GetDouble(13) < GRAVELACCOMP)
        //            {

        //                ACHANGES.fname = "GRAVEL";
        //                ACHANGES.comp = "LESS THAN";

        //                ac.SaveChanges(ACHANGES);



        //            }

        //            if (reader.GetDouble(13) == GRAVELACCOMP)
        //            {

        //                ACHANGES.fname = "GRAVEL";
        //                ACHANGES.comp = "EQUAL";

        //                ac.SaveChanges(ACHANGES);



        //            }







        //            var COCONETACCOMP = FODB.COCONETACCOMP;
        //            if (COCONETACCOMP == null)
        //            {
        //                COCONETACCOMP = 0;
        //            }


        //            if (reader.GetDouble(12) > COCONETACCOMP)
        //            {

        //                ACHANGES.fname = "COCONET";
        //                ACHANGES.comp = "GREATER THAN";

        //                ac.SaveChanges(ACHANGES);


        //            }


        //            if (reader.GetDouble(12) < COCONETACCOMP)
        //            {

        //                ACHANGES.fname = "COCONET";
        //                ACHANGES.comp = "LESS THAN";

        //                ac.SaveChanges(ACHANGES);


        //            }


        //            if (reader.GetDouble(12) == COCONETACCOMP)
        //            {

        //                ACHANGES.fname = "COCONET";
        //                ACHANGES.comp = "EQUAL";

        //                ac.SaveChanges(ACHANGES);


        //            }



        //            var HDPEACCOMP = FODB.HDPEACCOMP;
        //            if (HDPEACCOMP == null)
        //            {
        //                HDPEACCOMP = 0;
        //            }

        //            if (reader.GetDouble(11) > HDPEACCOMP)
        //            {
        //                ACHANGES.fname = "HDPE";
        //                ACHANGES.comp = "GREATER THAN";

        //                ac.SaveChanges(ACHANGES);



        //            }

        //            if (reader.GetDouble(11) < HDPEACCOMP)
        //            {
        //                ACHANGES.fname = "HDPE";
        //                ACHANGES.comp = "LESS THAN";

        //                ac.SaveChanges(ACHANGES);



        //            }


        //            if (reader.GetDouble(11) == HDPEACCOMP)
        //            {
        //                ACHANGES.fname = "HDPE";
        //                ACHANGES.comp = "EQUAL";

        //                ac.SaveChanges(ACHANGES);



        //            }





        //            //2-10-2020
        //            var structuresaccomp = FODB.structuresaccomp;
        //            if (structuresaccomp == null)
        //            {
        //                structuresaccomp = 0;
        //            }

        //            if (reader.GetDouble(10) > structuresaccomp)
        //            {

        //                ACHANGES.fname = "STRUCTURES";
        //                ACHANGES.comp = "GREATER THAN";

        //                ac.SaveChanges(ACHANGES);


        //            }



        //            if (reader.GetDouble(10) < structuresaccomp)
        //            {

        //                ACHANGES.fname = "STRUCTURES";
        //                ACHANGES.comp = "LESS THAN";

        //                ac.SaveChanges(ACHANGES);


        //            }
        //            if (reader.GetDouble(10) == structuresaccomp)
        //            {

        //                ACHANGES.fname = "STRUCTURES";
        //                ACHANGES.comp = "EQUAL";

        //                ac.SaveChanges(ACHANGES);


        //            }









        //            var roadsaccomp = FODB.roadsaccomp;
        //            if (roadsaccomp == null)
        //            {
        //                roadsaccomp = 0;
        //            }

        //            if (reader.GetDouble(9) > roadsaccomp)
        //            {

        //                ACHANGES.fname = "ROADS";
        //                ACHANGES.comp = "GREATER THAN";

        //                ac.SaveChanges(ACHANGES);


        //            }


        //            if (reader.GetDouble(9) < roadsaccomp)
        //            {

        //                ACHANGES.fname = "ROADS";
        //                ACHANGES.comp = "LESS THAN";

        //                ac.SaveChanges(ACHANGES);

        //            }


        //            if (reader.GetDouble(9) == roadsaccomp)
        //            {

        //                ACHANGES.fname = "ROADS";
        //                ACHANGES.comp = "EQUAL";

        //                ac.SaveChanges(ACHANGES);

        //            }





        //            var canal_liningaccomp = FODB.canal_liningaccomp;
        //            if (canal_liningaccomp == null)
        //            {
        //                canal_liningaccomp = 0;
        //            }

        //            if (reader.GetDouble(8) > canal_liningaccomp)
        //            {

        //                ACHANGES.fname = "CANAL LINING";
        //                ACHANGES.comp = "GREATER THAN";

        //                ac.SaveChanges(ACHANGES);


        //            }

        //            if (reader.GetDouble(8) < canal_liningaccomp)
        //            {

        //                ACHANGES.fname = "CANAL LINING";
        //                ACHANGES.comp = "LESS THAN";

        //                ac.SaveChanges(ACHANGES);

        //            }



        //            if (reader.GetDouble(8) == canal_liningaccomp)
        //            {

        //                ACHANGES.fname = "CANAL LINING";
        //                ACHANGES.comp = "EQUAL";

        //                ac.SaveChanges(ACHANGES);

        //            }












        //            //2-10-2020
        //            var canalsaccomp = FODB.canalsaccomp;
        //            if (canalsaccomp == null)
        //            {
        //                canalsaccomp = 0;
        //            }

        //            if (reader.GetDouble(7) > canalsaccomp)
        //            {

        //                ACHANGES.fname = "CANAL";
        //                ACHANGES.comp = "GREATER THAN";

        //                ac.SaveChanges(ACHANGES);


        //            }



        //            if (reader.GetDouble(7) < canalsaccomp)
        //            {

        //                ACHANGES.fname = "CANAL";
        //                ACHANGES.comp = "LESS THAN";

        //                ac.SaveChanges(ACHANGES);


        //            }



        //            if (reader.GetDouble(7) == canalsaccomp)
        //            {

        //                ACHANGES.fname = "CANAL";
        //                ACHANGES.comp = "EQUAL";

        //                ac.SaveChanges(ACHANGES);


        //            }





        //            var rehabarea = FODB.rehabarea;
        //            if (rehabarea == null)
        //            {
        //                rehabarea = 0;
        //            }


        //            if (reader.GetInt32(6) > rehabarea)
        //            {

        //                ACHANGES.fname = "REHAB";
        //                ACHANGES.comp = "GREATER THAN";

        //                ac.SaveChanges(ACHANGES);


        //            }


        //            if (reader.GetInt32(6) < rehabarea)
        //            {

        //                ACHANGES.fname = "REHAB";
        //                ACHANGES.comp = "LESS THAN";

        //                ac.SaveChanges(ACHANGES);


        //            }


        //            if (reader.GetInt32(6) == rehabarea)
        //            {

        //                ACHANGES.fname = "REHAB";
        //                ACHANGES.comp = "EQUAL";

        //                ac.SaveChanges(ACHANGES);


        //            }



        //            //2-10-2020
        //            var newarea = FODB.newarea;
        //            if (newarea == null)
        //            {
        //                newarea = 0;
        //            }

        //            if (reader.GetInt32(4) > newarea)
        //            {

        //                ACHANGES.fname = "NEW";
        //                ACHANGES.comp = "GREATER THAN";

        //                ac.SaveChanges(ACHANGES);


        //            }


        //            if (reader.GetInt32(4) < newarea)
        //            {

        //                ACHANGES.fname = "NEW";
        //                ACHANGES.comp = "LESS THAN";

        //                ac.SaveChanges(ACHANGES);


        //            }


        //            if (reader.GetInt32(4) == newarea)
        //            {

        //                ACHANGES.fname = "NEW";
        //                ACHANGES.comp = "EQUAL";

        //                ac.SaveChanges(ACHANGES);


        //            }




        //            var restorearea = FODB.restorearea;
        //            if (restorearea == null)
        //            {
        //                restorearea = 0;
        //            }

        //            if (reader.GetInt32(5) > restorearea)
        //            {

        //                ACHANGES.fname = "RESTORE";
        //                ACHANGES.comp = "GREATER THAN";

        //                ac.SaveChanges(ACHANGES);


        //            }

        //            if (reader.GetInt32(5) < restorearea)
        //            {

        //                ACHANGES.fname = "RESTORE";
        //                ACHANGES.comp = "LESS THAN";

        //                ac.SaveChanges(ACHANGES);


        //            }


        //            if (reader.GetInt32(5) == restorearea)
        //            {

        //                ACHANGES.fname = "RESTORE";
        //                ACHANGES.comp = "EQUAL";

        //                ac.SaveChanges(ACHANGES);


        //            }




        //            var Physical = FODB.Physical;
        //            if (Physical == null)
        //            {
        //                Physical = 0;
        //            }
        //            if (reader.GetDouble(0) > Physical)
        //            {

        //                ACHANGES.fname = "PHYSICAL";
        //                ACHANGES.comp = "GREATER THAN";

        //                ac.SaveChanges(ACHANGES);

        //            }


        //            if (reader.GetDouble(0) < Physical)
        //            {

        //                ACHANGES.fname = "PHYSICAL";
        //                ACHANGES.comp = "LESS THAN";

        //                ac.SaveChanges(ACHANGES);


        //            }


        //            if (reader.GetDouble(0) == Physical)
        //            {

        //                ACHANGES.fname = "PHYSICAL";
        //                ACHANGES.comp = "EQUAL";

        //                ac.SaveChanges(ACHANGES);


        //            }





        //            var Financial = FODB.Financial;
        //            if (Financial == null)
        //            {
        //                Financial = 0;
        //            }

        //            if (reader.GetDouble(3) > Financial)
        //            {

        //                ACHANGES.fname = "FINANCIAL";
        //                ACHANGES.comp = "GREATER THAN";

        //                ac.SaveChanges(ACHANGES);


        //            }


        //            if (reader.GetDouble(3) < Financial)
        //            {

        //                ACHANGES.fname = "FINANCIAL";
        //                ACHANGES.comp = "GREATER THAN";

        //                ac.SaveChanges(ACHANGES);


        //            }


        //            if (reader.GetDouble(3) == Financial)
        //            {

        //                ACHANGES.fname = "FINANCIAL";
        //                ACHANGES.comp = "EQUAL";

        //                ac.SaveChanges(ACHANGES);


        //            }


        //        }

        //        // Data is accessible through the


        //    }


        //    if (FODB.Physical > 100)
        //    {

        //        valids = false;
        //        TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\"> Physical Accomplishment must not greater than 100! </div>";

        //    }


        //    if (FODB.Financial > 100)
        //    {

        //        valids = false;
        //        TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\"> Financial Accomplishment must not greater than 100! </div>";

        //    }



        //    if (upload == null)
        //    {

        //        valids = false;
        //        TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\"> Upload Justification for Editing </div>";

        //    }



        //    else
        //    {

        //        if (upload.ContentLength <= 0)
        //        {

        //            valids = false;
        //            TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\"> Upload Justification for Editing </div>";

        //        }
                    

        //    }

        //    #endregion

        //    if ((ModelState.IsValid) && (valids != false))
        //    {



        //        //audit trail

        //        string Constring = ConfigurationManager.ConnectionStrings["AccomplishmentEntitiesSecurity"].ConnectionString;
        //        using (SqlConnection CON = new SqlConnection(Constring))
        //        {
        //            CON.Open();
        //            SqlCommand cmd = new SqlCommand("insert into PhysicalAccompLog (userid,dateedit,timeedit,idaccomp) values (@userid,@dateedit,@timeedit,@idaccomp)", CON);
        //            cmd.Parameters.AddWithValue("@userid", Session["userid"].ToString());
        //            cmd.Parameters.AddWithValue("@idaccomp", FODB.IDAccomp);
        //            cmd.Parameters.AddWithValue("@dateedit", DateTime.Now);
        //            cmd.Parameters.AddWithValue("@timeedit", DateTime.Now.TimeOfDay);
        //            cmd.ExecuteNonQuery();


        //        }


        //        var FinOBD = new PhysicalAccomp()
        //        {
        //            IDPhysical = FODB.IDPhysical,
        //            IDAccomp = FODB.IDAccomp,
        //            newarea = FODB.newarea,
        //            restorearea = FODB.restorearea,
        //            rehabarea = FODB.rehabarea,
        //            canalsaccomp = FODB.canalsaccomp,
        //            canal_liningaccomp = FODB.canal_liningaccomp,
        //            structuresaccomp = FODB.structuresaccomp,
        //            roadsaccomp = FODB.roadsaccomp,
        //            mnt = FODB.mnt,
        //            yr = FODB.yr,
        //            asof = FODB.asof,
        //            fbaccomp = FODB.fbaccomp,
        //            jobsaccomp = FODB.jobsaccomp,
        //            remarksAccomp = FODB.remarksAccomp,
        //            Physical = FODB.Physical,
        //            Financial = FODB.Financial,
        //            ValueAccomp = FODB.ValueAccomp,
        //            Expenditures = FODB.Expenditures,
        //            COCONETACCOMP = FODB.COCONETACCOMP,
        //            HDPEACCOMP = FODB.HDPEACCOMP,
        //            GRAVELACCOMP = FODB.GRAVELACCOMP


        //        };

        //        db.Entry(FinOBD).State = System.Data.Entity.EntityState.Modified;
        //        db.SaveChanges();


        //        FileUploadServiceDam service = new FileUploadServiceDam();
        //        service.SaveFileDetailsTargetAccomp(upload, FODB.IDPhysical.ToString(), "Accomplishment");
              




        //        //6-23-2020

        //        //var msg = "";
        //        //msg = msg + " " + FODB.region + " updated the project " + FODB.subproject + " under " + FODB.mainproject;
        //        //msg = msg + " Physical (%): " + FODB.Physical;
        //        //msg = msg + " under GAA " + FODB.year;
        //        //msg = msg + " for details refer to Reportserver";

        //        //SMS sSMS = new SMS();

        //        //if (FODB.projectmonitor == "OD")
        //        //{
        //        //    var smsnum = db.SMS
        //        //   .Where(a => a.monitoredarea == FODB.region && a.projectmonitor == FODB.projectmonitor && a.position == "AREAMONITOR").ToList();

        //        //    if (smsnum.Count != 0)
        //        //    {
        //        //        for (int i = 0; i < smsnum.Count; i++)
        //        //        {
        //        //            if (smsnum[i].status.ToString() == "YES")
        //        //            {


        //        //                sSMS.sendSMS(smsnum[i].SMSnumber.ToString(), msg);
        //        //                //  System.Threading.Thread.Sleep(3000);
        //        //            }


        //        //        }
        //        //    }

        //        //}

        //        //if (FODB.projectmonitor == "ED")
        //        //{
        //        //    var smsnum = db.SMS
        //        //   .Where(a => a.projectmonitor == FODB.projectmonitor && a.position == "AREAMONITOR").ToList();

        //        //    if (smsnum.Count != 0)
        //        //    {
        //        //        for (int i = 0; i < smsnum.Count; i++)
        //        //        {
        //        //            if (smsnum[i].status.ToString() == "YES")
        //        //            {


        //        //                sSMS.sendSMS(smsnum[i].SMSnumber.ToString(), msg);
        //        //                //  System.Threading.Thread.Sleep(3000);
        //        //            }


        //        //        }
        //        //    }

        //        //}


        //        //if (FODB.projectmonitor == "ED")
        //        //{




        //        //}



        //        //Upload file



        //        FileAccompAccomp faa = db.FileAccompAccomps.AsNoTracking().Where(c => c.IDPhysical == FODB.IDPhysical).FirstOrDefault();


        //        if (faa != null)
        //        {
        //            using (var context = new AccomplishmentEntities())
        //            {
        //                context.FileAccompAccomps.Attach(faa);
        //                context.FileAccompAccomps.Remove(faa);

        //                context.SaveChanges();
        //            }
        //        }

        //        using (var db3 = new AccomplishmentEntities())
        //        {

        //            var PA = new FileAccompAccomp()
        //            {

        //                IDAccomp = FODB.IDAccomp,
        //                IDPhysical = FODB.IDPhysical,
        //                dateupload = DateTime.Now,
        //                timeupload = DateTime.Now.TimeOfDay,
        //                FileName = System.IO.Path.GetFileName(upload.FileName),
        //                FileType = 1,
        //                ContentType = ""

        //            };
        //            //using (var reader = new System.IO.BinaryReader(upload.InputStream))
        //            //{
        //            //    PA.Content = reader.ReadBytes(upload.ContentLength);
        //            //}

        //            db3.FileAccompAccomps.Add(PA);
        //            db3.SaveChanges();
        //        }



        //        string url = Url.Action("Index", "PhysicalAccomp", new { id = FODB.IDAccomp });

        //        return Json(new { success = true, url = url });
        //        //return Json(new { suceess = true });
        //    }

        //    return PartialView("_MyEdit", FODB);

        //}




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyEdit(PhysicalAccompView FODB)
        {
            var valids = true;

            #region checking

            TempData["msg"] = "<h4 class = \"alert alert-danger fde\"> Error list </h4>";
            var mntcheck = (FODB.mnt) - 1;
            var myyr = FODB.yr;


            if (mntcheck < 1)
            {

                mntcheck = 12;
                myyr = (myyr) - 1;
            }

            var strhoney = "select isnull(physical,0), mnt, yr, isnull(financial,0), isnull(newarea,0), isnull(restorearea,0) , isnull(rehabarea,0), isnull(canalsaccomp,0), isnull(canal_liningaccomp,0) " +
                " , isnull(roadsaccomp,0), isnull(structuresaccomp,0), isnull(HDPEACCOMP,0), isnull(COCONETACCOMP,0), isnull(GRAVELACCOMP,0)" +
                " from physicalaccomp where idaccomp = '" + FODB.IDAccomp + "'" +
                " and mnt = " + mntcheck +
                " and yr = " + myyr;


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
                    var GRAVELACCOMP = FODB.GRAVELACCOMP;
                    if (GRAVELACCOMP == null)
                    {
                        GRAVELACCOMP = 0;
                    }
                    if (reader.GetDouble(13) > GRAVELACCOMP)
                    {

                        TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\"> GRAVEL Accomplishment is lower than previous month! </div>";
                        valids = false;

                    }


                    var COCONETACCOMP = FODB.COCONETACCOMP;
                    if (COCONETACCOMP == null)
                    {
                        COCONETACCOMP = 0;
                    }


                    if (reader.GetDouble(12) > COCONETACCOMP)
                    {

                        TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\"> COCONET Accomplishment is lower than previous month! </div>";
                        valids = false;

                    }



                    var HDPEACCOMP = FODB.HDPEACCOMP;
                    if (HDPEACCOMP == null)
                    {
                        HDPEACCOMP = 0;
                    }

                    if (reader.GetDouble(11) > HDPEACCOMP)
                    {

                        TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\"> HDPE Accomplishment is lower than previous month! </div>";
                        valids = false;

                    }

                    //2-10-2020
                    var structuresaccomp = FODB.structuresaccomp;
                    if (structuresaccomp == null)
                    {
                        structuresaccomp = 0;
                    }

                    if (reader.GetDouble(10) > structuresaccomp)
                    {

                        TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\"> Structures Accomplishment is lower than previous month! </div>";
                        valids = false;

                    }


                    var roadsaccomp = FODB.roadsaccomp;
                    if (roadsaccomp == null)
                    {
                        roadsaccomp = 0;
                    }

                    if (reader.GetDouble(9) > roadsaccomp)
                    {

                        TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\"> Roads Accomplishment is lower than previous month! </div>";
                        valids = false;

                    }



                    var canal_liningaccomp = FODB.canal_liningaccomp;
                    if (canal_liningaccomp == null)
                    {
                        canal_liningaccomp = 0;
                    }

                    if (reader.GetDouble(8) > canal_liningaccomp)
                    {

                        TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\"> Canal Lining Accomplishment is lower than previous month! </div>";
                        valids = false;

                    }
                    //2-10-2020
                    var canalsaccomp = FODB.canalsaccomp;
                    if (canalsaccomp == null)
                    {
                        canalsaccomp = 0;
                    }

                    if (reader.GetDouble(7) > canalsaccomp)
                    {

                        TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\"> Canal Accomplishment is lower than previous month! </div>";
                        valids = false;

                    }


                    var rehabarea = FODB.rehabarea;
                    if (rehabarea == null)
                    {
                        rehabarea = 0;
                    }


                    if (reader.GetInt32(6) > rehabarea)
                    {

                        TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\"> New Rehab Accomplishment is lower than previous month! </div>";
                        valids = false;

                    }



                    //2-10-2020
                    var newarea = FODB.newarea;
                    if (newarea == null)
                    {
                        newarea = 0;
                    }

                    if (reader.GetDouble(4) > newarea)
                    {

                        TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\"> New Area Accomplishment is lower than previous month! </div>";
                        valids = false;

                    }




                    var restorearea = FODB.restorearea;
                    if (restorearea == null)
                    {
                        restorearea = 0;
                    }

                    if (reader.GetDouble(5) > restorearea)
                    {

                        TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\"> Restore Area Accomplishment is lower than previous month! </div>";
                        valids = false;

                    }



                    var Physical = FODB.Physical;
                    if (Physical == null)
                    {
                        Physical = 0;
                    }
                    if (reader.GetDouble(0) > Physical)
                    {

                        TempData["msg"] += "  <div class=\"alert alert-danger fde\" role=\"alert\">Physical Accomplishment is lower than previous month! </div>";
                        valids = false;

                    }


                    var Financial = FODB.Financial;
                    if (Financial == null)
                    {
                        Financial = 0;
                    }

                    if (reader.GetDouble(3) > Financial)
                    {

                        TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\"> Financial Accomplishment is lower than previous month! </div>";
                        valids = false;

                    }


                    TempData["msg"] += "";
                }

                // Data is accessible through the


            }


            if (FODB.Physical > 100)
            {

                valids = false;
                TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\"> Physical Accomplishment must not greater than 100! </div>";

            }


            if (FODB.Financial > 100)
            {

                valids = false;
                TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\"> Financial Accomplishment must not greater than 100! </div>";

            }


            #endregion

            if ((ModelState.IsValid) && (valids == true))
            //if (ModelState.IsValid) //without validation
            {


                //audit trail

                string Constring = ConfigurationManager.ConnectionStrings["AccomplishmentEntitiesSecurity"].ConnectionString;
                using (SqlConnection CON = new SqlConnection(Constring))
                {
                    CON.Open();
                    SqlCommand cmd = new SqlCommand("insert into PhysicalAccompLog (userid,dateedit,timeedit,idaccomp) values (@userid,@dateedit,@timeedit,@idaccomp)", CON);
                    cmd.Parameters.AddWithValue("@userid", Session["userid"].ToString());
                    cmd.Parameters.AddWithValue("@idaccomp", FODB.IDAccomp);
                    cmd.Parameters.AddWithValue("@dateedit", DateTime.Now);
                    cmd.Parameters.AddWithValue("@timeedit", DateTime.Now.TimeOfDay);
                    cmd.ExecuteNonQuery();


                }


                var FinOBD = new PhysicalAccomp()
                {
                    IDPhysical = FODB.IDPhysical,
                    IDAccomp = FODB.IDAccomp,
                    newarea = FODB.newarea,
                    restorearea = FODB.restorearea,
                    rehabarea = FODB.rehabarea,
                    canalsaccomp = FODB.canalsaccomp,
                    canal_liningaccomp = FODB.canal_liningaccomp,
                    structuresaccomp = FODB.structuresaccomp,
                    roadsaccomp = FODB.roadsaccomp,
                    mnt = FODB.mnt,
                    yr = FODB.yr,
                    asof = FODB.asof,
                    fbaccomp = FODB.fbaccomp,
                    jobsaccomp = FODB.jobsaccomp,
                    remarksAccomp = FODB.remarksAccomp,
                    Physical = FODB.Physical,
                    Financial = FODB.Financial,
                    ValueAccomp = FODB.ValueAccomp,
                    Expenditures = FODB.Expenditures,
                    COCONETACCOMP = FODB.COCONETACCOMP,
                    HDPEACCOMP = FODB.HDPEACCOMP,
                    GRAVELACCOMP = FODB.GRAVELACCOMP,
                    physicaltarget = FODB.physicaltarget,
                    physicaltargetvalueaccomp = FODB.physicaltargetvalueaccomp


                };

                db.Entry(FinOBD).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                //6-23-2020

                //var msg = "";
                //msg = msg + " " + FODB.region + " updated the project " + FODB.subproject + " under " + FODB.mainproject;
                //msg = msg + " Physical (%): " + FODB.Physical;
                //msg = msg + " under GAA " + FODB.year;
                //msg = msg + " Please Don't reply this is Auto Generated by the System for details refer to Reportserver";

                //SMS sSMS = new SMS();

                //if (FODB.projectmonitor == "OD")
                //{
                //    var smsnum = db.SMS
                //   .Where(a => a.monitoredarea == FODB.region && a.projectmonitor == FODB.projectmonitor && a.position == "AREAMONITOR").ToList();

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


                //if (FODB.projectmonitor == "ED")
                //{




                //}






                string url = Url.Action("Index", "PhysicalAccomp", new { id = FODB.IDAccomp });

                return Json(new { success = true, url = url });
                //return Json(new { suceess = true });
            }

            return PartialView("_MyEdit", FODB);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyEditTarget(PhysicalAccompView FODB)
        {
            var valids = true;

        
            if (valids == true)
            {


                //audit trail

         

                var FinOBD = new PhysicalAccomp()
                {
                    IDPhysical = FODB.IDPhysical,
                    IDAccomp = FODB.IDAccomp,
                    newarea = FODB.newarea,
                    restorearea = FODB.restorearea,
                    rehabarea = FODB.rehabarea,
                    canalsaccomp = FODB.canalsaccomp,
                    canal_liningaccomp = FODB.canal_liningaccomp,
                    structuresaccomp = FODB.structuresaccomp,
                    roadsaccomp = FODB.roadsaccomp,
                    mnt = FODB.mnt,
                    yr = FODB.yr,
                    asof = FODB.asof,
                    fbaccomp = FODB.fbaccomp,
                    jobsaccomp = FODB.jobsaccomp,
                    remarksAccomp = FODB.remarksAccomp,
                    Physical = FODB.Physical,
                    Financial = FODB.Financial,
                    ValueAccomp = FODB.ValueAccomp,
                    Expenditures = FODB.Expenditures,
                    COCONETACCOMP = FODB.COCONETACCOMP,
                    HDPEACCOMP = FODB.HDPEACCOMP,
                    GRAVELACCOMP = FODB.GRAVELACCOMP,
                    physicaltarget = FODB.physicaltarget,
                    physicaltargetvalueaccomp = FODB.physicaltargetvalueaccomp


                };

                db.Entry(FinOBD).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                //6-23-2020

                //var msg = "";
                //msg = msg + " " + FODB.region + " updated the project " + FODB.subproject + " under " + FODB.mainproject;
                //msg = msg + " Physical (%): " + FODB.Physical;
                //msg = msg + " under GAA " + FODB.year;
                //msg = msg + " Please Don't reply this is Auto Generated by the System for details refer to Reportserver";

                //SMS sSMS = new SMS();

                //if (FODB.projectmonitor == "OD")
                //{
                //    var smsnum = db.SMS
                //   .Where(a => a.monitoredarea == FODB.region && a.projectmonitor == FODB.projectmonitor && a.position == "AREAMONITOR").ToList();

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


                //if (FODB.projectmonitor == "ED")
                //{




                //}






                string url = Url.Action("Index", "PhysicalAccomp", new { id = FODB.IDAccomp });

                return Json(new { success = true, url = url });
                //return Json(new { suceess = true });
            }

            return PartialView("_MyEditTarget", FODB);

        }


        public ActionResult MyEditStatus(int? id)
        {


            ProjectStatusImplementationView FD = db.ProjectStatusImplementationViews.Find(id);


            if (FD == null)
            {
                return HttpNotFound();
            }


            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Not Yet Started", Value = "Not Yet Started" });

            items.Add(new SelectListItem { Text = "Pre-Procurement", Value = "Pre-Procurement" });

            items.Add(new SelectListItem { Text = "Procurement", Value = "Procurement" });

            items.Add(new SelectListItem { Text = "Awarded", Value = "Awarded" });

            items.Add(new SelectListItem { Text = "On-Going", Value = "On-Going" });
            items.Add(new SelectListItem { Text = "Completed", Value = "Completed" });
            //            ViewBag.TypeOfDam = items;

            ViewBag.StatusProj = new SelectList(items, "Value", "Text", FD.statusProject);


            return PartialView("_MyEditStatus", FD);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyEditStatus(ProjectStatusImplementationView FODB)
        {

            if (ModelState.IsValid)
            {


                var FinOBD = new ProjectStatusImplementation()
                {
                    IDProject = FODB.IDProject,
                    IDAccomp = FODB.IDAccomp,
                    mnt = FODB.mnt,
                    yr = FODB.yr,
                    asof = FODB.asof,
                    remarksAccomp = FODB.remarksAccomp,
                    statusProject = FODB.statusProject

                };

                db.Entry(FinOBD).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                string url = Url.Action("IndexStatus", "PhysicalAccomp", new { id = FODB.IDAccomp });

                return Json(new { success = true, url = url });
                //return Json(new { suceess = true });
            }

            return PartialView("_MyEditStatus", FODB);

        }




        public ActionResult MyStatusEngineering(int? id)
        {


            FileAccompView FD = db.FileAccompViews.Find(id);


            if (FD == null)
            {
                return HttpNotFound();
            }


            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "For Approval", Value = "For Approval" });

            items.Add(new SelectListItem { Text = "Approved", Value = "Approved" });
            items.Add(new SelectListItem { Text = "Disapproved", Value = "Disapproved" });

            //            ViewBag.TypeOfDam = items;

            ViewBag.status = new SelectList(items, "Value", "Text", FD.status);


            return PartialView("_MyStatusEngineering", FD);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyStatusEngineering(FileAccompView FODB)
        {

            if (ModelState.IsValid)
            {


                var FinOBD = new FileAccomp()
                {
                    FileId = FODB.FileId,
                    IDAccomp = FODB.IDAccomp,
                    status = FODB.status,
                    FileName = FODB.FileName,
                    Content = FODB.Content,
                    ContentType = FODB.ContentType,
                    FileType = FODB.FileType,
                    mnt = FODB.mnt,
                    yr = FODB.yr,
                    email = FODB.email,
                    userid = FODB.userid



                };

                db.Entry(FinOBD).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();



                var FinOBD1 = new Notification()
                {
                    Details = FODB.status,
                    Title = "Project Aprroval",
                    SentTo = FODB.email,
                    idphysical = FODB.IDPhysical,
                    IDAccomp = FODB.IDAccomp,
                    IsRead = false,
                    Date = DateTime.Today,
                    remarks = FODB.remarks




                };


                NotificationsVoid(FinOBD1);



                if (FODB.status == "Disapproved")
                {
                    var mnt = FODB.mnt;
                    var yr = FODB.yr;

                    if (FODB.mnt == 1)
                    {

                        mnt = 12;
                        yr = ((FODB.yr) - 1);
                    }
                    else
                    {
                        mnt = (FODB.mnt - 1);
                        yr = FODB.yr;

                    }


                    var strhoney = "select isnull(physical,0), isnull(financial,0), isnull(newarea,0), isnull(restorearea,0) , isnull(rehabarea,0), isnull(canalsaccomp,0), isnull(canal_liningaccomp,0) " +
             " , isnull(roadsaccomp,0), isnull(structuresaccomp,0), isnull(HDPEACCOMP,0), isnull(COCONETACCOMP,0), isnull(GRAVELACCOMP,0), isnull(valueaccomp,0) , isnull(expenditures,0)" +
             " from physicalaccomp where idaccomp = '" + FODB.IDAccomp + "'" +
             " and mnt = " + mnt +
             " and yr = " + yr;


                    string Constring1 = ConfigurationManager.ConnectionStrings["AccomplishmentEntitiesSecurity"].ConnectionString;
                    using (SqlConnection CON = new SqlConnection(Constring1))
                    {
                        CON.Open();
                        SqlCommand cmd = new SqlCommand(strhoney, CON);
                        SqlDataReader reader;


                        reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {


                            string Constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                            using (SqlConnection CON1 = new SqlConnection(Constring))
                            {
                                CON1.Open();

                                var strhoney1 = "update physicalaccomp set physical = @physical, " +
                                    " financial = @financial, newarea = @newarea, restorearea= @restorearea, rehabarea = @rehabarea, canalsaccomp=@canalsaccomp,canal_liningaccomp = @canal_liningaccomp, " +
                                    " roadsaccomp=@roadsaccomp, structuresaccomp=@structuresaccomp, HDPEACCOMP=@HDPEACCOMP,COCONETACCOMP=@COCONETACCOMP,GRAVELACCOMP = @GRAVELACCOMP, valueaccomp=@valueaccomp, expenditures=@expenditures " +
                                    "  where idaccomp = '" + FODB.IDAccomp + "'" +
                                    " and mnt = " + (FODB.mnt) +
                                    " and yr = " + FODB.yr;


                                SqlCommand cmd1 = new SqlCommand(strhoney1, CON1);
                                cmd1.Parameters.AddWithValue("@physical", reader.GetDouble(0));
                                cmd1.Parameters.AddWithValue("@financial", reader.GetDouble(1));
                                cmd1.Parameters.AddWithValue("@newarea", reader.GetInt32(2));
                                cmd1.Parameters.AddWithValue("@restorearea", reader.GetInt32(3));
                                cmd1.Parameters.AddWithValue("@rehabarea", reader.GetInt32(4));
                                cmd1.Parameters.AddWithValue("@canalsaccomp", reader.GetDouble(5));
                                cmd1.Parameters.AddWithValue("@canal_liningaccomp", reader.GetDouble(6));
                                cmd1.Parameters.AddWithValue("@roadsaccomp", reader.GetDouble(7));
                                cmd1.Parameters.AddWithValue("@structuresaccomp", reader.GetDouble(8));
                                cmd1.Parameters.AddWithValue("@HDPEACCOMP", reader.GetDouble(9));
                                cmd1.Parameters.AddWithValue("@COCONETACCOMP", reader.GetDouble(10));
                                cmd1.Parameters.AddWithValue("@GRAVELACCOMP", reader.GetDouble(11));

                                cmd1.Parameters.AddWithValue("@valueaccomp", reader.GetDouble(12));
                                cmd1.Parameters.AddWithValue("@expenditures", reader.GetDouble(13));


                                cmd1.ExecuteNonQuery();
                                // return Json(new { success = true });

                            }



                        }
                    }





                }



                string url = Url.Action("Index", "PhysicalAccomp", new { id = FODB.IDAccomp });

                return Json(new { success = true, url = url });
                //return Json(new { suceess = true });
            }

            return PartialView("_MyStatusEngineering", FODB);

        }






        public ActionResult MyEditFSDE(int? id)
        {


            FSDEPhysicalView FD = db.FSDEPhysicalViews.Find(id);


            if (FD == null)
            {
                return HttpNotFound();
            }


            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "", Value = "" });
            items.Add(new SelectListItem { Text = "FS", Value = "FS" });

            items.Add(new SelectListItem { Text = "DE", Value = "DE" });

            items.Add(new SelectListItem { Text = "FSDE", Value = "FSDE" });


            ViewBag.StatusProj = new SelectList(items, "Value", "Text", FD.typeofStudy);



            List<SelectListItem> items1 = new List<SelectListItem>();
            items1.Add(new SelectListItem { Text = "", Value = "" });
            items1.Add(new SelectListItem { Text = "By Administration", Value = "By Administration" });
            items1.Add(new SelectListItem { Text = "By Contract", Value = "By Contract" });

            ViewBag.StatusMode = new SelectList(items1, "Value", "Text", FD.modeImplementation);


            return PartialView("_MyEditFSDE", FD);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyEditFSDE(FSDEPhysicalView FODB)
        {

            if (ModelState.IsValid)
            {


                var FinOBD = new FSDEPhysical()
                {
                    IdFSDE = FODB.IdFSDE,
                    IDAccomp = FODB.IDAccomp,
                    mnt = FODB.mnt,
                    yr = FODB.yr,
                    asof = FODB.asof,
                    typeofStudy = FODB.typeofStudy,
                    modeImplementation = FODB.modeImplementation,
                    dateNTP = FODB.dateNTP,
                    ActivityStart = FODB.ActivityStart,
                    ActivityFinish = FODB.ActivityFinish,
                    remarks = FODB.remarks,
                    startConstruction = FODB.startConstruction,
                    valueaccompde = FODB.valueaccompde,
                    valueaccompfs = FODB.valueaccompfs,
                    expendituresde = FODB.expendituresde,
                    expendituresfs = FODB.expendituresfs,
                    physicalde = FODB.physicalde,
                    physicalfs = FODB.physicalfs,
                    financialde = FODB.financialde,
                    financialfs = FODB.financialfs,
                    fsobligation = FODB.fsobligation,
                    deobligation = FODB.deobligation


                };

                db.Entry(FinOBD).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                string url = Url.Action("IndexFSDE", "PhysicalAccomp", new { id = FODB.IDAccomp });

                return Json(new { success = true, url = url });
                //return Json(new { suceess = true });
            }

            return PartialView("_MyEditFSDE", FODB);

        }


        public ActionResult MyEditFSDEStudy(int? id)
        {


            FSDEContractStudyView FD = db.FSDEContractStudyViews.Find(id);


            if (FD == null)
            {
                return HttpNotFound();
            }


            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "", Value = "" });
            items.Add(new SelectListItem { Text = "FS", Value = "FS" });

            items.Add(new SelectListItem { Text = "DE", Value = "DE" });
            items.Add(new SelectListItem { Text = "FSDE", Value = "FSDE" });

            ViewBag.StatusProj = new SelectList(items, "Value", "Text", FD.TypeofStudy);
            ViewBag.ConsultantList = new SelectList(db.ConsultantLists, "IdConsultant", "Consultant", FD.IdConsultant);



            return PartialView("_MyEditFSDEStudy", FD);
        }



        public ActionResult MyEditFSDEStudyPersonel(int? id)
        {


            FSDEConsultantListView FD = db.FSDEConsultantListViews.Find(id);


            if (FD == null)
            {
                return HttpNotFound();
            }


            ViewBag.PositionLists = new SelectList(db.PositionLists, "PositionID", "Position", FD.PositionID);



            return PartialView("_MyEditFSDEStudyPersonel", FD);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyEditFSDEStudy(FSDEContractStudyView FODB)
        {

            if (ModelState.IsValid)
            {


                var FinOBD = new FSDEContractStudy()
                {

                    IDAccomp = FODB.IDAccomp,
                    IDStudy = FODB.IDStudy,
                    TypeofStudy = FODB.TypeofStudy,
                    Consultant = FODB.IdConsultant

                };

                db.Entry(FinOBD).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                string url = Url.Action("IndexFSDEStudy", "PhysicalAccomp", new { id = FODB.IDAccomp });

                return Json(new { success = true, url = url });
                //return Json(new { suceess = true });
            }

            return PartialView("_MyEditFSDEStudy", FODB);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyEditFSDEStudyPersonel(FSDEConsultantListView FODB)
        {

            if (ModelState.IsValid)
            {


                var FinOBD = new StudyConsultant()
                {

                    IDStudyConsultant = FODB.IDStudyConsultant,
                    IDStudy = FODB.IDStudy,
                    LNAME = FODB.LNAME,
                    FNAME = FODB.FNAME,
                    MIDNAME = FODB.MIDNAME,
                    SUFIX = FODB.SUFIX,
                    POSITION = FODB.PositionID

                };

                db.Entry(FinOBD).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                string url = Url.Action("IndexFSDEStudyPersonel", "PhysicalAccomp", new { id = FODB.IDStudy });

                return Json(new { success = true, url = url });
                //return Json(new { suceess = true });
            }

            return PartialView("_MyEditFSDEStudyPersonel", FODB);

        }




        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhysicalAccompView FD = db.PhysicalAccompViews.Find(id);

            if (FD == null)
            {
                return HttpNotFound();

            }
            return PartialView("_Delete", FD);

        }


        public ActionResult DeleteIssues(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectIssuesView FD = db.ProjectIssuesViews.Find(id);

            if (FD == null)
            {
                return HttpNotFound();

            }
            return PartialView("_DeleteIssues", FD);

        }



        [HttpPost, ActionName("DeleteIssues")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteIssues(int id)
        {

            ProjectIssue FD = db.ProjectIssues.Find(id);
            db.ProjectIssues.Remove(FD);
            db.SaveChanges();

            string url = Url.Action("IndexIssues", "PhysicalAccomp", new { id = FD.Idaccomp });

            return Json(new { success = true, url = url });

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {

            PhysicalAccomp FD = db.PhysicalAccomps.Find(id);
            db.PhysicalAccomps.Remove(FD);
            db.SaveChanges();

            string url = Url.Action("Index", "PhysicalAccomp", new { id = FD.IDAccomp });

            return Json(new { success = true, url = url });

        }

        public ActionResult DeleteFSDEStudy(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FSDEContractStudyView FD = db.FSDEContractStudyViews.Find(id);

            if (FD == null)
            {
                return HttpNotFound();

            }
            return PartialView("_DeleteFSDEStudy", FD);

        }

        [HttpPost, ActionName("DeleteFSDEStudy")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFSDEStudy(int id)
        {

            FSDEContractStudy FD = db.FSDEContractStudies.Find(id);
            db.FSDEContractStudies.Remove(FD);
            db.SaveChanges();

            string url = Url.Action("IndexFSDEStudy", "PhysicalAccomp", new { id = FD.IDAccomp });

            return Json(new { success = true, url = url });

        }



        public ActionResult DeleteFSDEStudyPersonel(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FSDEConsultantListView FD = db.FSDEConsultantListViews.Find(id);

            if (FD == null)
            {
                return HttpNotFound();

            }
            return PartialView("_DeleteFSDEStudyPersonel", FD);

        }

        [HttpPost, ActionName("DeleteFSDEStudyPersonel")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFSDEStudyPersonel(int id)
        {

            StudyConsultant FD = db.StudyConsultants.Find(id);
            db.StudyConsultants.Remove(FD);
            db.SaveChanges();

            string url = Url.Action("IndexFSDEStudyPersonel", "PhysicalAccomp", new { id = FD.IDStudy });

            return Json(new { success = true, url = url });

        }




        public ActionResult EditFile(string id, string subp, string mainp, double amt, int mnt, int yr, string pm)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //DBMView dBMView = db.DBMViews.Find(id);
            //var PAV = db.FileAccompViews
            //.Where(x => x.IDAccomp == id && x.mnt == mnt && x.yr == yr);

            var PAV = db.FileAccompViews.FirstOrDefault(x => x.IDAccomp == id && x.mnt == mnt && x.yr == yr);

            Session["idaccomp"] = id;
            Session["mainp"] = mainp;
            Session["subp"] = subp;
            Session["amt"] = amt;
            Session["mnt"] = mnt;
            Session["yr"] = yr;
            Session["pm"] = pm;


            //if (PAV == null)
            //{
            //    //return HttpNotFound();
            //    return RedirectToAction("CreateFile");
            //}


            if (PAV != null)
            {

                string Constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection CON = new SqlConnection(Constring))
                {
                    CON.Open();
                    SqlCommand cmd = new SqlCommand("delete from fileaccomp where fileid = " + PAV.FileId, CON);


                    cmd.ExecuteNonQuery();
                    // return Json(new { success = true });

                }


            }


            return RedirectToAction("CreateFile");
        }

        // POST: DBM/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditFile(FileAccompView PAV, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {

                var mnt = Session["mnt"].ToString();
                var yr = Session["yr"].ToString();
                if (upload != null && upload.ContentLength > 0)
                {

                    var DBB = db.FileAccomps.Where(x => x.IDAccomp == PAV.IDAccomp || x.mnt == PAV.mnt || x.yr == PAV.yr);
                    if (DBB != null)
                    {
                        foreach (var courseenrollment in DBB)
                        {
                            db.FileAccomps.Remove(courseenrollment);
                        }
                        db.SaveChanges();

                    }



                    var avatar = new FileAccomp()
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        //     FileType = FileType.Avatar,
                        ContentType = upload.ContentType,
                        IDAccomp = PAV.IDAccomp,
                        mnt = int.Parse(mnt),
                        yr = int.Parse(yr)
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        avatar.Content = reader.ReadBytes(upload.ContentLength);
                    }

                    db.FileAccomps.Add(avatar);
                    db.SaveChanges();
                }




                return RedirectToAction("Edit", new { id = Session["idaccomp"].ToString() });
            }
            return View(PAV);
        }



        public ActionResult CreateFile()
        {
            return View();
        }

        // POST: DBM/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateFile(FileAccompView PAV, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {


                if (upload != null && upload.ContentLength > 0)
                {
                    var mnt = Session["mnt"].ToString();
                    var yr = Session["yr"].ToString();
                    var avatara = new FileAccomp()
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = 1,
                        ContentType = upload.ContentType,
                        IDAccomp = PAV.IDAccomp,
                        mnt = int.Parse(mnt),
                        yr = int.Parse(yr),
                        status = "For Approval",
                        email = User.Identity.Name.ToString(),
                        userid = Session["userid"].ToString()
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        avatara.Content = reader.ReadBytes(upload.ContentLength);
                    }

                    db.FileAccomps.Add(avatara);
                    db.SaveChanges();


                    string emailMsg = "Dear Engineering  <br /><br /> We submitted a MPR for the project of <b style='color: red'>" + Session["subp"].ToString() + "  </b> <br /><br /> Thanks & Regards, <br />" + Session["regiontolog"].ToString();
                    string emailSubject = "Monthly Progress Report (MPR)";
                    string emailMsgSMS = "Dear Engineering We submitted a MPR for the project of " + Session["subp"].ToString() + " Thanks & Regards, " + Session["regiontolog"].ToString();
                    // Sending Email.  
                    await this.SendEmailAsync("pbmes.cmdco@gmail.com", emailMsg, emailSubject);
                     // await this.SendEmailAsync("johnroneldc@gmail.com", emailMsg, emailSubject);
                    




                    //var msg = "";
                    //msg = emailMsgSMS;
                    //msg = msg + " Please Don't reply this is Auto Generated by the System for details refer to Online Monitoring";

                    //SMS sSMS = new SMS();

                    //if (Session["pm"].ToString() == "ED")
                    //{
                    //    var smsnum = db.SMSMonitoredViews
                    //   .Where(a => a.monitoredarea == PAV.IDAccomp && a.position == "AREAMONITOR").ToList();

                    //    if (smsnum.Count != 0)
                    //    {
                    //        for (int i = 0; i < smsnum.Count; i++)
                    //        {
                    //            if (smsnum[i].status.ToString() == "YES")
                    //            {


                    //                await this.SendingSMS(smsnum[i].SMSnumber.ToString(), msg);

                    //            }


                    //        }
                    //    }

                    ////}


                }




                return RedirectToAction("Edit", new { id = Session["idaccomp"].ToString() });
            }

            return View(PAV);
        }

        public void NotificationsVoid(Notification noti)
        {


            NotificationHub objNotifHub = new NotificationHub();
            //Notification objNotif = new Notification();
            //objNotif.SentTo = noti.SentTo;
            //objNotif.IsRead = true;

            db.Configuration.ProxyCreationEnabled = false;
            db.Notifications.Add(noti);
            db.SaveChanges();

            objNotifHub.SendNotification(noti.SentTo);


        }

        public FileContentResult Download(int id)
        {
            //declare byte array to get file content from database and string to store file name
            using (AccomplishmentEntities dc = new AccomplishmentEntities())
            {

                FileAccompView l = null;
                l = dc.FileAccompViews.Where(a => a.FileId.Equals(id)).FirstOrDefault();
                //l = dc.CoordinatesPerSystems.Where(a => a.Row.Equals(Row)).FirstOrDefault();

                var FinOBD1 = new Notification()
                {
                    Details = "The MPR has downloaded by the Engineering Department!",
                    Title = "Project Aprroval",
                    SentTo = l.email,
                    idphysical = l.IDPhysical,
                    IDAccomp = l.IDAccomp,
                    IsRead = false,
                    Date = DateTime.Today




                };
                NotificationsVoid(FinOBD1);
            }







            byte[] fileData;
            string fileName;
            //create object of LINQ to SQL class
            //  DBContext dataContext = new DBContext();
            //using LINQ expression to get record from database for given id value
            var record = from p in db.FileAccomps
                         where p.FileId == id
                         select p;
            //only one record will be returned from database as expression uses condtion on primary field
            //so get first record from returned values and retrive file content (binary) and filename
            fileData = (byte[])record.FirstOrDefault().Content.ToArray();
            fileName = record.FirstOrDefault().FileName;
            //return file and provide byte file content and file name
            return File(fileData, "text", fileName);

        }



        public async Task<bool> SendEmailAsync(string email, string msg, string subject = "")
        {
            // Initialization.  
            bool isSend = false;

            try
            {
                // Initialization.  
                var body = msg;
                var message = new MailMessage();

                // Settings.  
                message.To.Add(new MailAddress(email));
                message.From = new MailAddress(EmailInfo.FROM_EMAIL_ACCOUNT);
                message.Subject = !string.IsNullOrEmpty(subject) ? subject : EmailInfo.EMAIL_SUBJECT_DEFAUALT;
                message.Body = body;
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    // Settings.  
                    var credential = new NetworkCredential
                    {
                        UserName = EmailInfo.FROM_EMAIL_ACCOUNT,
                        Password = EmailInfo.FROM_EMAIL_PASSWORD
                    };

                    // Settings.  
                  //  smtp.UseDefaultCredentials = false;
                    smtp.Credentials = credential;
                    smtp.Host = EmailInfo.SMTP_HOST_GMAIL;
                    smtp.Port = Convert.ToInt32(EmailInfo.SMTP_PORT_GMAIL);
                    smtp.EnableSsl = true;

                    // Sending  
                    await smtp.SendMailAsync(message);

                    // Settings.  
                    isSend = true;
                }
            }
            catch (Exception ex)
            {
                // Info  
                throw ex;
            }

            // info.  
            return isSend;
        }



        public Task<bool> SendingSMS(string mobNo, string msg)
        {


            return Task.Factory.StartNew(() => sendSMSAttach(mobNo, msg));

        }


        public bool sendSMSAttach(string mobNo, string msg)
        {
            // Initialization.  
            bool isSend = false;

            try
            {

                string telNo = Char.ConvertFromUtf32(34) + mobNo + Char.ConvertFromUtf32(34);
                string msg1 = "";
                string msg2 = "";

                //      sp.PortName = "COM3";
                sp.PortName = "COM5";

                var cnt = 1;
                if (msg.Length >= 140)
                {
                    msg1 = msg.Substring(0, 140);
                    msg2 = msg.Substring(msg.Length - (msg.Length - 140), (msg.Length - 140));
                    cnt = 2;

                }





                for (int i = 1; i <= cnt; i++)
                {

                    sp.Open();
                    sp.Write("AT+CMGF=1" + Char.ConvertFromUtf32(13));
                    sp.Write("AT+CMGS=" + telNo + Char.ConvertFromUtf32(13));
                    if (cnt == 2)
                    {
                        if (i == 1)
                        {
                            sp.Write(msg1 + Char.ConvertFromUtf32(26) + Char.ConvertFromUtf32(13));
                        }

                        if (i == 2)
                        {
                            sp.Write(msg2 + Char.ConvertFromUtf32(26) + Char.ConvertFromUtf32(13));
                        }
                    }
                    else
                    {
                        sp.Write(msg + Char.ConvertFromUtf32(26) + Char.ConvertFromUtf32(13));

                    }
                    sp.Close();

                    if (i < 2)
                    {
                        System.Threading.Thread.Sleep(3000);
                    }
                }
            }
            catch (Exception ex)
            {
                // Info  
                throw ex;
            }

            // info.  
            return isSend;

        }



        public ActionResult Gallery(string id)
        {
            List<DIMEPicView> all = new List<DIMEPicView>();
            Session["myidsys"] = id;
            // Here MyDatabaseEntities is our datacontext

            all = db.DIMEPicViews.Where(r => r.IDAccomp == id).ToList();

            return View(all);





        }

        public ActionResult Upload()
        {

            return View();



        }
        [HttpPost]
        public ActionResult Upload(DIMEPicView fileModel)
        {
            FileUploadServiceDam service = new FileUploadServiceDam();

            var id = Session["myidsys"].ToString();
            foreach (var item in fileModel.File)
            {

                service.SaveFileDetails(item, id, "Physical");
            }
            return RedirectToAction("Photos", new { id = id });
        }


        public ActionResult Photos(string id)
        {
            // Default.
            //   string folder =  "https://bob.nia.gov.ph/DIME/" + id + "/" ;
            string folder = "~/Pic/DIME/" + id + "/";

            Session["myidsys"] = id;


            return View(new PhotoModel(folder));



        }



    }
}