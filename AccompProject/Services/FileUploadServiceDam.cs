using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.Mvc;
using AccompProject.Models;
using AccompProject.Models.EntityModel;
using System.IO;
using ExifLib;
using System.Web.UI;
using System.Net;
using AccompProject.Models.Others;

namespace AccompProject.Services
{
    public class FileUploadServiceDam
    {
        private DamEntities db = new DamEntities();
        private AccomplishmentEntities db1 = new AccomplishmentEntities();
        public void SaveFileDetailsTargetAccomp(HttpPostedFileBase file, string id, string controller)
        {
            var direc = "";
            string paths = "";
            if (controller == "TARGET")
            {


                paths = System.Web.HttpContext.Current.Server.MapPath("~/Pic/TARGET/" + id + "/");
            }
            else
            {

                paths = System.Web.HttpContext.Current.Server.MapPath("~/Pic/ACCOMP/" + id + "/");



            }

            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(paths + fileName);

            if (!Directory.Exists(paths))
            {
                Directory.CreateDirectory(paths);
              //  savedb = "yes";
            }
            else
            {

                if (!System.IO.File.Exists(path))
                {

              //      savedb = "yes";
                }


            }






            
            direc = Path.GetDirectoryName(path);
            file.SaveAs(path);

            string filename = Path.GetFileName(path);
       


            ftp ftpclient = new ftp(@"ftp://172.16.4.50:1616/","administrator","W2008edzki");

            ftpclient.createDirectory(controller + "/" + id + "/");
          


            string[] simpleDirectoryListing = ftpclient.directoryListSimple("/"+  controller + "/" + id);
            for (int i = 0; i < simpleDirectoryListing.Count(); i++) 
            { 
              

                ftpclient.delete("/" + controller + "/" + id + "/"  + simpleDirectoryListing[i]);
            }
           
            
            
            ftpclient.upload(controller + "/" + id + "/" + filename, path);

          
        }




        public void SaveFileDetails(HttpPostedFileBase file, string id,string controller)
        {
            var direc = "";
            string savedb  = "yes";
            string paths = "";
            if (controller == "Dam")
            {

                
                paths = System.Web.HttpContext.Current.Server.MapPath("~/Pic/Dam/" + id + "/");
            }
            else
            {

                paths = System.Web.HttpContext.Current.Server.MapPath("~/Pic/DIME/" + id + "/");



            }

            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(paths + fileName);

            if (!Directory.Exists(paths))
            {
                Directory.CreateDirectory(paths);
                savedb = "yes";
            }
            else
            {

                if (!System.IO.File.Exists(path))
                {

                    savedb = "yes";
                }


            }






            //    System.Diagnostics.Debug.WriteLine(path);
            direc = Path.GetDirectoryName(path);
            file.SaveAs(path);


            if (controller == "Dam")
            {

            }
            else
            {


                //String sourcefilepath = "@absolutepath"; // e.g. "d:/test.docx"
                //String ftpurl = "ftp://172.16.4.53/DIME/"; // e.g. ftp://serverip/foldername/foldername
                //String ftpusername = "bob"; // e.g. username
                //String ftppassword = "p@ssw0rd"; // e.g. password

                //string filename = Path.GetFileName(path);
                //string ftpfullpath = ftpurl;
                //FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);
                //ftp.Credentials = new NetworkCredential(ftpusername, ftppassword);

                //ftp.KeepAlive = true;
                //ftp.UseBinary = true;
                //ftp.Method = WebRequestMethods.Ftp.UploadFile;

                //FileStream fs = System.IO.File.OpenRead(path);
                //byte[] buffer = new byte[fs.Length];
                //fs.Read(buffer, 0, buffer.Length);
                //fs.Close();

                //Stream ftpstream = ftp.GetRequestStream();
                //ftpstream.Write(buffer, 0, buffer.Length);
                //ftpstream.Close();

                //var uploadurl = "ftp://172.16.4.50/DIME/" + file.FileName.ToString() + "/";
                //var uploadfilename = file.FileName;
                //var username = "bob";
                //var password = "p@ssw0rd";
                //Stream streamObj = file.InputStream;
                //byte[] buffer = new byte[file.ContentLength];
                //streamObj.Read(buffer, 0, buffer.Length);
                //streamObj.Close();
                //streamObj = null;
                //string ftpurl = String.Format("{0}/{1}", uploadurl, uploadfilename);
                //var requestObj = FtpWebRequest.Create(ftpurl) as FtpWebRequest;
                //requestObj.Method = WebRequestMethods.Ftp.UploadFile;
                //requestObj.Credentials = new NetworkCredential(username, password);
                //Stream requestStream = requestObj.GetRequestStream();
                //requestStream.Write(buffer, 0, buffer.Length);
                //requestStream.Flush();
                //requestStream.Close();
                //requestObj = null;

   
            }




            var newFile = new PicClass();
            if (savedb == "yes") {

               

                try
                {
                    using (var reader = new ExifReader(path))
                    {

                        object datePictureTaken;
                        if (reader.GetTagValue(ExifTags.GPSLongitude, out datePictureTaken))
                        {

                            var arr = datePictureTaken as Array;
                            double deg = Convert.ToDouble(((Array)arr).GetValue(0).ToString());
                            double min = Convert.ToDouble(((Array)arr).GetValue(1).ToString());
                            double sec = Convert.ToDouble(((Array)arr).GetValue(2).ToString());
                            min = (min / 60);
                            sec = (sec / 3600);
                            deg = (deg + min + sec);
                            deg = double.Parse(deg.ToString("N4"));

                            newFile.longitude = deg.ToString();
                        }
                        object datePictureTaken1;
                        if (reader.GetTagValue(ExifTags.GPSLatitude, out datePictureTaken1))
                        {

                            var arr1 = datePictureTaken1 as Array;
                            double deg1 = Convert.ToDouble(((Array)arr1).GetValue(0).ToString());
                            double min1 = Convert.ToDouble(((Array)arr1).GetValue(1).ToString());
                            double sec1 = Convert.ToDouble(((Array)arr1).GetValue(2).ToString());
                            min1 = (min1 / 60);
                            sec1 = (sec1 / 3600);
                            deg1 = (deg1 + min1 + sec1);
                            deg1 = double.Parse(deg1.ToString("N4"));

                            newFile.latitude = deg1.ToString();
                        }

                    }


                    //newFile.imagesize = path;
                   newFile.mapLoc = fileName;
                 //   newFile.mapLoc = path;
                    //  newFile.MAPsystem = ConvertToBytes(file);
                    newFile.IDDam = id;

                    if (!string.IsNullOrWhiteSpace(newFile.longitude.ToString()))
                    {

                        if (controller == "Dam")
                        {

                            var pc = new DamPic()
                            {
                                IDDam = newFile.IDDam,
                                DamPic1 = newFile.DamPic1,
                                latitude = newFile.latitude,
                                longitude = newFile.longitude,
                                mapLoc = newFile.mapLoc


                            };

                            db.DamPics.Add(pc);
                              db.SaveChanges();
                        }
                        else {

                            var pc1 = new DIMEPic()
                            {
                                IDAccomp = newFile.IDDam,
                                DIMEPic1 = newFile.DamPic1,
                                latitude = newFile.latitude,
                                longitude = newFile.longitude,
                                mapLoc = newFile.mapLoc


                            };



                            db1.DIMEPics.Add(pc1);
                              db1.SaveChanges();
                        }

                            
                    }
                }
                catch (Exception e)
                {

                    Console.WriteLine("{0} Exception caught.", e);

                }
            
            
            }

         
         
            
        }

