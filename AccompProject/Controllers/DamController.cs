using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using AccompProject.Models;
using PagedList;
using System.Net;
using System.Configuration;
using AccompProject.Services;
using System.Data.SqlClient;
using AccompProject.Models.DamModel;
using System.IO;
using System.Web;


namespace AccompProject.Controllers
{
    public class DamController : Controller
    {
        // GET: Dam
        private DamEntities db = new DamEntities();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());  

        public ActionResult Autocomplete(string term)
        {
            if (Session["regiontolog"] == null)
            {

                return RedirectToAction("LogIn", "Account", new { area = "" });

            }
            string strauto = Session["regiontolog"].ToString();

            var accomplishments = db.GeneralInformations
            .OrderBy(r => r.DamName)
             .Where(r => (term == null || r.DamName.Contains(term)) && r.Region == strauto)
             .Select(r => new
                  {
                      label = r.DamName
                  })
                  .Take(5);

            return Json(accomplishments, JsonRequestBehavior.AllowGet);

        }


        public ActionResult Index(string searchTerm = null, int page = 1)
        {

            if (Session["regiontolog"] == null)
            {

                return RedirectToAction("LogIn", "Account", new { area = "" });

            }
            string strauto = Session["regiontolog"].ToString();


            var dam = db.GeneralInformations
                 .OrderBy(r => r.DamName)
                 .Where(r => (searchTerm == null || r.DamName.StartsWith(searchTerm)) && r.Region == strauto)
                 .ToPagedList(page, 20);


            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListOfDam", dam);
            }


