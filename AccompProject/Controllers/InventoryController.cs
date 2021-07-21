using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccompProject.Models;
using AccompProject.Models.EntityModel;
using PagedList;
using System.Net;
using System.Data.Entity;
using System.Globalization;
using System.Configuration;
using System.Data.SqlClient;

namespace AccompProject.Controllers
{
    public class InventoryController : Controller
    {
        // GET: Inventory

        private InventoryEntities db = new InventoryEntities();
        private OPAIPBP_YR_2021Entities db1 = new OPAIPBP_YR_2021Entities();
        private DamEntities db2 = new DamEntities();
        [Authorize]


        [HttpGet]
        public ActionResult GetRegions(string iso3)
        {
            if (!string.IsNullOrWhiteSpace(iso3.ToString()))
            {
               Session["province"] = iso3;
                IEnumerable<SelectListItem> regions = GetRegions1(iso3);
                return Json(regions, JsonRequestBehavior.AllowGet);
            }
            return null;
        }
        
        public IEnumerable<SelectListItem> GetRegions1(string iso3)
        {
            if (!String.IsNullOrWhiteSpace(iso3.ToString()))
            {
                using (var context = new OPAIPBP_YR_2021Entities())
                {
                    IEnumerable<SelectListItem> regions = context.inventoryLocationProvinceMunicipalities.AsNoTracking()
                        .OrderBy(n => n.Municipality)
                        .Where(n => n.Province== iso3)
                        .Select(n =>
                           new SelectListItem
                           {
                               Value = n.MunicipalityPk.ToString(),
                               Text = n.Municipality
                           }).ToList();
                    return new SelectList(regions, "Value", "Text");
                }
            }
            return null;
        }


