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
using System.IO;

namespace AccompProject.Controllers
{
    [Authorize]
    public class ASAAttachmentController : Controller
    {
        private AccomplishmentEntities db = new AccomplishmentEntities();

        // GET: /ASAAttachment/
        public ActionResult Index()
        {
             string strauto = Session["regiontolog"].ToString();
            
            var asaattachlist = db.ASAAttachmentViews
                                 .Where(r => r.region == strauto)
                                 .OrderByDescending(r=> r.ASANO);


            return View(asaattachlist);
        }
         public FileResult DownloadFile(string fileName)
        {
            var filepath = System.IO.Path.Combine(Server.MapPath("/Files/"), fileName);
           // return File(filepath, MimeMapping.GetMimeMapping(filepath), fileName);

            //FTP Server URL.
           //   string ftp = "ftp://172.16.4.25/";

            //string ftp = "ftp://isds1.nia.gov.ph/";
            string ftp = "ftp://172.16.4.11/";

            //FTP Folder name. Leave blank if you want to Download file from root folder.
            string ftpFolder = "asa/";

            try
            {
                //Create FTP Request.
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp + ftpFolder + fileName);
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                //Enter FTP Server credentials.
                //request.Credentials = new NetworkCredential("budgetdb", "Passw0rd");
                request.Credentials = new NetworkCredential("asa", "!P@ssw0rd123");
                request.UsePassive = true;
                request.UseBinary = true;
                request.EnableSsl = false;

                //Fetch the Response and read it into a MemoryStream object.
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();



                using (MemoryStream stream = new MemoryStream())
                {
                    //Download the File.
                    response.GetResponseStream().CopyTo(stream);
                    Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
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
