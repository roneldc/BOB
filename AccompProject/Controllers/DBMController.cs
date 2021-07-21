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
using PagedList;

namespace AccompProject.Controllers
{
    public class DBMController : Controller
    {
        private AccomplishmentEntities db = new AccomplishmentEntities();

        public ActionResult Autocomplete(string term)
        {
            //Session["regiontolog"] = "UPRIIS";
            if (Session["regiontolog"] == null)
            {

                return RedirectToAction("LogIn", "Account", new { area = "" });

            }
            string strauto = Session["regiontolog"].ToString();


            //all projects

            //var accomplishments = db.ACCOMPLISHMENTs
            // .OrderByDescending(r => r.year)
            // .Where(r => r.subproject.Contains(term) && r.region == strauto)
            //.Select(r => new
            //    {
            //        label = r.subproject
            //    })
            //.Take(5);



            var accomplishments = db.ACCOMPLISHMENTs
           .OrderByDescending(r => r.year)
           .Where(r => r.subproject.Contains(term) && r.region == strauto && r.year == 2019)
          .Select(r => new
          {
              label = r.subproject
          })
          .Take(5);

            return Json(accomplishments, JsonRequestBehavior.AllowGet);


        }



        public ActionResult Index(string id)
        {

            ViewBag.IDAccomp = id;
            Session["idaccomp"] = id;

            var physicalaccomp = db.DBMViews
                .OrderByDescending(a => a.yr).ThenByDescending(a => a.mnt)
                .Where(a => a.IDAccomp == id);



            if (physicalaccomp == null)
            {
                return HttpNotFound();
            }
            // return View(asa);
            return PartialView("_Index", physicalaccomp);



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




        public ActionResult MyEdit(int? id)
        {


            DBMView FD = db.DBMViews.Find(id);


            if (FD == null)
            {
                return HttpNotFound();
            }




            return PartialView("_MyEdit", FD);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyEdit(DBMView dBMView, HttpPostedFileBase upload)
        {

            if (ModelState.IsValid)
            {


                if (upload != null && upload.ContentLength > 0)
                {

                    var DBB = db.Files.Where(x => x.IDAccomp == dBMView.IDAccomp);
                    if (DBB != null)
                    {
                        foreach (var courseenrollment in DBB)
                        {
                            db.Files.Remove(courseenrollment);
                        }
                        db.SaveChanges();

                    }



                    var avatar = new File()
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        //     FileType = FileType.Avatar,
                        ContentType = upload.ContentType,
                        IDAccomp = dBMView.IDAccomp
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        avatar.Content = reader.ReadBytes(upload.ContentLength);
                    }

                    db.Files.Add(avatar);
                    db.SaveChanges();
                }



                var dbm = new DBM()
                {

                    IDAccomp = dBMView.IDAccomp,
                    CW_Bidded = dBMView.CW_Bidded,
                    CW_Schedule = dBMView.CW_Schedule,
                    FAW = dBMView.FAW,
                    FAW_Schedule = dBMView.FAW_Schedule,
                    remarks = dBMView.remarks,
                    bid_date = dBMView.bid_date,
                    IDDBM = dBMView.IDDBM,
                    mnt =  dBMView.mnt,
                    yr = dBMView.yr,
                    asof = dBMView.asof,
                    NoOfPOW = dBMView.noofpow,
                    AmountBidded = dBMView.amountbidded,
                    AmountFAW = dBMView.amountfaw,
                    UnbiddedAmount = dBMView.UnbiddedAmount,
                    UnbiddedNoOfPOW = dBMView.UnbiddedNoOfPOW
                    

                };

                db.Entry(dbm).State = EntityState.Modified;
                db.SaveChanges();
                string url = Url.Action("Index", "DBM", new { id = dBMView.IDAccomp });

                return Json(new { success = true, url = url });
            }
            return PartialView("_MyEdit", dBMView);

      

        }


        //public ActionResult Index(string searchTerm = null, int page = 1)
        //{


        //    //Session["regiontolog"] = "UPRIIS";

        //    if (Session["regiontolog"] == null)
        //    {

        //        return RedirectToAction("LogIn", "Account", new { area = "" });

        //    }
        //    string str = Session["regiontolog"].ToString();

        //    //all projects
        //    //var accomplishments = db.ACCOMPLISHMENTs
        //    //      .OrderByDescending(r => r.year)
        //    //      .Where(r => (searchTerm == null || r.subproject.Contains(searchTerm)) && r.region == str)
        //    //      .ToPagedList(page, 30);

        //    var accomplishments = db.ACCOMPLISHMENTs
        //        .OrderByDescending(r => r.year)
        //        .Where(r => (searchTerm == null || r.subproject.Contains(searchTerm)) && r.region == str && r.year == 2019)
        //        .ToPagedList(page, 30);




        //    if (Request.IsAjaxRequest())
        //    {
        //        return PartialView("_ListOfProjectDBM", accomplishments);
        //    }

        //    return View(accomplishments);


        //}

        //// GET: DBM/Details/5



        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DBMView dBMView = db.DBMViews.Find(id);
            if (dBMView == null)
            {
                return HttpNotFound();
            }
            return View(dBMView);
        }