            return View(dam);
        }

        public ActionResult Create()
        {
            

            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Intake", Value = "0" });

            items.Add(new SelectListItem { Text = "Diversion", Value = "1" });

            items.Add(new SelectListItem { Text = "Reservoir", Value = "2" });

       //     items.Add(new SelectListItem { Text = "Spillway", Value = "3" });

//            ViewBag.TypeOfDam = items;

            ViewBag.TypeOfDam = new SelectList(items,"Value","Text");



            string Constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection CON = new SqlConnection(Constring))
            {
                CON.Open();
                SqlCommand cmd = new SqlCommand("select  Province from tblprovince" +
                                                " order by province ", CON);


                SqlDataReader reader;


                reader = cmd.ExecuteReader();


                List<SelectListItem> items1 = new List<SelectListItem>();

             
                while(reader.Read()){

                    items1.Add(new SelectListItem { Text = reader.GetString(0), Value = reader.GetString(0) });

                
                }







                ViewBag.Myprovince = new SelectList(items1, "Value", "Text");


                
            }

            //            ViewBag.TypeOfDam = items;






            return View();
        }


     

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GeneralInformation dm)
        {
            if (ModelState.IsValid)
            {

                var iddam = Guid.NewGuid();

                var GI = new GeneralInformation()
                {
                    IDDam = iddam.ToString(),
                    DamName = dm.DamName,
                    TypeDam = dm.TypeDam,
                    Location = dm.Location,
                    Longitude = dm.Longitude,
                    Latitude = dm.Latitude,
                    CompletionDate = dm.CompletionDate,
                    ConstructionCost = dm.ConstructionCost,
                    WaterSource = dm.WaterSource,
                    Region = Session["regiontolog"].ToString(),
                    Province = dm.Province,
                    Municipality = dm.Municipality,
                    SA = dm.SA
                   
                };

                db.GeneralInformations.Add(GI);
                db.SaveChanges();

                //var HD = new HydrologicalData()
                //{
                //    IDDam = iddam.ToString(),
                //    DrainageArea = dm.DrainageArea,
                //    InflowDesignFlood = dm.InflowDesignFlood,
                //    FloodFrequency = dm.FloodFrequency,
                //    AveAnnualRunoff = dm.AveAnnualRunoff

                //};

                //db.HydrologicalDatas.Add(HD);
                //db.SaveChanges();


                //var td = new TypeDam() {

                //    IDDam = iddam.ToString(),
                //    DamType = dm.DamType,
                //    HeightStreambed = dm.HeightStreambed,
                //    CrestElevation = dm.CrestElevation,
                //    CrestWidth = dm.CrestWidth,
                //    CrestLength = dm.CrestLength,
                //    Embankment = dm.Embankment
                
                //};

                //db.TypeDams.Add(td);
                //db.SaveChanges();

                //var typediv = new TypeDiversion() {

                //    IDDam = iddam.ToString(),
                //    DiversionType = dm.DiversionType,
                //    NumberAndSize = dm.NumberAndSize,
                //    Control = dm.Control,
                //    DesignCapacity = dm.DesignCapacity

                
                //};
                //db.TypeDiversions.Add(typediv);
                //db.SaveChanges();


                //var typeresarea = new TypeReservoirArea() { 
                
                //    IDDam = iddam.ToString(),
                //    TopSediment = dm.TopSediment,
                //    TopConservation = dm.TopConservation,
                //    MaximumRes = dm.MaximumRes

                
                //};

                //db.TypeReservoirAreas.Add(typeresarea);
                //db.SaveChanges();


                //var typerescap = new TypeReservoirCapacity() { 
                
                //    IDDam = iddam.ToString(),
                //    InactiveStorage = dm.InactiveStorage,
                //    ConservationStorage = dm.ConservationStorage,
                //    SurchargeStorage = dm.SurchargeStorage
                
                //};

                //db.TypeReservoirCapacities.Add(typerescap);
                //db.SaveChanges();


                //var typeresele = new TypeReservoirElevation() { 
                    
                //    IDDam = iddam.ToString(),
                //    Maximum = dm.Maximum,
                //    Normal = dm.Normal,
                //    Minimum = dm.Minimum
                
                
                //};

                //db.TypeReservoirElevations.Add(typeresele);
                //db.SaveChanges();



                //var spillway = new TypeSpillway() { 
                //    IDDam = iddam.ToString(),
                //    CrestWidth = dm.CrestWidth_spillway,
                //    CrestElevation = dm.CrestElevation_spillway,
                //    DesignCapacity = dm.DesignCapacity_spillway,
                //    SpillwayType = dm.SpillwayType,
                //    TypeDissipitator = dm.TypeDissipitator

                
                //};

                //db.TypeSpillways.Add(spillway);
                //db.SaveChanges();

             return RedirectToAction("Index");
           

            }

            return View(dm);
        }


        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneralInformation DAM = db.GeneralInformations.Find(id);
            if (DAM == null)
            {
                return HttpNotFound();
            }


            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Intake", Value = "0" });

            items.Add(new SelectListItem { Text = "Diversion", Value = "1" });

            items.Add(new SelectListItem { Text = "Reservoir", Value = "2" });


            //            ViewBag.TypeOfDam = items;

            ViewBag.TypeOfDam = new SelectList(items, "Value", "Text",DAM.TypeDam);




            string Constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection CON = new SqlConnection(Constring))
            {
                CON.Open();
                SqlCommand cmd = new SqlCommand("select  Province from tblprovince" +
                                                " order by province ", CON);


                SqlDataReader reader;


                reader = cmd.ExecuteReader();


                List<SelectListItem> items1 = new List<SelectListItem>();


                while (reader.Read())
                {

                    items1.Add(new SelectListItem { Text = reader.GetString(0), Value = reader.GetString(0) });


                }







                ViewBag.Myprovince = new SelectList(items1, "Value", "Text",DAM.Province);



            }












            return View(DAM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GeneralInformation dm)
        {


            if (ModelState.IsValid)
            {

              
                var GI = new GeneralInformation()
                {
                    IDDam = dm.IDDam,
                    DamName = dm.DamName,
                    TypeDam = dm.TypeDam,
                    Location = dm.Location,
                    Longitude = dm.Longitude,
                    Latitude = dm.Latitude,
                    CompletionDate = dm.CompletionDate,
                    ConstructionCost = dm.ConstructionCost,
                    WaterSource = dm.WaterSource,
                    Region = Session["regiontolog"].ToString(),
                    Province = dm.Province,
                    Municipality = dm.Municipality,
                    Description = dm.Description,
                    SA = dm.SA

                };

                db.Entry(GI).State = EntityState.Modified;
                db.SaveChanges();

                //var HD = new HydrologicalData()
                //{
                //    IDDam = dm.IDDam,
                //    IDHydro = Convert.ToInt32(dm.IDHydro),
                //    DrainageArea = dm.DrainageArea,
                //    InflowDesignFlood = dm.InflowDesignFlood,
                //    FloodFrequency = dm.FloodFrequency,
                //    AveAnnualRunoff = dm.AveAnnualRunoff

                //};

                //db.Entry(HD).State = EntityState.Modified;
                //db.SaveChanges();


                //var td = new TypeDam()
                //{

                //    IDDam = dm.IDDam,
                //    IDDamType = Convert.ToInt32(dm.IDDamType),
                //    DamType = dm.DamType,
                //    HeightStreambed = dm.HeightStreambed,
                //    CrestElevation = dm.CrestElevation,
                //    CrestWidth = dm.CrestWidth,
                //    CrestLength = dm.CrestLength,
                //    Embankment = dm.Embankment

                //};

                //db.Entry(td).State = EntityState.Modified;
                //db.SaveChanges();

                //var typediv = new TypeDiversion()
                //{

                //    IDDam = dm.IDDam,
                //    IDDiversion = Convert.ToInt32(dm.IDDiversion),
                //    DiversionType = dm.DiversionType,
                //    NumberAndSize = dm.NumberAndSize,
                //    Control = dm.Control,
                //    DesignCapacity = dm.DesignCapacity


                //};
                //db.Entry(typediv).State = EntityState.Modified;
                //db.SaveChanges();


                //var typeresarea = new TypeReservoirArea()
                //{

                //    IDDam = dm.IDDam,
                //    IDReservoirArea = Convert.ToInt32(dm.IDReservoirArea),
                //    TopSediment = dm.TopSediment,
                //    TopConservation = dm.TopConservation,
                //    MaximumRes = dm.MaximumRes


                //};

                //db.Entry(typeresarea).State = EntityState.Modified;
                //db.SaveChanges();


                //var typerescap = new TypeReservoirCapacity()
                //{

                //    IDDam = dm.IDDam,
                //    IDReservoirCapacity = Convert.ToInt32(dm.IDReservoirCapacity),
                //    InactiveStorage = dm.InactiveStorage,
                //    ConservationStorage = dm.ConservationStorage,
                //    SurchargeStorage = dm.SurchargeStorage

                //};

                //db.Entry(typerescap).State = EntityState.Modified;
                //db.SaveChanges();


                //var typeresele = new TypeReservoirElevation()
                //{

                //    IDDam = dm.IDDam,
                //    IDReservoirElevation = Convert.ToInt32(dm.IDReservoirElevation),
                //    Maximum = dm.Maximum,
                //    Normal = dm.Normal,
                //    Minimum = dm.Minimum


                //};

                //db.Entry(typeresele).State = EntityState.Modified;
                //db.SaveChanges();



                //var spillway = new TypeSpillway()
                //{
                //    IDDam = dm.IDDam,
                //    IDSpillway = Convert.ToInt32(dm.IDSpillway),
                //    CrestWidth = dm.CrestWidth_spillway,
                //    CrestElevation = dm.CrestElevation_spillway,
                //    DesignCapacity = dm.DesignCapacity_spillway,
                //    SpillwayType = dm.SpillwayType,
                //    TypeDissipitator = dm.TypeDissipitator


                //};

                //db.Entry(spillway).State = EntityState.Modified;
                //db.SaveChanges();

                return RedirectToAction("Index");


            }



            return View(dm);

        }




        // GET: dam/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneralInformation dm = db.GeneralInformations.Find(id);
            if (dm == null)
            {
                return HttpNotFound();
            }
            return View(dm);
        }

        // POST: dam/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {

            GeneralInformation GI = db.GeneralInformations.Find(id);
            db.GeneralInformations.Remove(GI);
            db.SaveChanges();


            string Constring = ConfigurationManager.ConnectionStrings["DamConnection"].ConnectionString;
            using (SqlConnection CON = new SqlConnection(Constring))
            {
                CON.Open();
                SqlCommand cmd = new SqlCommand("delete from Appurtenance where iddam = '" + id + "'", CON);
                cmd.ExecuteNonQuery();

                // return Json(new { success = true });

            }


            using (SqlConnection CON = new SqlConnection(Constring))
            {
                CON.Open();
                SqlCommand cmd = new SqlCommand("delete from Catchment where iddam = '" + id + "'", CON);
                cmd.ExecuteNonQuery();

                // return Json(new { success = true });

            }


            using (SqlConnection CON = new SqlConnection(Constring))
            {
                CON.Open();
                SqlCommand cmd = new SqlCommand("delete from DamBody where iddam = '" + id + "'", CON);
                cmd.ExecuteNonQuery();

                // return Json(new { success = true });

            }



            using (SqlConnection CON = new SqlConnection(Constring))
            {
                CON.Open();
                SqlCommand cmd = new SqlCommand("delete from InspectionReports where iddam = '" + id + "'", CON);
                cmd.ExecuteNonQuery();

                // return Json(new { success = true });

            }


            using (SqlConnection CON = new SqlConnection(Constring))
            {
                CON.Open();
                SqlCommand cmd = new SqlCommand("delete from Rehabilitation where iddam = '" + id + "'", CON);
                cmd.ExecuteNonQuery();

                // return Json(new { success = true });

            }
                         
            return RedirectToAction("Index");

        }

    
    
    
    
    
    //Dam Informations


        public ActionResult IndexInformation(string searchTerm)
        {

        
            
            if (searchTerm == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            GeneralInformation financ = db.GeneralInformations.Find(searchTerm);
            Session["iddam"] = searchTerm;
            
            if (financ == null)
            {
                return HttpNotFound();
            }
            return View(financ);



        }

        public ActionResult IndexDamInformation(string searchTerm)
        {

            if (Session["regiontolog"] == null)
            {

                return RedirectToAction("LogIn", "Account", new { area = "" });

            }
            string strauto = Session["regiontolog"].ToString();




          
     //       Session["idaccomp"] = searchTerm;

            //var dam = db.DamInforamtions
            //    .Where(a => a.IDDam == searchTerm);

            var physicalaccomp = db.DamInforamtions
              .OrderByDescending(a => a.YearCovered)
              .Where(a => a.IDDam == searchTerm);


            if (physicalaccomp == null)
            {
                return HttpNotFound();
            }


            return PartialView("_IndexDamInformation", physicalaccomp);
          













        }

        public ActionResult CreateInformation(string Id)
        {




            List<SelectListItem> itemyear = new List<SelectListItem>();

            for (int yr = DateTime.Now.Year - 3; yr <= DateTime.Now.Year + 3; yr++) {

                itemyear.Add(new SelectListItem { Text = yr.ToString(), Value = yr.ToString() });

            
            }



            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "", Value = "" });
            items.Add(new SelectListItem { Text = "Project", Value = "Project" });

            items.Add(new SelectListItem { Text = "Completed", Value = "Completed" });
            //            ViewBag.TypeOfDam = items;

            ViewBag.stats = new SelectList(items, "Value", "Text");

            
            //            ViewBag.TypeOfDam = items;

            ViewBag.Myyear = new SelectList(itemyear, "Value", "Text");

            DamInforamtion Fview = new DamInforamtion();
            Fview.IDDam = Id;


            return PartialView("_CreateInformation", Fview);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateInformation(DamInforamtion FD)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var PA = new Appurtenance()
                    {
                        IDDam = FD.IDDam,
                        SpillwayDimension = FD.SpillwayDimension,
                        SpillwayIDF = FD.SpillwayIDF,
                        SpillwayType = FD.SpillwayType,
                        OutletDimension = FD.OutletDimension,
                        OutletIDF = FD.OutletIDF,
                        OutletType = FD.OutletType,
                        YearCovered = FD.YearCovered,
                        SpillwayCapacity = FD.SpillwayCapacity,
                        SpillwayCrestlength = FD.SpillwayCrestlength,
                        SpillwayCrestLevel = FD.SpillwayCrestLevel,
                        OutletConduitLength = FD.OutletConduitLength,
                        OutletConduitSize = FD.OutletConduitSize,
                        OutletDesignDischarge = FD.OutletDesignDischarge,
                        DiversionType = FD.DiversionType,
                        DiversionConduitDiameter = FD.DiversionConduitDiameter,
                        DiversionConduitLength = FD.DiversionConduitLength,
                        DiversionDesignCapacity = FD.DiversionDesignCapacity,
                        status= FD.status
                    

                    };

                    db.Appurtenances.Add(PA);
                    db.SaveChanges();

                    var PAA = new Catchment()
                    {
                       IDDAm = FD.IDDam,
                       SedimentArea = FD.SedimentArea,
                       SedimentVolume = FD.SedimentVolume,
                       SurfaceArea = FD.SurfaceArea,
                       CatchmentArea = FD.CatchmentArea,
                       Cover = FD.Cover,
                       LandUse = FD.LandUse,
                       YearCovered = FD.YearCovered


                    };

                    db.Catchments.Add(PAA);
                    db.SaveChanges();

                    var PAAA = new DamBody()
                    {
                        IDDam = FD.IDDam,
                        Structural = FD.Structural,
                        CrestLength  = FD.CrestLength,
                        RiverbedElevation = FD.RiverbedElevation,
                        CrestWidth = FD.CrestWidth,
                        ReservoirCapacity = FD.ReservoirCapacity,
                        SlopeUpstream = FD.SlopeUpstream,
                        SlopeDownstream = FD.SlopeDownstream,
                        DamSize = FD.DamSize,
                        DamHazardRisk = FD.DamHazardRisk,
                        YearCovered = FD.YearCovered,
                        ElevationLowestStreambed = FD.ElevationLowestStreambed,
                        HeightLowestStreambed = FD.HeightLowestStreambed,
                        DamCrest = FD.DamCrest,
                        NormalReservoir = FD.NormalReservoir,
                        TopInactive = FD.TopInactive,
                        MaximumReservoir = FD.MaximumReservoir

                    };

                    db.DamBodies.Add(PAAA);
                    db.SaveChanges();

                    string url = Url.Action("IndexDamInformation", "Dam", new { searchterm = FD.IDDam });

                    return Json(new { success = true, url = url });
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }


            return PartialView("_CreateInformation", FD);

        }

        public ActionResult EditDamInformation(int? id)
        {

            List<SelectListItem> items = new List<SelectListItem>();


        


   DamInforamtion DAM = db.DamInforamtions.Find(id);
   items.Add(new SelectListItem { Text = "", Value = "" });
   items.Add(new SelectListItem { Text = "Project", Value = "Project" });

   items.Add(new SelectListItem { Text = "Completed", Value = "Completed" });

          

            //            ViewBag.TypeOfDam = items;

            ViewBag.stats = new SelectList(items, "Value", "Text", DAM.status);


           
         
            
            if (DAM == null)
            {
                return HttpNotFound();
            }

         
            return PartialView("_EditDamInformation", DAM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDamInformation(DamInforamtion dm)
        {


            if (ModelState.IsValid)
            {


                var AP = new Appurtenance()
                {
                    IDDam = dm.IDDam,
                    IDAppurtenance = dm.IDAppurtenance,
                    SpillwayDimension = dm.SpillwayDimension,
                    SpillwayIDF = dm.SpillwayIDF,
                    SpillwayType = dm.SpillwayType,
                    OutletType = dm.OutletType,
                    OutletDimension = dm.OutletDimension,
                    OutletIDF = dm.OutletIDF,
                    YearCovered = dm.YearCovered,
                    SpillwayCapacity = dm.SpillwayCapacity,
                    SpillwayCrestlength = dm.SpillwayCrestlength,
                    SpillwayCrestLevel = dm.SpillwayCrestLevel,
                    OutletConduitLength = dm.OutletConduitLength,
                    OutletConduitSize = dm.OutletConduitSize,
                    OutletDesignDischarge = dm.OutletDesignDischarge,
                    DiversionType = dm.DiversionType,
                    DiversionConduitDiameter = dm.DiversionConduitDiameter,
                    DiversionConduitLength = dm.DiversionConduitLength,
                    DiversionDesignCapacity = dm.DiversionDesignCapacity,
                    status = dm.status

                };

                db.Entry(AP).State = EntityState.Modified;
                db.SaveChanges();

                var DB = new DamBody()
                {
                    IDDam = dm.IDDam,
                    IDDamBody = dm.IDDamBody,
                    Structural = dm.Structural,
                    CrestLength = dm.CrestLength,
                    RiverbedElevation = dm.RiverbedElevation,
                    CrestWidth = dm.CrestWidth,
                    ReservoirCapacity = dm.ReservoirCapacity,
                    SlopeDownstream = dm.SlopeDownstream,
                    SlopeUpstream = dm.SlopeUpstream,
                    DamSize = dm.DamSize,
                    DamHazardRisk = dm.DamHazardRisk,
                    YearCovered = dm.YearCovered,
                    ElevationLowestStreambed = dm.ElevationLowestStreambed,
                    HeightLowestStreambed = dm.HeightLowestStreambed,
                    DamCrest = dm.DamCrest,
                    NormalReservoir = dm.NormalReservoir,
                    TopInactive = dm.TopInactive,
                    MaximumReservoir = dm.MaximumReservoir
                    

                };

                db.Entry(DB).State = EntityState.Modified;
                db.SaveChanges();


                var CA = new Catchment()
                {

                    IDDAm = dm.IDDam,
                     IDCatachment = Convert.ToInt32(dm.IDCatachment),
                    SedimentArea = dm.SedimentArea,
                    SedimentVolume = dm.SedimentVolume,
                    SurfaceArea = dm.SurfaceArea,
                    CatchmentArea = dm.CatchmentArea,
                    Cover = dm.Cover,
                    LandUse = dm.LandUse,
                    YearCovered = dm.YearCovered

                };

                db.Entry(CA).State = EntityState.Modified;
                db.SaveChanges();

                //var typediv = new TypeDiversion()
                //{

                //    IDDam = dm.IDDam,
                //    IDDiversion = Convert.ToInt32(dm.IDDiversion),
                //    DiversionType = dm.DiversionType,
                //    NumberAndSize = dm.NumberAndSize,
                //    Control = dm.Control,
                //    DesignCapacity = dm.DesignCapacity


                //};
                //db.Entry(typediv).State = EntityState.Modified;
                //db.SaveChanges();


                //var typeresarea = new TypeReservoirArea()
                //{

                //    IDDam = dm.IDDam,
                //    IDReservoirArea = Convert.ToInt32(dm.IDReservoirArea),
                //    TopSediment = dm.TopSediment,
                //    TopConservation = dm.TopConservation,
                //    MaximumRes = dm.MaximumRes


                //};

                //db.Entry(typeresarea).State = EntityState.Modified;
                //db.SaveChanges();


                //var typerescap = new TypeReservoirCapacity()
                //{

                //    IDDam = dm.IDDam,
                //    IDReservoirCapacity = Convert.ToInt32(dm.IDReservoirCapacity),
                //    InactiveStorage = dm.InactiveStorage,
                //    ConservationStorage = dm.ConservationStorage,
                //    SurchargeStorage = dm.SurchargeStorage

                //};

                //db.Entry(typerescap).State = EntityState.Modified;
                //db.SaveChanges();


                //var typeresele = new TypeReservoirElevation()
                //{

                //    IDDam = dm.IDDam,
                //    IDReservoirElevation = Convert.ToInt32(dm.IDReservoirElevation),
                //    Maximum = dm.Maximum,
                //    Normal = dm.Normal,
                //    Minimum = dm.Minimum


                //};

                //db.Entry(typeresele).State = EntityState.Modified;
                //db.SaveChanges();



                //var spillway = new TypeSpillway()
                //{
                //    IDDam = dm.IDDam,
                //    IDSpillway = Convert.ToInt32(dm.IDSpillway),
                //    CrestWidth = dm.CrestWidth_spillway,
                //    CrestElevation = dm.CrestElevation_spillway,
                //    DesignCapacity = dm.DesignCapacity_spillway,
                //    SpillwayType = dm.SpillwayType,
                //    TypeDissipitator = dm.TypeDissipitator


                //};

                //db.Entry(spillway).State = EntityState.Modified;
                //db.SaveChanges();


                string url = Url.Action("IndexDaminformation", "Dam", new { searchTerm = dm.IDDam });

                return Json(new { success = true, url = url });
                //return Json(new { suceess = true });
            }

            return PartialView("_EditDamInformation", dm);

        }

        
        // GET: dam/Delete/5
        public ActionResult DeleteDamInformation(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DamInforamtion dm = db.DamInforamtions.Find(id);
            if (dm == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DeleteDamInformation", dm);
        }

        [HttpPost, ActionName("DeleteDamInformation")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDamInformation(DamInforamtion dm)
        {

            Appurtenance FD = db.Appurtenances.Find(dm.IDAppurtenance);
            db.Appurtenances.Remove(FD);
            db.SaveChanges();


            Catchment FDa = db.Catchments.Find(dm.IDCatachment);
            db.Catchments.Remove(FDa);
            db.SaveChanges();

            DamBody FDb = db.DamBodies.Find(dm.IDDamBody);
            db.DamBodies.Remove(FDb);
            db.SaveChanges();


            string url = Url.Action("IndexDaminformation", "Dam", new { searchTerm = dm.IDDam });

            return Json(new { success = true, url = url });
            //return Json(new { suceess = true });

        }



        //Rehabilitation





        public ActionResult IndexRehabInformation(string searchTerm)
        {



            if (searchTerm == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            GeneralInformation financ = db.GeneralInformations.Find(searchTerm);
            Session["iddam"] = searchTerm;

            if (financ == null)
            {
                return HttpNotFound();
            }
            return View(financ);



        }

        public ActionResult IndexRehabilitationInformation(string searchTerm)
        {

            if (Session["regiontolog"] == null)
            {

                return RedirectToAction("LogIn", "Account", new { area = "" });

            }
            string strauto = Session["regiontolog"].ToString();





            //       Session["idaccomp"] = searchTerm;

            //var dam = db.DamInforamtions
            //    .Where(a => a.IDDam == searchTerm);

            var physicalaccomp = db.DamRehabilitations
              .OrderByDescending(a => a.YearCovered)
              .Where(a => a.IDDam == searchTerm);


            if (physicalaccomp == null)
            {
                return HttpNotFound();
            }


            return PartialView("_IndexRehabilitationInformation", physicalaccomp);














        }




        public ActionResult CreateRehabilitationInformation(string Id)
        {


            
            DamRehabilitation Fview = new DamRehabilitation();
            Fview.IDDam = Id;


            return PartialView("_CreateRehabilitationInformation", Fview);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRehabilitationInformation(DamRehabilitation FD)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    DateTime date = Convert.ToDateTime(FD.Daterehab);

                    var PA = new Rehabilitation()
                    {
                        IDDam = FD.IDDam,
                        Daterehab = FD.Daterehab,
                        NatureOfRevision = FD.NatureOfRevision,
                        YearCovered = date.Year
                        


                    };

                    db.Rehabilitations.Add(PA);
                    db.SaveChanges();



                    string url = Url.Action("IndexRehabilitationInformation", "Dam", new { searchterm = FD.IDDam });

                    return Json(new { success = true, url = url });

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }


            return PartialView("_CreateInformation", FD);

        }




        public ActionResult EditRehabilitationInformation(int? id)
        {





            DamRehabilitation DAM = db.DamRehabilitations.Find(id);

            if (DAM == null)
            {
                return HttpNotFound();
            }


            return PartialView("_EditRehabilitationInformation", DAM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRehabilitationInformation(DamRehabilitation dm)
        {


            if (ModelState.IsValid)
            {

                DateTime date = Convert.ToDateTime(dm.Daterehab);

                var PA = new Rehabilitation()
                {
                    IDDam = dm.IDDam,
                    Daterehab = dm.Daterehab,
                    NatureOfRevision = dm.NatureOfRevision,
                    YearCovered = date.Year,
                    IDRehab = dm.IDRehab



                };

                db.Entry(PA).State = EntityState.Modified;
                db.SaveChanges();






                string url = Url.Action("IndexRehabilitationinformation", "Dam", new { searchTerm = dm.IDDam });

                return Json(new { success = true, url = url });
                //return Json(new { suceess = true });
            }

            return PartialView("_EditRehabilitationInformation", dm);

        }


        // GET: dam/Delete/5
        public ActionResult DeleteRehabilitationInformation(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DamRehabilitation dm = db.DamRehabilitations.Find(id);
            if (dm == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DeleteRehabilitationInformation", dm);
        }

        [HttpPost, ActionName("DeleteRehabilitationInformation")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRehabilitationInformation(DamRehabilitation dm)
        {

            Rehabilitation FD = db.Rehabilitations.Find(dm.IDRehab);
            db.Rehabilitations.Remove(FD);
            db.SaveChanges();


            //Catchment FDa = db.Catchments.Find(dm.IDCatachment);
            //db.Catchments.Remove(FDa);
            //db.SaveChanges();

            //DamBody FDb = db.DamBodies.Find(dm.IDDamBody);
            //db.DamBodies.Remove(FDb);
            //db.SaveChanges();


            string url = Url.Action("IndexRehabilitationinformation", "Dam", new { searchTerm = dm.IDDam });

            return Json(new { success = true, url = url });
            //return Json(new { suceess = true });

        }


    //inspection



        public ActionResult IndexInspectionInformation(string searchTerm)
        {



            if (searchTerm == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            GeneralInformation financ = db.GeneralInformations.Find(searchTerm);
            Session["iddam"] = searchTerm;

            if (financ == null)
            {
                return HttpNotFound();
            }
            return View(financ);



        }

        public ActionResult IndexDamInspectionInformation(string searchTerm)
        {

            if (Session["regiontolog"] == null)
            {

                return RedirectToAction("LogIn", "Account", new { area = "" });

            }
            string strauto = Session["regiontolog"].ToString();





            //       Session["idaccomp"] = searchTerm;

            //var dam = db.DamInforamtions
            //    .Where(a => a.IDDam == searchTerm);

            var physicalaccomp = db.DamInspections
              .OrderByDescending(a => a.YearCovered)
              .Where(a => a.IDDam == searchTerm);


            if (physicalaccomp == null)
            {
                return HttpNotFound();
            }


            return PartialView("_IndexDamInspectionInformation", physicalaccomp);














        }

        public ActionResult CreateInspectionInformation(string Id)
        {




            //List<SelectListItem> itemyear = new List<SelectListItem>();

            //for (int yr = DateTime.Now.Year - 3; yr <= DateTime.Now.Year + 3; yr++)
            //{

            //    itemyear.Add(new SelectListItem { Text = yr.ToString(), Value = yr.ToString() });


            //}


            //            ViewBag.TypeOfDam = items;

      //      ViewBag.Myyear = new SelectList(itemyear, "Value", "Text");

            DamInspection Fview = new DamInspection();
            Fview.IDDam = Id;


            return PartialView("_CreateInspectionInformation", Fview);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateInspectionInformation(DamInspection FD, HttpPostedFileBase upload)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    DateTime date = Convert.ToDateTime(FD.DamSafetyInspection);
                    var PA = new InspectionReport()
                    {


                        IDDAm = FD.IDDam,
                        DamSafetyInspection = FD.DamSafetyInspection,
                        TypeofInspection = FD.TypeofInspection,
                        Remarks = FD.Remarks,
                        YearCovered = date.Year


                    };


                    PA.Remarks = upload != null ? upload.FileName : "";

                    db.InspectionReports.Add(PA);
                    db.SaveChanges();

                    if (upload != null)
                    {

                        FileUploadServiceDam service = new FileUploadServiceDam();
                        service.SaveFileDetailsDAM(upload, FD.IDDam, "DAM", PA.IDInspection.ToString());
                    }


                


                    string url = Url.Action("IndexDamInspectionInformation", "Dam", new { searchterm = FD.IDDam });

                    return Json(new { success = true, url = url });

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }


            return PartialView("_CreateInspectionInformation ", FD);

        }

        public ActionResult EditInspectionInformation(int? id)
        {





            DamInspection DAM = db.DamInspections.Find(id);

            if (DAM == null)
            {
                return HttpNotFound();
            }


            return PartialView("_EditInspectionInformation", DAM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditInspectionInformation(DamInspection FD)
        {


            if (ModelState.IsValid)
            {

                DateTime date = Convert.ToDateTime(FD.DamSafetyInspection);
                   
                var AP = new InspectionReport()
                {
                    IDDAm = FD.IDDam,
                    DamSafetyInspection = FD.DamSafetyInspection,
                    TypeofInspection = FD.TypeofInspection,
                    Remarks = FD.Remarks,
                    YearCovered = date.Year,
                    IDInspection = FD.IDInspection

                };

                db.Entry(AP).State = EntityState.Modified;
                db.SaveChanges();



                string url = Url.Action("IndexDamInspectionInformation", "Dam", new { searchTerm = FD.IDDam });

                return Json(new { success = true, url = url });
                //return Json(new { suceess = true });
            }

            return PartialView("_EditInspectionInformation", FD);

        }


        // GET: dam/Delete/5
        public ActionResult DeleteInspectionInformation(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DamInspection dm = db.DamInspections.Find(id);
            if (dm == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DeleteInspectionInformation", dm);
        }

        [HttpPost, ActionName("DeleteInspectionInformation")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteInspectionInformation(DamInspection dm)
        {

            InspectionReport FD = db.InspectionReports.Find(dm.IDInspection);
            db.InspectionReports.Remove(FD);
            db.SaveChanges();




            string url = Url.Action("IndexDamInspectionInformation", "Dam", new { searchTerm = dm.IDDam });

            return Json(new { success = true, url = url });
            //return Json(new { suceess = true });

        }


        //Instrumentation

        public ActionResult IndexInstrumentation(string searchTerm)
        {



            if (searchTerm == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            GeneralInformation financ = db.GeneralInformations.Find(searchTerm);
            Session["iddam"] = searchTerm;

            if (financ == null)
            {
                return HttpNotFound();
            }
            return View(financ);



        }

        public ActionResult IndexDamInstrumentation(string searchTerm)
        {

            if (Session["regiontolog"] == null)
            {

                return RedirectToAction("LogIn", "Account", new { area = "" });

            }
            string strauto = Session["regiontolog"].ToString();





            //       Session["idaccomp"] = searchTerm;

            //var dam = db.DamInforamtions
            //    .Where(a => a.IDDam == searchTerm);

            var physicalaccomp = db.InstrumentDataViews
              .Where(a => a.IDDam == searchTerm);


            if (physicalaccomp == null)
            {
                return HttpNotFound();
            }


            return PartialView("_IndexDamInstrumentation", physicalaccomp);

            

        }


        public ActionResult CreateInstrumentation(string Id)
        {


            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Pneumatic Piezometer", Value = "Pneumatic Piezometer" });

            items.Add(new SelectListItem { Text = "Standpipe Diezometer", Value = "Standpipe Diezometer" });

            items.Add(new SelectListItem { Text = "Surface Monitoring Points (SMP)", Value = "Surface Monitoring Points (SMP)" });

            items.Add(new SelectListItem { Text = "V-NotchMeasuring Weir", Value = "V-NotchMeasuring Weir" });
            items.Add(new SelectListItem { Text = "Water Level Staff Gauge (Reservoir)", Value = "Water Level Staff Gauge (Reservoir)" });
            items.Add(new SelectListItem { Text = "Pendulum", Value = "Pendulum" });
            items.Add(new SelectListItem { Text = "Water Quality", Value = "Water Quality" });

            //            ViewBag.TypeOfDam = items;

            ViewBag.Instrument = new SelectList(items, "Value", "Text");

            InstrumentDataView Fview = new InstrumentDataView();
            Fview.IDDam = Id;


            return PartialView("_CreateInstrumentation", Fview);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateInstrumentation(InstrumentDataView FD)
        {

            if (ModelState.IsValid)
            {
                try
                {
                 //   DateTime date = Convert.ToDateTime(FD.DamSafetyInspection);
                    var PA = new InstrumentData()
                    {


                        IDDam = FD.IDDam,
                        ElevationTip = FD.ElevationTip,
                        ElevationLower = FD.ElevationLower,
                        ElevationUpper = FD.ElevationUpper,
                        Latitude = FD.Latitude,
                        Longitude = FD.Longitude,
                        Instrument = FD.Instrument,
                        WaterLocation = FD.WaterLocation


                    };

                    db.InstrumentDatas.Add(PA);
                    db.SaveChanges();



                    string url = Url.Action("IndexDamInstrumentation", "Dam", new { searchterm = FD.IDDam });

                    return Json(new { success = true, url = url });

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }


            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Pneumatic Piezometer", Value = "Pneumatic Piezometer" });

            items.Add(new SelectListItem { Text = "Standpipe Diezometer", Value = "Standpipe Diezometer" });

            items.Add(new SelectListItem { Text = "Surface Monitoring Points (SMP)", Value = "Surface Monitoring Points (SMP)" });

            items.Add(new SelectListItem { Text = "V-NotchMeasuring Weir", Value = "V-NotchMeasuring Weir" });
            items.Add(new SelectListItem { Text = "Water Level Staff Gauge (Reservoir)", Value = "Water Level Staff Gauge (Reservoir)" });
            items.Add(new SelectListItem { Text = "Pendulum", Value = "Pendulum" });
            items.Add(new SelectListItem { Text = "Water Quality", Value = "Water Quality" });

            //            ViewBag.TypeOfDam = items;

            ViewBag.Instrument = new SelectList(items, "Value", "Text");




            return PartialView("_CreateInstrumentation", FD);

        }


        public ActionResult EditInstrumentation(int? id)
        {

            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Pneumatic Piezometer", Value = "Pneumatic Piezometer" });

            items.Add(new SelectListItem { Text = "Standpipe Diezometer", Value = "Standpipe Diezometer" });

            items.Add(new SelectListItem { Text = "Surface Monitoring Points (SMP)", Value = "Surface Monitoring Points (SMP)" });

            items.Add(new SelectListItem { Text = "V-NotchMeasuring Weir", Value = "V-NotchMeasuring Weir" });
            items.Add(new SelectListItem { Text = "Water Level Staff Gauge (Reservoir)", Value = "Water Level Staff Gauge (Reservoir)" });
            items.Add(new SelectListItem { Text = "Pendulum", Value = "Pendulum" });
            items.Add(new SelectListItem { Text = "Water Quality", Value = "Water Quality" });

            //            ViewBag.TypeOfDam = items;


            InstrumentDataView DAM = db.InstrumentDataViews.Find(id);

            if (DAM == null)
            {
                return HttpNotFound();
            }


            ViewBag.Instruments = new SelectList(items, "Value", "Text",DAM.Instrument);

            return PartialView("_EditInstrumentation", DAM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditInstrumentation(InstrumentDataView FD)
        {


            if (ModelState.IsValid)
            {

             
                var AP = new InstrumentData()
                {
                    IDDam = FD.IDDam,
                    IDInstrument = FD.IDInstrument,
                    ElevationTip = FD.ElevationTip,
                    ElevationLower = FD.ElevationLower,
                    ElevationUpper = FD.ElevationUpper,
                    Latitude = FD.Latitude,
                    Longitude = FD.Longitude,
                    Instrument = FD.Instrument,
                    WaterLocation = FD.WaterLocation
                };

                db.Entry(AP).State = EntityState.Modified;
                db.SaveChanges();



                string url = Url.Action("IndexDamInstrumentation", "Dam", new { searchTerm = FD.IDDam });

                return Json(new { success = true, url = url });
                //return Json(new { suceess = true });
            }

            return PartialView("_EditInstrumentation", FD);

        }


        // GET: dam/Delete/5
        public ActionResult DeleteInstrumentation(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstrumentDataView dm = db.InstrumentDataViews.Find(id);
            if (dm == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DeleteInstrumentation", dm);
        }

        [HttpPost, ActionName("DeleteInstrumentation")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteInstrumentation(InstrumentDataView dm)
        {

            InstrumentData FD = db.InstrumentDatas.Find(dm.IDInstrument);
            db.InstrumentDatas.Remove(FD);
            db.SaveChanges();




            string url = Url.Action("IndexDamInstrumentation", "Dam", new { searchTerm = dm.IDDam });

            return Json(new { success = true, url = url });
            //return Json(new { suceess = true });

        }





        //DATA Reading

        public ActionResult IndexReadingInstrumentation(int? searchTerm)
        {



            if (searchTerm == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            InstrumentDataView financ = db.InstrumentDataViews.Find(searchTerm);
            Session["idinstrument"] = searchTerm;
            Session["instrum"] = financ.Instrument;


            if (financ == null)
            {
                return HttpNotFound();
            }
            return View(financ);



        }

        public ActionResult IndexReadingDamInstrumentation(int? searchTerm)
        {

            if (Session["regiontolog"] == null)
            {

                return RedirectToAction("LogIn", "Account", new { area = "" });

            }
            string strauto = Session["regiontolog"].ToString();

            



            //       Session["idaccomp"] = searchTerm;

            //var dam = db.DamInforamtions
            //    .Where(a => a.IDDam == searchTerm);

            var physicalaccomp = db.InstrumentReadingViews
              .Where(a => a.IDInstrument == searchTerm);

           

            if (physicalaccomp == null)
            {
                return HttpNotFound();
            }


            return PartialView("_IndexReadingDamInstrumentation", physicalaccomp.ToList());



        }

        public ActionResult CreateReadingInstrumentation(string Id)
        {


           InstrumentReadingView Fview = new InstrumentReadingView();
        //   Fview.Instrument = Id;


            return PartialView("_CreateReadingInstrumentation", Fview);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateReadingInstrumentation(InstrumentReadingView FD, HttpPostedFileBase upload)
        {

            if (ModelState.IsValid)
            {
                //try
                //{
                       DateTime date = Convert.ToDateTime(FD.DateRead);
                    var PA = new InstrumentReading()
                    {

                       // UploadedFile = upload.ContentType,
                      
                        IDInstrument = FD.IDInstrument,
                        DataRead = FD.DataRead,
                        DateRead = FD.DateRead,
                        TimeRead = FD.TimeRead,
                        DataNo = FD.DataNo,
                        DataX = FD.DataX,
                        DataY = FD.DataY,
                        YearCovered = date.Year

                      

                    };

                    if (upload != null) { 

                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        PA.filetype = 1;
                       PA.contenttype = System.IO.Path.GetFileName(upload.FileName);
                        PA.UploadedFile = reader.ReadBytes(upload.ContentLength);
                    }}

                    db.InstrumentReadings.Add(PA);
                    db.SaveChanges();



                    string url = Url.Action("IndexReadingDamInstrumentation", "Dam", new { searchterm = FD.IDInstrument });

                    return Json(new { success = true, url = url });
                  //  return RedirectToAction("IndexReadingInstrumentation", "Dam", new { searchterm = FD.IDInstrument });


                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex);
                //}
            }


            return PartialView("_CreateReadingInstrumentation", FD);

        }

        public ActionResult EditReadingInstrumentation(int? id)
        {



            InstrumentReadingView DAM = db.InstrumentReadingViews.Find(id);

            if (DAM == null)
            {
                return HttpNotFound();
            }



            return PartialView("_EditReadingInstrumentation", DAM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditReadingInstrumentation(InstrumentReadingView FD, HttpPostedFileBase upload)
        {


            if (ModelState.IsValid)
            {

                DateTime date = Convert.ToDateTime(FD.DateRead);

                var PA = new InstrumentReading()
                {

                    // UploadedFile = upload.ContentType,

                    IDInstrument = FD.IDInstrument,
                    IDReading = FD.IDReading,
                    DataRead = FD.DataRead,
                    DateRead = FD.DateRead,
                    TimeRead = FD.TimeRead,
                    DataNo = FD.DataNo,
                    DataX = FD.DataX,
                    DataY = FD.DataY,
                    YearCovered = date.Year


                };

                if (upload != null)
                {

                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        PA.filetype = 1;
                        PA.contenttype = System.IO.Path.GetFileName(upload.FileName);
                        PA.UploadedFile = reader.ReadBytes(upload.ContentLength);
                    }
                }

                db.Entry(PA).State = EntityState.Modified;
                db.SaveChanges();



                string url = Url.Action("IndexReadingDamInstrumentation", "Dam", new { searchTerm = FD.IDInstrument });

                return Json(new { success = true, url = url });
                //return Json(new { suceess = true });
            }

            return PartialView("_EditReadingInstrumentation", FD);

        }


        // GET: dam/Delete/5
        public ActionResult DeleteReadingInstrumentation(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstrumentReadingView dm = db.InstrumentReadingViews.Find(id);
            if (dm == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DeleteReadingInstrumentation", dm);
        }

        [HttpPost, ActionName("DeleteReadingInstrumentation")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteReadingInstrumentation(InstrumentReadingView dm)
        {

            InstrumentReading FD = db.InstrumentReadings.Find(dm.IDReading);
            db.InstrumentReadings.Remove(FD);
            db.SaveChanges();




            string url = Url.Action("IndexReadingDamInstrumentation", "Dam", new { searchTerm = dm.IDInstrument });

            return Json(new { success = true, url = url });
            //return Json(new { suceess = true });

        }



        //   photos


        public ActionResult Gallery(string id)
        {
            List<DamPicView> all = new List<DamPicView>();
            Session["myidsys"] = id;
            // Here MyDatabaseEntities is our datacontext

            all = db.DamPicViews.Where(r => r.IDDam == id).ToList();

            return View(all);





        }

        public ActionResult Upload()
        {

            return View();



        }
        [HttpPost]
        public ActionResult Upload(DamPicView fileModel)
        {
            FileUploadServiceDam service = new FileUploadServiceDam();

            var id = Session["myidsys"].ToString();
            foreach (var item in fileModel.File)
            {

                service.SaveFileDetails(item, id,"Dam");
            }
            return RedirectToAction("Photos", new { id = id });
        }


        public ActionResult Photos(string id)
        {
            // Default.
            string folder = "~/Pic/DAM/" + id + "/";

            Session["myidsys"] = id;

          
                return View(new PhotoModel(folder));



        }


        public FileResult DownloadFTP(int id = 0)
        {


            var d = db.InspectionReports.Where(p => p.IDInspection == id).FirstOrDefault();


            var filepath = System.IO.Path.Combine(Server.MapPath("/Files/"), d.Remarks);


            string ftp = "ftp://172.16.4.50:1616/";

            //FTP Folder name. Leave blank if you want to Download file from root folder.
            string ftpFolder = "DAM/" + d.IDDAm + "/" + d.IDInspection + "/";

            try
            {
                //Create FTP Request.
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp + ftpFolder + d.Remarks);
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
                    Response.AddHeader("content-disposition", "attachment;filename=" + d.Remarks);
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


    }
}