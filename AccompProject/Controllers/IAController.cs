using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AccompProject.Models;
using PagedList;
using System.Threading.Tasks;
using AccompProject.Helpers;
using AccompProject.Models.IA.Other;

namespace AccompProject.Controllers
{
    public class IAController : Controller
    {
        private IAEntities db = new IAEntities();
        private InventoryEntities db1 = new InventoryEntities();
        PolygonCode empDB = new PolygonCode();

        #region OLD CODE
        // GET: IA
        public ActionResult Index()
        {
            return View(db.FSEntries.ToList());
        }


        // GET: IA
        public ActionResult IndexIA(string searchTerm = null, int page = 1)
        {
            string str = Session["regiontolog"].ToString();

            var IALIST = db.IAListViews
                  .OrderByDescending(r => r.IAname)
                      .Where(r => (searchTerm == null || r.IAname.Contains(searchTerm)) && r.region == str)
                      .ToPagedList(page, 60);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListOfIA", IALIST);
            }

            return View(IALIST);
        }


        // GET: IA/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FSEntry fSEntry = db.FSEntries.Find(id);
            if (fSEntry == null)
            {
                return HttpNotFound();
            }
            return View(fSEntry);
        }

        // GET: IA/Create
        public ActionResult Create()
        {


            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "MAJOR CRITERIA", Value = "MAJOR CRITERIA" });

            items.Add(new SelectListItem { Text = "IA FUNCTIONALITY RATING", Value = "IA FUNCTIONALITY RATING" });
            ViewBag.BASED = new SelectList(items, "Value", "Text");


            List<SelectListItem> itemsCAT = new List<SelectListItem>();

            itemsCAT.Add(new SelectListItem { Text = "CIS", Value = "CIS" });

            itemsCAT.Add(new SelectListItem { Text = "NIS", Value = "NIS" });
            ViewBag.CAT = new SelectList(itemsCAT, "Value", "Text");

            return View();
        }

        // POST: IA/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FSEntry fSEntry)
        {
            if (ModelState.IsValid)
            {
                var invent = new FSEntry()
                {
                    Category = fSEntry.Category,
                    Year_Covered = fSEntry.Year_Covered,
                    Criteria = fSEntry.Criteria,
                    NoIA = fSEntry.NoIA,
                    Entry1 = fSEntry.Entry1,
                    Entry2 = fSEntry.Entry2,
                    Entry3 = fSEntry.Entry3,
                    Entry4 = fSEntry.Entry4,
                    Entry5 = fSEntry.Entry5,
                    TotalScore = fSEntry.TotalScore,
                    Entry1_Percent = fSEntry.Entry1_Percent,
                    Entry2_Percent = fSEntry.Entry2_Percent,
                    Entry3_Percent = fSEntry.Entry3_Percent,
                    Entry4_Percent = fSEntry.Entry4_Percent,
                    Entry5_Percent = fSEntry.Entry5_Percent,
                    Region = Session["regiontolog"].ToString()

                };

                db.FSEntries.Add(invent);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(fSEntry);
        }

        // GET: IA/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            FSEntry fSEntry = db.FSEntries.Find(id);
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "MAJOR CRITERIA", Value = "MAJOR CRITERIA" });
            items.Add(new SelectListItem { Text = "IA FUNCTIONALITY RATING", Value = "IA FUNCTIONALITY RATING" });




            List<SelectListItem> itemsCAT = new List<SelectListItem>();
            itemsCAT.Add(new SelectListItem { Text = "CIS", Value = "CIS" });
            itemsCAT.Add(new SelectListItem { Text = "NIS", Value = "NIS" });


            if (fSEntry != null)
            {


            ViewBag.BASED = new SelectList(items, "Value", "Text", fSEntry.Criteria);
            ViewBag.CAT = new SelectList(itemsCAT, "Value", "Text", fSEntry.Category);
            }
            else
            {
                ViewBag.CAT = new SelectList(itemsCAT, "Value", "Text");
          

                ViewBag.BASED = new SelectList(items, "Value", "Text");

            }


           

            //if (fSEntry == null)
            //{
            //    return HttpNotFound();
            //}
            return View(fSEntry);
        }

        // POST: IA/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FSEntry fSEntry)
        {
            if (ModelState.IsValid)
            {


                var invent = new FSEntry()
                {
                    Category = fSEntry.Category,
                    Year_Covered = fSEntry.Year_Covered,
                    Criteria = fSEntry.Criteria,
                    NoIA = fSEntry.NoIA,
                    Entry1 = fSEntry.Entry1,
                    Entry2 = fSEntry.Entry2,
                    Entry3 = fSEntry.Entry3,
                    Entry4 = fSEntry.Entry4,
                    Entry5 = fSEntry.Entry5,
                    TotalScore = fSEntry.TotalScore,
                    Entry1_Percent = fSEntry.Entry1_Percent,
                    Entry2_Percent = fSEntry.Entry2_Percent,
                    Entry3_Percent = fSEntry.Entry3_Percent,
                    Entry4_Percent = fSEntry.Entry4_Percent,
                    Entry5_Percent = fSEntry.Entry5_Percent,
                    Region = Session["regiontolog"].ToString(),
                    FSId = fSEntry.FSId

                };

                db.Entry(invent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fSEntry);
        }

        // GET: IA/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FSEntry fSEntry = db.FSEntries.Find(id);
            if (fSEntry == null)
            {
                return HttpNotFound();
            }
            return View(fSEntry);
        }

        // POST: IA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FSEntry fSEntry = db.FSEntries.Find(id);
            db.FSEntries.Remove(fSEntry);
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


        #endregion


        #region IA New Code

        #region IA

        public ActionResult IAList()
        {

            if (Session["regiontolog"] == null)
            {

                return RedirectToAction("Login", "Account", "");
            }
            string region = Session["regiontolog"].ToString();

                return View(db.IAListViews.Where(f => f.region == region).ToList());
        }


        public ActionResult CreateIA()
        {
            string region = Session["regiontolog"].ToString();


            IAList il = new IAList();
            ViewBag.TypeSystem = new SelectList(db1.SystemSelections.Where(a => a.res_region == region), "IDSystem", "SYSTEMS");

            return View(il);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateIA(IAList il)
        {

            NewIA nia = new NewIA();

            string region = Session["regiontolog"].ToString();
            var yr = db.YearReferences.FirstAsync(z => z.idyear == 1);
            var e = db1.SystemSelections.AsNoTracking().FirstAsync(r => r.IDSystem == il.systemID);
            var g = await e;
            var b = await yr;

            g.ID_CATEGORY = g.ID_CATEGORY;

            if (ModelState.IsValid)
            {
                var iaid = Guid.NewGuid();
                var f = new IAList()
                {

                    category = g.ID_CATEGORY,
                    region = Convert.ToInt32(g.RESPONSIBILITY),
                    IAname = il.IAname,
                    systemID = il.systemID,
                    systemname = il.systemname,
                    IDIA = iaid.ToString()


                };

                db.IALists.Add(f);
                await db.SaveChangesAsync();


                //save to ia profile
                await nia.SaveIAProfileNew(il.systemname,iaid.ToString(),il.IAname);
                


                return RedirectToAction("IAList");


            }


            ViewBag.TypeSystem = new SelectList(db1.SystemSelections.Where(a => a.res_region == region), "IDSystem", "SYSTEMS", il.systemID);

            return View(il);

        }



        public ActionResult EditIA(string id = null)
        {

            if (id == null)
            {

                return RedirectToAction("Login", "Account", "");
            }

            string region = Session["regiontolog"].ToString();


            var il = db.IAListViews.Find(id);
            ViewBag.TypeSystem = new SelectList(db1.SystemSelections.Where(a => a.res_region == region), "IDSystem", "SYSTEMS",il.systemID);

            return View(il);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditIA(IAListView il)
        {

           
              if (ModelState.IsValid)
            {
                var f = new IAList()
                {

                    category = il.ID_CATEGORY,
                    region = il.id_region,
                    IAname = il.IAname,
                    systemID = il.systemID,
                    systemname = il.systemname,
                    IDIA = il.IDIA


                };

                db.Entry(f).State = EntityState.Modified;
             
                await db.SaveChangesAsync();



                //save to ia profile
                NewIA nia = new NewIA();

      
                await nia.UpdateIAProfileNew(il);
                


                return RedirectToAction("IAList");


            }


            ViewBag.TypeSystem = new SelectList(db1.SystemSelections.Where(a => a.res_region == il.region), "IDSystem", "SYSTEMS", il.systemID);

            return View(il);

        }

        #endregion


        #region IA Profile

        public ActionResult ListIAProfile(string id = null)
        {


            var aa = db.IA_PROFILE.Where(x => x.IAID == id).ToList();
            var yr = db.YearReferences.First(z => z.idyear == 1);
          

            foreach (var a in aa)
            {

                a.SHARING_IA = Convert.ToInt32(yr.year);
                
            }


            Session["ianame"] = aa[0].IA_NAME.ToString();


            if (aa == null)
            {

                return RedirectToAction("Login", "Account", "");
            
            }


            

            return View(aa);
        
        }


        public ActionResult EditIAProfile(int? id)
        {

            if (id == null)
            {

                return RedirectToAction("Login", "Account", "");
            }

          
            var il = db.IA_PROFILE.Find(id);
         
            return View(il);

        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditIAProfile(IA_PROFILE il)
        {


            if (ModelState.IsValid)
            {
              

                db.Entry(il).State = EntityState.Modified;
                await db.SaveChangesAsync();
 

                return RedirectToAction("IAList");


            }


         
            return View(il);

        }

        #endregion



        #region IA Area


        public ActionResult IAAreaPeryear(int yr1 = 0, string iaid = null)
        {

            if (Session["regiontolog"] == null)
            {

                return RedirectToAction("Login", "Account", "");
            }
            if (iaid== null)
            {

                return RedirectToAction("Login", "Account", "");
            }
            if (Session["ianame"] == null)
            {

                return RedirectToAction("Login", "Account", "");
            }


            Session["iaid"] = iaid.ToString();
            return View(db.IA_AREA_VIEW.Where(f => f.YEAR_COVERED == yr1 && f.IDIA == iaid).ToList());
        }



        public ActionResult CreateIAArea()
        {



            if (Session["regiontolog"] == null)
            {

                return RedirectToAction("Login", "Account", "");

            }

            string region = Session["regiontolog"].ToString();

            IA_AREA_VIEW iav = new IA_AREA_VIEW();
            var yr = db.YearReferences.First(z => z.idyear == 1);
         

            if (Session["iaid"] == null)
            {

                return RedirectToAction("Login", "Account", "");
            }

            iav.IDIA = Session["iaid"].ToString();
            iav.YEAR_COVERED = yr.year;

            ViewBag.listProvince = new SelectList(db1.TblProvinces.Where(a => a.ID_POLITICAL > 0), "ID_PROVINCE", "PROVINCE");
            ViewBag.district = new SelectList(db1.TblDistricts, "ID_DISTRICT", "DISTRICT");

            return View(iav);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateIAArea(IA_AREA_VIEW il)
        {

          

            if (ModelState.IsValid)
            {
                var f = new IA_AREA()
                {

                    DISTRICT = il.ID_DISTRICT,
                    FARMERS = il.FARMERS,
                    FARMERS_FEMALE = il.FARMERS_FEMALE,
                    FUSA = il.FUSA,
                    SERVICE_AREA = il.SERVICE_AREA,
                    TSA_NO = il.TSA_NO,
                    FB = il.FB,
                    IA_NAME = "",
                    IDIA = il.IDIA,
                    MUNICIPALITY = il.MUNICIPALITY,
                    MAIN = "",
                    NONOPERATIONAL = il.NONOPERATIONAL,
                    OPERATIONAL = il.OPERATIONAL,
                    PROVINCE = il.ID_PROVINCE,
                    YEAR_COVERED = il.YEAR_COVERED


                };

                db.IA_AREA.Add(f);
                await db.SaveChangesAsync();


             

                return RedirectToAction("IAList");


            }


            ViewBag.listProvince = new SelectList(db1.TblProvinces.Where(a => a.ID_POLITICAL > 0), "ID_PROVINCE", "PROVINCE",il.PROVINCE);
            ViewBag.district = new SelectList(db1.TblDistricts, "ID_DISTRICT", "DISTRICT",il.DISTRICT);

            return View(il);

        }



        public ActionResult EditIAArea(int? id)
        {


            var il = db.IA_AREA_VIEW.Find(id);


            if (il == null)
            {

                return RedirectToAction("Login", "Account", "");

            }


            ViewBag.listProvince = new SelectList(db1.TblProvinces.Where(a => a.ID_POLITICAL > 0), "ID_PROVINCE", "PROVINCE",il.ID_PROVINCE);
            ViewBag.district = new SelectList(db1.TblDistricts, "ID_DISTRICT", "DISTRICT",il.ID_DISTRICT);

            return View(il);

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditIAArea(IA_AREA_VIEW il)
        {



            if (ModelState.IsValid)
            {
                var f = new IA_AREA()
                {

                    DISTRICT = il.ID_DISTRICT,
                    FARMERS = il.FARMERS,
                    FARMERS_FEMALE = il.FARMERS_FEMALE,
                    FUSA = il.FUSA,
                    SERVICE_AREA = il.SERVICE_AREA,
                    TSA_NO = il.TSA_NO,
                    FB = il.FB,
                    IA_NAME = "",
                    IDIA = il.IDIA,
                    MUNICIPALITY = il.MUNICIPALITY,
                    MAIN = "",
                    NONOPERATIONAL = il.NONOPERATIONAL,
                    OPERATIONAL = il.OPERATIONAL,
                    PROVINCE = il.ID_PROVINCE,
                    YEAR_COVERED = il.YEAR_COVERED,
                    IAAREAID = il.IAAREAID


                };

                db.Entry(f).State = EntityState.Modified;
                await db.SaveChangesAsync();



                return RedirectToAction("IAList");


            }


            ViewBag.listProvince = new SelectList(db1.TblProvinces.Where(a => a.ID_POLITICAL > 0), "ID_PROVINCE", "PROVINCE", il.PROVINCE);
            ViewBag.district = new SelectList(db1.TblDistricts, "ID_DISTRICT", "DISTRICT", il.DISTRICT);

            return View(il);

        }







        public ActionResult DeleteIAArea(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IA_AREA_VIEW fSEntry = db.IA_AREA_VIEW.Find(id);
            if (fSEntry == null)
            {
                return HttpNotFound();
            }
            return View(fSEntry);
        }

        // POST: IA/Delete/5
        [HttpPost, ActionName("DeleteIAArea")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteIAArea(int id)
        {
            IA_AREA fSEntry = db.IA_AREA.Find(id);
            db.IA_AREA.Remove(fSEntry);
            db.SaveChanges();
            return RedirectToAction("IAList");
        }








        #endregion



        #region Canals


        public ActionResult IACanalsPeryear(int yr1 = 0, string iaid = null)
        {

            if (Session["regiontolog"] == null)
            {

                return RedirectToAction("Login", "Account", "");
            }
            if (iaid == null)
            {

                return RedirectToAction("Login", "Account", "");
            }
            if (Session["ianame"] == null)
            {

                return RedirectToAction("Login", "Account", "");
            }


            Session["iaid"] = iaid.ToString();
            return View(db.CANALS_STATIONING.Where(f => f.YEAR_COVERED == yr1 && f.IDIA == iaid).ToList());
        }


        public ActionResult CreateIACanal()
        {


            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Main", Value = "Main" });
            items.Add(new SelectListItem { Text = "Lateral", Value = "Lateral" });
            
            ViewBag.typecanal = new SelectList(items, "Value", "Text");



            if (Session["iaid"] == null)
            {

                return RedirectToAction("Login", "Account", "");
            }


            var yearReference = db.YearReferences.First(z => z.idyear == 1);
        
            CANALS_STATIONING cs = new CANALS_STATIONING();

            cs.IDIA = Session["iaid"].ToString();
            cs.YEAR_COVERED = yearReference.year;
            return View(cs);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateIACanal(CANALS_STATIONING cs)
        {


            if (ModelState.IsValid)
            {
             

                db.CANALS_STATIONING.Add(cs);
                await db.SaveChangesAsync();

                return RedirectToAction("IAList");


            }

            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Main", Value = "Main" });
            items.Add(new SelectListItem { Text = "Lateral", Value = "Lateral" });

            ViewBag.typecanal = new SelectList(items, "Value", "Text", cs.TYPE_CANAL);

   
            return View(cs);

        }



        public ActionResult EditIACanal(int? id)
        {


            var il = db.CANALS_STATIONING.Find(id);



            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Main", Value = "Main" });
            items.Add(new SelectListItem { Text = "Lateral", Value = "Lateral" });

            ViewBag.typecanal = new SelectList(items, "Value", "Text",il.TYPE_CANAL);

            if (il == null)
            {

                return RedirectToAction("Login", "Account", "");

            }


            //ViewBag.listProvince = new SelectList(db1.TblProvinces.Where(a => a.ID_POLITICAL > 0), "ID_PROVINCE", "PROVINCE", il.ID_PROVINCE);
            //ViewBag.district = new SelectList(db1.TblDistricts, "ID_DISTRICT", "DISTRICT", il.ID_DISTRICT);

            return View(il);

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditIACanal(CANALS_STATIONING il)
        {



            if (ModelState.IsValid)
            {
              

                db.Entry(il).State = EntityState.Modified;
                await db.SaveChangesAsync();



                return RedirectToAction("IAList");


            }

            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Main", Value = "Main" });
            items.Add(new SelectListItem { Text = "Lateral", Value = "Lateral" });

            ViewBag.typecanal = new SelectList(items, "Value", "Text", il.TYPE_CANAL);
         
            return View(il);

        }

        public ActionResult DeleteIACanal(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CANALS_STATIONING fSEntry = db.CANALS_STATIONING.Find(id);
            if (fSEntry == null)
            {
                return HttpNotFound();
            }
            return View(fSEntry);
        }

        // POST: IA/Delete/5
        [HttpPost, ActionName("DeleteIACanal")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteIACanal(int id)
        {
            CANALS_STATIONING fSEntry = db.CANALS_STATIONING.Find(id);
            db.CANALS_STATIONING.Remove(fSEntry);
            db.SaveChanges();
            return RedirectToAction("IAList");
        }











        #endregion


        #region Map


        public ActionResult IndexIAMap(int? id)
        {



            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IA_PROFILE ip = db.IA_PROFILE.Find(id);
            if (ip == null)
            {
                return HttpNotFound();
            }

            Session["idsystem"] = id;
            return View(ip);


        }

        public ActionResult IndexIAMapNationwide()
        {
        
            return View();
        }


        public ActionResult IndexDelineate(int id = 0, double lati = 0, double longi = 0)
        {
            //0212D24B-8B9F-4544-89E3-0369ECF9B213
            //   string strauto = "0212D24B-8B9F-4544-89E3-0369ECF9B213";


            Session["idsystem"] = id;


            string Coordinates = "[";
            Coordinates += "{";
            Coordinates += string.Format("\"latitude\": \"{0}\"" + ",", lati);
            Coordinates += string.Format("\"longitude\": \"{0}\"" + ",", longi);
            Coordinates += "}]";
            ViewBag.Coordinates = Coordinates;




            ViewBag.MarkersPICpoly = "[]";
            ViewBag.Markers = "[]";




            //new 


            var systemsprofile = db.MappingDataIAViews.Where(w => w.ProfileID == id).ToList();


            return PartialView("_IndexDelineate", systemsprofile);
            //    return View();
        }


        public ActionResult IndexDelineateNationwide()
        {
            //0212D24B-8B9F-4544-89E3-0369ECF9B213
            //   string strauto = "0212D24B-8B9F-4544-89E3-0369ECF9B213";

            ViewBag.Coordinates = "[]";




            ViewBag.MarkersPICpoly = "[]";
            ViewBag.Markers = "[]";




        


            var mdivs = db.MappingDataIAViews.ToList();
            var ips = db.IA_PROFILE.Where(a => a.AMOUNT_NIA_CIA > 0).ToList();

           

            var mapmodel = new MapModel 
            { 
                
            mappingDataIAView = mdivs,
            iA_PROFILE = ips

            };


            return PartialView("_IndexDelineateNationwide", mapmodel);
          
        }

        public ActionResult UpdateCoord(double latitude, double longitude, int id)
        {


            var iaCoordinate = db.IA_PROFILE.Where(d => d.IAPROFILEID == id).FirstOrDefault();

            iaCoordinate.AMOUNT_NIA_CIA = latitude;
            iaCoordinate.AMOUNT_NIA_FIA = longitude;
            db.SaveChanges();

            string url = Url.Action("IndexDelineate", "IA", new { id = id, lati = latitude, longi = longitude });

            return Json(new { success = true, url = url });


        }



        public JsonResult Add(Polygon emp)
        {


            if (Session["idsystem"] == null)
            {
                return Json(new
                {
                    redirectUrl = Url.Action("Login", "Account"),
                    isRedirect = true
                });
            
            }

            emp.profileid = Convert.ToInt32(Session["idsystem"].ToString()); 
            return Json(empDB.AddPolygonIA(emp), JsonRequestBehavior.AllowGet);

        }

        public JsonResult Delete(int id = 0)
        {

            return Json(empDB.DeleteIA(id), JsonRequestBehavior.AllowGet);

        }
        #endregion


        #region officers



        public ActionResult IAOfficers(int yr1 = 0, string iaid = null)
        {

            if (Session["regiontolog"] == null)
            {

                return RedirectToAction("Login", "Account", "");
            }
            if (iaid == null)
            {

                return RedirectToAction("Login", "Account", "");
            }
            if (Session["ianame"] == null)
            {

                return RedirectToAction("Login", "Account", "");
            }


            Session["iaid"] = iaid.ToString();
            return View(db.BOARDs.Where(f => f.YEAR_COVERED == yr1 && f.IDIA == iaid).ToList());
        }

        public ActionResult CreateIAOfficer()
        {


            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "MALE", Value = "MALE" });
            items.Add(new SelectListItem { Text = "FEMALE", Value = "FEMALE" });

            ViewBag.GENDERname = new SelectList(items, "Value", "Text");
            ViewBag.boardposition = new SelectList(db.LIST_OF_POSITION, "POSITION", "POSITION");
            ViewBag.OFFICERposition = new SelectList(db.LIST_OF_POSITION, "POSITION", "POSITION");



            if (Session["iaid"] == null)
            {

                return RedirectToAction("Login", "Account", "");
            }


            var yearReference = db.YearReferences.First(z => z.idyear == 1);

            BOARD cs = new BOARD();

            cs.IDIA = Session["iaid"].ToString();
            cs.YEAR_COVERED = yearReference.year;
            return View(cs);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateIAOfficer(BOARD cs)
        {


            if (ModelState.IsValid)
            {


                db.BOARDs.Add(cs);
                await db.SaveChangesAsync();

                return RedirectToAction("IAOfficers");


            }

            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "MALE", Value = "MALE" });
            items.Add(new SelectListItem { Text = "FEMALE", Value = "FEMALE" });

            ViewBag.GENDERname = new SelectList(items, "Value", "Text",cs.GENDER);
            ViewBag.boardposition = new SelectList(db.LIST_OF_POSITION, "POSITION", "POSITION",cs.POSITION);
            ViewBag.OFFICERposition = new SelectList(db.LIST_OF_POSITION, "POSITION", "POSITION", cs.POSITION);


            return View(cs);

        }





        public ActionResult EditIAOfficer(int? id)
        {


            var il = db.BOARDs.Find(id);



            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "MALE", Value = "MALE" });
            items.Add(new SelectListItem { Text = "FEMALE", Value = "FEMALE" });

            ViewBag.GENDERname = new SelectList(items, "Value", "Text",il.GENDER);
            ViewBag.boardposition = new SelectList(db.LIST_OF_POSITION, "POSITION", "POSITION", il.POSITION);
            ViewBag.OFFICERposition = new SelectList(db.LIST_OF_POSITION, "POSITION", "POSITION", il.OFFICER_POSITION);



            if (il == null)
            {

                return RedirectToAction("Login", "Account", "");

            }


            //ViewBag.listProvince = new SelectList(db1.TblProvinces.Where(a => a.ID_POLITICAL > 0), "ID_PROVINCE", "PROVINCE", il.ID_PROVINCE);
            //ViewBag.district = new SelectList(db1.TblDistricts, "ID_DISTRICT", "DISTRICT", il.ID_DISTRICT);

            return View(il);

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditIAOfficer(BOARD il)
        {



            if (ModelState.IsValid)
            {


                db.Entry(il).State = EntityState.Modified;
                await db.SaveChangesAsync();



                return RedirectToAction("IAOfficers");


            }

            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "MALE", Value = "MALE" });
            items.Add(new SelectListItem { Text = "FEMALE", Value = "FEMALE" });

            ViewBag.GENDERname = new SelectList(items, "Value", "Text", il.GENDER);
            ViewBag.boardposition = new SelectList(db.LIST_OF_POSITION, "POSITION", "POSITION", il.POSITION);
            ViewBag.OFFICERposition = new SelectList(db.LIST_OF_POSITION, "POSITION", "POSITION", il.OFFICER_POSITION);


            return View(il);

        }

        public ActionResult DeleteIAOfficer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOARD fSEntry = db.BOARDs.Find(id);
            if (fSEntry == null)
            {
                return HttpNotFound();
            }
            return View(fSEntry);
        }

        // POST: IA/Delete/5
        [HttpPost, ActionName("DeleteIAOfficer")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteIAOfficer(int id)
        {
            BOARD fSEntry = db.BOARDs.Find(id);
            db.BOARDs.Remove(fSEntry);
            db.SaveChanges();
            return RedirectToAction("IAOfficers");
        }
















        #endregion




        #endregion



















    }
}
