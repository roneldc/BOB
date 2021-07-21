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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core;

namespace AccompProject.Controllers
{
    public class PhysicalController : Controller
    {

        private AccomplishmentEntities db = new AccomplishmentEntities();

        // GET: Physical
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(string id)
        {



            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
          //  PhysicalDetail PD = db.PhysicalDetails.Find(id);
            var dte = Convert.ToDateTime(Session["asof"].ToString());
            var PD = db.PhysicalDetails.FirstOrDefault(a => a.IDAccomp == id && a.as_of == dte);

            if (PD == null)
            {
                return HttpNotFound();
            }
            return View(PD);


            
        }

        // POST: ACCOMPLISHMENT/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(PhysicalView viewmodel)
        //{

        //    try
        //    {


        //        if (ModelState.IsValid)
        //        {

        //            var PD = new PhysicalDetail()
        //            {

        //                IDAccomp = viewmodel.IDAccomp,
        //                as_of = viewmodel.as_of,
        //                IDPhysical = viewmodel.IDPhysical,
        //                newed = viewmodel.newed,
        //                restored = viewmodel.restored,
        //                rehab = viewmodel.rehab,
        //                canals = viewmodel.canals,
        //                canal_lining = viewmodel.canal_lining,
        //                structures = viewmodel.structures,
        //                roads = viewmodel.roads,
        //                farmer_beneficiaries = viewmodel.farmer_beneficiaries,
        //                jobs = viewmodel.jobs,
        //                remarks = viewmodel.remarks,
        //                new_accomp = viewmodel.new_accomp,
        //                resto_accomp = viewmodel.resto_accomp,
        //                rehab_accomp = viewmodel.rehab_accomp,
        //                canals_accomp = viewmodel.canals_accomp,
        //                canal_lining_accomp = viewmodel.canal_lining_accomp,
        //                structures_accomp = viewmodel.structures_accomp,
        //                roads_accomp = viewmodel.roads_accomp,
        //                Beneficiary_accomp = viewmodel.Beneficiary_accomp,
        //                JobGen = viewmodel.JobGen,
        //                Physical = viewmodel.Physical,
        //                Financial = viewmodel.Financial,
        //                Remarks_accomp = viewmodel.Remarks_accomp,
        //                status = viewmodel.status,
        //                Value_accomp = viewmodel.Value_accomp,
        //                Expenditures = viewmodel.Expenditures,
        //                Phy = viewmodel.Phy,
        //                Fin = viewmodel.Fin,
        //                FUSA = viewmodel.FUSA,
        //                OK = viewmodel.OK,
        //                MONITORING1 = viewmodel.MONITORING1,
        //                MONITORING2 = viewmodel.MONITORING2,
        //                PC = viewmodel.PC,
        //                VAL = viewmodel.VAL,
        //                EXP = viewmodel.EXP,
        //                SAMPLE = viewmodel.SAMPLE,
        //                saro = viewmodel.saro,
        //                asa = viewmodel.asa,
        //                p_new = viewmodel.p_new,
        //                p_resto = viewmodel.p_resto,
        //                p_rehab = viewmodel.p_rehab,
        //                p_canal = viewmodel.p_canal,
        //                p_canal_lining = viewmodel.p_canal_lining,
        //                p_structure = viewmodel.p_structure,
        //                p_road = viewmodel.p_road,
        //                p_job = viewmodel.p_job,
        //                p_fb = viewmodel.p_fb,
        //                disbursement = viewmodel.disbursement,
        //                RowVersion = viewmodel.RowVersion,
        //                mnt = viewmodel.mnt,
        //                year_covered = viewmodel.year_covered
        //            };

        //          //  db.PhysicalDetails.Attach(PD);
        //            db.Entry(PD).State = EntityState.Modified;


        //            db.SaveChanges();
        //            return RedirectToAction("Project Monitoring", "Index", "ACCOMPLISHMENT");
        //        }
        //    }
        //    //catch (Exception ex) {
        //    //    Console.WriteLine(ex);
        //    //}

        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        var entry = ex.Entries.Single();
        //        var clientValues = (PhysicalDetail)entry.Entity;
        //        var databaseEntry = (PhysicalDetail)entry.GetDatabaseValues().ToObject();

        //        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
        //        + "was modified by another user after you got the original value. The "
        //        + "edit operation was canceled and the current values in the database "
        //        + "have been displayed. If you still want to edit this record, click "
        //        + "the Save button again. Otherwise click the Back to List hyperlink.");
        //        viewmodel.RowVersion = databaseEntry.RowVersion;

        //    }

        //    return View(viewmodel);
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDAccomp,IDPhysical,newed,restored,rehab,canals,canal_lining,structures,roads,farmer_beneficiaries,jobs,remarks,new_accomp,resto_accomp,rehab_accomp,canals_accomp,canal_lining_accomp,structures_accomp,roads_accomp,Beneficiary_accomp,JobGen,Physical,Financial,Remarks_accomp,status,Value_accomp,Expenditures,Phy,Fin,FUSA,OK,MONITORING1,MONITORING2,PC,VAL,EXP,as_of,SAMPLE,saro,asa,p_new,p_resto,p_rehab,p_canal,p_canal_lining,p_structure,p_road,p_job,p_fb,disbursement,RowVersion,mnt,year_covered,mainproject, amount, province,subproject,municipality")] PhysicalDetail PD)
        {
            try
            {


                if (ModelState.IsValid)
                {

                    db.Entry(PD).State = System.Data.Entity.EntityState.Modified;


                    db.SaveChanges();
                    return RedirectToAction("Index","ACCOMPLISHMENT");
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var clientValues = (PhysicalDetail)entry.Entity;
                var databaseEntry = (PhysicalDetail)entry.GetDatabaseValues().ToObject();

                ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                + "was modified by another user after you got the original value. The "
                + "edit operation was canceled and the current values in the database "
                + "have been displayed. If you still want to edit this record, click "
                + "the Save button again. Otherwise click the Back to List hyperlink.");
                PD.RowVersion = databaseEntry.RowVersion;

            }

            return View(PD);
        }


    }
}