using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccompProject.Models;
using AccompProject.Services;
using AccompProject.Models.EntityModel;
using System.Configuration;
using System.Data.SqlClient;
using ExifLib;
using System.IO;


namespace AccompProject.Controllers
{
    public class MapController : Controller
    {

        // GET: Map
        private InventoryEntities db = new InventoryEntities();
        // GET: Map
        PolygonCode empDB = new PolygonCode();

    //    [Authorize]

        public JsonResult Add(Polygon emp)
        {

            return Json(empDB.Add(emp), JsonRequestBehavior.AllowGet);

        }


        public JsonResult AddPromotional(PolygonPromotional emp)
        {

            return Json(empDB.AddPromotional(emp), JsonRequestBehavior.AllowGet);

        }

        public JsonResult Delete(string id, string tempdata)
        {

            return Json(empDB.Delete(id, tempdata), JsonRequestBehavior.AllowGet);

        }

        public ActionResult sampleView()
        {

            return View();

        }



        public JsonResult getPolyInfo(string id)
        {

            MappingDataview mappingdata = db.MappingDataviews.Find(id);

       //     var mappingdataview = db.MappingDataviews
          //      .Where(r => r.id.Contains(id));

            //List<Student> students = new List<Student>();
            //students = context.Students.ToList();
            return Json(mappingdata, JsonRequestBehavior.AllowGet);
        }  




        public ActionResult SView()
        {

            return View();

        }

        public ActionResult UpdateCoord(string latitude, string longitude, string id) {


            string Constring = ConfigurationManager.ConnectionStrings["CoordinatesEntities"].ConnectionString;
            using (SqlConnection CON = new SqlConnection(Constring))
            {
                CON.Open();
                SqlCommand cmd = new SqlCommand("update tbldata set latitude = '" + latitude + "'" + ", longitude = '" + longitude + "'" + " where idsystem = '" + id + "'", CON);


                cmd.ExecuteNonQuery();

                string url = Url.Action("IndexMapInventory", "Map", new { id = id });

                return Json(new { success = true, url = url });

            }
        
        }


        //public ActionResult Autocomplete(string term)
        //{
        //    //Session["regiontolog"] = "UPRIIS";
        //    if (Session["regiontolog"] == null)
        //    {

        //        return RedirectToAction("LogIn", "Account", new { area = "" });

        //    }
        //    string strauto = Session["regiontolog"].ToString();

        //    var accomplishments = db.CoordinatesPerSystems
        //          .Where(r => r.SYSTEMS.Contains(term) && r.res_region == strauto)
        //          .Select(r => new
        //          {
        //              label = r.SYSTEMS
        //          })
        //          .Take(5);

        //    return new JsonResult { Data = accomplishments, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        //}

        [HttpPost]
        public ActionResult IndexPic(int id, string lat, string lng)
        {
            //try
            //{
                string Constring = ConfigurationManager.ConnectionStrings["CoordinatesEntities"].ConnectionString;
                using (SqlConnection CON = new SqlConnection(Constring))
                {
                    CON.Open();
                    SqlCommand cmd = new SqlCommand("update mappic set latitude = '" + lat + "'" + ", longitude = '" + lng + "'" + " where idtable = " + id, CON);
                
                    
                    cmd.ExecuteNonQuery();

                    return Json(new { success = true });

                }

           // }
            //catch (Exception ex)
            //{
            //    return Json(new { success = false, ExceptionMessage = "Some error here" });
            //}
        }

        [HttpPost]
        public ActionResult Index(string id, string lat, string lng)
        {
            //try
            //{
            string Constring = ConfigurationManager.ConnectionStrings["CoordinatesEntities"].ConnectionString;
            using (SqlConnection CON = new SqlConnection(Constring))
            {
                CON.Open();
                SqlCommand cmd = new SqlCommand("update TBLDATA set latitude = '" + lat + "'" + ", longitude = '" + lng + "'" + " where IDSYSTEM = '" + id + "'" , CON);


                cmd.ExecuteNonQuery();

                return Json(new { success = true });

            }

            // }
            //catch (Exception ex)
            //{
            //    return Json(new { success = false, ExceptionMessage = "Some error here" });
            //}
        }



        [HttpPost]
        public ActionResult IndexDam(string id, string lat, string lng)
        {
            //try
            //{
            string Constring = ConfigurationManager.ConnectionStrings["DamConnection"].ConnectionString;
            using (SqlConnection CON = new SqlConnection(Constring))
            {
                CON.Open();
                SqlCommand cmd = new SqlCommand("update GeneralInformation set latitude = '" + lat + "'" + ", longitude = '" + lng + "'" + " where iddam = '" + id + "'", CON);


                cmd.ExecuteNonQuery();

                return Json(new { success = true });

            }

            // }
            //catch (Exception ex)
            //{
            //    return Json(new { success = false, ExceptionMessage = "Some error here" });
            //}
        }





