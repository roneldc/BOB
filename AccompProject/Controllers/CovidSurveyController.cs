using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AccompProject.Models;
using AccompProject.Models.InventoryModel;
using System.Configuration;
using System.Data.SqlClient;
using AccompProject;
using AccompProject.Services;

namespace AccompProject.Controllers
{
    public class CovidSurveyController : Controller
    {
        private InventoryEntities db = new InventoryEntities();
        private PDSWebEntities db1 = new PDSWebEntities();
        private HealthEntities db2 = new HealthEntities();



        //   private LIMSSQLEntities db1 = new LIMSSQLEntities();


        //  Biometrics

        public ActionResult SearchEmp()
        {


            return PartialView("_SearchEmp", db.EM_ERoster.ToList());
        }
        public ActionResult Temp()
        {

            ViewBag.Temp = "TEMPERATURE IS HIGH!";
            return PartialView("_Temp", ViewBag.Temp);
        }

        public ActionResult EquipCheck(string employeeid = null, string alcohol = null, string vitamins = null, string facemask = null, int mnt = 0, int yr = 0, string faceshield = null)
        {



            var data = new EquipChecker();

            string Constring = ConfigurationManager.ConnectionStrings["CoordinatesEntities"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SELECT * FROM CovidEquipmentEmployee where employeeid = '" + employeeid + "'" +
                                     " and mnt = " + mnt +
                                      " and yr = " + yr);


            using (SqlConnection CON = new SqlConnection(Constring))
            {

                cmd.Connection = CON;
                CON.Open();
                using (SqlDataReader SDR = cmd.ExecuteReader())
                {
                    while (SDR.Read())
                    {
                        data.vitamins = SDR["vitamins"].ToString();
                        Session["vitamins"] = SDR["vitamins"].ToString();

                        data.facemask = SDR["facemask"].ToString();
                        Session["facemask"] = SDR["facemask"].ToString();

                        data.alcohol = SDR["alcohol"].ToString();
                        Session["alcohol"] = SDR["alcohol"].ToString();

                        data.faceshield = SDR["faceshield"].ToString();
                        Session["faceshield"] = SDR["faceshield"].ToString();

                        data.equipid = Convert.ToInt32(SDR["CovidEquipmentID"].ToString());
                        data.success = true;
                        Session["equipid"] = Convert.ToInt32(SDR["CovidEquipmentID"].ToString());


                        //data.trance = SDR["trance"].ToString();
                        //Session["trance"] = SDR["trance"].ToString();

                    }


                }
                CON.Close();



            }


            string strhoney = "select * from CovidEquipmentEmployee a where a.dategiven = (select max(dategiven) from CovidEquipmentEmployee b where a.employeeid = b.employeeid) and a.employeeid = '" + employeeid + "'";
            string Constring2 = ConfigurationManager.ConnectionStrings["CoordinatesEntities"].ConnectionString;
            SqlCommand cmd2 = new SqlCommand(strhoney);



            using (SqlConnection CON2 = new SqlConnection(Constring2))
            {

                cmd2.Connection = CON2;
                CON2.Open();
                using (SqlDataReader SDR2 = cmd2.ExecuteReader())
                {
                    while (SDR2.Read())
                    {



                        data.trance = SDR2["trance"].ToString();
                        Session["trance"] = SDR2["trance"].ToString();

                    }


                }
                CON2.Close();



            }


            string Constring1 = ConfigurationManager.ConnectionStrings["HRMISValidator"].ConnectionString;
            SqlCommand cmd1 = new SqlCommand("SELECT * FROM ___Table_CovidSurvey_unVerified where employeeid = '" + employeeid + "'");


            using (SqlConnection CON1 = new SqlConnection(Constring1))
            {

                cmd1.Connection = CON1;
                CON1.Open();
                using (SqlDataReader SDR1 = cmd1.ExecuteReader())
                {
                    if (SDR1.HasRows)
                    {

                        data.hrmis = "Yes";

                    }
                    else
                    {
                        data.hrmis = "No";
                    }




                }
                CON1.Close();



            }

            return Json(data);
            //  return Json(new { success = true, vitamins = data.vitamins,facemask = data.facemask,alcohol = data.alcohol });////
        }

        //public ActionResult SearchEmployeeQR(string employeeid = null)
        //{



        //    var data = new EmployeeQR();

        //    var empqr = db.EM_ERoster.Where(r => r.empno == employeeid).FirstOrDefault();


        //    if (empqr != null)
        //    {

        //        data.lname = empqr.lname;
        //        data.fname = empqr.fname;
        //        data.mname = empqr.mname;
        //        data.office = empqr.abrev_currdept;
        //        data.suffix = empqr.suffix;
        //        data.empno = empqr.empno;
        //        data.success = true;


        //    }
        //    else
        //    {

        //        int em = Convert.ToInt32(employeeid);
        //        var empqrnewid = db1.C__vw_idtr.Where(r => r.NewID == em).FirstOrDefault();


        //        if (empqrnewid != null)
        //        {

        //            data.lname = empqrnewid.lname;
        //            data.fname = empqrnewid.fname;
        //            data.mname = empqrnewid.mname;
        //            data.office = empqrnewid.CurrentOffice;
        //            data.suffix = empqrnewid.suffix;
        //            data.empno = empqrnewid.empno;
        //            data.success = true;


