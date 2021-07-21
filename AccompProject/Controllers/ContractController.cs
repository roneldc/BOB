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
using System.Globalization;
using AccompProject.Services;


namespace AccompProject.Controllers
{
    public class ContractController : Controller
    {
        private AccomplishmentEntities db = new AccomplishmentEntities();

        // GET: Contract
        public ActionResult Index(string id)
        {

            ViewBag.IDAccomp = id;
            Session["idaccomp"] = id;

            var contractview = db.ContractViews
                .OrderByDescending(a => a.ContractName)
                .Where(a => a.IDAccomp == id);



            if (contractview == null)
            {
                return HttpNotFound();
            }
            // return View(asa);
            return PartialView("_Index", contractview);



        }

        public ActionResult IndexContractStatus(int id)
        {

            ViewBag.ContractID = id;
            Session["ContractID"] = id;

            var contractstatusview = db.ContractStatusViews
                  .OrderByDescending(a => a.yr).ThenByDescending(a => a.mnt)
                .Where(a => a.ContractID == id);

            var contractstatusviewFirst = db.ContractViews
               .Where(a => a.ContractID == id).FirstOrDefault();


            if (contractstatusview == null)
            {
                return HttpNotFound();
            }
            // return View(asa);



            Session["regs"] = contractstatusviewFirst.region.ToString();
            Session["subs"] = contractstatusviewFirst.subproject.ToString();
            Session["moni"] = contractstatusviewFirst.projectmonitor.ToString();
            Session["idac"] = contractstatusviewFirst.IDAccomp.ToString();


            return PartialView("_IndexContractStatus", contractstatusview);



        }



        public ActionResult IndexContractFile(int id)
        {

            ViewBag.ContractID = id;
            Session["ContractID"] = id;

            var contractstatusview = db.FileAccompContractViews
                  .OrderByDescending(a => a.title)
                .Where(a => a.IDContract == id);



            if (contractstatusview == null)
            {
                return HttpNotFound();
            }
            // return View(asa);






            return PartialView("_IndexContractFile", contractstatusview);



        }

        public ActionResult IndexContractBilling(int id)
        {

            ViewBag.ContractID = id;
            Session["ContractID"] = id;

            var contractstatusview = db.ContractBillingViews
                  .OrderByDescending(a => a.yr).ThenByDescending(a => a.mnt)
                .Where(a => a.ContractID == id);



            if (contractstatusview == null)
            {
                return HttpNotFound();
            }
            // return View(asa);
            return PartialView("_IndexContractBilling", contractstatusview);



        }



        public ActionResult IndexContractAmountHistory(int id)
        {

            ViewBag.ContractID = id;
            Session["ContractID"] = id;

            var contractstatusview = db.ContractAmountHistoryViews
                  .OrderByDescending(a => a.yr).ThenByDescending(a => a.mnt)
                .Where(a => a.ContractID == id);



            if (contractstatusview == null)
            {
                return HttpNotFound();
            }
            // return View(asa);
            return PartialView("_IndexContractAmountHistory", contractstatusview);



        }

        public ActionResult IndexContractSuspension(int id)
        {

            ViewBag.ContractID = id;
            Session["ContractID"] = id;

            var contractstatusview = db.ContractSuspensionViews
                  .OrderByDescending(a => a.DateSuspension)
                .Where(a => a.ContractID == id);



            if (contractstatusview == null)
            {
                return HttpNotFound();
            }
            // return View(asa);
            return PartialView("_IndexContractSuspension", contractstatusview);



        }


        public ActionResult IndexContractExtension(int id)
        {

            ViewBag.ContractID = id;
            Session["ContractID"] = id;

            var contractstatusview = db.ContractExtensionViews
                  .OrderByDescending(a => a.DateExtension)
                .Where(a => a.ContractID == id);



            if (contractstatusview == null)
            {
                return HttpNotFound();
            }
            // return View(asa);
            return PartialView("_IndexContractExtension", contractstatusview);



        }