        // GET: DBM/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DBM/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDAccomp,CW_Bidded,CW_Schedule,FAW,FAW_Schedule,remarks,bid_date")] DBM dBMView, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {


                if (upload != null && upload.ContentLength > 0)
                {
                    var avatar = new File()
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                         //FileType = FileType.Avatar,
                        ContentType = upload.ContentType,
                        IDAccomp = dBMView.IDAccomp
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        avatar.Content = reader.ReadBytes(upload.ContentLength);
                    }

                    db.Files.Add(avatar);
                    db.SaveChanges();
                }



                db.DBMs.Add(dBMView);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dBMView);
        }

        // GET: DBM/Edit/5
        //public ActionResult Edit(string id, string subp, string mainp, double amt)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    //DBMView dBMView = db.DBMViews.Find(id);
        //    var dBMView = db.DBMViews.Where(x => x.IDAccomp == id).FirstOrDefault();
            
        //    Session["idaccomp"] = id;
        //    Session["mainp"] = mainp;
        //    Session["subp"] = subp;
        //    Session["amt"] = amt;


        //    if (dBMView == null)
        //    {
        //        //return HttpNotFound();
        //        return RedirectToAction("Create");
        //    }
        //    return View(dBMView);
        //}

        // POST: DBM/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DBMView dBMView, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {


                if (upload != null && upload.ContentLength > 0)
                {

                    var DBB = db.Files.Where(x => x.IDAccomp == dBMView.IDAccomp);
                    if (DBB != null) {
                        foreach (var courseenrollment in DBB)
                        {
                            db.Files.Remove(courseenrollment);
                        }
                        db.SaveChanges();
                      
                    }
                  


                    var avatar = new File()
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        //     FileType = FileType.Avatar,
                        ContentType = upload.ContentType,
                        IDAccomp = dBMView.IDAccomp
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        avatar.Content = reader.ReadBytes(upload.ContentLength);
                    }

                    db.Files.Add(avatar);
                    db.SaveChanges();
                }



                var dbm = new DBM()
                {

                    IDAccomp = dBMView.IDAccomp,
                    CW_Bidded = dBMView.CW_Bidded,
                    CW_Schedule = dBMView.CW_Schedule,
                    FAW = dBMView.FAW,
                    FAW_Schedule = dBMView.FAW_Schedule,
                    remarks = dBMView.remarks,
                    bid_date = dBMView.bid_date

                };

                db.Entry(dbm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dBMView);
        }

        // GET: DBM/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DBMView dBMView = db.DBMViews.Find(id);
            if (dBMView == null)
            {
                return HttpNotFound();
            }
            return View(dBMView);
        }

        // POST: DBM/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DBMView dBMView = db.DBMViews.Find(id);
            db.DBMViews.Remove(dBMView);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public FileContentResult Download(string id)
        {
            //declare byte array to get file content from database and string to store file name
         
            byte[] fileData;
            string fileName;
            //create object of LINQ to SQL class
            //  DBContext dataContext = new DBContext();
            //using LINQ expression to get record from database for given id value
            var record = from p in db.Files
                         where p.IDAccomp == id
                         select p;
            //only one record will be returned from database as expression uses condtion on primary field
            //so get first record from returned values and retrive file content (binary) and filename
            fileData = (byte[])record.First().Content.ToArray();
            fileName = record.First().FileName;
            //return file and provide byte file content and file name
            return File(fileData, "text", fileName);

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
