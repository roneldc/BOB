using AccompProject.Models;
using AccompProject.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AccompProject.Controllers
{

    public class POWController : Controller
    {
        private AccomplishmentEntities db = new AccomplishmentEntities();

        // GET: PhysicalAccomp
        public ActionResult Index(string id)
        {

            ViewBag.IDAccomp = id;
            Session["idaccomp"] = id;

            var powaccomp = db.Pow_View
                .Where(a => a.IDAccomp == id);



            if (powaccomp == null)
            {
                return HttpNotFound();
            }
            // return View(asa);
            return PartialView("_Index", powaccomp);



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



        public ActionResult Create(string Id)
        {

            //ACCOMPLISHMENT Fview = new ACCOMPLISHMENT();
            //Fview.IDAccomp = Id;

            //      Pow_View financ = db.Pow_View.Find(Id);
            //Session["subproject"] = financ.subproject;
            //if (financ == null)
            //{
            //    return HttpNotFound();
            //}
            // return View(financ);
            //   financ.IDAccomp = Id;
            //      return PartialView("_Create", financ);
            return PartialView("_Create");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pow_View FD)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var iddam = Guid.NewGuid();
                    var PA = new tblPOW_Main()
                    {
                        id_Main = iddam.ToString(),
                        IDAccomp = FD.IDAccomp,
                        projectTitle = FD.projectTitle,
                        projectDescription = FD.projectDescription,
                        progNo = FD.progNo,
                        powYear = FD.powYear,
                        powDate = FD.powDate,
                        serviceArea = FD.serviceArea,
                        waterSource = FD.waterSource,
                        drainageArea = FD.drainageArea,
                        dependArea = FD.dependArea,
                        rotr_Latitude = FD.rotr_Latitude,
                        rotr_Longitude = FD.rotr_Longitude,
                        reservoir_Latitude = FD.reservoir_Latitude,
                        reservoir_Longitude = FD.reservoir_Longitude,
                        gwp_Latitude = FD.gwp_Latitude,
                        gwp_Longitude = FD.gwp_Longitude,
                        other_typeOfIrrig = FD.other_typeOfIrrig,
                        other_Latitude = FD.other_Latitude,
                        other_Longitude = FD.other_Longitude,
                        opc_origGOP = FD.opc_origGOP,
                        opc_revGOP = FD.opc_revGOP,
                        opc_origLP = FD.opc_origLP,
                        opc_revLP = FD.opc_revLP,
                        opc_origLGU = FD.opc_origLGU,
                        opc_revLGU = FD.opc_revLGU,
                        opc_origIA = FD.opc_origIA,
                        opc_revIA = FD.opc_revIA,
                        opc_Other = FD.opc_Other,
                        opc_origOther = FD.opc_origOther,
                        opc_revOther = FD.opc_revOther,
                        main_new = FD.main_new,
                        main_restored = FD.main_restored,
                        main_earthCanal = FD.main_earthCanal,
                        main_lineCanal = FD.main_lineCanal,
                        main_structure = FD.main_structure,
                        main_coconet = FD.main_coconet,
                        main_roads = FD.main_roads,
                        main_gravel = FD.main_gravel,
                        main_Repair = FD.main_Repair,
                        main_RepairValue = FD.main_RepairValue,
                        main_rehab = FD.main_rehab,
                        main_Activity = FD.main_Activity,
                        main_ActivityValue = FD.main_ActivityValue,
                        origDateStart = FD.origDateStart,
                        origDateEnd = FD.origDateEnd,
                        revDateStart = FD.revDateStart,
                        revDateEnd = FD.revDateEnd


                    };

                    db.tblPOW_Main.Add(PA);
                    db.SaveChanges();

                    var PAA = new tblPOW_Breakdown()
                    {
                        id_Main = iddam.ToString(),
                        IDAccomp = FD.IDAccomp,
                        cwField_NIA = FD.cwField_NIA,
                        cwField_LGU = FD.cwField_LGU,
                        cwField_IA = FD.cwField_IA,
                        cwCO_NIA = FD.cwCO_NIA,
                        cwCO_LGU = FD.cwCO_LGU,
                        cwCO_IA = FD.cwCO_IA,
                        fawField_NIA = FD.fawField_NIA,
                        fawField_LGU = FD.fawField_LGU,
                        fawField_IA = FD.fawField_IA,
                        fawCO_NIA = FD.fawCO_NIA,
                        fawCO_LGU = FD.fawCO_LGU,
                        fawCO_IA = FD.fawCO_IA,
                        field_devProg = FD.field_devProg,
                        co_devProg = FD.co_devProg,
                        field_conSurvey = FD.field_conSurvey,
                        co_conSurvey = FD.co_conSurvey,
                        field_parcelMap = FD.field_parcelMap,
                        co_parcelMap = FD.co_parcelMap,
                        field_rightOfWay = FD.field_rightOfWay,
                        co_rightOfWay = FD.co_rightOfWay,
                        field_procureEquip = FD.field_procureEquip,
                        co_procureEquip = FD.co_procureEquip,
                        field_fieldSupport = FD.field_fieldSupport,
                        co_fieldSupport = FD.co_fieldSupport,
                        field_consulting = FD.field_consulting,
                        co_consulting = FD.co_consulting,
                        field_taxes = FD.field_taxes,
                        co_taxes = FD.co_taxes,
                        field_contingencies = FD.field_contingencies,
                        co_contingencies = FD.co_contingencies,
                        field_GOP = FD.field_GOP,
                        field_LP = FD.field_LP,
                        co_GOP = FD.co_GOP,
                        co_LP = FD.co_LP


                    };

                    db.tblPOW_Breakdown.Add(PAA);
                    db.SaveChanges();

                    var PAAA = new tblPOW_Work()
                    {


                        id_Main = iddam.ToString(),
                        IDAccomp = FD.IDAccomp,
                        contract = FD.contract,
                        forceAccount = FD.forceAccount,
                        otherWorks = FD.otherWorks,
                        workStart = FD.workStart,
                        daysComplete = FD.daysComplete,
                        work_new = FD.work_new,
                        work_restored = FD.work_restored,
                        work_earthCanal = FD.work_earthCanal,
                        work_lineCanal = FD.work_lineCanal,
                        work_structure = FD.work_structure,
                        work_coconet = FD.work_coconet,
                        work_roads = FD.work_roads,
                        work_gravel = FD.work_gravel,
                        work_Repair = FD.work_Repair,
                        work_RepairValue = FD.work_RepairValue,
                        work_rehab = FD.work_rehab,
                        work_Activity = FD.work_Activity,
                        work_ActivityValue = FD.work_ActivityValue

                    };


                    db.tblPOW_Work.Add(PAAA);
                    db.SaveChanges();



                    string url = Url.Action("Index", "POW", new { id = FD.IDAccomp });

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


        public ActionResult MyEdit(string id)
        {


            Pow_View FD = db.Pow_View.Find(id);


            if (FD == null)
            {
                return HttpNotFound();
            }




            return PartialView("_MyEdit", FD);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyEdit(Pow_View FD)
        {

            if (ModelState.IsValid)
            {

                var PA = new tblPOW_Main()
                {
                    id_Main = FD.id_Main,
                    IDAccomp = FD.IDAccomp,
                    projectTitle = FD.projectTitle,
                    projectDescription = FD.projectDescription,
                    progNo = FD.progNo,
                    powYear = FD.powYear,
                    powDate = FD.powDate,
                    serviceArea = FD.serviceArea,
                    waterSource = FD.waterSource,
                    drainageArea = FD.drainageArea,
                    dependArea = FD.dependArea,
                    rotr_Latitude = FD.rotr_Latitude,
                    rotr_Longitude = FD.rotr_Longitude,
                    reservoir_Latitude = FD.reservoir_Latitude,
                    reservoir_Longitude = FD.reservoir_Longitude,
                    gwp_Latitude = FD.gwp_Latitude,
                    gwp_Longitude = FD.gwp_Longitude,
                    other_typeOfIrrig = FD.other_typeOfIrrig,
                    other_Latitude = FD.other_Latitude,
                    other_Longitude = FD.other_Longitude,
                    opc_origGOP = FD.opc_origGOP,
                    opc_revGOP = FD.opc_revGOP,
                    opc_origLP = FD.opc_origLP,
                    opc_revLP = FD.opc_revLP,
                    opc_origLGU = FD.opc_origLGU,
                    opc_revLGU = FD.opc_revLGU,
                    opc_origIA = FD.opc_origIA,
                    opc_revIA = FD.opc_revIA,
                    opc_Other = FD.opc_Other,
                    opc_origOther = FD.opc_origOther,
                    opc_revOther = FD.opc_revOther,
                    main_new = FD.main_new,
                    main_restored = FD.main_restored,
                    main_earthCanal = FD.main_earthCanal,
                    main_lineCanal = FD.main_lineCanal,
                    main_structure = FD.main_structure,
                    main_coconet = FD.main_coconet,
                    main_roads = FD.main_roads,
                    main_gravel = FD.main_gravel,
                    main_Repair = FD.main_Repair,
                    main_RepairValue = FD.main_RepairValue,
                    main_rehab = FD.main_rehab,
                    main_Activity = FD.main_Activity,
                    main_ActivityValue = FD.main_ActivityValue,
                    origDateStart = FD.origDateStart,
                    origDateEnd = FD.origDateEnd,
                    revDateStart = FD.revDateStart,
                    revDateEnd = FD.revDateEnd


                };


                db.Entry(PA).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();





                var PAA = new tblPOW_Breakdown()
                {
                    id_breakdown = FD.id_breakdown,
                    id_Main = FD.id_Main,
                    IDAccomp = FD.IDAccomp,
                    cwField_NIA = FD.cwField_NIA,
                    cwField_LGU = FD.cwField_LGU,
                    cwField_IA = FD.cwField_IA,
                    cwCO_NIA = FD.cwCO_NIA,
                    cwCO_LGU = FD.cwCO_LGU,
                    cwCO_IA = FD.cwCO_IA,
                    fawField_NIA = FD.fawField_NIA,
                    fawField_LGU = FD.fawField_LGU,
                    fawField_IA = FD.fawField_IA,
                    fawCO_NIA = FD.fawCO_NIA,
                    fawCO_LGU = FD.fawCO_LGU,
                    fawCO_IA = FD.fawCO_IA,
                    field_devProg = FD.field_devProg,
                    co_devProg = FD.co_devProg,
                    field_conSurvey = FD.field_conSurvey,
                    co_conSurvey = FD.co_conSurvey,
                    field_parcelMap = FD.field_parcelMap,
                    co_parcelMap = FD.co_parcelMap,
                    field_rightOfWay = FD.field_rightOfWay,
                    co_rightOfWay = FD.co_rightOfWay,
                    field_procureEquip = FD.field_procureEquip,
                    co_procureEquip = FD.co_procureEquip,
                    field_fieldSupport = FD.field_fieldSupport,
                    co_fieldSupport = FD.co_fieldSupport,
                    field_consulting = FD.field_consulting,
                    co_consulting = FD.co_consulting,
                    field_taxes = FD.field_taxes,
                    co_taxes = FD.co_taxes,
                    field_contingencies = FD.field_contingencies,
                    co_contingencies = FD.co_contingencies,
                    field_GOP = FD.field_GOP,
                    field_LP = FD.field_LP,
                    co_GOP = FD.co_GOP,
                    co_LP = FD.co_LP


                };

                db.Entry(PAA).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                var PAAA = new tblPOW_Work()
                {

                    id_work = FD.id_work,
                    id_Main = FD.id_Main,
                    IDAccomp = FD.IDAccomp,
                    contract = FD.contract,
                    forceAccount = FD.forceAccount,
                    otherWorks = FD.otherWorks,
                    workStart = FD.workStart,
                    daysComplete = FD.daysComplete,
                    work_new = FD.work_new,
                    work_restored = FD.work_restored,
                    work_earthCanal = FD.work_earthCanal,
                    work_lineCanal = FD.work_lineCanal,
                    work_structure = FD.work_structure,
                    work_coconet = FD.work_coconet,
                    work_roads = FD.work_roads,
                    work_gravel = FD.work_gravel,
                    work_Repair = FD.work_Repair,
                    work_RepairValue = FD.work_RepairValue,
                    work_rehab = FD.work_rehab,
                    work_Activity = FD.work_Activity,
                    work_ActivityValue = FD.work_ActivityValue

                };
                db.Entry(PAAA).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();


                string url = Url.Action("Index", "POW", new { id = FD.IDAccomp });

                return Json(new { success = true, url = url });
                //return Json(new { suceess = true });
            }

            return PartialView("_MyEdit", FD);

        }

        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhysicalAccompView FD = db.PhysicalAccompViews.Find(id);

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

            PhysicalAccomp FD = db.PhysicalAccomps.Find(id);
            db.PhysicalAccomps.Remove(FD);
            db.SaveChanges();

            string url = Url.Action("Index", "PhysicalAccomp", new { id = FD.IDAccomp });

            return Json(new { success = true, url = url });

        }




        public ActionResult EditFile(string id, string subp, string mainp, double amt)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //DBMView dBMView = db.DBMViews.Find(id);
            //var PAV = db.FileAccompViews
            //.Where(x => x.IDAccomp == id && x.mnt == mnt && x.yr == yr);

           // var PAV = db.FilePOWAccompViews.Where(x => x.IDAccomp == id); // == id && x.mnt == mnt && x.yr == yr);

            var PAV = db.FilePOWAccompViews.FirstOrDefault(x => x.IDAccomp == id);
           
            Session["idaccomp"] = id;
            Session["mainp"] = mainp;
            Session["subp"] = subp;
            Session["amt"] = amt;
         //  Session["mnt"] = mnt;
        //    Session["yr"] = yr;


            if (PAV == null)
            {
                //return HttpNotFound();
                return RedirectToAction("CreateFile");
            }
            return View(PAV);
        }

        // POST: DBM/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditFile(FilePOWAccompView PAV, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {


                if (upload != null && upload.ContentLength > 0)
                {

                    var DBB = db.FilePOWAccomps.Where(x => x.IDAccomp == PAV.IDAccomp);// || x.mnt == PAV.mnt || x.yr == PAV.yr);
                    if (DBB != null)
                    {
                        foreach (var courseenrollment in DBB)
                        {
                            db.FilePOWAccomps.Remove(courseenrollment);
                        }
                        db.SaveChanges();

                    }



                    var avatar1 = new FilePOWAccomp()
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        //     FileType = FileType.Avatar,
                        ContentType = upload.ContentType,
                        IDAccomp = PAV.IDAccomp
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        avatar1.Content = reader.ReadBytes(upload.ContentLength);
                    }

                    db.FilePOWAccomps.Add(avatar1);
                    db.SaveChanges();
                }




                return RedirectToAction("Edit", new { id = Session["idaccomp"].ToString() });
            }
            return View(PAV);
        }



        public ActionResult CreateFile()
        {
            return View();
        }

        // POST: DBM/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFile(FilePOWAccompView PAV, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {


                if (upload != null && upload.ContentLength > 0)
                {
              //      var mnt = Session["mnt"].ToString();
                  //  var yr = Session["yr"].ToString();
                    var avataraa = new FilePOWAccomp()
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        //FileType = FileType.Avatar,
                        ContentType = upload.ContentType,
                        IDAccomp = PAV.IDAccomp,
               //         mnt = int.Parse(mnt),
                //        yr = int.Parse(yr)
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        avataraa.Content = reader.ReadBytes(upload.ContentLength);
                    }

                    db.FilePOWAccomps.Add(avataraa);
                    db.SaveChanges();
                }




                return RedirectToAction("Edit", new { id = Session["idaccomp"].ToString() });
            }

            return View(PAV);
        }

        public FileContentResult Download(int id)
        {
            //declare byte array to get file content from database and string to store file name

            byte[] fileData;
            string fileName;
            //create object of LINQ to SQL class
            //  DBContext dataContext = new DBContext();
            //using LINQ expression to get record from database for given id value
            var record = from p in db.FilePOWAccomps
                         where p.FileId == id
                         select p;
            //only one record will be returned from database as expression uses condtion on primary field
            //so get first record from returned values and retrive file content (binary) and filename
            fileData = (byte[])record.FirstOrDefault().Content.ToArray();
            fileName = record.FirstOrDefault().FileName;
            //return file and provide byte file content and file name
            return File(fileData, "text", fileName);

        }



        //public FileContentResult Download(string id)
        //{
        //    //declare byte array to get file content from database and string to store file name

        //    byte[] fileData;
        //    string fileName;
        //    //create object of LINQ to SQL class
        //    //  DBContext dataContext = new DBContext();
        //    //using LINQ expression to get record from database for given id value
        //    var record = from p in db.FilePOWAccomps
        //                 where p.IDAccomp == id
        //                 select p;
        //    //only one record will be returned from database as expression uses condtion on primary field
        //    //so get first record from returned values and retrive file content (binary) and filename
        //    fileData = (byte[])record.First().Content.ToArray();
        //    fileName = record.First().FileName;
        //    //return file and provide byte file content and file name
        //    return File(fileData, "text", fileName);

        //}

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