        public ActionResult EditContractExtension(int id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractView financ = db.ContractViews.Find(id);
            Session["ContractName"] = financ.ContractName;
            if (financ == null)
            {
                return HttpNotFound();
            }
            return View(financ);
        }

        public ActionResult EditContractSuspension(int id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractView financ = db.ContractViews.Find(id);
            Session["ContractName"] = financ.ContractName;
            if (financ == null)
            {
                return HttpNotFound();
            }
            return View(financ);
        }

        public ActionResult EditContractStatus(int id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractView financ = db.ContractViews.Find(id);
            Session["ContractName"] = financ.ContractName;
            if (financ == null)
            {
                return HttpNotFound();
            }
            return View(financ);
        }



        public ActionResult EditContractFile(int id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractView financ = db.ContractViews.Find(id);
            Session["ContractName"] = financ.ContractName;
            if (financ == null)
            {
                return HttpNotFound();
            }
            return View(financ);
        }

        public ActionResult EditContractBilling(int id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractView financ = db.ContractViews.Find(id);
            Session["ContractName"] = financ.ContractName;
            if (financ == null)
            {
                return HttpNotFound();
            }
            return View(financ);
        }


        public ActionResult EditContractAmountHistory(int id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractView financ = db.ContractViews.Find(id);
            Session["ContractName"] = financ.ContractName;
            if (financ == null)
            {
                return HttpNotFound();
            }
            return View(financ);
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

        // GET: Contract/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create(string Id)
        {
          
            ContractView Fview = new ContractView();
            Fview.IDAccomp = Id;




            return PartialView("_Create", Fview);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContractView FD)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var PA = new ContractProfile()
                    {

                        IDAccomp = FD.IDAccomp,
                        ContractName = FD.ContractName,
                        ContractDescription = FD.ContractDescription,
                        ContractAmount = FD.ContractAmount,
                        ContractDate = FD.ContractDate,
                        ContractorName = FD.ContractorName,
                        TargetStart = FD.TargetStart,
                        TargetEnd = FD.TargetEnd,
                        ActualStart = FD.ActualStart,
                        EstimatedEnd = FD.EstimatedEnd,
                        ContractDuration = FD.ContractDuration,
                        RevisedExpiryDate = FD.RevisedExpiryDate,
                        ABCAmount = FD.ABCAmount,
                        RemarksContract = FD.RemarksContract,
                        ContractReferenceNo = FD.ContractReferenceNo,
                        PhilGepsPositng = FD.PhilGepsPositng,
                        ReviseContractAmount = FD.ReviseContractAmount,
                        ReviseContractDuration = FD.ReviseContractDuration,
                        dateentered = DateTime.Now,
                        userid = User.Identity.Name,
                        type = FD.type


                    };

                    db.ContractProfiles.Add(PA);
                    db.SaveChanges();




                    string url = Url.Action("Index", "Contract", new { id = FD.IDAccomp });

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


        public ActionResult CreateFile(int Id = 0)
        {
            ViewBag.ProcDoc = new SelectList(db.ProDocuments, "ProcDoc", "ProcDoc");
        
            FileAccompContractView Fview = new FileAccompContractView();
            Fview.IDContract = Id;


            return PartialView("_CreateFile", Fview);

        }


      ////  multiple upload
      //  [HttpPost]
      //  [ValidateAntiForgeryToken]
      //  public ActionResult CreateFile(FileAccompContractView FD, HttpPostedFileBase[] upload)
      //  {

      //      //  if (ModelState.IsValid)
      //      //   {
      //      try
      //      {

      //          foreach (HttpPostedFileBase file in upload)
      //          {


      //              if (upload != null && file.ContentLength > 0)
      //              {

      //                  //var DBB = db.Files.Where(x => x.IDAccomp == FD.IDAccomp);
      //                  //if (DBB != null)
      //                  //{
      //                  //    foreach (var courseenrollment in DBB)
      //                  //    {
      //                  //        db.Files.Remove(courseenrollment);
      //                  //    }
      //                  //    db.SaveChanges();

      //                  //}



      //                  //var avatar = new FileAccompContract()
      //                  //{

      //                  //    id = FD.IDContract
      //                  //};
      //                  //using (var reader = new System.IO.BinaryReader(upload.InputStream))
      //                  //{
      //                  //    avatar.Content = reader.ReadBytes(upload.ContentLength);
      //                  //}

      //                  //db.Files.Add(avatar);
      //                  //     db.SaveChanges();









      //                  var PA = new FileAccompContract()
      //                  {

      //                      IDContract = FD.IDContract,
      //                      title = FD.title,
      //                      location = FD.location,
      //                      dateupload = DateTime.Now,
      //                      timeupload = DateTime.Now.TimeOfDay,
      //                      remarks = FD.remarks,
      //                      FileName = System.IO.Path.GetFileName(file.FileName),
      //                      FileType = 1,
      //                      ContentType = FD.ContentType,

      //                  };
      //                  using (var reader = new System.IO.BinaryReader(file.InputStream))
      //                  {
      //                      PA.Content = reader.ReadBytes(file.ContentLength);
      //                  }

      //                  db.FileAccompContracts.Add(PA);
      //                  db.SaveChanges();





      //              }


      //          }

      //          string url = Url.Action("IndexContractFile", "Contract", new { id = FD.IDContract });

      //          return Json(new { success = true, url = url });
      //          //return Json(new { suceess = true });
      //      }
      //      catch (Exception ex)
      //      {
      //          Console.WriteLine(ex);
      //      }
      //      //   }


      //      return PartialView("_Create", FD);

      //  }

      //  single upload
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFile(FileAccompContractView FD, HttpPostedFileBase upload)
        {

            //  if (ModelState.IsValid)
            //   {
            try
            {


                if (upload != null && upload.ContentLength > 0)
                    {

                       
                        var PA = new FileAccompContract()
                        {

                            IDContract = FD.IDContract,
                            title = FD.title,
                            location = FD.location,
                            dateupload = DateTime.Now,
                            timeupload = DateTime.Now.TimeOfDay,
                            remarks = FD.remarks,
                            FileName = System.IO.Path.GetFileName(upload.FileName),
                            FileType = 1,
                            ContentType = FD.ContentType,

                        };
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            PA.Content = reader.ReadBytes(upload.ContentLength);
                        }

                        db.FileAccompContracts.Add(PA);
                        db.SaveChanges();





                    }


             

                string url = Url.Action("IndexContractFile", "Contract", new { id = FD.IDContract });

                return Json(new { success = true, url = url });
                //return Json(new { suceess = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            //   }


            return PartialView("_Create", FD);

        }


        public ActionResult CreateContractStatus(int Id)
        {

            ContractStatusView Fview = new ContractStatusView();
            Fview.ContractID = Id;


            return PartialView("_CreateContractStatus", Fview);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateContractStatus(ContractStatusView FD)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var PA = new ContractStatu()
                    {

                        ContractID = FD.ContractID,
                        mnt = FD.mnt,
                        yr = FD.yr,
                        Revised = FD.Revised,
                        Actual = FD.Actual,
                        Planned = FD.Planned,
                        Remarks = FD.Remarks,
                        ActionDate = FD.ActionDate,
                        ActionWarning = FD.ActionWarning,
                        Revised1 = FD.Revised1,
                        Revised2 = FD.Revised2,
                        Revised3 = FD.Revised3,
                        Revised4 = FD.Revised4,
                        Revised5 = FD.Revised5,
                        Revised6 = FD.Revised6,
                        Revised7 = FD.Revised7,
                        Revised8 = FD.Revised8,
                        Revised9 = FD.Revised9,
                        Revised10 = FD.Revised10



                    };

                    db.ContractStatus.Add(PA);
                    db.SaveChanges();


                    string Constring = ConfigurationManager.ConnectionStrings["AccomplishmentEntitiesSecurity"].ConnectionString;
                    using (SqlConnection CON = new SqlConnection(Constring))
                    {
                        CON.Open();
                        SqlCommand cmd = new SqlCommand("insert into ContractStatuslog (userid,dateedit,timeedit,ContractID,action) values (@userid,@dateedit,@timeedit,@ContractID,@action)", CON);
                        cmd.Parameters.AddWithValue("@userid", Session["userid"].ToString());
                        cmd.Parameters.AddWithValue("@ContractID", FD.ContractID);
                        cmd.Parameters.AddWithValue("@dateedit", DateTime.Now);
                        cmd.Parameters.AddWithValue("@action", "ADD");
                        cmd.Parameters.AddWithValue("@timeedit", DateTime.Now.TimeOfDay);
                        cmd.ExecuteNonQuery();


                    }



                    DateTimeFormatInfo mfi = new DateTimeFormatInfo();
                    var msg = "";
                    msg = msg + " " + FD.region + " create a contract status for " + FD.subproject + ", " + mfi.GetAbbreviatedMonthName(Convert.ToInt32(FD.mnt)) + " " + FD.yr + ", Actual : " + FD.Actual;
                    msg = msg + "% Please Don't reply this is Auto Generated by the System for details refer to Reportserver";

                    SMS sSMS = new SMS();

                    if (FD.projectmonitor == "ED")
                    {
                        var smsnum = db.SMSMonitoredViews
                       .Where(a => a.monitoredarea == FD.IDAccomp && a.position == "AREAMONITOR").ToList();

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

                    }

                    string url = Url.Action("IndexContractStatus", "Contract", new { id = FD.ContractID });

                    return Json(new { success = true, url = url });
                    //return Json(new { suceess = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }


            return PartialView("_CreateContractStatus", FD);

        }


        public ActionResult CreateContractAmountHistory(int Id)
        {

            ContractAmountHistoryView Fview = new ContractAmountHistoryView();
            Fview.ContractID = Id;


            return PartialView("_CreateContractAmountHistory", Fview);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateContractAmountHistory(ContractAmountHistoryView FD)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var PA = new ContractAmountHistory()
                    {

                        ContractID = FD.ContractID,
                        mnt = FD.DateReceived.Value.Month,
                        yr = FD.DateReceived.Value.Year,
                        AmountBilled = FD.AmountBilled,
                        DateReceived = FD.DateReceived,
                        //      DateApplied = FD.DateApplied,
                        DateEncode = DateTime.Today,
                        Remarks = FD.Remarks



                    };

                    db.ContractAmountHistories.Add(PA);
                    db.SaveChanges();




                    string url = Url.Action("IndexContractAmountHistory", "Contract", new { id = FD.ContractID });

                    return Json(new { success = true, url = url });
                    //return Json(new { suceess = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }


            return PartialView("_CreateContractAmountHistory", FD);

        }



        public ActionResult CreateContractBilling(int Id)
        {

            ContractBillingView Fview = new ContractBillingView();
            Fview.ContractID = Id;


            return PartialView("_CreateContractBilling", Fview);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateContractBilling(ContractBillingView FD)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var PA = new ContractBilling()
                    {

                        ContractID = FD.ContractID,
                        mnt = FD.DateApplied.Value.Month,
                        yr = FD.DateApplied.Value.Year,
                        AmountBilled = FD.AmountBilled,
                        DateReceived = FD.DateReceived,
                        DateApplied = FD.DateApplied,
                        DateEncode = DateTime.Today,
                        Remarks = FD.Remarks



                    };

                    db.ContractBillings.Add(PA);
                    db.SaveChanges();




                    string url = Url.Action("IndexContractBilling", "Contract", new { id = FD.ContractID });

                    return Json(new { success = true, url = url });
                    //return Json(new { suceess = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }


            return PartialView("_CreateContractBilling", FD);

        }


        public ActionResult CreateContractSuspension(int Id)
        {

            ContractSuspensionView Fview = new ContractSuspensionView();
            Fview.ContractID = Id;


            return PartialView("_CreateContractSuspension", Fview);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateContractSuspension(ContractSuspensionView FD)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var PA = new ContractSuspension()
                    {

                        ContractID = FD.ContractID,
                        DateSuspension = FD.DateSuspension,
                        DateofResumption = FD.DateofResumption,
                        PeriodDuration = FD.PeriodDuration,
                        Reason = FD.Reason,
                        IDAccomp = Session["idaccomp"].ToString()



                    };

                    db.ContractSuspensions.Add(PA);
                    db.SaveChanges();




                    string url = Url.Action("IndexContractSuspension", "Contract", new { id = FD.ContractID });

                    return Json(new { success = true, url = url });
                    //return Json(new { suceess = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }


            return PartialView("_CreateContractSuspension", FD);

        }



        public ActionResult CreateContractExtension(int Id)
        {

            ContractExtensionView Fview = new ContractExtensionView();
            Fview.ContractID = Id;


            return PartialView("_CreateContractExtension", Fview);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateContractExtension(ContractExtensionView FD)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var PA = new ContractExtension()
                    {

                        ContractID = FD.ContractID,
                        DateExtension = FD.DateExtension,
                        PeriodDuration = FD.PeriodDuration,
                        Reason = FD.Reason,
                        IDAccomp = Session["idaccomp"].ToString()



                    };

                    db.ContractExtensions.Add(PA);
                    db.SaveChanges();




                    string url = Url.Action("IndexContractExtension", "Contract", new { id = FD.ContractID });

                    return Json(new { success = true, url = url });
                    //return Json(new { suceess = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }


            return PartialView("_CreateContractExtension", FD);

        }



        public ActionResult MyEdit(int? id)
        {


            ContractView FD = db.ContractViews.Find(id);


            if (FD == null)
            {
                return HttpNotFound();
            }




            return PartialView("_MyEdit", FD);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyEdit(ContractView FODB)
        {

            if (ModelState.IsValid)
            {


                var FinOBD = new ContractProfile()
                {

                    IDAccomp = FODB.IDAccomp,
                    ContractName = FODB.ContractName,
                    ContractDescription = FODB.ContractDescription,
                    ContractAmount = FODB.ContractAmount,
                    ContractDate = FODB.ContractDate,
                    ContractID = FODB.ContractID,
                    ContractorName = FODB.ContractorName,
                    TargetStart = FODB.TargetStart,
                    TargetEnd = FODB.TargetEnd,
                    ActualStart = FODB.ActualStart,
                    EstimatedEnd = FODB.EstimatedEnd,
                    ContractDuration = FODB.ContractDuration,
                    RevisedExpiryDate = FODB.RevisedExpiryDate,
                    ABCAmount = FODB.ABCAmount,
                    RemarksContract = FODB.RemarksContract,
                    ContractReferenceNo = FODB.ContractReferenceNo,
                    PhilGepsPositng = FODB.PhilGepsPositng,
                    ReviseContractDuration = FODB.ReviseContractDuration,
                    ReviseContractAmount = FODB.ReviseContractAmount,
                    type = FODB.type

                };

                db.Entry(FinOBD).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                string url = Url.Action("Index", "Contract", new { id = FODB.IDAccomp });

                return Json(new { success = true, url = url });
                //return Json(new { suceess = true });
            }

            return PartialView("_MyEdit", FODB);

        }

        public ActionResult MyEditContractStatus(int? id)
        {


            ContractStatusView FD = db.ContractStatusViews.Find(id);


            if (FD == null)
            {
                return HttpNotFound();
            }




            return PartialView("_MyEditContractStatus", FD);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyEditContractStatus(ContractStatusView FODB)
        {
            var valids = true;

            if (FODB.Revised > 0 || FODB.Revised != null)
            {

                if (FODB.Actual < FODB.Revised)
                {

                    var sa = (FODB.Revised - FODB.Actual);


                    if (sa >= 5)
                    {

                        if ((FODB.ActionDate == null))
                        {
                            TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\">  Fields for MC is required </div>";
                            valids = false;
                        }
                    }

                }


            }
            else
            {



                if (FODB.Actual < FODB.Planned)
                {

                    var sa = (FODB.Planned - FODB.Actual);


                    if (sa >= 5)
                    {

                        if ((FODB.ActionDate == null))
                        {
                            TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\">  Fields for MC is required </div>";
                            valids = false;
                        }
                    }

                }
            }

            if ((ModelState.IsValid) && (valids == true))
            {


                var FinOBD = new ContractStatu()
                {

                    ContractStatusID = FODB.ContractStatusID,

                    Actual = FODB.Actual,
                    Revised = FODB.Revised,
                    Remarks = FODB.Remarks,
                    Planned = FODB.Planned,
                    ContractID = FODB.ContractID,
                    mnt = FODB.mnt,
                    yr = FODB.yr,
                    ActionDate = FODB.ActionDate,
                    ActionWarning = FODB.ActionWarning,
                    Revised1 = FODB.Revised1,
                    Revised2 = FODB.Revised2,
                    Revised3 = FODB.Revised3,
                    Revised4 = FODB.Revised4,
                    Revised5 = FODB.Revised5,
                    Revised6 = FODB.Revised6,
                    Revised7 = FODB.Revised7,
                    Revised8 = FODB.Revised8,
                    Revised9 = FODB.Revised9,
                    Revised10 = FODB.Revised10


                };

                db.Entry(FinOBD).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();



                string Constring = ConfigurationManager.ConnectionStrings["AccomplishmentEntitiesSecurity"].ConnectionString;
                using (SqlConnection CON = new SqlConnection(Constring))
                {
                    CON.Open();
                    SqlCommand cmd = new SqlCommand("insert into ContractStatuslog (userid,dateedit,timeedit,ContractID,action) values (@userid,@dateedit,@timeedit,@ContractID,@action)", CON);
                    cmd.Parameters.AddWithValue("@userid", Session["userid"].ToString());
                    cmd.Parameters.AddWithValue("@ContractID", FODB.ContractID);
                    cmd.Parameters.AddWithValue("@action", "EDIT");
                    cmd.Parameters.AddWithValue("@dateedit", DateTime.Now);
                    cmd.Parameters.AddWithValue("@timeedit", DateTime.Now.TimeOfDay);
                    cmd.ExecuteNonQuery();


                }

                DateTimeFormatInfo mfi = new DateTimeFormatInfo();
                var msg = "";
                msg = msg + " " + FODB.region + " update a contract status for " + FODB.subproject + ", " + mfi.GetAbbreviatedMonthName(Convert.ToInt32(FODB.mnt)) + " " + FODB.yr + ", Actual : " + FODB.Actual;
                msg = msg + "% Please Don't reply this is Auto Generated by the System for details refer to Reportserver";

                SMS sSMS = new SMS();

                if (FODB.projectmonitor == "ED")
                {
                    var smsnum = db.SMSMonitoredViews
                   .Where(a => a.monitoredarea == FODB.IDAccomp && a.position == "AREAMONITOR").ToList();

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

                }

                string url = Url.Action("IndexContractStatus", "Contract", new { id = FODB.ContractID });

                return Json(new { success = true, url = url });
                //return Json(new { suceess = true });
            }

            return PartialView("_MyEditContractStatus", FODB);

        }



        public ActionResult MyEditContractBilling(int? id)
        {


            ContractBillingView FD = db.ContractBillingViews.Find(id);


            if (FD == null)
            {
                return HttpNotFound();
            }




            return PartialView("_MyEditContractBilling", FD);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyEditContractBilling(ContractBillingView FODB)
        {
            var valids = true;




            if ((ModelState.IsValid) && (valids == true))
            {


                var FinOBD = new ContractBilling()
                {

                    ContractBillingID = FODB.ContractBillingID,

                    ContractID = FODB.ContractID,
                    mnt = FODB.DateReceived.Value.Month,
                    yr = FODB.DateReceived.Value.Year,
                    AmountBilled = FODB.AmountBilled,
                    DateReceived = FODB.DateReceived,
                    DateApplied = FODB.DateApplied,
                    DateEncode = DateTime.Today,
                    Remarks = FODB.Remarks

                };

                db.Entry(FinOBD).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                string url = Url.Action("IndexContractBilling", "Contract", new { id = FODB.ContractID });

                return Json(new { success = true, url = url });
                //return Json(new { suceess = true });
            }

            return PartialView("_MyEditContractBilling", FODB);

        }


        public ActionResult MyEditContractAmountHistory(int? id)
        {


            ContractAmountHistoryView FD = db.ContractAmountHistoryViews.Find(id);


            if (FD == null)
            {
                return HttpNotFound();
            }




            return PartialView("_MyEditContractAmountHistory", FD);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyEditContractAmountHistory(ContractAmountHistoryView FODB)
        {
            var valids = true;




            if ((ModelState.IsValid) && (valids == true))
            {


                var FinOBD = new ContractAmountHistory()
                {

                    ContractAmountID = FODB.ContractAmountID,

                    ContractID = FODB.ContractID,
                    mnt = FODB.DateReceived.Value.Month,
                    yr = FODB.DateReceived.Value.Year,
                    AmountBilled = FODB.AmountBilled,
                    DateReceived = FODB.DateReceived,
                    //   DateApplied = FODB.DateApplied,
                    DateEncode = DateTime.Today,
                    Remarks = FODB.Remarks

                };

                db.Entry(FinOBD).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                string url = Url.Action("IndexContractAmountHistory", "Contract", new { id = FODB.ContractID });

                return Json(new { success = true, url = url });
                //return Json(new { suceess = true });
            }

            return PartialView("_MyEditContractAmountHistory", FODB);

        }

        public ActionResult MyEditContractSuspension(int? id)
        {


            ContractSuspensionView FD = db.ContractSuspensionViews.Find(id);


            if (FD == null)
            {
                return HttpNotFound();
            }




            return PartialView("_MyEditContractSuspension", FD);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyEditContractSuspension(ContractSuspensionView FODB)
        {

            if (ModelState.IsValid)
            {


                var FinOBD = new ContractSuspension()
                {

                    idsuspension = FODB.idsuspension,

                    ContractID = FODB.ContractID,
                    DateSuspension = FODB.DateSuspension,
                    DateofResumption = FODB.DateofResumption,
                    PeriodDuration = FODB.PeriodDuration,
                    Reason = FODB.Reason,
                    IDAccomp = FODB.IDAccomp

                };

                db.Entry(FinOBD).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                string url = Url.Action("IndexContractSuspension", "Contract", new { id = FODB.ContractID });

                return Json(new { success = true, url = url });
                //return Json(new { suceess = true });
            }

            return PartialView("_MyEditContractSuspension", FODB);

        }


        public ActionResult MyEditContractExtension(int? id)
        {


            ContractExtensionView FD = db.ContractExtensionViews.Find(id);


            if (FD == null)
            {
                return HttpNotFound();
            }




            return PartialView("_MyEditContractExtension", FD);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyEditContractExtension(ContractExtensionView FODB)
        {

            if (ModelState.IsValid)
            {


                var FinOBD = new ContractExtension()
                {

                    IDExtension = FODB.IDExtension,

                    ContractID = FODB.ContractID,
                    DateExtension = FODB.DateExtension,
                    PeriodDuration = FODB.PeriodDuration,
                    Reason = FODB.Reason,
                    IDAccomp = FODB.IDAccomp

                };

                db.Entry(FinOBD).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                string url = Url.Action("IndexContractExtension", "Contract", new { id = FODB.ContractID });

                return Json(new { success = true, url = url });
                //return Json(new { suceess = true });
            }

            return PartialView("_MyEditContractExtension", FODB);

        }


        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractView FD = db.ContractViews.Find(id);

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

            ContractProfile FD = db.ContractProfiles.Find(id);
            db.ContractProfiles.Remove(FD);
            db.SaveChanges();

            string url = Url.Action("Index", "Contract", new { id = FD.IDAccomp });

            return Json(new { success = true, url = url });

        }



        public ActionResult DeleteContractStatus(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractStatusView FD = db.ContractStatusViews.Find(id);

            if (FD == null)
            {
                return HttpNotFound();

            }
            return PartialView("_DeleteContractStatus", FD);

        }

        [HttpPost, ActionName("DeleteContractStatus")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteContractStatus(int id)
        {

            ContractStatu FD = db.ContractStatus.Find(id);
            db.ContractStatus.Remove(FD);
            db.SaveChanges();

            string url = Url.Action("IndexContractStatus", "Contract", new { id = FD.ContractID });

            return Json(new { success = true, url = url });

        }



        public ActionResult DeleteFile(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileAccompContractView FD = db.FileAccompContractViews.Find(id);

            if (FD == null)
            {
                return HttpNotFound();

            }
            return PartialView("_DeleteFile", FD);

        }

        [HttpPost, ActionName("DeleteFile")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFile(int id)
        {

            FileAccompContract FD = db.FileAccompContracts.Find(id);
            db.FileAccompContracts.Remove(FD);
            db.SaveChanges();

            string url = Url.Action("IndexContractFile", "Contract", new { id = FD.IDContract });

            return Json(new { success = true, url = url });

        }


        public ActionResult DeleteContractBilling(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractBillingView FD = db.ContractBillingViews.Find(id);

            if (FD == null)
            {
                return HttpNotFound();

            }
            return PartialView("_DeleteContractBilling", FD);

        }

        [HttpPost, ActionName("DeleteContractBilling")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteContractBilling(int id)
        {

            ContractBilling FD = db.ContractBillings.Find(id);
            db.ContractBillings.Remove(FD);
            db.SaveChanges();

            string url = Url.Action("IndexContractBilling", "Contract", new { id = FD.ContractID });

            return Json(new { success = true, url = url });

        }


        public ActionResult DeleteContractAmountHistory(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractAmountHistoryView FD = db.ContractAmountHistoryViews.Find(id);

            if (FD == null)
            {
                return HttpNotFound();

            }
            return PartialView("_DeleteContractAmountHistory", FD);

        }

        [HttpPost, ActionName("DeleteContractAmountHistory")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteContractAmountHistory(int id)
        {

            ContractAmountHistory FD = db.ContractAmountHistories.Find(id);
            db.ContractAmountHistories.Remove(FD);
            db.SaveChanges();

            string url = Url.Action("IndexContractAmountHistory", "Contract", new { id = FD.ContractID });

            return Json(new { success = true, url = url });

        }


        public ActionResult DeleteContractSuspension(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractSuspensionView FD = db.ContractSuspensionViews.Find(id);

            if (FD == null)
            {
                return HttpNotFound();

            }
            return PartialView("_DeleteContractSuspension", FD);

        }

        [HttpPost, ActionName("DeleteContractSuspension")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteContractSuspension(int id)
        {

            ContractSuspension FD = db.ContractSuspensions.Find(id);
            db.ContractSuspensions.Remove(FD);
            db.SaveChanges();

            string url = Url.Action("IndexContractSuspension", "Contract", new { id = FD.ContractID });

            return Json(new { success = true, url = url });

        }


        public ActionResult DeleteContractExtension(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContractExtensionView FD = db.ContractExtensionViews.Find(id);

            if (FD == null)
            {
                return HttpNotFound();

            }
            return PartialView("_DeleteContractExtension", FD);

        }

        [HttpPost, ActionName("DeleteContractExtension")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteContractExtension(int id)
        {

            ContractExtension FD = db.ContractExtensions.Find(id);
            db.ContractExtensions.Remove(FD);
            db.SaveChanges();

            string url = Url.Action("IndexContractExtension", "Contract", new { id = FD.ContractID });

            return Json(new { success = true, url = url });

        }





    }
}
