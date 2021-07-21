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

namespace AccompProject.Services
{
    public class FileUploadService
    {
        private InventoryEntities db = new InventoryEntities();
      
        
        public void SaveFileDetails(HttpPostedFileBase file, string id)
        {
            var direc = "";
    
            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Pic/UploadedPics/") + fileName);
            
            System.Diagnostics.Debug.WriteLine(path);
            direc = Path.GetDirectoryName(path);
            file.SaveAs(path);

            MAPPIC newFile = new MAPPIC();
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
            newFile.MAPLOC =  fileName;
            newFile.MAPsystem = ConvertToBytes(file);
            newFile.IDsystem = id;
            
            using (InventoryEntities dataContext = new InventoryEntities())
            {
                dataContext.MAPPICs.Add(newFile);
                dataContext.SaveChanges();
            }
        }

        public byte[] ConvertToBytes(HttpPostedFileBase file)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(file.InputStream);
            imageBytes = reader.ReadBytes((int)file.ContentLength);
            return imageBytes;
        }
    }
}