        //        }
        //        else {


        //        data.success = false;
        //        }




        //    }



        //    return Json(data);
        //    //  return Json(new { success = true, vitamins = data.vitamins,facemask = data.facemask,alcohol = data.alcohol });////
        //}

        public ActionResult SearchEmployeeQR(string employeeid = null)
        {

            string NewID = employeeid;
          
            var OldID = MapIDClass.GetOldID(NewID);

            var data = new EmployeeQR();

            var empqr = db1.PDS_Personalinfo_Active_QR.Where(r => r.DTRid == OldID).FirstOrDefault();

//            var empqr = db1.C__vw_idtr.Where(r => r.empno == OldID).FirstOrDefault();

            DateTime myDate = DateTime.Now;
            DateTime dt1 = Convert.ToDateTime(myDate.ToString("d"));
            int empno = Convert.ToInt32(employeeid);
            var stats = db2.EmployeeWorkStatusViews.Where(r => r.employeeID == empno && DbFunctions.TruncateTime(r.FortheDate) == dt1).FirstOrDefault();

            if (stats != null)
            {

                data.stats = stats.Status;

            }
            else
            {
                data.stats = "No Daily Health Disclosure Found!";
            
            }

            if (empqr != null)
            {



                data.lname = empqr.lname;
                data.fname = empqr.fname;
                data.mname = empqr.mname;
                data.office = empqr.office;
                data.suffix = empqr.suffix;
                data.empno = empqr.DTRid;
                data.newid = employeeid;
                data.success = true;


            }
            else
            {
                //old
                //int em = Convert.ToInt32(employeeid);
                //var empqrnewid = db1.PDS_Personalinfo_Active_QR.Where(r => r.ID == em).FirstOrDefault();

               
                var empqrnewid = db1.PDS_Personalinfo_Active_QR.Where(r => r.IdNumber == employeeid).FirstOrDefault();

                if (empqrnewid != null)
                {

                    data.lname = empqrnewid.lname;
                    data.fname = empqrnewid.fname;
                    data.mname = empqrnewid.mname;
                    data.office = empqrnewid.office;
                    data.suffix = empqrnewid.suffix;
                    data.empno = empqrnewid.DTRid;
                    data.newid = employeeid;
                    data.success = true;


                }
                else
                {


                    data.success = false;
                }




            }



            return Json(data);
            //  return Json(new { success = true, vitamins = data.vitamins,facemask = data.facemask,alcohol = data.alcohol });////
        }

        public ActionResult SearchEmpTenant(string office = null)
        {


            return PartialView("_SearchEmpTenant", db.EmployeeTenants.Where(r => r.Office == office).ToList());
        }

        // GET: /CovidSurvey/
        public ActionResult Index()
        {
            return View(db.CovidSurveys.ToList());
        }

        public ActionResult IndexEmp()
        {
            return View(db.EmployeeTenants.ToList());
        }
        public ActionResult IndexEquip()
        {
            return View(db.CovidEquipmentEmployees.ToList());
        }




        public ActionResult IndexActivity(string id)
        {

            ViewBag.empno = id;
            Session["empno"] = id;

            DateTime myDate = DateTime.Now;
            string dt = myDate.ToString("d");
            DateTime dt1 = Convert.ToDateTime(myDate.ToString("d"));

            var physicalaccomp = db.CovidActivityEmployees
                               .Where(a => a.EmployeeID == id && DbFunctions.TruncateTime(a.dateEncoded) == dt1);



            if (physicalaccomp == null)
            {
                return HttpNotFound();
            }
            // return View(asa);
            return PartialView("_IndexActivity", physicalaccomp);



        }


        // GET: /CovidSurvey/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CovidSurvey covidsurvey = db.CovidSurveys.Find(id);
            if (covidsurvey == null)
            {
                return HttpNotFound();
            }
            return View(covidsurvey);
        }

