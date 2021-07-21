using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AccompProject.Models;
using System.Configuration;
using System.Data.SqlClient;
using AccompProject.Services;

namespace AccompProject.Controllers
{
    // [Authorize]
    public class InventoryMappingController : Controller
    {
        private InventoryEntities db = new InventoryEntities();




        PolygonCode empDB = new PolygonCode();

        //    [Authorize]

        public JsonResult Add(Polygon emp)
        {

            return Json(empDB.Add(emp), JsonRequestBehavior.AllowGet);

        }

        public ActionResult IndexDelineate(string id = null, string lati = null, string longi = null)
        {
            //0212D24B-8B9F-4544-89E3-0369ECF9B213
            //   string strauto = "0212D24B-8B9F-4544-89E3-0369ECF9B213";


            Session["idsystem"] = id;

            string markers = "[";





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
                        //       lat = SDR["latitude"].ToString();
                        //     longi = SDR["longitude"].ToString();

                    }


                }
                CON.Close();



            }




            markers += "];";
            ViewBag.Markers = markers;


            //polygon

            //  string strauto = Session["regiontolog"].ToString();

            ViewBag.lati = lati;
            ViewBag.longi = longi;


            string Coordinates = "[";
            Coordinates += "{";
            Coordinates += string.Format("\"latitude\": \"{0}\"" + ",", lati);
            Coordinates += string.Format("\"longitude\": \"{0}\"" + ",", longi);
            Coordinates += "}]";
            ViewBag.Coordinates = Coordinates;




            Session["iddam"] = id;
            //    Session["latitude"] = lati;
            //  Session["longitude"] = longi;
            string markersPICpoly = "[";
            string ConstringPICPOLY = ConfigurationManager.ConnectionStrings["CoordinatesEntities"].ConnectionString;
            Boolean tm = false;


            SqlCommand cmd1 = new SqlCommand("SELECT * FROM mappingdataVIEW where idsystem = '" + id + "'");
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




            ViewBag.MarkersPICpoly = "[]";
            ViewBag.Markers = "[]";




            //new 


            var systemsprofile = db.MappingDataSystemsViews.Where(w => w.IDSystems == id).ToList();


            return PartialView("_IndexDelineate", systemsprofile);
            //    return View();
        }



        public ActionResult IndexEmployeeSMS()
        {


            //     var systemsprofile = db.EmployeeLocations.ToList();

            return View();
        }

        public ActionResult CovidSurveyDashboard()
        {


            //     var systemsprofile = db.EmployeeLocations.ToList();

            return View();
        }

        public ActionResult IndexEmployee()
        {


            //     var systemsprofile = db.EmployeeLocations.ToList();

            return View();
        }

        public ActionResult SMSMessage(int? id)
        {
            EmployeeLocation FD = db.EmployeeLocations.Find(id);


            if (FD == null)
            {
                return HttpNotFound();
            }




            return PartialView("_SMSMessage", FD);
        }

        public ActionResult LoadMessage()
        {
          
            string a = "1";
            var FD = db.emps.Where(r => r.stat == a).ToList();

            if (FD == null)
            {
                return HttpNotFound();
            }


    

             foreach (var item in FD)
        {

            Console.WriteLine(item.number);
            SMS sSMS = new SMS();

            sSMS.sendSMS(item.number, "hellow");
            System.Threading.Thread.Sleep(3000);
        }


            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SMSMessage(EmployeeLocation FODB)
        {


            if (ModelState.IsValid)
            {


                //  var msg = "";


                SMS sSMS = new SMS();

                sSMS.sendSMS(FODB.Cp, FODB.sms);














                string url = "Suceessful Message";

                return Json(new { success = true, url = url });
                //return Json(new { suceess = true });
            }

            return PartialView("_SMSMessage", FODB);
        }



        public ActionResult IndexEmployee1(string olddays = "", string searchterms = "")
        {
            ViewBag.MarkersPICpoly = "[]";
            ViewBag.Markers = "[]";
            ViewBag.Coordinates = "[]";
            //    ViewBag.city = "[]";
         
            ////original
            //var systemsprofile = db.EmployeeLocations.Where(r => (olddays == null || r.Address.Contains(olddays)) && r.lng != null).ToList();
            //ViewBag.cnter = db.EmployeeLocations.Where(r => (olddays == null || r.Address.Contains(olddays)) && r.lng != null).Count();

            var systemsprofile = db.EmployeeLocations.Where(r => (olddays == null || r.Address.Contains(olddays)) && r.lng == null).ToList();
            ViewBag.cnter = db.EmployeeLocations.Where(r => (olddays == null || r.Address.Contains(olddays)) && r.lng == null).Count();


            if (searchterms == "address")
            {

                systemsprofile = db.EmployeeLocations.Where(r => (olddays == null || r.Address.Contains(olddays)) && r.lng != null).ToList();
                ViewBag.cnter = db.EmployeeLocations.Where(r => (olddays == null || r.Address.Contains(olddays)) && r.lng != null).Count();

            }

            ViewBag.city = db.CityBoundaries.ToList();

            ViewBag.MM = db.CityBoundaries.Where(r => r.Cityname.Contains("Metro Manila")).FirstOrDefault();
            if (searchterms == "city")
            {

                systemsprofile = db.EmployeeLocations.Where(r => (olddays == null || r.City.Contains(olddays)) && r.lng != null).ToList();
                ViewBag.cnter = db.EmployeeLocations.Where(r => (olddays == null || r.City.Contains(olddays)) && r.lng != null).Count();
                ViewBag.city = db.CityBoundaries.Where(r => r.Cityname.Contains(olddays)).FirstOrDefault();

            }
            if (searchterms == "name")
            {

                systemsprofile = db.EmployeeLocations.Where(r => (olddays == null || r.EmployeeName.Contains(olddays)) && r.lng != null).ToList();
                ViewBag.cnter = db.EmployeeLocations.Where(r => (olddays == null || r.EmployeeName.Contains(olddays)) && r.lng != null).Count();


            }
            if (searchterms == "Top")
            {

                systemsprofile = db.EmployeeLocations.Where(r => r.SG >= 27 && r.lng != null).ToList();
                ViewBag.cnter = db.EmployeeLocations.Where(r => r.SG >= 27 && r.lng != null).Count();


            }
            if (searchterms == "Division")
            {

                systemsprofile = db.EmployeeLocations.Where(r => (r.SG >= 24 && r.SG <= 25) && r.lng != null).ToList();
                ViewBag.cnter = db.EmployeeLocations.Where(r => (r.SG >= 24 && r.SG <= 25) && r.lng != null).Count();


            }
            if (searchterms == "Department")
            {

                systemsprofile = db.EmployeeLocations.Where(r => (r.SG >= 26 && r.SG <= 26) && r.lng != null).ToList();
                ViewBag.cnter = db.EmployeeLocations.Where(r => (r.SG >= 26 && r.SG <= 26) && r.lng != null).Count();


            }

            if (searchterms == "Section")
            {

                systemsprofile = db.EmployeeLocations.Where(r => (r.SG >= 22 && r.SG <= 23) && r.lng != null).ToList();
                ViewBag.cnter = db.EmployeeLocations.Where(r => (r.SG >= 22 && r.SG <= 23) && r.lng != null).Count();


            }


            if (searchterms == "Rank")
            {

                systemsprofile = db.EmployeeLocations.Where(r => (r.SG >= 0 && r.SG <= 21) && r.lng != null).ToList();
                ViewBag.cnter = db.EmployeeLocations.Where(r => (r.SG >= 0 && r.SG <= 21) && r.lng != null).Count();


            }




            return PartialView("_IndexEmployee", systemsprofile);
            //    return View(systemsprofile);
        }
        public ActionResult IndexEmployee1SMS(string olddays = "", string searchterms = "")
        {
            ViewBag.MarkersPICpoly = "[]";
            ViewBag.Markers = "[]";
            ViewBag.Coordinates = "[]";
            //    ViewBag.city = "[]";

            //var systemsprofile = db.EmployeeSMSReceiveds.Where(r => r.smsNumber != null).ToList();
            //ViewBag.cnter = db.EmployeeSMSReceiveds.Where(r => r.smsNumber != null).Count();


            var systemsprofile = db.EmployeeSMSReceivedConsoes.Where(r => r.smsNumber != null).ToList();
            ViewBag.cnter = db.EmployeeSMSReceivedConsoes.Where(r => r.smsNumber != null).Count();


            ViewBag.city = db.CityBoundaries.ToList();
           



            return PartialView("_IndexEmployeeSMS", systemsprofile);
            //    return View(systemsprofile);
        }

        public ActionResult IndexEmployee2(string olddays = null, string searchterms = null)
        {
            ViewBag.MarkersPICpoly = "[]";
            ViewBag.Markers = "[]";
            ViewBag.Coordinates = "[]";




            string url = Url.Action("IndexEmployee1", "InventoryMapping", new { olddays = olddays, searchterms = searchterms });

            return Json(new { success = true, url = url });

            //    return View(systemsprofile);
        }



        public ActionResult UpdateCoord(string latitude, string longitude, string id)
        {


            string Constring = ConfigurationManager.ConnectionStrings["CoordinatesEntities"].ConnectionString;
            using (SqlConnection CON = new SqlConnection(Constring))
            {
                CON.Open();
                SqlCommand cmd = new SqlCommand("update systemsprofile set latitude = '" + latitude + "'" + ", longitude = '" + longitude + "'" + " where IDSystems = '" + id + "'", CON);


                cmd.ExecuteNonQuery();


            }

            string url = Url.Action("IndexDelineate", "InventoryMapping", new { id = id, lati = latitude, longi = longitude });

            return Json(new { success = true, url = url });


        }

        public ActionResult UpdateEmployeeCoord(string latitude = "", string longitude = "", int id = 0)
        {



            string Constring = ConfigurationManager.ConnectionStrings["CoordinatesEntities"].ConnectionString;
            using (SqlConnection CON = new SqlConnection(Constring))
            {
                CON.Open();
                SqlCommand cmd = new SqlCommand("update employeelocation set lat = '" + latitude + "'" + ", lng = '" + longitude + "'" + " where id = " + id, CON);


                cmd.ExecuteNonQuery();


            }

            //   string url = Url.Action("IndexDelineate", "InventoryMapping", new { id = id, lati = latitude, longi = longitude });

            return Json(new { success = true });


        }





        // GET: /InventoryMapping/
        public ActionResult Index(string region = "REGION 9")
        {
            Session["region"] = region;
            return View(db.SystemsProfiles.Where(r => r.Region == region).ToList());
        }





        // GET: /InventoryMapping/Details/5
        public ActionResult Map(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SystemsProfile systemsprofile = db.SystemsProfiles.Find(id);
            if (systemsprofile == null)
            {
                return HttpNotFound();
            }

            Session["idsystem"] = id;
            return View(systemsprofile);
        }

        // GET: /InventoryMapping/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /InventoryMapping/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SystemsProfile systemsprofile)
        {
            if (ModelState.IsValid)
            {


                var idsystem = Guid.NewGuid();


                var systems = new SystemsProfile()
                {

                    IDSystems = idsystem.ToString(),
                    Systems = systemsprofile.Systems,
                    Region = systemsprofile.Region

                };

                db.SystemsProfiles.Add(systems);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(systemsprofile);
        }

        // GET: /InventoryMapping/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SystemsProfile systemsprofile = db.SystemsProfiles.Find(id);
            if (systemsprofile == null)
            {
                return HttpNotFound();
            }
            return View(systemsprofile);
        }

        // POST: /InventoryMapping/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDSystems,Systems,Region")] SystemsProfile systemsprofile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(systemsprofile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(systemsprofile);
        }

        // GET: /InventoryMapping/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SystemsProfile systemsprofile = db.SystemsProfiles.Find(id);
            if (systemsprofile == null)
            {
                return HttpNotFound();
            }
            return View(systemsprofile);
        }

        // POST: /InventoryMapping/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SystemsProfile systemsprofile = db.SystemsProfiles.Find(id);
            db.SystemsProfiles.Remove(systemsprofile);
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
    }
}