        public ActionResult IndexPic()
        {
            string strauto = Session["regiontolog"].ToString();
            string markersPIC = "[";
            string Constring = ConfigurationManager.ConnectionStrings["CoordinatesEntities"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SELECT * FROM MAPPICVIEW where res_region = '" + strauto + "'");
            using (SqlConnection CON = new SqlConnection(Constring))
            {

                cmd.Connection = CON;
                CON.Open();
                using (SqlDataReader SDR = cmd.ExecuteReader())
                {
                    while (SDR.Read())
                    {
                        MAPPIC MPIC = db.MAPPICs.Find(SDR["IDtable"]);

                        
                        var mapsys = Convert.ToBase64String(MPIC.MAPsystem);

                        markersPIC += "{";
                        markersPIC += string.Format("'ID': {0},", SDR["IDtable"]);
                        markersPIC += string.Format("'GeoLat': '{0}',", SDR["long"]);
                        markersPIC += string.Format("'GeoLong': '{0}',", SDR["lat"]);
                        markersPIC += string.Format("'Systems': '{0}',", SDR["systems"]);
                        markersPIC += string.Format("'Municipality': '{0}',", SDR["municipality"]);
                        markersPIC += string.Format("'SA': '{0}',", SDR["Service_original"]);
                        markersPIC += string.Format("'FUSA': '{0}',", SDR["SERVICE_FIRMED"]);
                        markersPIC += string.Format("'MAPLOC': '{0}',", SDR["MAPLOC"]);
                        markersPIC += string.Format("'MAPSYSTEM': '{0}',", mapsys);
                        markersPIC += string.Format("'CATEGORY': '{0}',", SDR["ID_CATEGORY"]);

                        markersPIC += "},";

                    
                    
                    }


                }
                CON.Close();



            }


            
            markersPIC += "];";
            ViewBag.MarkersPIC = markersPIC;

         


            return View();
        }



        public ActionResult IndexPicProj()
        {
            string strauto = Session["regiontolog"].ToString();
            string markersPIC = "[";
            string Constring = ConfigurationManager.ConnectionStrings["CoordinatesEntities"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SELECT * FROM dampicview");
            using (SqlConnection CON = new SqlConnection(Constring))
            {

                cmd.Connection = CON;
                CON.Open();
                using (SqlDataReader SDR = cmd.ExecuteReader())
                {
                    while (SDR.Read())
                    {
                        MAPPIC MPIC = db.MAPPICs.Find(SDR["IDtable"]);


                        var mapsys = Convert.ToBase64String(MPIC.MAPsystem);

                        markersPIC += "{";
                        markersPIC += string.Format("'ID': {0},", SDR["IDtable"]);
                        markersPIC += string.Format("'GeoLat': '{0}',", SDR["long"]);
                        markersPIC += string.Format("'GeoLong': '{0}',", SDR["lat"]);
                        markersPIC += string.Format("'Systems': '{0}',", SDR["systems"]);
                        markersPIC += string.Format("'Municipality': '{0}',", SDR["municipality"]);
                        markersPIC += string.Format("'SA': '{0}',", SDR["Service_original"]);
                        markersPIC += string.Format("'FUSA': '{0}',", SDR["SERVICE_FIRMED"]);
                        markersPIC += string.Format("'MAPLOC': '{0}',", SDR["MAPLOC"]);
                        markersPIC += string.Format("'MAPSYSTEM': '{0}',", mapsys);
                        markersPIC += string.Format("'CATEGORY': '{0}',", SDR["ID_CATEGORY"]);

                        markersPIC += "},";



                    }


                }
                CON.Close();



            }



            markersPIC += "];";
            ViewBag.MarkersPIC = markersPIC;




            return View();
        }

        //public string ImageToBase64(byte[] paths)
        //{
        
        //    //byte[] header = (byte[])paths;
        //    base64String = Convert.ToBase64String(paths, 0, paths.Length);
        //    return base64String;
        
        //}  

        public ActionResult Index()
        {
            string strauto = Session["regiontolog"].ToString();
            string markers = "[";
            string Constring = ConfigurationManager.ConnectionStrings["CoordinatesEntities"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SELECT * FROM COORD where res_region = '" + strauto + "'");
            using (SqlConnection CON = new SqlConnection(Constring))
            {

                cmd.Connection = CON;
                CON.Open();
                using (SqlDataReader SDR = cmd.ExecuteReader())
                {
                    while (SDR.Read())
                    {

                        markers += "{";
                        markers += string.Format("'ID': {0},", SDR["Id"]);
                        markers += string.Format("'GeoLat': '{0}',", SDR["long"]);
                        markers += string.Format("'GeoLong': '{0}',", SDR["lat"]);
                        markers += string.Format("'Systems': '{0}',", SDR["systems"]);
                        markers += string.Format("'Municipality': '{0}',", SDR["municipality"]);
                        markers += string.Format("'SA': '{0}',", SDR["Service_original"]);
                        markers += string.Format("'FUSA': '{0}',", SDR["SERVICE_FIRMED"]);
                        markers += string.Format("'IDSYSTEM': '{0}',", SDR["IDSYSTEM"]);
                        markers += string.Format("'CATEGORY': '{0}',", SDR["ID_CATEGORY"]);
                       
                        markers += "},";
                    }


                }
                CON.Close();



            }




            markers += "];";
            ViewBag.Markers = markers;



            return View();
        }


        [AllowAnonymous]
        public ActionResult IndexMapExe()
        {


          

          //  string strauto = Session["regiontolog"].ToString();
            string markers = "[";
            string Constring = ConfigurationManager.ConnectionStrings["CoordinatesEntities"].ConnectionString;
            //SqlCommand cmd = new SqlCommand("SELECT * FROM COORD where res_region = '" + strauto + "'");
            SqlCommand cmd = new SqlCommand("SELECT * FROM COORD where CoordinatesChecking = 'WITH COORDINATES' AND ID_CATEGORY = 1 and (res_region = 'region 4a' or res_region = 'region 4b')");

         
            
            
            
            using (SqlConnection CON = new SqlConnection(Constring))
            {

                cmd.Connection = CON;
                CON.Open();
                using (SqlDataReader SDR = cmd.ExecuteReader())
                {
                    while (SDR.Read())
                    {

                        markers += "{";
                        markers += string.Format("'ID': {0},", SDR["Id"]);
                        markers += string.Format("'GeoLat': '{0}',", SDR["long"]);
                        markers += string.Format("'GeoLong': '{0}',", SDR["lat"]);
                        markers += string.Format("'Systems': '{0}',", SDR["systems"]);
                        markers += string.Format("'Municipality': '{0}',", SDR["municipality"]);
                        markers += string.Format("'SA': '{0}',", SDR["Service_original"]);
                        markers += string.Format("'FUSA': '{0}',", SDR["SERVICE_FIRMED"]);
                        markers += string.Format("'IDSYSTEM': '{0}',", SDR["IDSYSTEM"]);
                        markers += string.Format("'CATEGORY': '{0}',", SDR["ID_CATEGORY"]);

                        markers += "},";
                    }


                }
                CON.Close();



            }




            markers += "];";
            ViewBag.Markers = markers;



            return View();
        }

           [AllowAnonymous]
        public ActionResult IndexMapExeCIS()
        {




       //     string strauto = Session["regiontolog"].ToString();
            string markers = "[";
            string Constring = ConfigurationManager.ConnectionStrings["CoordinatesEntities"].ConnectionString;
            //SqlCommand cmd = new SqlCommand("SELECT * FROM COORD where res_region = '" + strauto + "'");
            SqlCommand cmd = new SqlCommand("SELECT * FROM COORD where CoordinatesChecking = 'WITH COORDINATES' AND ID_CATEGORY = 2 and (res_region = 'region 4a' or res_region = 'region 4b')");





            using (SqlConnection CON = new SqlConnection(Constring))
            {

                cmd.Connection = CON;
                CON.Open();
                using (SqlDataReader SDR = cmd.ExecuteReader())
                {
                    while (SDR.Read())
                    {

                        markers += "{";
                        markers += string.Format("'ID': {0},", SDR["Id"]);
                        markers += string.Format("'GeoLat': '{0}',", SDR["long"]);
                        markers += string.Format("'GeoLong': '{0}',", SDR["lat"]);
                        markers += string.Format("'Systems': '{0}',", SDR["systems"]);
                        markers += string.Format("'Municipality': '{0}',", SDR["municipality"]);
                        markers += string.Format("'SA': '{0}',", SDR["Service_original"]);
                        markers += string.Format("'FUSA': '{0}',", SDR["SERVICE_FIRMED"]);
                        markers += string.Format("'IDSYSTEM': '{0}',", SDR["IDSYSTEM"]);
                        markers += string.Format("'CATEGORY': '{0}',", SDR["ID_CATEGORY"]);

                        markers += "},";
                    }


                }
                CON.Close();



            }




            markers += "];";
            ViewBag.Markers = markers;



            return View();
        }


           [AllowAnonymous]
           public ActionResult IndexProjRegion()
           {
               string des = "";
             //  string sub = "";
               //  string strauto = Session["regiontolog"].ToString();

               string markers = "[";
               //string Constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
               ////    SqlCommand cmd = new SqlCommand("SELECT * FROM projectcoordinatesview where region = '" + strauto + "'");
               //SqlCommand cmd = new SqlCommand("SELECT * FROM projectcoordinatesview");

               string Constring = ConfigurationManager.ConnectionStrings["CoordinatesEntities"].ConnectionString;
               SqlCommand cmd = new SqlCommand("SELECT * FROM CoordinatesPerSystem1 where (res_region = 'REGION 2' OR RES_REGION = 'MARIIS')");
            
               using (SqlConnection CON = new SqlConnection(Constring))
               {

                   cmd.Connection = CON;
                   CON.Open();
                   using (SqlDataReader SDR = cmd.ExecuteReader())
                   {
                       while (SDR.Read())
                       {


                           //des = SDR["description"].ToString();
                           //des = des.Replace("'", "");


                           //sub = SDR["subproject"].ToString();
                           //sub = sub.Replace("'", "");


                           markers += "{";
                           markers += string.Format("'GeoLat': '{0}',", SDR["longi"]);
                           markers += string.Format("'GeoLong': '{0}',", SDR["lat"]);
                           markers += string.Format("'Systems': '{0}',", SDR["SYSTEMS"]);
                           markers += string.Format("'Description': '{0}',", des);
                           markers += string.Format("'MUNICIPALITY': '{0}',", "");
                           markers += string.Format("'SERVICE_ORIGINAL': '{0}',", SDR["sa"]);
                           markers += string.Format("'SERVICE_FIRMED': '{0}',", SDR["fusa"]);

                           markers += "},";
                       }


                   }
                   CON.Close();



               }




               markers += "];";
               ViewBag.Markers = markers;



               return View();
           }

           [AllowAnonymous]
           public ActionResult IndexProjRegion1()
           {
               string des = "";
               //  string sub = "";
               //  string strauto = Session["regiontolog"].ToString();

               string markers = "[";
               //string Constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
               ////    SqlCommand cmd = new SqlCommand("SELECT * FROM projectcoordinatesview where region = '" + strauto + "'");
               //SqlCommand cmd = new SqlCommand("SELECT * FROM projectcoordinatesview");

               string Constring = ConfigurationManager.ConnectionStrings["CoordinatesEntities"].ConnectionString;
               SqlCommand cmd = new SqlCommand("SELECT * FROM CoordinatesPerSystem1 where (res_region = 'REGION 2' OR RES_REGION = 'MARIIS')");

               using (SqlConnection CON = new SqlConnection(Constring))
               {

                   cmd.Connection = CON;
                   CON.Open();
                   using (SqlDataReader SDR = cmd.ExecuteReader())
                   {
                       while (SDR.Read())
                       {


                           //des = SDR["description"].ToString();
                           //des = des.Replace("'", "");


                           //sub = SDR["subproject"].ToString();
                           //sub = sub.Replace("'", "");


                           markers += "{";
                           markers += string.Format("'GeoLat': '{0}',", SDR["longi"]);
                           markers += string.Format("'GeoLong': '{0}',", SDR["lat"]);
                           markers += string.Format("'Systems': '{0}',", SDR["SYSTEMS"]);
                           markers += string.Format("'Description': '{0}',", des);
                           markers += string.Format("'MUNICIPALITY': '{0}',", "");
                           markers += string.Format("'SERVICE_ORIGINAL': '{0}',", SDR["sa"]);
                           markers += string.Format("'SERVICE_FIRMED': '{0}',", SDR["fusa"]);

                           markers += "},";
                       }


                   }
                   CON.Close();



               }




               markers += "];";
               ViewBag.Markers = markers;



               return View();
           }


        [AllowAnonymous]
        public ActionResult IndexProj()
        {
            string des = "";
            string sub = "";
          //  string strauto = Session["regiontolog"].ToString();

            string markers = "[";
            string Constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //    SqlCommand cmd = new SqlCommand("SELECT * FROM projectcoordinatesview where region = '" + strauto + "'");
            SqlCommand cmd = new SqlCommand("SELECT * FROM projectcoordinatesview where isnum = 'yes'");
            using (SqlConnection CON = new SqlConnection(Constring))
            {

                cmd.Connection = CON;
                CON.Open();
                using (SqlDataReader SDR = cmd.ExecuteReader())
                {
                    while (SDR.Read())
                    {


                        //des = SDR["description"].ToString();
              //          des = des.Replace("'", "");


                        sub = SDR["subproject"].ToString();
                        sub = sub.Replace("'", "");


                        markers += "{";
                        markers += string.Format("'GeoLat': '{0}',", SDR["longi"]);
                        markers += string.Format("'GeoLong': '{0}',", SDR["lati"]);
                        markers += string.Format("'Systems': '{0}',", sub);
                     //   markers += string.Format("'Description': '{0}',", des);

                        markers += "},";
                    }


                }
                CON.Close();



            }




            markers += "];";
            ViewBag.Markers = markers;



            return View();
        }

         [AllowAnonymous]
        public ActionResult IndexSubProj(string id)
        {
            //0212D24B-8B9F-4544-89E3-0369ECF9B213
         //   string strauto = "0212D24B-8B9F-4544-89E3-0369ECF9B213";
            string markers = "[";
            string Constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SELECT * FROM projectcoordinatesview where idaccomp = '" + id + "'");
            using (SqlConnection CON = new SqlConnection(Constring))
            {

                cmd.Connection = CON;
                CON.Open();
                using (SqlDataReader SDR = cmd.ExecuteReader())
                {
                    while (SDR.Read())
                    {

                        markers += "{";
                        markers += string.Format("'GeoLat': '{0}',", SDR["longi"]);
                        markers += string.Format("'GeoLong': '{0}',", SDR["lati"]);
                        markers += string.Format("'Systems': '{0}',", SDR["subproject"]);
                        markers += string.Format("'Description': '{0}',", SDR["description"]);

                        markers += "},";
                    }


                }
                CON.Close();



            }




            markers += "];";
            ViewBag.Markers = markers;



            return View();
        }


   

     //    [AllowAnonymous]
         public ActionResult IndexDam(string id)
         {
             //0212D24B-8B9F-4544-89E3-0369ECF9B213
             //   string strauto = "0212D24B-8B9F-4544-89E3-0369ECF9B213";
             string markers = "[";
             string Constring = ConfigurationManager.ConnectionStrings["DamConnection"].ConnectionString;
             SqlCommand cmd = new SqlCommand("SELECT * FROM GeneralInformation where iddam = '" + id + "'");
             using (SqlConnection CON = new SqlConnection(Constring))
             {

                 cmd.Connection = CON;
                 CON.Open();
                 using (SqlDataReader SDR = cmd.ExecuteReader())
                 {
                     while (SDR.Read())
                     {

                         markers += "{";
                         markers += string.Format("'GeoLat': '{0}',", SDR["longitude"]);
                         markers += string.Format("'GeoLong': '{0}',", SDR["latitude"]);
                         markers += string.Format("'Systems': '{0}',", SDR["DamName"]);
                         markers += string.Format("'Description': '{0}',", SDR["description"]);
                         markers += string.Format("'IDDam': '{0}',", SDR["IDDam"]);

                         markers += "},";
                     }


                 }
                 CON.Close();



             }




             markers += "];";
             ViewBag.Markers = markers;


             //pictaur

             string markersPIC = "[";
             string ConstringPIC = ConfigurationManager.ConnectionStrings["DamConnection"].ConnectionString;
             SqlCommand cmdPIC = new SqlCommand("SELECT * FROM DAMPICVIEW where iddam = '" + id + "'");
             using (SqlConnection CON = new SqlConnection(ConstringPIC))
             {

                 cmdPIC.Connection = CON;
                 CON.Open();
                 using (SqlDataReader SDR = cmdPIC.ExecuteReader())
                 {
                     while (SDR.Read())
                     {

                         markersPIC += "{";
                         markersPIC += string.Format("'GeoLat': '{0}',", SDR["longitude"]);
                         markersPIC += string.Format("'GeoLong': '{0}',", SDR["latitude"]);
                         markersPIC += string.Format("'Systems': '{0}',", SDR["DamName"]);
                         markersPIC += string.Format("'Description': '{0}',", SDR["MAPLOC"]);
                         markersPIC += string.Format("'iddam': '{0}',", SDR["iddam"]);

                         markersPIC += "},";
                     }


                 }
                 CON.Close();



             }




             markersPIC += "];";
             ViewBag.MarkersPIC = markersPIC;




             //polygon

           //  string strauto = Session["regiontolog"].ToString();
             Session["iddam"] = id;
             string markersPICpoly = "[";
             string ConstringPICPOLY = ConfigurationManager.ConnectionStrings["DamConnection"].ConnectionString;
             Boolean tm = false;


             SqlCommand cmd1 = new SqlCommand("SELECT * FROM mappingdata where iddam = '" + id + "'");
             using (SqlConnection CON = new SqlConnection(ConstringPICPOLY))
             {

                 cmd1.Connection = CON;
                 CON.Open();
                 using (SqlDataReader SDR = cmd1.ExecuteReader())
                 {
                     while (SDR.Read())
                     {
                         tm = true;
                         markersPICpoly += "{";
                         markersPICpoly += string.Format("\"type\": \"{0}\"" + ",", SDR["DRAWINGTYPE"]);
                         markersPICpoly += string.Format("\"id\": \"{0}\"" + ",", SDR["id"]);

                         if (SDR["DRAWINGTYPE"].ToString() == "POLYGON")
                         {
                             markersPICpoly += string.Format("\"geometry\":[" + "\"{0}\"" + "]", SDR["geometry"]);
                         }
                         else
                         {
                             markersPICpoly += string.Format("\"geometry\":" + "\"{0}\"" + "", SDR["geometry"]);
                         }

                         //markersPIC += string.Format("'Systems': '{0}',", SDR["systems"]);
                         //markersPIC += string.Format("'Municipality': '{0}',", SDR["municipality"]);
                         //markersPIC += string.Format("'SA': '{0}',", SDR["Service_original"]);
                         //markersPIC += string.Format("'FUSA': '{0}',", SDR["SERVICE_FIRMED"]);
                         //markersPIC += string.Format("'MAPLOC': '{0}',", SDR["MAPLOC"]);
                         //markersPIC += string.Format("'MAPSYSTEM': '{0}',", SDR["MAPSYSTEM"]);
                         //markersPIC += string.Format("'CATEGORY': '{0}',", SDR["ID_CATEGORY"]);

                         markersPICpoly += "},";


                     }


                 }
                 CON.Close();



             }

             if (tm == true)
             {
                 markersPICpoly += "]";
                 ViewBag.MarkersPICpoly = markersPICpoly.Remove(markersPICpoly.Length - 2, 1).Insert(markersPICpoly.Length - 2, "");
             }
             else
             {
                 markersPICpoly = "[]";
                 ViewBag.MarkersPICpoly = "[]";
             }
            












             return View();
         }



     


         public ActionResult IndexMapInventory1(string id)
         {




             //new 

             InventoryEntities IE = new InventoryEntities();
             var mapSystem = IE.MAPInventories.Where(i => i.ID_CATEGORY == 1).ToList();


             return View(mapSystem);





         }


         public ActionResult IndexMapInventory(string id)
         {
             //0212D24B-8B9F-4544-89E3-0369ECF9B213
             //   string strauto = "0212D24B-8B9F-4544-89E3-0369ECF9B213";
             string markers = "[";

             string lat = "";
             string longi = "";
             string Constring = ConfigurationManager.ConnectionStrings["CoordinatesEntities"].ConnectionString;
             SqlCommand cmd = new SqlCommand("SELECT * FROM Inventory_view where idsystem = '" + id + "'");
             using (SqlConnection CON = new SqlConnection(Constring))
             {

                 cmd.Connection = CON;
                 CON.Open();
                 using (SqlDataReader SDR = cmd.ExecuteReader())
                 {
                     while (SDR.Read())
                     {

                         markers += "{";
                         markers += string.Format("'GeoLat': '{0}',", SDR["longitude"]);
                         markers += string.Format("'GeoLong': '{0}',", SDR["latitude"]);
                         markers += string.Format("'Systems': '{0}',", SDR["systems"]);
                         markers += string.Format("'Region': '{0}',", SDR["region"]);
                         markers += string.Format("'Province': '{0}',", SDR["Province"]);
                         markers += string.Format("'Municipality': '{0}',", SDR["municipality"]);
                         markers += string.Format("'SA': '{0}',", SDR["service_original"]);
                         markers += string.Format("'FUSA': '{0}',", SDR["SERVICE_FIRMED"]);

                         markers += "},";
                         lat = SDR["latitude"].ToString();
                         longi = SDR["longitude"].ToString();

                     }


                 }
                 CON.Close();



             }




             markers += "];";
             ViewBag.Markers = markers;

             
             //polygon

             //  string strauto = Session["regiontolog"].ToString();


             Session["iddam"] = id;
             Session["latitude"] = lat;
             Session["longitude"] = longi;
             string markersPICpoly = "[";
             string ConstringPICPOLY = ConfigurationManager.ConnectionStrings["CoordinatesEntities"].ConnectionString;
             Boolean tm = false;


             SqlCommand cmd1 = new SqlCommand("SELECT * FROM mappingdataVIEW where idsystem = '" + id + "'" 
              + " and drawingtype = 'POLYGON'");

             using (SqlConnection CON = new SqlConnection(ConstringPICPOLY))
             {

                 cmd1.Connection = CON;
                 CON.Open();
                 using (SqlDataReader SDR = cmd1.ExecuteReader())
                 {
                     while (SDR.Read())
                     {
                         tm = true;
                         markersPICpoly += "{";
                         markersPICpoly += string.Format("\"type\": \"{0}\"" + ",", SDR["DRAWINGTYPE"]);
                         markersPICpoly += string.Format("\"id\": \"{0}\"" + ",", SDR["id"]);
                         markersPICpoly += string.Format("\"POLYGONAREA\": \"{0}\"" + ",", SDR["POLYGONAREA"]);

                         if (SDR["DRAWINGTYPE"].ToString() == "POLYGON")
                         {
                             markersPICpoly += string.Format("\"geometry\":[" + "\"{0}\"" + "]", SDR["geometry"]);
                         }
                         else
                         {
                             markersPICpoly += string.Format("\"geometry\":" + "\"{0}\"" + "", SDR["geometry"]);
                         }

                         //markersPIC += string.Format("'Systems': '{0}',", SDR["systems"]);
                         //markersPIC += string.Format("'Municipality': '{0}',", SDR["municipality"]);
                         //markersPIC += string.Format("'SA': '{0}',", SDR["Service_original"]);
                         //markersPIC += string.Format("'FUSA': '{0}',", SDR["SERVICE_FIRMED"]);
                         //markersPIC += string.Format("'MAPLOC': '{0}',", SDR["MAPLOC"]);
                         //markersPIC += string.Format("'MAPSYSTEM': '{0}',", SDR["MAPSYSTEM"]);
                         //markersPIC += string.Format("'CATEGORY': '{0}',", SDR["ID_CATEGORY"]);

                         markersPICpoly += "},";


                     }


                 }
                 CON.Close();



             }

             if (tm == true)
             {
                 markersPICpoly += "]";
                 ViewBag.MarkersPICpoly = markersPICpoly.Remove(markersPICpoly.Length - 2, 1).Insert(markersPICpoly.Length - 2, "");
             }
             else
             {
                 markersPICpoly = "[]";
                 ViewBag.MarkersPICpoly = "[]";
             }













             return View();
         }


         public ActionResult IndexMapInventoryRegion()
         {
             //0212D24B-8B9F-4544-89E3-0369ECF9B213
             //   string strauto = "0212D24B-8B9F-4544-89E3-0369ECF9B213";
             string markers = "[";

             string lat = "";
             string longi = "";
             string Constring = ConfigurationManager.ConnectionStrings["CoordinatesEntities"].ConnectionString;
             SqlCommand cmd = new SqlCommand("SELECT * FROM CoordinatesPerSystem where res_region = 'REGION 2'");
             using (SqlConnection CON = new SqlConnection(Constring))
             {

                 cmd.Connection = CON;
                 CON.Open();
                 using (SqlDataReader SDR = cmd.ExecuteReader())
                 {
                     while (SDR.Read())
                     {

                         markers += "{";
                         markers += string.Format("'GeoLat': '{0}',", SDR["longi"]);
                         markers += string.Format("'GeoLong': '{0}',", SDR["lat"]);
                         markers += string.Format("'Systems': '{0}',", SDR["systems"]);
                         markers += string.Format("'Region': '{0}',", SDR["res_region"]);
                         markers += string.Format("'Province': '{0}',", SDR["Province"]);
                         markers += string.Format("'Municipality': '{0}',", SDR["municipality"]);
                         markers += string.Format("'SA': '{0}',", SDR["service_original"]);
                         markers += string.Format("'FUSA': '{0}',", SDR["SERVICE_FIRMED"]);

                         markers += "},";
                         lat = SDR["lat"].ToString();
                         longi = SDR["longi"].ToString();

                     }


                 }
                 CON.Close();



             }




             markers += "];";
             ViewBag.Markers = markers;


             //polygon

             //  string strauto = Session["regiontolog"].ToString();


             //Session["iddam"] = id;
             //Session["latitude"] = lat;
             //Session["longitude"] = longi;
             string markersPICpoly;
             //string ConstringPICPOLY = ConfigurationManager.ConnectionStrings["CoordinatesEntities"].ConnectionString;
             //Boolean tm = false;


             //SqlCommand cmd1 = new SqlCommand("SELECT * FROM mappingdataVIEW where idsystem = '" + id + "'"
             // + " and drawingtype = 'POLYGON'");

             //using (SqlConnection CON = new SqlConnection(ConstringPICPOLY))
             //{

             //    cmd1.Connection = CON;
             //    CON.Open();
             //    using (SqlDataReader SDR = cmd1.ExecuteReader())
             //    {
             //        while (SDR.Read())
             //        {
             //            tm = true;
             //            markersPICpoly += "{";
             //            markersPICpoly += string.Format("\"type\": \"{0}\"" + ",", SDR["DRAWINGTYPE"]);
             //            markersPICpoly += string.Format("\"id\": \"{0}\"" + ",", SDR["id"]);
             //            markersPICpoly += string.Format("\"POLYGONAREA\": \"{0}\"" + ",", SDR["POLYGONAREA"]);

             //            if (SDR["DRAWINGTYPE"].ToString() == "POLYGON")
             //            {
             //                markersPICpoly += string.Format("\"geometry\":[" + "\"{0}\"" + "]", SDR["geometry"]);
             //            }
             //            else
             //            {
             //                markersPICpoly += string.Format("\"geometry\":" + "\"{0}\"" + "", SDR["geometry"]);
             //            }

             //            //markersPIC += string.Format("'Systems': '{0}',", SDR["systems"]);
             //            //markersPIC += string.Format("'Municipality': '{0}',", SDR["municipality"]);
             //            //markersPIC += string.Format("'SA': '{0}',", SDR["Service_original"]);
             //            //markersPIC += string.Format("'FUSA': '{0}',", SDR["SERVICE_FIRMED"]);
             //            //markersPIC += string.Format("'MAPLOC': '{0}',", SDR["MAPLOC"]);
             //            //markersPIC += string.Format("'MAPSYSTEM': '{0}',", SDR["MAPSYSTEM"]);
             //            //markersPIC += string.Format("'CATEGORY': '{0}',", SDR["ID_CATEGORY"]);

             //            markersPICpoly += "},";


             //        }


             //    }
             //    CON.Close();



             //}

             //if (tm == true)
             //{
             //    markersPICpoly += "]";
             //    ViewBag.MarkersPICpoly = markersPICpoly.Remove(markersPICpoly.Length - 2, 1).Insert(markersPICpoly.Length - 2, "");
             //}
             //else
          //   {
                 markersPICpoly = "[]";
                 ViewBag.MarkersPICpoly = "[]";
         //    }













             return View();
         }


         public ActionResult Autocomplete(string term)
         {

             var accomplishments = db.MappingDataviews
                   .Where(r => r.SYSTEMS.Contains(term))
                   .Select(r => new { label = r.SYSTEMS } )
                   .Distinct()
                   .Take(5);

             return Json(accomplishments, JsonRequestBehavior.AllowGet);
         }


     
         public ActionResult IndexMapInventoryNationwide(string searchTerm = null)
         {
             //0212D24B-8B9F-4544-89E3-0369ECF9B213
             //   string strauto = "0212D24B-8B9F-4544-89E3-0369ECF9B213";
        
             //polygon

             //  string strauto = Session["regiontolog"].ToString();
            
             string markersPICpoly = "[";
             string ConstringPICPOLY = ConfigurationManager.ConnectionStrings["CoordinatesEntitiesPromotional1"].ConnectionString;
             Boolean tm = false;


             SqlCommand cmd1 = new SqlCommand("SELECT * FROM mappingdataVIEW where (systems  like '%" + searchTerm + "%' or systems = null)");
             using (SqlConnection CON = new SqlConnection(ConstringPICPOLY))
             {

                 cmd1.Connection = CON;
                 CON.Open();
                 using (SqlDataReader SDR = cmd1.ExecuteReader())
                 {
                     while (SDR.Read())
                     {
                         tm = true;
                         markersPICpoly += "{";
                         markersPICpoly += string.Format("\"type\": \"{0}\"" + ",", SDR["DRAWINGTYPE"]);
                         markersPICpoly += string.Format("\"id\": \"{0}\"" + ",", SDR["id"]);
                         markersPICpoly += string.Format("\"POLYGONAREA\": \"{0}\"" + ",", SDR["POLYGONAREA"]);
                         markersPICpoly += string.Format("\"Systems\": \"{0}\"" + ",", SDR["systems"]);
                         markersPICpoly += string.Format("\"Municipality\": \"{0}\"" + ",", SDR["municipality"]);
                         markersPICpoly += string.Format("\"SA\": \"{0}\"" + ",", SDR["Service_original"]);
                         markersPICpoly += string.Format("\"FUSA\": \"{0}\"" + ",", SDR["SERVICE_FIRMED"]);


                         if (SDR["DRAWINGTYPE"].ToString() == "POLYGON")
                         {
                             markersPICpoly += string.Format("\"geometry\":[" + "\"{0}\"" + "]", SDR["geometry"]);
                         }
                         else
                         {
                             markersPICpoly += string.Format("\"geometry\":" + "\"{0}\"" + "", SDR["geometry"]);
                         }

                        
                       
                         markersPICpoly += "},";


                     }


                 }
                 CON.Close();



             }

             if (tm == true)
             {
                 markersPICpoly += "]";
                 ViewBag.MarkersPICpoly = markersPICpoly.Remove(markersPICpoly.Length - 2, 1).Insert(markersPICpoly.Length - 2, "");
             }
             else
             {
                 markersPICpoly = "[]";
                 ViewBag.MarkersPICpoly = "[]";
             }










           //  return PartialView("_IndexMapInventoryNationwide");


             return View();
         }

         public ActionResult IndexMapInventoryNationwidePromotional(string searchTerm = null)
         {
             //0212D24B-8B9F-4544-89E3-0369ECF9B213
             //   string strauto = "0212D24B-8B9F-4544-89E3-0369ECF9B213";

             //polygon

             //  string strauto = Session["regiontolog"].ToString();

             string markersPICpoly = "[";
             string ConstringPICPOLY = ConfigurationManager.ConnectionStrings["CoordinatesEntitiesPromotional"].ConnectionString;
             Boolean tm = false;


             SqlCommand cmd1 = new SqlCommand("SELECT * FROM mappingdataVIEW where (systems  like '%" + searchTerm + "%' or systems = null)");
             using (SqlConnection CON = new SqlConnection(ConstringPICPOLY))
             {

                 cmd1.Connection = CON;
                 CON.Open();
                 using (SqlDataReader SDR = cmd1.ExecuteReader())
                 {
                     while (SDR.Read())
                     {
                         tm = true;
                         markersPICpoly += "{";
                         markersPICpoly += string.Format("\"type\": \"{0}\"" + ",", SDR["DRAWINGTYPE"]);
                         markersPICpoly += string.Format("\"id\": \"{0}\"" + ",", SDR["id"]);
                         markersPICpoly += string.Format("\"POLYGONAREA\": \"{0}\"" + ",", SDR["POLYGONAREA"]);
                         markersPICpoly += string.Format("\"Systems\": \"{0}\"" + ",", SDR["systems"]);
                         markersPICpoly += string.Format("\"Municipality\": \"{0}\"" + ",", SDR["municipality"]);
                         markersPICpoly += string.Format("\"SA\": \"{0}\"" + ",", SDR["Service_original"]);
                         markersPICpoly += string.Format("\"FUSA\": \"{0}\"" + ",", SDR["SERVICE_FIRMED"]);


                         if (SDR["DRAWINGTYPE"].ToString() == "POLYGON")
                         {
                             markersPICpoly += string.Format("\"geometry\":[" + "\"{0}\"" + "]", SDR["geometry"]);
                         }
                         else
                         {
                             markersPICpoly += string.Format("\"geometry\":" + "\"{0}\"" + "", SDR["geometry"]);
                         }



                         markersPICpoly += "},";


                     }


                 }
                 CON.Close();



             }

             if (tm == true)
             {
                 markersPICpoly += "]";
                 ViewBag.MarkersPICpoly = markersPICpoly.Remove(markersPICpoly.Length - 2, 1).Insert(markersPICpoly.Length - 2, "");
             }
             else
             {
                 markersPICpoly = "[]";
                 ViewBag.MarkersPICpoly = "[]";
             }










             //  return PartialView("_IndexMapInventoryNationwide");


             return View();
         }

        public ActionResult IndexPerProj(int id)
        {
            string strauto = Session["regiontolog"].ToString();
            string markers = "[";
            string Constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SELECT * FROM projectcoordinatesview where idcoordinates = " + id);
            using (SqlConnection CON = new SqlConnection(Constring))
            {

                cmd.Connection = CON;
                CON.Open();
                using (SqlDataReader SDR = cmd.ExecuteReader())
                {
                    while (SDR.Read())
                    {

                        markers += "{";
                        markers += string.Format("'GeoLat': '{0}',", SDR["longi"]);
                        markers += string.Format("'GeoLong': '{0}',", SDR["lati"]);
                        markers += string.Format("'Systems': '{0}',", SDR["subproject"]);
                        markers += string.Format("'Description': '{0}',", SDR["description"]);

                        markers += "},";
                    }


                }
                CON.Close();



            }




            markers += "];";
            ViewBag.Markers = markers;



            return View();
        }

        [AllowAnonymous]
        public ActionResult IndexAllProj()
        {
            string strauto = Session["regiontolog"].ToString();
            string markers = "[";
            string Constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SELECT * FROM projectcoordinatesview where lati is not null and subproject <> 'malibago-esperanza cis'");
            using (SqlConnection CON = new SqlConnection(Constring))
            {

                cmd.Connection = CON;
                CON.Open();
                using (SqlDataReader SDR = cmd.ExecuteReader())
                {
                    while (SDR.Read())
                    {

                        markers += "{";
                        markers += string.Format("'GeoLat': '{0}',", SDR["longi"]);
                        markers += string.Format("'GeoLong': '{0}',", SDR["lati"]);
                        markers += string.Format("'Systems': '{0}',", SDR["subproject"]);
                        markers += string.Format("'Description': '{0}',", SDR["description"]);

                        markers += "},";
                    }


                }
                CON.Close();



            }




            markers += "];";
            ViewBag.Markers = markers;



            return View();
        }


        public ActionResult IndexAllDIME()
        {
           

            string markers = "[";
            string Constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SELECT * FROM dimepicview");
            using (SqlConnection CON = new SqlConnection(Constring))
            {

                cmd.Connection = CON;
                CON.Open();
                using (SqlDataReader SDR = cmd.ExecuteReader())
                {
                    while (SDR.Read())
                    {

                        markers += "{";
                        markers += string.Format("'GeoLat': '{0}',", SDR["longitude"]);
                        markers += string.Format("'GeoLong': '{0}',", SDR["latitude"]);
                        markers += string.Format("'Systems': '{0}',", SDR["maploc"]);
                        markers += string.Format("'idaccomp': '{0}',", SDR["idaccomp"]);
                    //    markers += string.Format("'Description': '{0}',", SDR["description"]);

                        markers += "},";
                    }


                }
                CON.Close();



            }




            markers += "];";
            ViewBag.Markers = markers;




            return View();
        }



        public ActionResult IndexAlldam()
        {
            //string strauto = Session["regiontolog"].ToString();
            //string markers = "[";
            //string Constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //SqlCommand cmd = new SqlCommand("SELECT * FROM projectcoordinatesview where lati is not null and subproject <> 'malibago-esperanza cis'");
            //using (SqlConnection CON = new SqlConnection(Constring))
            //{

            //    cmd.Connection = CON;
            //    CON.Open();
            //    using (SqlDataReader SDR = cmd.ExecuteReader())
            //    {
            //        while (SDR.Read())
            //        {

            //            markers += "{";
            //            markers += string.Format("'GeoLat': '{0}',", SDR["longi"]);
            //            markers += string.Format("'GeoLong': '{0}',", SDR["lati"]);
            //            markers += string.Format("'Systems': '{0}',", SDR["subproject"]);
            //            markers += string.Format("'Description': '{0}',", SDR["description"]);

            //            markers += "},";
            //        }


            //    }
            //    CON.Close();



            //}




            //markers += "];";
            //ViewBag.Markers = markers;


            string markers = "[";
            string Constring = ConfigurationManager.ConnectionStrings["DamConnection"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SELECT * FROM GeneralInformation where latitude is not null");
            using (SqlConnection CON = new SqlConnection(Constring))
            {

                cmd.Connection = CON;
                CON.Open();
                using (SqlDataReader SDR = cmd.ExecuteReader())
                {
                    while (SDR.Read())
                    {

                        markers += "{";
                        markers += string.Format("'GeoLat': '{0}',", SDR["longitude"]);
                        markers += string.Format("'GeoLong': '{0}',", SDR["latitude"]);
                        markers += string.Format("'Systems': '{0}',", SDR["DamName"]);
                        //    markers += string.Format("'Description': '{0}',", SDR["description"]);

                        markers += "},";
                    }


                }
                CON.Close();



            }




            markers += "];";
            ViewBag.Markers = markers;




            return View();
        }



        public ActionResult IndexUsers()
        {
          





            string markers = "[";
            string Constring = ConfigurationManager.ConnectionStrings["AccomplishmentEntitiesSecurity"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SELECT * FROM maplocuser where status = 'Online'");
            using (SqlConnection CON = new SqlConnection(Constring))
            {

                cmd.Connection = CON;
                CON.Open();
                using (SqlDataReader SDR = cmd.ExecuteReader())
                {
                    while (SDR.Read())
                    {

                        markers += "{";
                        markers += string.Format("'GeoLat': '{0}',", SDR["longi"]);
                        markers += string.Format("'GeoLong': '{0}',", SDR["lati"]);
                        markers += string.Format("'Username': '{0}',", SDR["username"]);
                        //    markers += string.Format("'Description': '{0}',", SDR["description"]);

                        markers += "},";
                    }


                }
                CON.Close();



            }




            markers += "];";
            ViewBag.Markers = markers;




            return View();
        }
      









        public ActionResult Gallery(string id)
        {
            List<MAPPIC> all = new List<MAPPIC>();
            Session["myidsys"] = id;
            // Here MyDatabaseEntities is our datacontext

            all = db.MAPPICs.Where(r => r.IDsystem == id).ToList();

            return View(all);





        }

        public ActionResult Upload()
        {

            return View();



        }
        [HttpPost]
        public ActionResult Upload(MAPPIC fileModel)
        {
            FileUploadService service = new FileUploadService();
          
            var id = Session["myidsys"].ToString();
            foreach (var item in fileModel.File)
            {

                service.SaveFileDetails(item, id);
            }
            return RedirectToAction("Gallery", new { id = id });
        }


        //[HttpPost]
        //public ActionResult Upload(MAPPIC IG)
        //{
        //    // Apply Validation Here


        //    if (IG.File.ContentLength > (2 * 1024 * 1024))
        //    {
        //        ModelState.AddModelError("CustomError", "File size must be less than 2 MB");
        //        return View();
        //    }
        //    if (!(IG.File.ContentType == "image/jpeg" || IG.File.ContentType == "image/gif"))
        //    {
        //        ModelState.AddModelError("CustomError", "File type allowed : jpeg and gif");
        //        return View();
        //    }

        //    IG.MAPLOC = IG.File.FileName;
        //    IG.ImageSize = IG.File.ContentLength;

        //    byte[] data = new byte[IG.File.ContentLength];
        //    IG.File.InputStream.Read(data, 0, IG.File.ContentLength);

        //    IG.MAPsystem = data;

        //        db.MAPPICs.Add(IG);
        //        db.SaveChanges();

        //    return RedirectToAction("Gallery");
        //}

        public ActionResult ViewImages(string id)
        {

            Session["idaccomp"] = id;
            var asa = db.SYSTEMPICTURES
                    .Where(r => r.IDSystem == id);


            if (asa == null)
            {
                return HttpNotFound();
            }
            return View(asa);


        }

        public JsonResult GetAllLocation()
        {

            string str = Session["regiontolog"].ToString();

            using (InventoryEntities dc = new InventoryEntities())
            {

                var v = dc.CoordinatesPerSystems.OrderBy(a => a.SYSTEMS)
                    .Where(a => a.res_region == str)
                    .ToList();
                return new JsonResult { Data = v, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }

        }

        public JsonResult GetAllLocationMarker()
        {

            string str = Session["regiontolog"].ToString();

            using (InventoryEntities dc = new InventoryEntities())
            {

                var v = dc.CoordinatesPerSystems.OrderBy(a => a.SYSTEMS)
                    .Where(a => a.res_region == str)
                    .ToList();
                return new JsonResult { Data = v, JsonRequestBehavior = JsonRequestBehavior.AllowGet };





            }

        }

        public JsonResult GetMarkerInfo(int Row)
        {

            using (InventoryEntities dc = new InventoryEntities())
            {

                CoordinatesPerSystem l = null;
                l = dc.CoordinatesPerSystems.Where(a => a.Row.Equals(Row)).FirstOrDefault();
                //l = dc.CoordinatesPerSystems.Where(a => a.Row.Equals(Row)).FirstOrDefault();
                Session["idsys"] = l.IDSystem.ToString();
                ViewBag.myidDDD = l.IDSystem.ToString();
                return new JsonResult { Data = l, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


            }

        }
    }


}