        // GET: /CovidSurvey/Create
        public ActionResult Create()
        {

            //List<SelectListItem> items = new List<SelectListItem>();

            //items.Add(new SelectListItem { Text = "NIA Employee", Value = "NIA" });

            //items.Add(new SelectListItem { Text = "M8", Value = "M8" });
            //items.Add(new SelectListItem { Text = "TOUGH GUARD", Value = "TOUGH GUARD" });
            //items.Add(new SelectListItem { Text = "NWRB", Value = "NWRB" });
            //items.Add(new SelectListItem { Text = "PCIC", Value = "PCIC" });
            //items.Add(new SelectListItem { Text = "COA", Value = "COA" });

            //items.Add(new SelectListItem { Text = "VISITOR", Value = "VISITOR" });


            //ViewBag.empcat = items;


            ViewBag.trance = new SelectList(db.CovidTrances, "code", "trance");
            ViewBag.empcat = new SelectList(db.EmployeeCategories, "code", "description");

            DateTime myDate = DateTime.Now;
            string dt = myDate.ToString("d");
            DateTime dt1 = Convert.ToDateTime(myDate.ToString("d"));

            int nia = db.CovidSurveys.Where(r => r.PersonnelCategory == "NIA" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();
            int M8 = db.CovidSurveys.Where(r => r.PersonnelCategory == "M8" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();
            int tough = db.CovidSurveys.Where(r => r.PersonnelCategory == "TOUGH GUARD" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();
            int nwrb = db.CovidSurveys.Where(r => r.PersonnelCategory == "NWRB" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();
            int coa = db.CovidSurveys.Where(r => r.PersonnelCategory == "COA" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();
            int pcic = db.CovidSurveys.Where(r => r.PersonnelCategory == "PCIC" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();
            int visitor = db.CovidSurveys.Where(r => r.PersonnelCategory == "VISITOR" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();
            int NIACOOP = db.CovidSurveys.Where(r => r.PersonnelCategory == "NIACOOP" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();
            int NIASLA = db.CovidSurveys.Where(r => r.PersonnelCategory == "NIASLA" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();

            int REGION = db.CovidSurveys.Where(r => r.PersonnelCategory == "REGION" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();
            int CONSULTANT = db.CovidSurveys.Where(r => r.PersonnelCategory == "CONSULTANT" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();
            int NIAESP = db.CovidSurveys.Where(r => r.PersonnelCategory == "NIAESP" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();
            int CSC = db.CovidSurveys.Where(r => r.PersonnelCategory == "CSC" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();
            int PHILCARABAO = db.CovidSurveys.Where(r => r.PersonnelCategory == "PHIL CARABAO" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();

            ViewBag.asof = dt;

            ViewBag.STAT = "NIA : " + nia +
            " M8  : " + M8 +
            " TOUGH GUARD : " + tough +
            " NWRB : " + nwrb +
            " COA : " + coa +
            " PCIC : " + pcic +
            " VISITOR : " + visitor +
             " COOP  : " + NIACOOP +
            " NIAES : " + NIAESP +
            " REGION : " + REGION +
            " CONSULTANT : " + CONSULTANT +
            " CSC : " + CSC +
            " PHIL CARABAO : " + PHILCARABAO +
             " NIASLA  : " + NIASLA;

            int tot = nia + M8 + tough + nwrb + coa + pcic + visitor + NIACOOP + NIASLA + NIAESP + REGION + CONSULTANT + CSC + PHILCARABAO;
            ViewBag.tot = "TOTAL OF : " + tot;
            return View();
        }
        // GET: /CovidSurvey/Create
        public ActionResult CreatePerEmployee(int empno = 833315)
        {

            //List<SelectListItem> items = new List<SelectListItem>();

            //items.Add(new SelectListItem { Text = "NIA Employee", Value = "NIA" });

            //items.Add(new SelectListItem { Text = "M8", Value = "M8" });
            //items.Add(new SelectListItem { Text = "TOUGH GUARD", Value = "TOUGH GUARD" });
            //items.Add(new SelectListItem { Text = "NWRB", Value = "NWRB" });
            //items.Add(new SelectListItem { Text = "PCIC", Value = "PCIC" });
            //items.Add(new SelectListItem { Text = "COA", Value = "COA" });

            //items.Add(new SelectListItem { Text = "VISITOR", Value = "VISITOR" });
            if (empno == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //ViewBag.empcat = items;

            var pds = db1.PDS_Personalinfo_Active.Where(r => r.New_ID == empno.ToString()).FirstOrDefault();


            CovidSurvey cs = new CovidSurvey();

            cs.EmployeeID = empno.ToString();
            cs.Lname = pds.lname;
            cs.Fname = pds.fname;
            cs.Fname = pds.mname;


            DateTime myDate = DateTime.Now;
            string dt = myDate.ToString("d");
            DateTime dt1 = Convert.ToDateTime(myDate.ToString("d"));






            return View(cs);
        }

        public ActionResult EditActivity(int empno = 833315)
        {



            if (empno == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //ViewBag.empcat = items;

            var pds = db1.PDS_Personalinfo_Active.Where(r => r.New_ID == empno.ToString()).FirstOrDefault();


            //CovidSurvey cs = new CovidSurvey();

            //cs.EmployeeID = empno.ToString();
            //cs.Lname = pds.lname;
            //cs.Fname = pds.fname;
            //cs.Fname = pds.mname;


            //DateTime myDate = DateTime.Now;
            //string dt = myDate.ToString("d");
            //DateTime dt1 = Convert.ToDateTime(myDate.ToString("d"));


            return View(pds);
        }

        public ActionResult CreateActivity(string Id = "")
        {

            CovidActivityEmployee Fview = new CovidActivityEmployee();
            Fview.EmployeeID = Id;
            ViewBag.places = new SelectList(db.CovidPlaces, "places", "places");


            return PartialView("_CreateActivity", Fview);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateActivity(CovidActivityEmployee cae)
        {


            if (ModelState.IsValid)
            {
                try
                {

                    cae.dateEncoded = DateTime.Now;
                    db.CovidActivityEmployees.Add(cae);
                    db.SaveChanges();

                    string url = Url.Action("IndexActivity", "CovidSurvey", new { id = cae.EmployeeID });

                    return Json(new { success = true, url = url });
                    //return Json(new { suceess = true });






                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }


            return PartialView("_CreateActivity", cae);


        }




        public ActionResult MyEditActivity(int? Id)
        {

            CovidActivityEmployee FD = db.CovidActivityEmployees.Find(Id);


            if (FD == null)
            {
                return HttpNotFound();
            }

            ViewBag.places = new SelectList(db.CovidPlaces, "places", "places", FD.CategoryPlace);



            return PartialView("_MyEditActivity", FD);



        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyEditActivity(CovidActivityEmployee cae)
        {


            if (ModelState.IsValid)
            {
                try
                {


                    var ae = new CovidActivityEmployee
                    {


                        CategoryPlace = cae.CategoryPlace,
                        dateEncoded = cae.dateEncoded,
                        Description = cae.Description,
                        DateTaken = cae.DateTaken,
                        EmployeeID = cae.EmployeeID,
                        IDActivityEmployee = cae.IDActivityEmployee,
                        TimeFrom = cae.TimeFrom,
                        TimeTo = cae.TimeTo




                    };


                    db.Entry(ae).State = EntityState.Modified;
                    db.SaveChanges();


                    string url = Url.Action("IndexActivity", "CovidSurvey", new { id = cae.EmployeeID });

                    return Json(new { success = true, url = url });
                    //return Json(new { suceess = true });






                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }


            return PartialView("_CreateActivity", cae);


        }


        public ActionResult CreateEmp()
        {




            ViewBag.empcat = new SelectList(db.EmployeeCategories, "code", "description");


            return View();
        }


        // POST: /CovidSurvey/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CovidSurvey covidsurvey)
        {
            DateTime myDate = DateTime.Now;
            string dt = myDate.ToString("d");
            DateTime dt1 = Convert.ToDateTime(myDate.ToString("d"));

            int mnt = DateTime.Now.Month;
            int yr = DateTime.Now.Year;


            int CNTCHECK = db.CovidSurveys.Where(r => r.Lname == covidsurvey.Lname && r.Fname == covidsurvey.Fname && r.Midname == covidsurvey.Midname && DbFunctions.TruncateTime(r.DateTaken) == covidsurvey.DateTaken).Count();
            //var medsupplieslist = db.CovidEquipmentEmployees.Where(r => r.employeeid == covidsurvey.EmployeeID && r.mnt == mnt && r.yr == yr).FirstOrDefault();

            if (CNTCHECK > 0)
            {

                TempData["msg"] = "<h2 class = \"alert alert-danger fde\"> Employee is already Checked! </h2>";
                //List<SelectListItem> items = new List<SelectListItem>();

                //items.Add(new SelectListItem { Text = "NIA Employee", Value = "NIA" });

                //items.Add(new SelectListItem { Text = "M8", Value = "M8" });
                //items.Add(new SelectListItem { Text = "TOUGH GUARD", Value = "TOUGH GUARD" });
                //items.Add(new SelectListItem { Text = "NWRB", Value = "NWRB" });
                //items.Add(new SelectListItem { Text = "PCIC", Value = "PCIC" });
                //items.Add(new SelectListItem { Text = "COA", Value = "COA" });

                //items.Add(new SelectListItem { Text = "VISITOR", Value = "VISITOR" });


                //ViewBag.empcat = items;
                ViewBag.trance = new SelectList(db.CovidTrances, "code", "trance", covidsurvey.trance);
                ViewBag.empcat = new SelectList(db.EmployeeCategories, "code", "description", covidsurvey.PersonnelCategory);


                int nia = db.CovidSurveys.Where(r => r.PersonnelCategory == "NIA" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();
                int M8 = db.CovidSurveys.Where(r => r.PersonnelCategory == "M8" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();
                int tough = db.CovidSurveys.Where(r => r.PersonnelCategory == "TOUGH GUARD" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();
                int nwrb = db.CovidSurveys.Where(r => r.PersonnelCategory == "NWRB" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();
                int coa = db.CovidSurveys.Where(r => r.PersonnelCategory == "COA" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();
                int pcic = db.CovidSurveys.Where(r => r.PersonnelCategory == "PCIC" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();
                int visitor = db.CovidSurveys.Where(r => r.PersonnelCategory == "VISITOR" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();

                int NIACOOP = db.CovidSurveys.Where(r => r.PersonnelCategory == "NIACOOP" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();
                int NIASLA = db.CovidSurveys.Where(r => r.PersonnelCategory == "NIASLA" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();

                int REGION = db.CovidSurveys.Where(r => r.PersonnelCategory == "REGION" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();
                int CONSULTANT = db.CovidSurveys.Where(r => r.PersonnelCategory == "CONSULTANT" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();
                int NIAESP = db.CovidSurveys.Where(r => r.PersonnelCategory == "NIAESP" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();
                int CSC = db.CovidSurveys.Where(r => r.PersonnelCategory == "CSC" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();
                int PHILCARABAO = db.CovidSurveys.Where(r => r.PersonnelCategory == "PHIL CARABAO" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();

                ViewBag.asof = dt;

                ViewBag.STAT = "NIA : " + nia +
                " M8  : " + M8 +
                " TOUGH GUARD : " + tough +
                " NWRB : " + nwrb +
                " COA : " + coa +
                " PCIC : " + pcic +
                " VISITOR : " + visitor +
                 " COOP  : " + NIACOOP +
                " NIAES : " + NIAESP +
                " REGION : " + REGION +
                " CONSULTANT : " + CONSULTANT +
                " CSC : " + CSC +
                " PHIL CARABAO : " + PHILCARABAO +
                 " NIASLA  : " + NIASLA;

                int tot = nia + M8 + tough + nwrb + coa + pcic + visitor + NIACOOP + NIASLA + NIAESP + REGION + CONSULTANT + CSC + PHILCARABAO;
                ViewBag.tot = "TOTAL OF : " + tot;



            }


            if (ModelState.IsValid && CNTCHECK == 0)
            {



                var invent = new CovidSurvey()
                {

                    PersonnelCategory = covidsurvey.PersonnelCategory,
                    EmployeeID = covidsurvey.EmployeeID,
                    Lname = covidsurvey.Lname,
                    Fname = covidsurvey.Fname,
                    Midname = covidsurvey.Midname,
                    Sufix = covidsurvey.Sufix,
                    DateTaken = covidsurvey.DateTaken,
                    TimeTaken = DateTime.Now.TimeOfDay,
                    Office = covidsurvey.Office,
                    ContactNo = covidsurvey.ContactNo,
                    cough = covidsurvey.cough,
                    cold = covidsurvey.cold,
                    sore = covidsurvey.sore,
                    diarrhea = covidsurvey.diarrhea,
                    bodyache = covidsurvey.bodyache,
                    headache = covidsurvey.headache,
                    temp = covidsurvey.temp,
                    breathing = covidsurvey.breathing,
                    fatigue = covidsurvey.fatigue,
                    travelled = covidsurvey.travelled,
                    travelWHere = covidsurvey.travelWHere,
                    travelWhen = covidsurvey.travelWhen,
                    travelInfectedArea = covidsurvey.travelInfectedArea,
                    directContact = covidsurvey.directContact,
                    Remarks = covidsurvey.Remarks,
                    DateEncoded = DateTime.Now,
                    facemask = covidsurvey.facemask,
                    vitamins = covidsurvey.vitamins,
                    alcohol = covidsurvey.alcohol,
                    faceshield = covidsurvey.faceshield,
                    trance = covidsurvey.trance,
                    newid = covidsurvey.newid

                };



                db.CovidSurveys.Add(invent);
                db.SaveChanges();



                //if (covidsurvey.PersonnelCategory == "NIA")
                //{
                //    int medsupplies = db.CovidEquipmentEmployees.Where(r => r.employeeid == covidsurvey.EmployeeID && r.mnt == mnt && r.yr == yr).Count();
                //    var equip = new CovidEquipmentEmployee()
                //{

                //    employeeid = covidsurvey.EmployeeID,
                //    dateGiven = DateTime.Now,
                //    alcohol = covidsurvey.alcohol,
                //    vitamins = covidsurvey.vitamins,
                //    facemask = covidsurvey.facemask,
                //    faceshield = covidsurvey.faceshield,
                //    lname = covidsurvey.Lname,
                //    fname = covidsurvey.Fname,
                //    midname = covidsurvey.Midname,
                //    trance = covidsurvey.trance,
                //    mnt = mnt,
                //    yr = yr



                //};

                //    if (medsupplies <= 0)
                //    {

                //        db.CovidEquipmentEmployees.Add(equip);
                //        db.SaveChanges();
                //    }
                //    else
                //    {


                //        if (covidsurvey.vitamins == "No")
                //        {

                //            equip.vitamins = Session["vitamins"].ToString();

                //        }


                //        if (covidsurvey.alcohol == "No")
                //        {

                //            equip.alcohol = Session["alcohol"].ToString();

                //        }

                //        if (covidsurvey.facemask == "No")
                //        {

                //            equip.facemask = Session["facemask"].ToString();

                //        }

                //        if (covidsurvey.faceshield == "No")
                //        {

                //            equip.faceshield = Session["faceshield"].ToString();

                //        }



                //        equip.CovidEquipmentID = Convert.ToInt32(Session["equipid"].ToString());

                //        db.Entry(equip).State = EntityState.Modified;
                //        db.SaveChanges();


                //    }


                //}

             //   int emp = Convert.ToInt32(covidsurvey.newid.ToString());
                int checkerrec = db2.DailyRecordEmployees.Where(r => r.EmployeeID == covidsurvey.newid && DbFunctions.TruncateTime(r.datescan) == dt1).Count();

                if (checkerrec > 0)
                {
                    string Constring = ConfigurationManager.ConnectionStrings["HealthEntitiesDirect"].ConnectionString;
                    using (SqlConnection CON = new SqlConnection(Constring))
                    {
                        CON.Open();


                        SqlCommand cmd = new SqlCommand("update DailyRecordEmployee set   medical = @medical, temp = @temp where employeeid = @employeeid and datescan = @dt", CON);


                        cmd.Parameters.AddWithValue("@medical", "Yes");
                        cmd.Parameters.AddWithValue("@temp", covidsurvey.temp);
                        cmd.Parameters.AddWithValue("@employeeid", covidsurvey.newid);


                        cmd.Parameters.AddWithValue("@dt", dt1);
                        cmd.ExecuteNonQuery();


                    }
                }

                return RedirectToAction("Create");
            }






            return View(covidsurvey);
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult CreatePerEmployee(CovidSurvey covidsurvey)
        {
            DateTime myDate = DateTime.Now;
            string dt = myDate.ToString("d");
            DateTime dt1 = Convert.ToDateTime(myDate.ToString("d"));

            int mnt = DateTime.Now.Month;
            int yr = DateTime.Now.Year;

            int CNTCHECK = db.CovidSurveys.Where(r => r.EmployeeID == covidsurvey.EmployeeID && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();


            //      TempData["msg"] = "<h2 class = \"alert alert-danger fde\"> Employee is already Checked! </h2>";


            if (ModelState.IsValid && CNTCHECK == 0)
            {
                TempData["msg"] = "";


                var invent = new CovidSurvey()
                {

                    PersonnelCategory = covidsurvey.PersonnelCategory,
                    EmployeeID = covidsurvey.EmployeeID,
                    Lname = covidsurvey.Lname,
                    Fname = covidsurvey.Fname,
                    Midname = covidsurvey.Midname,
                    Sufix = covidsurvey.Sufix,
                    DateTaken = DateTime.Now,
                    TimeTaken = DateTime.Now.TimeOfDay,
                    Office = covidsurvey.Office,
                    ContactNo = covidsurvey.ContactNo,
                    cough = covidsurvey.cough,
                    cold = covidsurvey.cold,
                    sore = covidsurvey.sore,
                    diarrhea = covidsurvey.diarrhea,
                    bodyache = covidsurvey.bodyache,
                    headache = covidsurvey.headache,
                    temp = covidsurvey.temp,
                    breathing = covidsurvey.breathing,
                    fatigue = covidsurvey.fatigue,
                    travelled = covidsurvey.travelled,
                    travelWHere = covidsurvey.travelWHere,
                    travelWhen = covidsurvey.travelWhen,
                    travelInfectedArea = covidsurvey.travelInfectedArea,
                    directContact = covidsurvey.directContact,
                    Remarks = covidsurvey.Remarks,
                    DateEncoded = DateTime.Now,
                    facemask = covidsurvey.facemask,
                    vitamins = covidsurvey.vitamins,
                    alcohol = covidsurvey.alcohol,
                    faceshield = covidsurvey.faceshield,
                    trance = covidsurvey.trance

                };



                db.CovidSurveys.Add(invent);
                db.SaveChanges();






                return RedirectToAction("CreatePerEmployee", "CovidSurvey", new { empno = covidsurvey.EmployeeID });
            }
            else
                TempData["msg"] = "<h2 class = \"alert alert-danger fde\"> Employee is already Checked! </h2>";
            {
            }






            return View(covidsurvey);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEmp(EM_ERoster covidsurvey)
        {

            ViewBag.empcat = new SelectList(db.EmployeeCategories, "code", "description", covidsurvey.office);





            if (ModelState.IsValid)
            {
                if (covidsurvey.office == "NIA")
                {


                    if (!(string.IsNullOrWhiteSpace(covidsurvey.empno)))
                    {



                        var invent = new EM_ERoster()
                        {

                            office = covidsurvey.office,
                            lname = covidsurvey.lname,
                            fname = covidsurvey.fname,
                            mname = covidsurvey.mname,
                            suffix = covidsurvey.suffix,
                            abrev_currdept = covidsurvey.abrev_currdept,
                            empno = covidsurvey.empno,
                            newid = covidsurvey.empno



                        };



                        db.EM_ERoster.Add(invent);
                    }
                    else
                    {

                        TempData["msg"] = "<h2 class = \"alert alert-danger fde\"> Employee must have Employee ID! </h2>";

                        return View(covidsurvey);
                    }





                }
                else
                {

                    var invent = new EmployeeTenant()
                    {

                        Office = covidsurvey.office,
                        Lname = covidsurvey.lname,
                        Fname = covidsurvey.fname,
                        midname = covidsurvey.mname,


                    };



                    db.EmployeeTenants.Add(invent);

                }






                db.SaveChanges();

                return RedirectToAction("Create");


            }






            return View(covidsurvey);
        }

        // GET: /CovidSurvey/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CovidSurvey covidsurvey = db.CovidSurveys.Find(id);



            ViewBag.empcat = new SelectList(db.EmployeeCategories, "code", "description", covidsurvey.PersonnelCategory);

            DateTime myDate = DateTime.Now;
            string dt = myDate.ToString("d");
            DateTime dt1 = Convert.ToDateTime(myDate.ToString("d"));

            int nia = db.CovidSurveys.Where(r => r.PersonnelCategory == "NIA" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();
            int M8 = db.CovidSurveys.Where(r => r.PersonnelCategory == "M8" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();
            int tough = db.CovidSurveys.Where(r => r.PersonnelCategory == "TOUGH GUARD" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();
            int nwrb = db.CovidSurveys.Where(r => r.PersonnelCategory == "NWRB" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();
            int coa = db.CovidSurveys.Where(r => r.PersonnelCategory == "COA" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();
            int pcic = db.CovidSurveys.Where(r => r.PersonnelCategory == "PCIC" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();
            int visitor = db.CovidSurveys.Where(r => r.PersonnelCategory == "VISITOR" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();

            int NIACOOP = db.CovidSurveys.Where(r => r.PersonnelCategory == "NIACOOP" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();
            int NIASLA = db.CovidSurveys.Where(r => r.PersonnelCategory == "NIASLA" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();

            int REGION = db.CovidSurveys.Where(r => r.PersonnelCategory == "REGION" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();
            int CONSULTANT = db.CovidSurveys.Where(r => r.PersonnelCategory == "CONSULTANT" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();
            int NIAESP = db.CovidSurveys.Where(r => r.PersonnelCategory == "NIAESP" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();
            int CSC = db.CovidSurveys.Where(r => r.PersonnelCategory == "CSC" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();
            int PHILCARABAO = db.CovidSurveys.Where(r => r.PersonnelCategory == "PHIL CARABAO" && DbFunctions.TruncateTime(r.DateTaken) == dt1).Count();

            ViewBag.asof = dt;

            ViewBag.STAT = "NIA : " + nia +
            " M8  : " + M8 +
            " TOUGH GUARD : " + tough +
            " NWRB : " + nwrb +
            " COA : " + coa +
            " PCIC : " + pcic +
            " VISITOR : " + visitor +
             " COOP  : " + NIACOOP +
            " NIAES : " + NIAESP +
            " REGION : " + REGION +
            " CONSULTANT : " + CONSULTANT +
            " CSC : " + CSC +
            " PHIL CARABAO : " + PHILCARABAO +
             " NIASLA  : " + NIASLA;

            int tot = nia + M8 + tough + nwrb + coa + pcic + visitor + NIACOOP + NIASLA + NIAESP + REGION + CONSULTANT + CSC + PHILCARABAO;
            ViewBag.tot = "TOTAL OF : " + tot;



            if (covidsurvey == null)
            {
                return HttpNotFound();
            }
            return View(covidsurvey);
        }

        // GET: /CovidSurvey/Edit/5
        public ActionResult EditEmp(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeTenant covidsurvey = db.EmployeeTenants.Find(id);


            if (covidsurvey == null)
            {
                return HttpNotFound();
            }
            return View(covidsurvey);
        }


        public ActionResult EditEquip(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CovidEquipmentEmployee covidsurvey = db.CovidEquipmentEmployees.Find(id);


            if (covidsurvey == null)
            {
                return HttpNotFound();
            }





            return View(covidsurvey);
        }

        // POST: /CovidSurvey/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CovidSurvey covidsurvey)
        {
            if (ModelState.IsValid)
            {


                var invent = new CovidSurvey()
                {
                    IDSurvey = covidsurvey.IDSurvey,
                    PersonnelCategory = covidsurvey.PersonnelCategory,
                    EmployeeID = covidsurvey.EmployeeID,
                    Lname = covidsurvey.Lname,
                    Fname = covidsurvey.Fname,
                    Midname = covidsurvey.Midname,
                    Sufix = covidsurvey.Sufix,
                    DateTaken = covidsurvey.DateTaken,
                    TimeTaken = covidsurvey.TimeTaken,
                    Office = covidsurvey.Office,
                    ContactNo = covidsurvey.ContactNo,
                    cough = covidsurvey.cough,
                    cold = covidsurvey.cold,
                    sore = covidsurvey.sore,
                    diarrhea = covidsurvey.diarrhea,
                    bodyache = covidsurvey.bodyache,
                    headache = covidsurvey.headache,
                    temp = covidsurvey.temp,
                    breathing = covidsurvey.breathing,
                    fatigue = covidsurvey.fatigue,
                    travelled = covidsurvey.travelled,
                    travelWHere = covidsurvey.travelWHere,
                    travelWhen = covidsurvey.travelWhen,
                    travelInfectedArea = covidsurvey.travelInfectedArea,
                    directContact = covidsurvey.directContact,
                    Remarks = covidsurvey.Remarks,
                    DateEncoded = covidsurvey.DateEncoded,
                    facemask = covidsurvey.facemask,
                    vitamins = covidsurvey.vitamins,
                    alcohol = covidsurvey.alcohol,
                    faceshield = covidsurvey.faceshield

                };


                db.Entry(invent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(covidsurvey);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEmp(EmployeeTenant covidsurvey)
        {
            if (ModelState.IsValid)
            {


                var invent = new EmployeeTenant()
                {
                    TenantID = covidsurvey.TenantID,
                    Lname = covidsurvey.Lname,
                    Fname = covidsurvey.Fname,
                    midname = covidsurvey.midname,
                    contact = covidsurvey.contact,
                    Office = covidsurvey.Office
                };


                db.Entry(invent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexEmp");
            }
            return View(covidsurvey);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEquip(CovidEquipmentEmployee covidsurvey)
        {
            if (ModelState.IsValid)
            {


                var invent = new CovidEquipmentEmployee()
                {
                    CovidEquipmentID = covidsurvey.CovidEquipmentID,
                    alcohol = covidsurvey.alcohol,
                    vitamins = covidsurvey.vitamins,
                    facemask = covidsurvey.facemask,
                    faceshield = covidsurvey.faceshield,
                    dateGiven = covidsurvey.dateGiven,
                    employeeid = covidsurvey.employeeid,
                    fname = covidsurvey.fname,
                    lname = covidsurvey.lname,
                    midname = covidsurvey.midname,
                    mnt = covidsurvey.mnt,
                    yr = covidsurvey.yr

                };


                db.Entry(invent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexEquip");
            }
            return View(covidsurvey);
        }

        // GET: /CovidSurvey/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CovidSurvey covidsurvey = db.CovidSurveys.Find(id);
            if (covidsurvey == null)
            {
                return HttpNotFound();
            }
            return View(covidsurvey);
        }
        public ActionResult DeleteEmp(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeTenant covidsurvey = db.EmployeeTenants.Find(id);
            if (covidsurvey == null)
            {
                return HttpNotFound();
            }
            return View(covidsurvey);
        }

        // POST: /CovidSurvey/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {



            CovidSurvey covidsurvey = db.CovidSurveys.Find(id);
            db.CovidSurveys.Remove(covidsurvey);
            db.SaveChanges();

            if (covidsurvey.facemask == "Yes")
            {

                string Constring = ConfigurationManager.ConnectionStrings["CoordinatesEntities"].ConnectionString;
                using (SqlConnection CON = new SqlConnection(Constring))
                {
                    CON.Open();


                    SqlCommand cmd = new SqlCommand("update CovidEquipmentEmployee set facemask = @facemask where employeeid = @employeeid and mnt = @mnt and yr = @yr ", CON);


                    cmd.Parameters.AddWithValue("@facemask", "No");


                    cmd.Parameters.AddWithValue("@employeeid", covidsurvey.EmployeeID);
                    cmd.Parameters.AddWithValue("@mnt", DateTime.Parse(covidsurvey.DateTaken.ToString()).Month);
                    cmd.Parameters.AddWithValue("@yr", DateTime.Parse(covidsurvey.DateTaken.ToString()).Year);
                    cmd.ExecuteNonQuery();


                }

            }



            if (covidsurvey.vitamins == "Yes")
            {


                string Constring1 = ConfigurationManager.ConnectionStrings["CoordinatesEntities"].ConnectionString;
                using (SqlConnection CON1 = new SqlConnection(Constring1))
                {
                    CON1.Open();


                    SqlCommand cmd1 = new SqlCommand("update CovidEquipmentEmployee set vitamins = @vitamins where employeeid = @employeeid and mnt = @mnt and yr = @yr ", CON1);


                    cmd1.Parameters.AddWithValue("@vitamins", "No");


                    cmd1.Parameters.AddWithValue("@employeeid", covidsurvey.EmployeeID);
                    cmd1.Parameters.AddWithValue("@mnt", DateTime.Parse(covidsurvey.DateTaken.ToString()).Month);
                    cmd1.Parameters.AddWithValue("@yr", DateTime.Parse(covidsurvey.DateTaken.ToString()).Year);
                    cmd1.ExecuteNonQuery();


                }

            }



            if (covidsurvey.alcohol == "Yes")
            {

                string Constring2 = ConfigurationManager.ConnectionStrings["CoordinatesEntities"].ConnectionString;
                using (SqlConnection CON2 = new SqlConnection(Constring2))
                {
                    CON2.Open();


                    SqlCommand cmd2 = new SqlCommand("update CovidEquipmentEmployee set alcohol = @alcohol where employeeid = @employeeid and mnt = @mnt and yr = @yr ", CON2);


                    cmd2.Parameters.AddWithValue("@alcohol", "No");


                    cmd2.Parameters.AddWithValue("@employeeid", covidsurvey.EmployeeID);
                    cmd2.Parameters.AddWithValue("@mnt", DateTime.Parse(covidsurvey.DateTaken.ToString()).Month);
                    cmd2.Parameters.AddWithValue("@yr", DateTime.Parse(covidsurvey.DateTaken.ToString()).Year);
                    cmd2.ExecuteNonQuery();


                }

            }

            if (covidsurvey.faceshield == "Yes")
            {

                string Constring3 = ConfigurationManager.ConnectionStrings["CoordinatesEntities"].ConnectionString;
                using (SqlConnection CON3 = new SqlConnection(Constring3))
                {
                    CON3.Open();


                    SqlCommand cmd3 = new SqlCommand("update CovidEquipmentEmployee set faceshield = @faceshield where employeeid = @employeeid and mnt = @mnt and yr = @yr ", CON3);


                    cmd3.Parameters.AddWithValue("@faceshield", "No");


                    cmd3.Parameters.AddWithValue("@employeeid", covidsurvey.EmployeeID);
                    cmd3.Parameters.AddWithValue("@mnt", DateTime.Parse(covidsurvey.DateTaken.ToString()).Month);
                    cmd3.Parameters.AddWithValue("@yr", DateTime.Parse(covidsurvey.DateTaken.ToString()).Year);
                    cmd3.ExecuteNonQuery();


                }

            }






            return RedirectToAction("Index");
        }



        // POST: /CovidSurvey/Delete/5
        [HttpPost, ActionName("DeleteEmp")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedEmp(int id)
        {
            EmployeeTenant covidsurvey = db.EmployeeTenants.Find(id);
            db.EmployeeTenants.Remove(covidsurvey);
            db.SaveChanges();
            return RedirectToAction("IndexEmp");
        }



        public ActionResult DeleteActivity(int id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var FD = db.CovidActivityEmployees.Where(r => r.IDActivityEmployee == id).FirstOrDefault();



            if (FD == null)
            {
                return HttpNotFound();

            }
            return PartialView("_DeleteActivity", FD);

        }

        [HttpPost, ActionName("DeleteActivity")]
        // [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedActivity(CovidActivityEmployee ce)
        {

            CovidActivityEmployee FD = db.CovidActivityEmployees.Find(ce.IDActivityEmployee);
            db.CovidActivityEmployees.Remove(FD);
            db.SaveChanges();

            string url = Url.Action("IndexActivity", "CovidSurvey", new { id = FD.EmployeeID });

            return Json(new { success = true, url = url });

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