        public ActionResult GetDistrict(string iso3)
        {
            if (!string.IsNullOrWhiteSpace(iso3.ToString()))
            {
                Session["Munic"] = iso3;
                IEnumerable<SelectListItem> regions = GetDistrict1(iso3);
                return Json(regions, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        public IEnumerable<SelectListItem> GetDistrict1(string iso3)
        {
            if (!String.IsNullOrWhiteSpace(iso3.ToString()))
            {
                using (var context = new OPAIPBP_YR_2021Entities())
                {
                    string prov = Session["province"].ToString();
                    IEnumerable<SelectListItem> regions = context.inventoryLocationMunicipalityDistricts.AsNoTracking()
                        .OrderBy(n => n.DistrictPK)
                        .Where(n => n.Municipality == iso3 && n.Province == prov)
                        .Select(n =>
                           new SelectListItem
                           {
                               Value = n.DistrictPK.ToString(),
                               Text = n.District
                           }).ToList();
                    return new SelectList(regions, "Value", "Text");
                }
            }
            return null;
        }

        public ActionResult Autocomplete(string term)   
        {
            //Session["regiontolog"] = "UPRIIS";
            if (Session["regiontolog"] == null)
            {

                return RedirectToAction("LogIn", "Account", new { area = "" });

            }
            string strauto = Session["regiontolog"].ToString();


            var accomplishments = db.Inventory_view
                      .OrderBy(r => r.SYSTEMS)
                      .Where(r => r.SYSTEMS.Contains(term) && r.res_region == strauto)
                      .Select(r => new
                      {
                          label = r.SYSTEMS
                      })
                      .Take(5);

            return Json(accomplishments, JsonRequestBehavior.AllowGet);


        }



        public ActionResult Index(string searchTerm= "", int page = 1, string asabag="", string sortOrder = null, string currentFilter=null)
        {
            string str = Session["regiontolog"].ToString();
            var asalst = new List<string>();
            var asaqry = from d in db.Inventory_view
                         orderby d.MUNICIPALITY
                         where d.res_region == str
                         select d.MUNICIPALITY;


           

            asalst.AddRange(asaqry.Distinct());
            ViewBag.asabag = new SelectList(asalst);

           // Session["searchterm"] = searchTerm;
            Session["asabag"] = asabag;

            if (Session["searchterm"] == null)
            {

                Session["searchterm"] = "";

            }
            else {
                Session["searchterm"] = searchTerm;
            
            }


            if (Session["regiontolog"] == null)
            {

                return RedirectToAction("LogIn", "Account", new { area = "" });

            }


     


            if (searchTerm != null || asabag != null)
            {
               // page = 1;
            }
            else
            {
                searchTerm = currentFilter;
            }

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "system_desc" : "";
            switch (sortOrder)
            {

                case "system_desc":
                    if (!string.IsNullOrEmpty(asabag) && asabag != "---")
                    {
                        if (!string.IsNullOrEmpty(searchTerm))
                        {
                            var accomplishments = db.Inventory_view
                                      .OrderByDescending(r => r.SYSTEMS)
                                     .Where(r => (searchTerm == null || r.SYSTEMS.StartsWith(searchTerm)) && r.res_region == str && r.MUNICIPALITY == asabag)
                                     .ToPagedList(page, 30);
                            if (Request.IsAjaxRequest())
                            {
                                return PartialView("_ListOfInventory", accomplishments);
                            }
                            return View(accomplishments);
                            //int pageSize = 3;
                            //int pageNumber = (page ?? 1);
                            //return View(accomplishments.ToPagedList(pageNumber, pageSize));

                        }
                        else
                        {
                            var accomplishments = db.Inventory_view
                                      .OrderByDescending(r => r.SYSTEMS)
                                     .Where(r => r.res_region == str && r.MUNICIPALITY == asabag)
                                     .ToPagedList(page, 30);
                            if (Request.IsAjaxRequest())
                            {
                                return PartialView("_ListOfInventory", accomplishments);
                            }
                            return View(accomplishments);
                        }
                    }
                    else
                    {

                        var accomplishments = db.Inventory_view
                               .OrderByDescending(r => r.SYSTEMS)
                              .Where(r => (searchTerm == null || r.SYSTEMS.StartsWith(searchTerm)) && r.res_region == str)
                              .ToPagedList(page, 30);
                        if (Request.IsAjaxRequest())
                        {
                            return PartialView("_ListOfInventory", accomplishments);
                        }
                        return View(accomplishments);

                    }

                    break;

                case "sytem_order":
                    if (!string.IsNullOrEmpty(asabag) && asabag != "---")
                    {
                        if (!string.IsNullOrEmpty(searchTerm))
                        {
                            var accomplishments = db.Inventory_view
                                      .OrderBy(r => r.SYSTEMS)
                                     .Where(r => (searchTerm == null || r.SYSTEMS.StartsWith(searchTerm)) && r.res_region == str && r.MUNICIPALITY == asabag)
                                     .ToPagedList(page, 30);
                            if (Request.IsAjaxRequest())
                            {
                                return PartialView("_ListOfInventory", accomplishments);
                            }
                            return View(accomplishments);
                        }
                        else
                        {
                            var accomplishments = db.Inventory_view
                                      .OrderBy(r => r.SYSTEMS)
                                     .Where(r => r.res_region == str && r.MUNICIPALITY == asabag)
                                     .ToPagedList(page, 30);
                            if (Request.IsAjaxRequest())
                            {
                                return PartialView("_ListOfInventory", accomplishments);
                            }
                            return View(accomplishments);
                        }


                    }
                    else
                    {

                        var accomplishments = db.Inventory_view
                               .OrderBy(r => r.SYSTEMS)
                              .Where(r => (searchTerm == null || r.SYSTEMS.StartsWith(searchTerm)) && r.res_region == str)
                              .ToPagedList(page, 30);
                        if (Request.IsAjaxRequest())
                        {
                            return PartialView("_ListOfInventory", accomplishments);
                        }
                        return View(accomplishments);

                    }
                    break;

                default:
                    if (!string.IsNullOrEmpty(asabag) && asabag != "---")
                    {
                        if (!string.IsNullOrEmpty(searchTerm))
                        {
                            var accomplishments = db.Inventory_view
                                      .OrderBy(r => r.SYSTEMS)
                                     .Where(r => (searchTerm == null || r.SYSTEMS.StartsWith(searchTerm)) && r.res_region == str && r.MUNICIPALITY == asabag)
                                     .ToPagedList(page, 30);
                            if (Request.IsAjaxRequest())
                            {
                                return PartialView("_ListOfInventory", accomplishments);
                            }
                            return View(accomplishments);
                        }
                        else
                        {
                            var accomplishments = db.Inventory_view
                                      .OrderBy(r => r.SYSTEMS)
                                     .Where(r => r.res_region == str && r.MUNICIPALITY == asabag)
                                     .ToPagedList(page, 30);
                            if (Request.IsAjaxRequest())
                            {
                                return PartialView("_ListOfInventory", accomplishments);
                            }
                            return View(accomplishments);
                        }


                    }
                    else
                    {

                        var accomplishments = db.Inventory_view
                               .OrderBy(r => r.SYSTEMS)
                              .Where(r => (searchTerm == null || r.SYSTEMS.StartsWith(searchTerm)) && r.res_region == str)
                              .ToPagedList(page, 30);
                        if (Request.IsAjaxRequest())
                        {
                            return PartialView("_ListOfInventory", accomplishments);
                        }
                        return View(accomplishments);

                    }
                    break;

            }


            //var accomplishments = db.Inventory_view
            //             .OrderBy(r => r.SYSTEMS)
            //            .Where(r => (searchTerm == null || r.SYSTEMS.StartsWith(searchTerm)) && r.res_region == str)
            //            .ToPagedList(page, 30);


            //if (Request.IsAjaxRequest())
            //{
            //    return PartialView("_ListOfInventory", accomplishments);
            //}
            //return View(accomplishments);


        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory_view aCCOMPLISHMENT = db.Inventory_view.Find(id);
            if (aCCOMPLISHMENT == null)
            {
                return HttpNotFound();
            }
            string reg = Session["regiontolog"].ToString();

            ViewBag.TypeOfRegion = new SelectList(db.TblRegionLogins, "id_region", "region", aCCOMPLISHMENT.ID_REGION);
            ViewBag.TypeOfCategory = new SelectList(db.TblCategories, "ID_CATEGORY", "DESCRIPTION", aCCOMPLISHMENT.ID_CATEGORY);
            ViewBag.TypeOfResponsibility = new SelectList(db.TblRegionLogins, "id_region", "region", aCCOMPLISHMENT.RESPONSIBILITY);

            ViewBag.TypeOfOci = new SelectList(db.TblOCIs, "ID_OCI", "DESCRIPTION", aCCOMPLISHMENT.ID_OCI);
            ViewBag.TypeOfProvince = new SelectList(db.TblProvinces, "ID_PROVINCE", "PROVINCE", aCCOMPLISHMENT.ID_PROVINCE);
            ViewBag.TypeOfDiversion = new SelectList(db.TblDiversions, "ID_DIVERSION", "TYPE", aCCOMPLISHMENT.ID_DIVERSION);
            ViewBag.TypeOfDistrict = new SelectList(db.TblDistricts, "ID_DISTRICT", "DISTRICT", aCCOMPLISHMENT.ID_DISTRICT);
            ViewBag.TypeOfCrops = new SelectList(db.TblCropsplanteds, "ID_CROPS", "CROPS", aCCOMPLISHMENT.ID_CROPS);
            ViewBag.TypeOfIMO = new SelectList(db.TblIMOes, "code", "description", aCCOMPLISHMENT.ID_CROPS);
            ViewBag.DAMDAM = new SelectList(db2.GeneralInformations.Where(R => R.Region == reg), "IDDam", "DamName",aCCOMPLISHMENT.dam);

     

            return View(aCCOMPLISHMENT);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Inventory_view FODB)
        {

            var valids = true;
            string reg = Session["regiontolog"].ToString();


            var sa = (FODB.SERVICE_FIRMED + FODB.CONVERTED_LAND + FODB.PERMANENT + FODB.NEWLY);

            var sasa = Convert.ToDouble(sa).ToString("F");



            if (String.IsNullOrWhiteSpace(FODB.dam))
            {
                if (FODB.ID_CATEGORY == 1)
                {

                    TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\"> Dam Name is required!  </div>";
                    valids = false;
                }
            }



            if (Convert.ToDouble(FODB.SERVICE_ORIGINAL).ToString("F") !=  sasa)
            {
                TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\">  Service Area is not equal to fusa, converted land, permanently non restorable and newly generated areas </div>";
                valids = false;
            
            }

            var fus = (FODB.AREA_NONOPERATIONAL + FODB.AREA_OPERATIONAL);
            var fusa = Convert.ToDouble(fus).ToString("F");


            if (Convert.ToDouble(FODB.SERVICE_FIRMED).ToString("F") != fusa)
            {
                TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\">  FUSA is not equal to Operational + non operational </div>";
                valids = false;

            }


            //if (FODB.AREA_OPERATIONAL < (FODB.IRRIGATED_DRY))
            //{
            //    TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\"> Irrigated dry is greater than Operational Area </div>";
            //    valids = false;

            //}

            //if (FODB.AREA_OPERATIONAL < (FODB.IRRIGATED_WET))
            //{
            //    TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\"> Irrigated wet is greater than Operational Area </div>";
            //    valids = false;

            //}

            //if (FODB.AREA_OPERATIONAL < (FODB.IRRIGATED_RATOONING))
            //{
            //    TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\"> Irrigated ratooning is greater than Operational Area </div>";
            //    valids = false;

            //}

            //if (FODB.AREA_OPERATIONAL < (FODB.THIRD_IRRIGATED))
            //{
            //    TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\"> Irrigated third crop is greater than Operational Area </div>";
            //    valids = false;

            //}
          



            if ((ModelState.IsValid) && (valids == true))
            {






                var Findtbldata = new TblData()
                {
                    IDSystem = FODB.IDSystem,
                    ID_DATA = FODB.ID_DATA,
                    YEAR_COVERED = FODB.YEAR_COVERED,
                    SYSTEMS = FODB.SYSTEMS,
                    CATEGORY = FODB.ID_CATEGORY,
                    REGION = FODB.ID_REGION,
                    PROVINCE = FODB.ID_PROVINCE,
                    MUNICIPALITY = FODB.MUNICIPALITY,
                    DISTRICT = FODB.ID_DISTRICT,
                    SERVICE_ORIGINAL = FODB.SERVICE_ORIGINAL,
                    SERVICE_FIRMED = FODB.SERVICE_FIRMED,
                    AREA_NONOPERATIONAL = FODB.AREA_NONOPERATIONAL,
                    AREA_OPERATIONAL = FODB.AREA_OPERATIONAL,
                    IRRIGATED_WET = FODB.IRRIGATED_WET,
                    IRRIGATED_DRY = FODB.IRRIGATED_DRY,
                    BENEFITED_WET = FODB.BENEFITED_WET,
                    BENEFITED_DRY = FODB.BENEFITED_DRY,
                    AVERAGE_YIELD = FODB.AVERAGE_YIELD,
                    DATE_CONSTRUCTED = FODB.DATE_CONSTRUCTED,
                    OCI = FODB.ID_OCI,
                    DIVERSION = FODB.ID_DIVERSION,
                    SPECIFIC_DIVERSION = FODB.SPECIFIC_DIVERSION,
                    WATER_SUPPLY = FODB.WATER_SUPPLY,
                    LATITUDE = FODB.LATITUDE,
                    LONGITUDE = FODB.LONGITUDE,
                    CROPS = FODB.ID_CROPS,
                    REMARKS = FODB.REMARKS,
                    DATE_UPDATED = FODB.DATE_UPDATED,
                    NOSYSTEM = FODB.NOSYSTEM,
                    AMORTIZING = FODB.AMORTIZING,
                    IRRIGATED_RATOONING = FODB.IRRIGATED_RATOONING,
                    BENEFITED_RATOONING = FODB.BENEFITED_RATOONING,
                    AVERAGE_YIELD_DRY = FODB.AVERAGE_YIELD_DRY,
                    AVERAGE_YIELD_RATOONING = FODB.AVERAGE_YIELD_RATOONING,
                    REMARKS_REASON = FODB.REMARKS_REASON,
                    FARMERS_BENEFICIARIES = FODB.FARMERS_BENEFICIARIES,
                    CONVERTED_LAND = FODB.CONVERTED_LAND,
                    PERMANENT = FODB.PERMANENT,
                    THIRD_IRRIGATED = FODB.THIRD_IRRIGATED,
                    THIRD_BENEFITED = FODB.THIRD_BENEFITED,
                    THIRD_AVERAGE = FODB.THIRD_AVERAGE,
                    RESPONSIBILITY = FODB.RESPONSIBILITY,
                    IMO_RES = FODB.IMO_RES,
                    spec = FODB.spec,
                    LINED = FODB.LINED,
                    UNLINED = FODB.UNLINED,
                    NEWLY = FODB.NEWLY,
                    DAMTYPE = FODB.DAMTYPE,
                    SD = FODB.SD,
                    newlyarea = FODB.newlyarea,
                    iaarea = FODB.iaarea,
                    iafb = FODB.iafb,
                    iamember = FODB.iamember,
                    iano = FODB.iano,
                    main_earth = FODB.main_earth,
                    main_lined = FODB.main_lined,
                    main_total = FODB.main_total,
                    lateral_earth = FODB.lateral_earth,
                    lateral_lined = FODB.lateral_lined,
                    lateral_total = FODB.lateral_total,
                    dam = FODB.dam


                };

                db.Entry(Findtbldata).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
                //string url = Url.Action("Index", "Inventory");

                //return Json(new { success = true, url = url });
                //return Json(new { suceess = true });
            }


            ViewBag.TypeOfRegion = new SelectList(db.TblRegionLogins, "id_region", "region", FODB.ID_REGION);
            ViewBag.TypeOfCategory = new SelectList(db.TblCategories, "ID_CATEGORY", "DESCRIPTION", FODB.ID_CATEGORY);
            ViewBag.TypeOfResponsibility = new SelectList(db.TblRegionLogins, "id_region", "region", FODB.RESPONSIBILITY);



            ViewBag.TypeOfOci = new SelectList(db.TblOCIs, "ID_OCI", "DESCRIPTION", FODB.ID_OCI);
            ViewBag.TypeOfProvince = new SelectList(db.TblProvinces, "ID_PROVINCE", "PROVINCE", FODB.ID_PROVINCE);
            ViewBag.TypeOfDiversion = new SelectList(db.TblDiversions, "ID_DIVERSION", "TYPE", FODB.ID_DIVERSION);
            ViewBag.TypeOfDistrict = new SelectList(db.TblDistricts, "ID_DISTRICT", "DISTRICT", FODB.ID_DISTRICT);
            ViewBag.TypeOfCrops = new SelectList(db.TblCropsplanteds, "ID_CROPS", "CROPS", FODB.ID_CROPS);
            ViewBag.TypeOfIMO = new SelectList(db.TblIMOes, "code", "description", FODB.ID_CROPS);
            ViewBag.DAMDAM = new SelectList(db2.GeneralInformations.Where(R => R.Region == reg), "IDDam", "DamName", FODB.dam);

     


            //return PartialView("_MyEdit", FODB);
            return View(FODB);

        }



        public ActionResult Create()
        {

            
            string reg = Session["regiontolog"].ToString();
            ViewBag.TypeOfRegion = new SelectList(db.TblRegionLogins.Where(a => a.region == reg), "id_region", "region");
            ViewBag.TypeOfCategory = new SelectList(db.TblCategories, "ID_CATEGORY", "DESCRIPTION");
            ViewBag.TypeOfResponsibility = new SelectList(db.TblRegionLogins, "id_region", "region");
            ViewBag.TypeOfOci = new SelectList(db.TblOCIs, "ID_OCI", "DESCRIPTION");
            ViewBag.TypeOfmuni = new SelectList(db1.inventoryLocationProvinceMunicipalities, "municipalityPk", "Municipality");
            ViewBag.TypeOfProvince = new SelectList(db1.inventoryLocationRegionProvinces.Where(a => a.Region == reg ).OrderBy(a => a.Province), "ProvincePK", "PROVINCE");
            ViewBag.TypeOfDiversion = new SelectList(db.TblDiversions, "ID_DIVERSION", "TYPE");
            ViewBag.TypeOfDistrict = new SelectList(db.TblDistricts, "ID_DISTRICT", "DISTRICT");
            ViewBag.TypeOfCrops = new SelectList(db.TblCropsplanteds, "ID_CROPS", "CROPS");
            ViewBag.TypeOfIMO = new SelectList(db.TblIMOes, "code", "description");

          
            ViewBag.DAMDAM = new SelectList(db2.GeneralInformations.Where(R => R.Region == reg), "IDDam", "DamName");

            return View();

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inventory_view FODB)
        {
            string REG = Session["regiontolog"].ToString();

            var valids = true;
            var sa = ( FODB.SERVICE_FIRMED  + FODB.CONVERTED_LAND + (FODB.PERMANENT) + FODB.NEWLY);

            var sasa = Convert.ToDouble(sa).ToString("F");


            if (String.IsNullOrWhiteSpace(FODB.dam))
            {
                if (FODB.ID_CATEGORY == 1)
                {

                    TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\"> Dam Name is required!  </div>";
                    valids = false;
                }
            }

            if (Convert.ToDouble(FODB.SERVICE_ORIGINAL).ToString("F") != sasa)
            {
                TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\">  Service Area is not equal to fusa, converted land, permanently non restorable and newly generated areas </div>";
                valids = false;

            }

            var fus = (FODB.AREA_NONOPERATIONAL + FODB.AREA_OPERATIONAL);
            var fusa = Convert.ToDouble(fus).ToString("F");


            if (Convert.ToDouble(FODB.SERVICE_FIRMED).ToString("F") != fusa)
            {
                TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\">  FUSA is not equal to Operational + non operational </div>";
                valids = false;

            }




            if (FODB.AREA_OPERATIONAL < (FODB.IRRIGATED_DRY))
            {
                TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\"> Irrigated dry is greater than Operational Area </div>";
                valids = false;

            }

            if (FODB.AREA_OPERATIONAL < (FODB.IRRIGATED_WET))
            {
                TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\"> Irrigated wet is greater than Operational Area </div>";
                valids = false;

            }

            if (FODB.AREA_OPERATIONAL < (FODB.IRRIGATED_RATOONING))
            {
                TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\"> Irrigated ratooning is greater than Operational Area </div>";
                valids = false;

            }

            if (FODB.AREA_OPERATIONAL < (FODB.THIRD_IRRIGATED))
            {
                TempData["msg"] += " <div class=\"alert alert-danger fde\" role=\"alert\"> Irrigated third crop is greater than Operational Area </div>";
                valids = false;

            }
            int prov = 0;
            string Constring = ConfigurationManager.ConnectionStrings["Inventory"].ConnectionString;
            using (SqlConnection CON = new SqlConnection(Constring))
            {
                CON.Open();
                SqlCommand cmd = new SqlCommand();




                cmd.CommandText = "select id_province from tblprovince where province = @province";


                cmd.Connection = CON;
              
                string prov1 = (Session["province"].ToString());
                cmd.Parameters.AddWithValue("@province", prov1);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //Console.WriteLine("{0}\t{1}", reader.GetInt32(0),
                        //    reader.GetString(1));

                        prov = reader.GetInt32(0);


                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                reader.Close();



            }



            if ((ModelState.IsValid) && (valids == true))
            {

                var iddam = Guid.NewGuid();

                var invent = new TblData()
                {
                    IDSystem = iddam.ToString(),
                    ID_DATA = FODB.ID_DATA,
                    YEAR_COVERED = FODB.YEAR_COVERED,
                    SYSTEMS = FODB.SYSTEMS,
                    CATEGORY = FODB.ID_CATEGORY,
                    REGION = FODB.ID_REGION,
                    PROVINCE = prov,
                    MUNICIPALITY = FODB.MUNICIPALITY,
                    DISTRICT = FODB.ID_DISTRICT,
                    SERVICE_ORIGINAL = FODB.SERVICE_ORIGINAL,
                    SERVICE_FIRMED = FODB.SERVICE_FIRMED,
                    AREA_NONOPERATIONAL = FODB.AREA_NONOPERATIONAL,
                    AREA_OPERATIONAL = FODB.AREA_OPERATIONAL,
                    IRRIGATED_WET = FODB.IRRIGATED_WET,
                    IRRIGATED_DRY = FODB.IRRIGATED_DRY,
                    BENEFITED_WET = FODB.BENEFITED_WET,
                    BENEFITED_DRY = FODB.BENEFITED_DRY,
                    AVERAGE_YIELD = FODB.AVERAGE_YIELD,
                    DATE_CONSTRUCTED = FODB.DATE_CONSTRUCTED,
                    OCI = FODB.ID_OCI,
                    DIVERSION = FODB.ID_DIVERSION,
                    SPECIFIC_DIVERSION = FODB.SPECIFIC_DIVERSION,
                    WATER_SUPPLY = FODB.WATER_SUPPLY,
                    LATITUDE = FODB.LATITUDE,
                    LONGITUDE = FODB.LONGITUDE,
                    CROPS = FODB.ID_CROPS,
                    REMARKS = FODB.REMARKS,
                    DATE_UPDATED = FODB.DATE_UPDATED,
                    NOSYSTEM = FODB.NOSYSTEM,
                    AMORTIZING = FODB.AMORTIZING,
                    IRRIGATED_RATOONING = FODB.IRRIGATED_RATOONING,
                    BENEFITED_RATOONING = FODB.BENEFITED_RATOONING,
                    AVERAGE_YIELD_DRY = FODB.AVERAGE_YIELD_DRY,
                    AVERAGE_YIELD_RATOONING = FODB.AVERAGE_YIELD_RATOONING,
                    REMARKS_REASON = FODB.REMARKS_REASON,
                    FARMERS_BENEFICIARIES = FODB.FARMERS_BENEFICIARIES,
                    CONVERTED_LAND = FODB.CONVERTED_LAND,
                    PERMANENT = FODB.PERMANENT,
                    THIRD_IRRIGATED = FODB.THIRD_IRRIGATED,
                    THIRD_BENEFITED = FODB.THIRD_BENEFITED,
                    THIRD_AVERAGE = FODB.THIRD_AVERAGE,
                    RESPONSIBILITY = FODB.RESPONSIBILITY,
                    IMO_RES = FODB.IMO_RES,
                    spec = FODB.spec,
                    LINED = FODB.LINED,
                    UNLINED = FODB.UNLINED,
                    NEWLY = FODB.NEWLY,
                    DAMTYPE = FODB.DAMTYPE,
                    SD = FODB.SD,
                    newlyarea = FODB.newlyarea,
                    iaarea = FODB.iaarea,
                    iafb = FODB.iafb,
                    iamember = FODB.iamember,
                    iano = FODB.iano,
                    main_earth = FODB.main_earth,
                    main_lined = FODB.main_lined,
                    main_total = FODB.main_total,
                    lateral_earth = FODB.lateral_earth,
                    lateral_lined = FODB.lateral_lined,
                    lateral_total = FODB.lateral_total,
                    municipalityCode = FODB.municipalityCode,
                    provinceCode = FODB.provinceCode,
                    dam = FODB.dam

                };

                db.TblDatas.Add(invent);
                db.SaveChanges();


                return RedirectToAction("Index");
            }
            else
            {


                List<SelectListItem> items = new List<SelectListItem>();

                items.Add(new SelectListItem { Text = FODB.MUNICIPALITY, Value = FODB.municipalityCode.ToString() });

                ViewBag.TypeOfmuni = new SelectList(items, "Value","Text",FODB.municipalityCode);


                ViewBag.TypeOfRegion = new SelectList(db.TblRegionLogins, "id_region", "region");
                ViewBag.TypeOfCategory = new SelectList(db.TblCategories, "ID_CATEGORY", "DESCRIPTION");
                ViewBag.TypeOfResponsibility = new SelectList(db.TblRegionLogins, "id_region", "region");

                ViewBag.TypeOfOci = new SelectList(db.TblOCIs, "ID_OCI", "DESCRIPTION");
                ViewBag.TypeOfProvince = new SelectList(db.TblProvinces, "ID_PROVINCE", "PROVINCE");
                ViewBag.TypeOfDiversion = new SelectList(db.TblDiversions, "ID_DIVERSION", "TYPE");
                ViewBag.TypeOfDistrict = new SelectList(db.TblDistricts, "ID_DISTRICT", "DISTRICT");
                ViewBag.TypeOfCrops = new SelectList(db.TblCropsplanteds, "ID_CROPS", "CROPS");
                ViewBag.TypeOfIMO = new SelectList(db.TblIMOes, "code", "description");
                ViewBag.DAMDAM = new SelectList(db2.GeneralInformations.Where(R => R.Region == REG), "IDDam", "DamName");

            }

            return View(FODB);

        }



        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory_view dm = db.Inventory_view.Find(id);
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

            TblData GI = db.TblDatas.Find(id);
            db.TblDatas.Remove(GI);
            db.SaveChanges();







            return RedirectToAction("Index");

        }


    }
}