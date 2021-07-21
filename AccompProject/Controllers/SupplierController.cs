using AccompProject.Helpers;
using AccompProject.Models;
using AccompProject.Models.EntityModel;
using AccompProject.Models.Others;
using AccompProject.Models.Supplier.Other;
using AccompProject.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AccompProject.Controllers
{
    public class SupplierController : Controller
    {
        procurementss_onlinejervyEntities db = new procurementss_onlinejervyEntities();
        private AccomplishmentEntities db1 = new AccomplishmentEntities();
        private PRLogClassHelper helper = new PRLogClassHelper();
        #region Supplier

        public ActionResult SupplierProfile()
        {
            var typeuser = db1.AspNetUsers
                    .Where(a => a.Email.Equals(User.Identity.Name.ToString())).FirstOrDefault();

            var sp = db.ProcurementSupplierProfiles.Where(r => r.SupplierID == typeuser.Id).FirstOrDefault();


            return View(sp);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SupplierProfile(ProcurementSupplierProfile prof)
        {


            if (ModelState.IsValid)
            {


                db.Entry(prof).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = prof.SupplierID });

            }



            return View(prof);

        }

        public ActionResult Index(string id = null)
        {


            string myid = User.Identity.Name;
            if (string.IsNullOrEmpty(myid))
            {

                return RedirectToAction("Login", "Account", null);
            }


            var ab = db1.AspNetUsers.Where(a => a.Email == myid).FirstOrDefault();

            Session["MYID"] = ab.Id.ToString();


            var physicalaccomp = db.ProcurementListForSuppliers
               .OrderByDescending(a => a.ProcurementID)
               .Where(x => x.supplieridstring == ab.Id && x.draft != "NO");





            if (physicalaccomp == null)
            {
                return HttpNotFound();
            }


            switch ((Session["checker"] == null ? "0" : Session["checker"].ToString()))
            {

                case "1":
                    ViewBag.Notitrigger = "1";
                    break;
                case "2":
                    ViewBag.Notitrigger = "2";
                    break;
                case "3":
                    ViewBag.Notitrigger = "3";
                    break;
                default:
                    ViewBag.Notitrigger = "0";
                    break;

            }

            return View(physicalaccomp);

        }


        public ActionResult IndexSubmitted(string id = null)
        {


            string myid = User.Identity.Name;
            if (string.IsNullOrEmpty(myid))
            {

                return RedirectToAction("Login", "Account", null);
            }


            var ab = db1.AspNetUsers.Where(a => a.Email == myid).FirstOrDefault();

            Session["MYID"] = ab.Id.ToString();


            var physicalaccomp = db.ProcurementListForSuppliers
               .OrderByDescending(a => a.ProcurementID)
               .Where(x => x.supplieridstring == ab.Id && x.draft != "YES");





            if (physicalaccomp == null)
            {
                return HttpNotFound();
            }


            //switch ((Session["checker"] == null ? "0" : Session["checker"].ToString()))
            //{

            //    case "1":
            //        ViewBag.Notitrigger = "1";
            //        break;
            //    case "2":
            //        ViewBag.Notitrigger = "2";
            //        break;
            //    case "3":
            //        ViewBag.Notitrigger = "3";
            //        break;
            //    default:
            //        ViewBag.Notitrigger = "0";
            //        break;

            //}

            return View(physicalaccomp);

        }


        #endregion

        #region CreateSupplier


        public ActionResult CreateSupplier(string procid = null)
        {

            if (string.IsNullOrEmpty(Session["MYID"].ToString()))
            {

                return RedirectToAction("Login", "Account", null);
            }

            string myid = Session["MYID"].ToString();

            int checker = db.ProcurementSuppliers.Where(a => a.Userid == myid && a.procurementID == procid).Count();


            if (checker > 0)
            {




                Session["checker"] = "1";
                return RedirectToAction("Index", "Supplier");


            }


            Session["checker"] = "0";

            var suppleirProfile = db.ProcurementSupplierProfiles.AsNoTracking().First(f => f.userid == myid);

            var mysup = new ProcurementSupplier()
            {

                SuplierName = suppleirProfile.SupplierName,
                Address = suppleirProfile.SupplierAddress,
                ContactNo = suppleirProfile.ContactNo,
                procurementID = procid,
                statusbid = "NW",
                Userid = myid,
                draft = "YES"
            };

            db.ProcurementSuppliers.Add(mysup);
            db.SaveChanges();

            ViewBag.supid = mysup.SupplierID.ToString();



            var items = db.ProcurementModeCartApproveds.Where(p => p.ProcurementID == procid).ToList();


            foreach (var item in items)
            {

                var eachitem = new ProcurementSupplierQuotation()
                {
                    SupplierID = mysup.SupplierID,
                    StatID = item.StatID

                };

                db.ProcurementSupplierQuotations.Add(eachitem);
                db.SaveChanges();



            }








            return RedirectToAction("EditSupplierItem", "Supplier", new { id = mysup.SupplierID });

        }



        public ActionResult EditSupplierItem(int id = 0, string userid = null, string procid = null)
        {

            var physicalaccomp = db.ProcurementSuppliers
                 .Where(a => a.SupplierID == id).FirstOrDefault();

            if (id == 0)
            {

                physicalaccomp = db.ProcurementSuppliers
        .Where(a => a.Userid == userid && a.procurementID == procid).FirstOrDefault();

            }



            if (physicalaccomp == null)
            {
                Session["checker"] = "2";
                return RedirectToAction("Index", "Supplier");

            }
            Session["checker"] = "0";
            return View(physicalaccomp);



        }


        public ActionResult IndexSupplierItem(int id = 0, string submitted = null)
        {


            var physicalaccomp = db.ProcurementSupplierQuotationViews
                .OrderByDescending(a => a.item_desc)
                .Where(a => a.SupplierID == id);



            if (physicalaccomp == null)
            {
                return HttpNotFound();
            }
            // return View(asa);
            return PartialView("_IndexSupplierItem", physicalaccomp);



        }













        #endregion


        #region EditItemFor Supplier





        public ActionResult MyEditSupplierItem(int? id)
        {


            ProcurementSupplierQuotationView FD = db.ProcurementSupplierQuotationViews.Find(id);


            if (FD == null)
            {
                return HttpNotFound();
            }




            return PartialView("_MyEditSupplierItem", FD);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyEditSupplierItem(ProcurementSupplierQuotationView FODB)
        {





            if ((ModelState.IsValid))
            {





                var FinOBD = new ProcurementSupplierQuotation()
                {
                    QuotationID = FODB.QuotationID,
                    SupplierID = FODB.SupplierID,
                    StatID = FODB.StatID,
                    remarks = FODB.remarks,
                    SupplierQuotation = FODB.SupplierQuotation,
                    unitPrice = FODB.unitPrice,
                    totalBidPrice = FODB.totalBidPrice


                };

                db.Entry(FinOBD).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();



                string url = Url.Action("IndexSupplierItem", "Supplier", new { id = FODB.SupplierID });

                return Json(new { success = true, url = url });
                //return Json(new { suceess = true });
            }

            return PartialView("_MyEditSupplierItem", FODB);

        }




        #endregion


        #region Upload Document


        public ActionResult EditSupplierDocument()
        {

            string myid = User.Identity.Name;
            if (string.IsNullOrEmpty(myid))
            {

                return RedirectToAction("Login", "Account", null);
            }


            var ab = db1.AspNetUsers.Where(a => a.Email == myid).FirstOrDefault();

            Session["MYID"] = ab.Id.ToString();

            var physicalaccomp = db.ProcurementSupplierProfiles
                 .Where(a => a.userid == ab.Id.ToString()).FirstOrDefault();

            if (physicalaccomp == null)
            {
                return HttpNotFound();
            }
            return View(physicalaccomp);



        }


        public ActionResult IndexSupplierDocument()
        {

            if (string.IsNullOrEmpty(Session["MYID"].ToString()))
            {

                return RedirectToAction("Login", "Account", null);
            }

            string myid = Session["MYID"].ToString();

            var items = db.ProcurementPerSupplierDocuments
                .OrderByDescending(a => a.DOcuID).ThenByDescending(a => a.DOcuID)
                .Where(a => a.userid == myid);



            if (items == null)
            {
                return HttpNotFound();
            }
            // return View(asa);

            var psdv = new List<ProcurementSupplierDocumentView>();
            foreach (var item in items)
            {


                psdv.Add(new ProcurementSupplierDocumentView
                {

                    DOcuID = item.DOcuID,
                    Docuname = (db.ProcurementDocuments.Where(a => a.IDDocument == item.DOcuID).Select(x => x.documentDescription).FirstOrDefault()),
                    Filename = item.Filename,
                    Filetype = item.Filetype,
                    IDSupplierDocu = item.IDSupplierDocu,
                    userid = item.userid


                });

            }






            return PartialView("_IndexSupplierDocument", psdv);



        }



        public ActionResult UploadDocu()
        {

            if (string.IsNullOrEmpty(Session["MYID"].ToString()))
            {

                return RedirectToAction("Login", "Account", null);
            }

            //ProcurementPerSupplierDocument FD = new ProcurementPerSupplierDocument();

            ProcurementSupplierDocumentView FD = new ProcurementSupplierDocumentView();

            ViewBag.docu = new SelectList(db.ProcurementDocuments, "IDDocument", "documentDescription");

            FD.userid = Session["MYID"].ToString();

            return PartialView("_UploadDocu", FD);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadDocu(ProcurementSupplierDocumentView FD, HttpPostedFileBase upload)
        {

            if (ModelState.IsValid && upload != null)
            {


                var del = db.ProcurementPerSupplierDocuments.Where(r => r.userid == FD.userid && r.DOcuID == FD.DOcuID);
                db.ProcurementPerSupplierDocuments.RemoveRange(del);
                db.SaveChanges();


                var p = new ProcurementPerSupplierDocument()
                {

                    DOcuID = FD.DOcuID,
                    Filename = upload.FileName,
                    userid = FD.userid,
                    Filetype = upload.ContentType


                };

                db.ProcurementPerSupplierDocuments.Add(p);
                db.SaveChanges();





                FileUploadServiceDam service = new FileUploadServiceDam();
                service.SaveFileDetailsSupplier(upload, FD.userid.ToString(), "Supplier", FD.DOcuID.ToString());



                string url = Url.Action("IndexSupplierDocument", "Supplier", null);

                return Json(new { success = true, url = url });

            }

            ViewBag.docu = new SelectList(db.ProcurementDocuments, "IDDocument", "documentDescription", FD.DOcuID);


            return PartialView("_UploadDocu", FD);

        }

        public ActionResult DeleteDocu(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcurementPerSupplierDocument FD = db.ProcurementPerSupplierDocuments.Find(id);

            if (FD == null)
            {
                return HttpNotFound();

            }
            return PartialView("_DeleteDocu", FD);

        }

        [HttpPost, ActionName("DeleteDocu")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDocu(int id)
        {


            if (id == 0)
            {

                return HttpNotFound();

            }

            ftp ftpclient = new ftp(@"ftp://172.16.4.50:1616/", "administrator", "W2008edzki");



            ProcurementPerSupplierDocument FD = db.ProcurementPerSupplierDocuments.Find(id);


            db.ProcurementPerSupplierDocuments.Remove(FD);
            db.SaveChanges();

            string[] simpleDirectoryListing = ftpclient.directoryListSimple("/" + "Supplier" + "/" + FD.userid + "/" + FD.DOcuID.ToString() + "/");
            for (int i = 0; i < simpleDirectoryListing.Count(); i++)
            {


                ftpclient.delete("/" + "Supplier" + "/" + FD.userid + "/" + FD.DOcuID.ToString() + "/" + simpleDirectoryListing[i]);
            }





            string url = Url.Action("IndexSupplierDocument", "Supplier", null);

            return Json(new { success = true, url = url });

        }




        public FileResult DownloadDocu(string userid = null, int docuid = 0, int id = 0)
        {


            var d = db.ProcurementPerSupplierDocuments.Where(p => p.IDSupplierDocu == id).FirstOrDefault();


            var filepath = System.IO.Path.Combine(Server.MapPath("/Files/"), d.Filename);


            string ftp = "ftp://172.16.4.50:1616/";

            //FTP Folder name. Leave blank if you want to Download file from root folder.
            string ftpFolder = "Supplier/" + d.userid + "/" + d.DOcuID + "/";

            try
            {
                //Create FTP Request.
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp + ftpFolder + d.Filename);
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                //Enter FTP Server credentials.
                request.Credentials = new NetworkCredential("administrator", "W2008edzki");
                request.UsePassive = true;
                request.UseBinary = true;
                request.EnableSsl = false;

                //Fetch the Response and read it into a MemoryStream object.
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();



                using (MemoryStream stream = new MemoryStream())
                {
                    //Download the File.
                    response.GetResponseStream().CopyTo(stream);
                    Response.AddHeader("content-disposition", "attachment;filename=" + d.Filename);
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(stream.ToArray());
                    Response.End();
                    return File(stream, MimeMapping.GetMimeMapping(filepath));

                }
            }
            catch (WebException ex)
            {
                throw new Exception((ex.Response as FtpWebResponse).StatusDescription);

            }






        }




        public ActionResult CheckDocu(string procmode = null)
        {


            if (string.IsNullOrEmpty(Session["MYID"].ToString()))
            {

                return RedirectToAction("Login", "Account", null);
            }

            //ProcurementPerSupplierDocument FD = new ProcurementPerSupplierDocument();


            var aa = db.ProcurementStandardDocumentViews.Where(r => r.Procurementmode == procmode).ToList();


            return PartialView("_CheckDocu", aa);


        }









        #endregion



        #region Submit Quotation

        public async Task<ActionResult> SubmitQuotation(string procmode = null, string procid = null)
        {


            if (Session["MYID"] == null)
            {

                return RedirectToAction("Login", "Account", null);
            }
            string myid = Session["MYID"].ToString();

            var docus = db.ProcurementStandardDocumentViews.AsNoTracking().Where(r => r.Procurementmode == procmode).ToList();
            var procsup = db.ProcurementSuppliers.First(r => r.Userid == myid);
            procsup.draft = "NO";
            db.SaveChanges();






            //remove if exist file

            var del = db.ProcurementAbstractofQuotations.Where(r => r.ProcurementID == procid && r.SupplierID == procsup.SupplierID);
            db.ProcurementAbstractofQuotations.RemoveRange(del);
            db.SaveChanges();


            var del1 = db.ProcurementFileDocuments.Where(r => r.procurementID == procid && r.userid == myid);
            db.ProcurementFileDocuments.RemoveRange(del1);
            db.SaveChanges();




            var carts = db.ProcurementSupplierQuotationViews.Where(e => e.ProcurementID == procid);

            foreach (var cart in carts)
            {

                helper.PRLOGS(Convert.ToInt32(cart.id_key), procid, "Supplier Submit Quotation : " + cart.SuplierName, "Quoted by Supplier");

            }

            foreach (var docu in docus)
            {



                var sd = db.ProcurementPerSupplierDocuments.Where(r => r.DOcuID == docu.IDDocument && r.userid == myid).FirstOrDefault();

                //first


                if (sd != null)
                {

                    var abs = new ProcurementAbstractofQuotation()
                    {

                        ProcurementID = procid,
                        SupplierID = procsup.SupplierID,
                        Document_status = "Submitted",
                        DocumentCode = docu.IDDocument

                    };
                    db.ProcurementAbstractofQuotations.Add(abs);
                    db.SaveChanges();

                    //second
                    var submittedDocu = new ProcurementFileDocument()
                        {
                            FileName = sd.Filename,
                            ContentType = sd.Filetype,
                            FileType = 123,
                            procurementID = procid,
                            DocumentCode = docu.IDDocument,
                            abstractid = abs.AbstractID,
                            userid = sd.userid



                        };



                    db.ProcurementFileDocuments.Add(submittedDocu);
                    db.SaveChanges();

                }

            }


            EMAIL myhelper = new EMAIL();

            string emailMsg = "The " + procsup.SuplierName + " submitted a quotation!";
            string emailSubject = "Supplier Quotation";
//            string emailMsgSMS = "DDD";
            // Sending Email.  
            await myhelper.SendEmailAsync("valenciajervy@gmail.com", emailMsg, emailSubject);



            #region sms
            //sms

            var msg = "";

            msg = msg + "Supplier " + procsup.SuplierName + " submitted a quotation";
            msg = msg + " Please Don't reply this is Auto Generated.";

            SMS sSMS = new SMS();

            var smsnum = db1.SMS
           .Where(a => a.monitoredarea == "Central Office" && a.projectmonitor == "PPD" && a.position == "CANVASSER").ToList();




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





            #endregion


            Session["checker"] = "3";
            return RedirectToAction("Index", "Supplier");


        }


        #endregion

    }
}