        public byte[] ConvertToBytes(HttpPostedFileBase file)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(file.InputStream);
            imageBytes = reader.ReadBytes((int)file.ContentLength);
            return imageBytes;
        }





        #region Supplier

        public void SaveFileDetailsSupplier(HttpPostedFileBase file, string id, string controller, string docu)
        {
            var direc = "";
            string paths = "";
    //        id = id + docu;

                paths = System.Web.HttpContext.Current.Server.MapPath("~/Pic/Supplier/" + id + "/" + docu + "/");



          

            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(paths + fileName);

            if (!Directory.Exists(paths))
            {
                Directory.CreateDirectory(paths);
               
            }
           







            direc = Path.GetDirectoryName(path);
            file.SaveAs(path);

            string filename = Path.GetFileName(path);



            ftp ftpclient = new ftp(@"ftp://172.16.4.50:1616/", "administrator", "W2008edzki");

            ftpclient.createDirectory(controller + "/" + id + "/");
            ftpclient.createDirectory(controller + "/" + id + "/" + docu + "/");



            string[] simpleDirectoryListing = ftpclient.directoryListSimple("/" + controller + "/" + id + "/" + docu + "/");
            for (int i = 0; i < simpleDirectoryListing.Count(); i++)
            {


                ftpclient.delete("/" + controller + "/" + id + "/" + docu + "/" + simpleDirectoryListing[i]);
            }



            ftpclient.upload(controller + "/" + id + "/" + docu + "/" + filename, path);


        }




        #endregion







        #region Supplier

        public void SaveFileDetailsDAM(HttpPostedFileBase file, string id, string controller, string docu)
        {
            var direc = "";
            string paths = "";
            //        id = id + docu;

            paths = System.Web.HttpContext.Current.Server.MapPath("~/Pic/DAMInspection/" + id + "/" + docu + "/");





            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(paths + fileName);

            if (!Directory.Exists(paths))
            {
                Directory.CreateDirectory(paths);

            }








            direc = Path.GetDirectoryName(path);
            file.SaveAs(path);

            string filename = Path.GetFileName(path);



            ftp ftpclient = new ftp(@"ftp://172.16.4.50:1616/", "administrator", "W2008edzki");

            ftpclient.createDirectory(controller + "/" + id + "/");
            ftpclient.createDirectory(controller + "/" + id + "/" + docu + "/");



            string[] simpleDirectoryListing = ftpclient.directoryListSimple("/" + controller + "/" + id + "/" + docu + "/");
            for (int i = 0; i < simpleDirectoryListing.Count(); i++)
            {


                ftpclient.delete("/" + controller + "/" + id + "/" + docu + "/" + simpleDirectoryListing[i]);
            }



            ftpclient.upload(controller + "/" + id + "/" + docu + "/" + filename, path);


        }




        #endregion













    }
}