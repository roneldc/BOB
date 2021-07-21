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
using System.Configuration;
using System.Data.SqlClient;

namespace AccompProject.Controllers
{
    public class IMTSSController : Controller
    {
        private AccomplishmentEntities db = new AccomplishmentEntities();

        public ActionResult Index(string id)
        {

          ViewBag.IDAccomp = id;
           Session["idaccomp"] = id;
            var imtss = db.IMTSSViewModels
                .OrderByDescending(a => a.YEAR_COVERED).ThenByDescending(a => a.MNT)
                .Where(a => a.IDACCOMP == id);




            if (imtss == null)
            {
                return HttpNotFound();
            }
            // return View(asa);
            return PartialView("_Index", imtss);

        }

        public ActionResult Create(string Id)
        {


            //var PARTIULARSload = db.IMTSSViewModels
            //            .OrderBy(r => r.ParticularsId)
            //            .Where(r => (r.categ == "Phy" || r.categ == "PhyFin"));

            //ViewBag.particularsid = new SelectList(PARTIULARSload, "ParticularsId", "Particulars");
            //ViewBag.subparticularsid = new SelectList(db.IMTSS_SubParticulars, "SubParticularsID", "SubParticulars");
            //ViewBag.subsubparticularsid = new SelectList(db.IMTSS_SubsubParticulars, "SubSubParticularsId", "SubSubParticulars");

            IMTSSViewModel imtssphysical = new IMTSSViewModel();
            imtssphysical.IDACCOMP = Id;


            return PartialView("_Create", imtssphysical);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "IDAccomp,mnt,year_covered,as_of,Particulars,subParticulars,subsubParticulars,total_for_year,target_actual,accomp_actual,quarter")]IMTSS_Physical imtssphysical)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        db.IMTSS_Physical.Add(imtssphysical);
        //        db.SaveChanges();

        //        string url = Url.Action("Index", "IMTSS", new { id = imtssphysical.IDAccomp });

        //        return Json(new { success = true, url = url });
        //        //return Json(new { suceess = true });
        //    }


        //    return PartialView("_Create", imtssphysical);

        //}

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(IMTSSViewModel IM)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var P1 = new IMTSS()
                    {

                      IDACCOMP = IM.IDACCOMP,
                      MNT= IM.MNT,
                      YEAR_COVERED = IM.YEAR_COVERED,
                      AS_OF = IM.AS_OF

                      
                    };

                    db.IMTSSes.Add(P1);
                    db.SaveChanges();
                    int imtssid = P1.IMTSSID;


                    var P2 = new IMTSS_Assistance() { 
                    
                        IDAccomp = IM.IDACCOMP,
                        Assist_Batches_Covered = IM.Assist_Batches_Covered,
                        Assist_Batches_TARGET =  IM.Assist_Batches_TARGET,
                        Assist_cost = IM.Assist_cost,
                        Assist_cost_target = IM.Assist_cost_target,
                        Assist_NoIA = IM.Assist_NoIA,
                        Assist_NoIA_TARGET = IM.Assist_NoIA_TARGET,
                        Assist_Participants = IM.Assist_Participants,
                        Assist_Participants_TARGET = IM.Assist_Participants_TARGET,
                        mnt = IM.MNT,
                        year_covered = IM.YEAR_COVERED,
                        as_of = IM.AS_OF
                    
                    
                    };
                    db.IMTSS_Assistance.Add(P2);
                    db.SaveChanges();


                    var P3 = new IMTSS_CapacityIA() { 

                        IDAccomp = IM.IDACCOMP,
                        CapacityIA_Batches_Covered = IM.CapacityIA_Batches_Covered,
                        CapacityIA_Batches_TARGET = IM.CapacityIA_Batches_TARGET,
                        CapacityIA_COST = IM.CapacityIA_COST,
                        CapacityIA_COST_TARGET = IM.CapacityIA_COST_TARGET,
                        CapacityIA_NoIA = IM.CapacityIA_NoIA,
                        CapacityIA_NoIA_TARGET = IM.CapacityIA_NoIA_TARGET,
                        CapacityIA_Participants = IM.CapacityIA_Participants,
                        CapacityIA_Participants_TARGET = IM.CapacityIA_Participants_TARGET,
                        mnt = IM.MNT,
                        year_covered = IM.YEAR_COVERED,
                        as_of = IM.AS_OF


                    };
                    db.IMTSS_CapacityIA.Add(P3);
                    db.SaveChanges();

                    var P4 = new IMTSS_CapacityIAWORKSHOP() { 
                    
                        IDAccomp = IM.IDACCOMP,
                        CapacityIAWORKSHOP_Batches_Covered= IM.CapacityIAWORKSHOP_Batches_Covered,
                        CapacityIAWORKSHOP_Batches_TARGET = IM.CapacityIAWORKSHOP_Batches_TARGET,
                        CapacityIAWORKSHOP_COST = IM.CapacityIAWORKSHOP_COST,
                        CapacityIAWORKSHOP_COST_TARGET = IM.CapacityIAWORKSHOP_COST_TARGET,
                        CapacityIAWORKSHOP_Participants = IM.CapacityIAWORKSHOP_Participants,
                        CapacityIAWORKSHOP_Participants_TARGET = IM.CapacityIAWORKSHOP_Participants_TARGET,
                        mnt = IM.MNT,
                        year_covered = IM.YEAR_COVERED,
                        as_of = IM.AS_OF
                    
                    
                    };
                    db.IMTSS_CapacityIAWORKSHOP.Add(P4);
                    db.SaveChanges();

                    var P5 = new IMTSS_CapacityStaff() { 
                    
                        IDAccomp = IM.IDACCOMP,
                        CapacityStaff_Batches_Covered = IM.CapacityStaff_Batches_Covered,
                        CapacityStaff_Batches_TARGET = IM.CapacityStaff_Batches_TARGET,
                        CapacityStaff_COST = IM.CapacityStaff_COST,
                        CapacityStaff_COST_TARGET = IM.CapacityStaff_COST_TARGET,
                        CapacityStaff_Participants = IM.CapacityStaff_Participants,
                        CapacityStaff_Participants_TARGET = IM.CapacityStaff_Participants_TARGET,
                        mnt = IM.MNT,
                        year_covered = IM.YEAR_COVERED,
                        as_of = IM.AS_OF
                    
                    
                    };
                    db.IMTSS_CapacityStaff.Add(P5);
                    db.SaveChanges();


                    var P6 = new IMTSS_CapacityStaffWORKSHOP() { 
                    
                        IDAccomp = IM.IDACCOMP,
                        CapacityStaffWORKSHOP_Batches_Covered = IM.CapacityStaffWORKSHOP_Batches_Covered,
                        CapacityStaffWORKSHOP_Batches_TARGET = IM.CapacityStaffWORKSHOP_Batches_TARGET,
                        CapacityStaffWORKSHOP_COST = IM.CapacityStaffWORKSHOP_COST,
                        CapacityStaffWORKSHOP_COST_TARGET = IM.CapacityStaffWORKSHOP_COST_TARGET,
                        CapacityStaffWORKSHOP_Participants = IM.CapacityStaffWORKSHOP_Participants,
                        CapacityStaffWORKSHOP_Participants_TARGET = IM.CapacityStaffWORKSHOP_Participants_TARGET,
                        mnt = IM.MNT,
                        year_covered = IM.YEAR_COVERED,
                        as_of = IM.AS_OF
                    
                    };

                    db.IMTSS_CapacityStaffWORKSHOP.Add(P6);
                    db.SaveChanges();

                  


                    var P8 = new IMTSS_IAOrganization() { 
                    
                        IDAccomp = IM.IDACCOMP,
                        Org_Area_Covered = IM.Org_Area_Covered,
                        Org_Area_Covered_TARGET = IM.Org_Area_Covered_TARGET,
                        Org_NoFB = IM.Org_NoFB,
                        Org_NoFB_TARGET = IM.Org_NoFB_TARGET,
                        Org_NoIA = IM.Org_NoIA,
                        Org_NoIA_TARGET = IM.Org_NoIA_TARGET,
                        mnt = IM.MNT,
                        year_covered = IM.YEAR_COVERED,
                        as_of = IM.AS_OF,
                        Org_NoMember = IM.Org_NoMember,
                        Org_NoMember_Target= IM.Org_NoMember_Target
                        
                    };



                    db.IMTSS_IAOrganization.Add(P8);
                    db.SaveChanges();


                    var P9 = new IMTSS_IARegistration() { 
                    
                        IDAccomp = IM.IDACCOMP,
                        Reg_Area_Covered = IM.Reg_Area_Covered,
                        Reg_Area_Covered_TARGET = IM.Reg_Area_Covered_TARGET,
                        Reg_NoFB = IM.Reg_NoFB,
                        Reg_NoFB_TARGET = IM.Reg_NoFB_TARGET,
                        Reg_NoIA = IM.Reg_NoIA,
                        Reg_NoIA_TARGET = IM.Reg_NoIA_TARGET,
                        mnt = IM.MNT,
                        year_covered = IM.YEAR_COVERED,
                        as_of = IM.AS_OF,
                        Reg_NoMember = IM.Reg_NoMember,
                        Reg_NoMember_Target = IM.Reg_NoIA_TARGET
                    
                    };


                    db.IMTSS_IARegistration.Add(P9);
                    db.SaveChanges();


                    var P10 = new IMTSS_IAStrengthening() { 
                    
                        IDAccomp = IM.IDACCOMP,
                        Str_Area_Covered = IM.Str_Area_Covered,
                        Str_Area_Covered_TARGET = IM.Str_Area_Covered_TARGET,
                        Str_NoFB = IM.Str_NoFB,
                        Str_NoFB_TARGET = IM.Str_NoFB_TARGET,
                        Str_NoIA = IM.Str_NoIA,
                        Str_NoIA_TARGET = IM.Str_NoIA_TARGET,
                        Str_Tsag = IM.Str_Tsag,
                        Str_Tsag_TARGET = IM.Str_Tsag_TARGET,
                        mnt = IM.MNT,
                        year_covered = IM.YEAR_COVERED,
                        as_of = IM.AS_OF
                    
                    
                    };

                    db.IMTSS_IAStrengthening.Add(P10);
                    db.SaveChanges();


                    var P11 = new IMTSS_IASustenance() { 
                    
                        IDAccomp = IM.IDACCOMP,
                        Sus_Area_Covered = IM.Sus_Area_Covered,
                        Sus_Area_Covered_TARGET = IM.Sus_Area_Covered_TARGET,
                        Sus_NoFB = IM.Sus_NoFB,
                        Sus_NoFB_TARGET = IM.Sus_NoFB_TARGET,
                        Sus_NoIA = IM.Sus_NoIA,
                        Sus_NoIA_TARGET = IM.Sus_NoIA_TARGET,
                        Sus_Tsag = IM.Sus_Tsag,
                        Sus_Tsag_TARGET = IM.Sus_Tsag_TARGET,
                        mnt = IM.MNT,
                        year_covered = IM.YEAR_COVERED,
                        as_of = IM.AS_OF
                    
                    };

                    db.IMTSS_IASustenance.Add(P11);
                    db.SaveChanges();

                    var P12 = new IMTSS_IDPPersonnel() { 
                    
                       IDAccomp = IM.IDACCOMP,
                       dailyjob = IM.dailyjob,
                       dailyjob_TARGET = IM.dailyjob_TARGET,
                       Contractual = IM.Contractual,
                       Contractual_TARGET = IM.Contractual_TARGET,
                       COST_ACCOMP = IM.COST_ACCOMP,
                       COST_TARGET = IM.COST_TARGET,
                       mnt = IM.MNT,
                       year_covered = IM.YEAR_COVERED,
                       as_of = IM.AS_OF,
                       joborder = IM.joborder,
                       joborder_target = IM.joborder_target
                    
                    
                    };
                    db.IMTSS_IDPPersonnel.Add(P12);
                    db.SaveChanges();



                    var P13 = new IMTSS_ModelOther()
                    {

                        ModOther_Area_Covered = IM.ModOther_Area_Covered,
                        ModOther_Area_Covered_TARGET = IM.ModOther_Area_Covered_TARGET,
                        ModOther_lined= IM.ModOther_Area_Covered_TARGET,
                        ModOther_lined_target= IM.ModOther_Area_Covered_TARGET,
                        ModOther_NoFB = IM.ModOther_NoFB,
                        ModOther_NoFB_TARGET = IM.ModOther_NoFB_TARGET,
                        ModOther_NoIA = IM.ModOther_NoIA,
                        ModOther_NoIA_TARGET = IM.ModOther_NoIA_TARGET,
                        ModOther_NoMember = IM.ModOther_NoMember,
                        ModOther_NoMember_Target = IM.ModOther_NoMember_Target,
                        ModOther_unlined = IM.ModOther_unlined,
                        ModOther_unlined_target = IM.ModOther_unlined_target,
                        mnt = IM.MNT,
                        year_covered = IM.YEAR_COVERED,
                        as_of = IM.AS_OF,
                        IDAccomp = IM.IDACCOMP
                    };

                    db.IMTSS_ModelOther.Add(P13);
                    db.SaveChanges();

                    var P14 = new IMTSS_ModContract()
                    {

                        ModCon_Area_Covered = IM.ModCon_Area_Covered,
                        ModCon_Area_Covered_TARGET = IM.ModCon_Area_Covered_TARGET,
                        ModCon_lined = IM.ModCon_Area_Covered_TARGET,
                        ModCon_lined_target = IM.ModCon_Area_Covered_TARGET,
                        ModCon_NoFB = IM.ModCon_NoFB,
                        ModCon_NoFB_TARGET = IM.ModCon_NoFB_TARGET,
                        ModCon_NoIA = IM.ModCon_NoIA,
                        ModCon_NoIA_TARGET = IM.ModCon_NoIA_TARGET,
                        ModCon_NoMember = IM.ModCon_NoMember,
                        ModCon_NoMember_Target = IM.ModCon_NoMember_Target,
                        ModCon_unlined = IM.ModCon_unlined,
                        ModCon_unlined_target = IM.ModCon_unlined_target,
                        mnt = IM.MNT,
                        ModCon_FUSA = IM.ModCon_FUSA,
                        ModCon_FUSA_target = IM.ModCon_FUSA_target,
                        ModCon_Remarks = IM.ModCon_Remarks,
                        ModCon_SA = IM.ModCon_SA,
                        ModCon_SA_target = IM.ModCon_SA_target,
                        year_covered = IM.YEAR_COVERED,
                        as_of = IM.AS_OF,
                        IDAccomp = IM.IDACCOMP

                    };

                    db.IMTSS_ModContract.Add(P14);
                    db.SaveChanges();

                    //var P13 = new IMTSS_Model1() { 
                    
                    //    IDAccomp =IM.IDACCOMP,
                    //    Mod1_Area_Covered = IM.Mod1_Area_Covered,
                    //    Mod1_Area_Covered_TARGET = IM.Mod1_Area_Covered_TARGET,
                    //    Mod1_NoFB = IM.Mod1_NoFB,
                    //    Mod1_NoFB_TARGET = IM.Mod1_NoFB_TARGET,
                    //    Mod1_NoIA = IM.Mod1_NoIA,
                    //    Mod1_NoIA_TARGET = IM.Mod1_NoIA_TARGET,
                    //    Mod1_NoMember = IM.Mod1_NoMember,
                    //    Mod1_NoMember_TARGET = IM.Mod1_NoMember_TARGET,
                    //    mnt = IM.MNT,
                    //    year_covered = IM.YEAR_COVERED,
                    //    as_of = IM.AS_OF
                    
                    
                    //};

                    //db.IMTSS_Model1.Add(P13);
                    //db.SaveChanges();



                    //var P14 = new IMTSS_Model2()
                    //{

                    //    IDAccomp = IM.IDACCOMP,
                    //    Mod2_Area_Covered = IM.Mod2_Area_Covered,
                    //    Mod2_Area_Covered_TARGET = IM.Mod2_Area_Covered_TARGET,
                    //    Mod2_NoFB = IM.Mod2_NoFB,
                    //    Mod2_NoFB_TARGET = IM.Mod2_NoFB_TARGET,
                    //    Mod2_NoIA = IM.Mod2_NoIA,
                    //    Mod2_NoIA_TARGET = IM.Mod2_NoIA_TARGET,
                    //    Mod2_NoMember = IM.Mod2_NoMember,
                    //    Mod2_NoMember_TARGET = IM.Mod2_NoMember_TARGET,
                    //    mnt = IM.MNT,
                    //    year_covered = IM.YEAR_COVERED,
                    //    as_of = IM.AS_OF


                    //};

                    //db.IMTSS_Model2.Add(P14);
                    //db.SaveChanges();

                    //var P15 = new IMTSS_Model3()
                    //{

                    //    IDAccomp = IM.IDACCOMP,
                    //    Mod3_Area_Covered = IM.Mod3_Area_Covered,
                    //    Mod3_Area_Covered_TARGET = IM.Mod3_Area_Covered_TARGET,
                    //    Mod3_NoFB = IM.Mod3_NoFB,
                    //    Mod3_NoFB_TARGET = IM.Mod3_NoFB_TARGET,
                    //    Mod3_NoIA = IM.Mod3_NoIA,
                    //    Mod3_NoIA_TARGET = IM.Mod3_NoIA_TARGET,
                    //    Mod3_NoMember = IM.Mod3_NoMember,
                    //    Mod3_NoMember_TARGET = IM.Mod3_NoMember_TARGET,
                    //    mnt = IM.MNT,
                    //    year_covered = IM.YEAR_COVERED,
                    //    as_of = IM.AS_OF


                    //};

                    //db.IMTSS_Model3.Add(P15);
                    //db.SaveChanges();

                    //var P16 = new IMTSS_Model4()
                    //{

                    //    IDAccomp = IM.IDACCOMP,
                    //    Mod4_Area_Covered = IM.Mod4_Area_Covered,
                    //    Mod4_Area_Covered_TARGET = IM.Mod4_Area_Covered_TARGET,
                    //    Mod4_NoFB = IM.Mod4_NoFB,
                    //    Mod4_NoFB_TARGET = IM.Mod4_NoFB_TARGET,
                    //    Mod4_NoIA = IM.Mod4_NoIA,
                    //    Mod4_NoIA_TARGET = IM.Mod4_NoIA_TARGET,
                    //    Mod4_NoMember = IM.Mod4_NoMember,
                    //    Mod4_NoMember_TARGET = IM.Mod4_NoMember_TARGET,
                    //    mnt = IM.MNT,
                    //    year_covered = IM.YEAR_COVERED,
                    //    as_of = IM.AS_OF


                    //};

                    //db.IMTSS_Model4.Add(P16);
                    //db.SaveChanges();


                    string url = Url.Action("Index", "IMTSS", new { id = IM.IDACCOMP });

                    return Json(new { success = true, url = url });
                    //return Json(new { suceess = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }


            return PartialView("_Create", IM);
        }



        public ActionResult MyEdit(int? id)
        {


            IMTSSViewModel imtssp = db.IMTSSViewModels.Find(id);


            if (imtssp == null)
            {
                return HttpNotFound();
            }


            //var PARTIULARSload = db.IMTSS_Particulars
            //           .OrderBy(r => r.ParticularsId)
            //           .Where(r => (r.categ == "Phy" || r.categ == "PhyFin"));

            //ViewBag.particularslang = new SelectList(PARTIULARSload, "ParticularsId", "Particulars", imtssp.Particulars);
            //ViewBag.subparticularslang = new SelectList(db.IMTSS_SubParticulars, "SubParticularsID", "SubParticulars", imtssp.subParticulars);
            //ViewBag.subsubparticularslang = new SelectList(db.IMTSS_SubsubParticulars, "SubSubParticularsId", "SubSubParticulars", imtssp.subsubparticulars);


            return PartialView("_MyEdit", imtssp);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult MyEdit([Bind(Include = "imtssID,IDAccomp,mnt,year_covered,as_of,Particulars,subParticulars,subsubParticulars,total_for_year,target_actual,accomp_actual,quarter")]IMTSS_Physical imtssphysical)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(imtssphysical).State = EntityState.Modified;
        //        db.SaveChanges();

        //        string url = Url.Action("Index", "IMTSS", new { id = imtssphysical.IDAccomp });

        //        return Json(new { success = true, url = url });
        //        //return Json(new { suceess = true });
        //    }
        //    var PARTIULARSload = db.IMTSS_Particulars
        //                  .OrderBy(r => r.ParticularsId)
        //                  .Where(r => (r.categ == "Phy" || r.categ == "PhyFin"));

        //    ViewBag.particularslang = new SelectList(PARTIULARSload, "ParticularsId", "Particulars", imtssphysical.Particulars);
        //    ViewBag.subparticularslang = new SelectList(db.IMTSS_SubParticulars, "SubParticularsID", "SubParticulars", imtssphysical.subParticulars);
        //    ViewBag.subsubparticularslang = new SelectList(db.IMTSS_SubsubParticulars, "SubSubParticularsId", "SubSubParticulars", imtssphysical.subsubparticulars);

        //    return PartialView("_MyEdit", imtssphysical);

        //}


        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult MyEdit(IMTSSViewModel IM) {


            if (ModelState.IsValid)
            {

                //audit trail

                string Constring = ConfigurationManager.ConnectionStrings["AccomplishmentEntitiesSecurity"].ConnectionString;
                using (SqlConnection CON = new SqlConnection(Constring))
                {
                    CON.Open();
                    SqlCommand cmd = new SqlCommand("insert into IMTSSLog (userid,dateedit,timeedit,idaccomp) values (@userid,@dateedit,@timeedit,@idaccomp)", CON);
                    cmd.Parameters.AddWithValue("@userid", Session["userid"].ToString());
                    cmd.Parameters.AddWithValue("@idaccomp", IM.IDACCOMP);
                    cmd.Parameters.AddWithValue("@dateedit", DateTime.Now);
                    cmd.Parameters.AddWithValue("@timeedit", DateTime.Now.TimeOfDay);
                    cmd.ExecuteNonQuery();


                }

                var P1 = new IMTSS()
                {

                    IDACCOMP = IM.IDACCOMP,
                    IMTSSID = IM.IMTSSID,
                    MNT = IM.MNT,
                    YEAR_COVERED = IM.YEAR_COVERED,
                    AS_OF = IM.AS_OF


                };

                   db.Entry(P1).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();


                var P2 = new IMTSS_Assistance()
                {

                    IDAccomp = IM.IDACCOMP,
                    ida = IM.ida,
                    Assist_Batches_Covered = IM.Assist_Batches_Covered,
                    Assist_Batches_TARGET = IM.Assist_Batches_TARGET,
                    Assist_cost = IM.Assist_cost,
                    Assist_cost_target = IM.Assist_cost_target,
                    Assist_NoIA = IM.Assist_NoIA,
                    Assist_NoIA_TARGET = IM.Assist_NoIA_TARGET,
                    Assist_Participants = IM.Assist_Participants,
                    Assist_Participants_TARGET = IM.Assist_Participants_TARGET,
                    mnt = IM.MNT,
                    year_covered = IM.YEAR_COVERED,
                    as_of = IM.AS_OF


                };
                db.Entry(P2).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();


                var P3 = new IMTSS_CapacityIA()
                {

                    IDAccomp = IM.IDACCOMP,
                    idc = IM.idc,
                    CapacityIA_Batches_Covered = IM.CapacityIA_Batches_Covered,
                    CapacityIA_Batches_TARGET = IM.CapacityIA_Batches_TARGET,
                    CapacityIA_COST = IM.CapacityIA_COST,
                    CapacityIA_COST_TARGET = IM.CapacityIA_COST_TARGET,
                    CapacityIA_NoIA = IM.CapacityIA_NoIA,
                    CapacityIA_NoIA_TARGET = IM.CapacityIA_NoIA_TARGET,
                    CapacityIA_Participants = IM.CapacityIA_Participants,
                    CapacityIA_Participants_TARGET = IM.CapacityIA_Participants_TARGET,
                    mnt = IM.MNT,
                    year_covered = IM.YEAR_COVERED,
                    as_of = IM.AS_OF


                };
                db.Entry(P3).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();


                var P4 = new IMTSS_CapacityIAWORKSHOP()
                {

                    IDAccomp = IM.IDACCOMP,
                    idcC = IM.idcC,
                    CapacityIAWORKSHOP_Batches_Covered = IM.CapacityIAWORKSHOP_Batches_Covered,
                    CapacityIAWORKSHOP_Batches_TARGET = IM.CapacityIAWORKSHOP_Batches_TARGET,
                    CapacityIAWORKSHOP_COST = IM.CapacityIAWORKSHOP_COST,
                    CapacityIAWORKSHOP_COST_TARGET = IM.CapacityIAWORKSHOP_COST_TARGET,
                    CapacityIAWORKSHOP_Participants = IM.CapacityIAWORKSHOP_Participants,
                    CapacityIAWORKSHOP_Participants_TARGET = IM.CapacityIAWORKSHOP_Participants_TARGET,
                    mnt = IM.MNT,
                    year_covered = IM.YEAR_COVERED,
                    as_of = IM.AS_OF


                };
                db.Entry(P4).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();


                var P5 = new IMTSS_CapacityStaff()
                {

                    IDAccomp = IM.IDACCOMP,
                    idb = IM.idb,
                    CapacityStaff_Batches_Covered = IM.CapacityStaff_Batches_Covered,
                    CapacityStaff_Batches_TARGET = IM.CapacityStaff_Batches_TARGET,
                    CapacityStaff_COST = IM.CapacityStaff_COST,
                    CapacityStaff_COST_TARGET = IM.CapacityStaff_COST_TARGET,
                    CapacityStaff_Participants = IM.CapacityStaff_Participants,
                    CapacityStaff_Participants_TARGET = IM.CapacityStaff_Participants_TARGET,
                    mnt = IM.MNT,
                    year_covered = IM.YEAR_COVERED,
                    as_of = IM.AS_OF


                };
                db.Entry(P5).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();



                var P6 = new IMTSS_CapacityStaffWORKSHOP()
                {

                    IDAccomp = IM.IDACCOMP,
                    idbB = IM.idbB,
                    CapacityStaffWORKSHOP_Batches_Covered = IM.CapacityStaffWORKSHOP_Batches_Covered,
                    CapacityStaffWORKSHOP_Batches_TARGET = IM.CapacityStaffWORKSHOP_Batches_TARGET,
                    CapacityStaffWORKSHOP_COST = IM.CapacityStaffWORKSHOP_COST,
                    CapacityStaffWORKSHOP_COST_TARGET = IM.CapacityStaffWORKSHOP_COST_TARGET,
                    CapacityStaffWORKSHOP_Participants = IM.CapacityStaffWORKSHOP_Participants,
                    CapacityStaffWORKSHOP_Participants_TARGET = IM.CapacityStaffWORKSHOP_Participants_TARGET,
                    mnt = IM.MNT,
                    year_covered = IM.YEAR_COVERED,
                    as_of = IM.AS_OF

                };

                db.Entry(P6).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();


               



                var P8 = new IMTSS_IAOrganization()
                {

                    IDAccomp = IM.IDACCOMP,
                    idd = IM.idd,
                    Org_Area_Covered = IM.Org_Area_Covered,
                    Org_Area_Covered_TARGET = IM.Org_Area_Covered_TARGET,
                    Org_NoFB = IM.Org_NoFB,
                    Org_NoFB_TARGET = IM.Org_NoFB_TARGET,
                    Org_NoIA = IM.Org_NoIA,
                    Org_NoIA_TARGET = IM.Org_NoIA_TARGET,
                    mnt = IM.MNT,
                    year_covered = IM.YEAR_COVERED,
                    as_of = IM.AS_OF,
                    Org_NoMember = IM.Org_NoMember,
                    Org_NoMember_Target = IM.Org_NoMember_Target

                };



                db.Entry(P8).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();



                var P9 = new IMTSS_IARegistration()
                {

                    IDAccomp = IM.IDACCOMP,
                    ide = IM.ide,
                    Reg_Area_Covered = IM.Reg_Area_Covered,
                    Reg_Area_Covered_TARGET = IM.Reg_Area_Covered_TARGET,
                    Reg_NoFB = IM.Reg_NoFB,
                    Reg_NoFB_TARGET = IM.Reg_NoFB_TARGET,
                    Reg_NoIA = IM.Reg_NoIA,
                    Reg_NoIA_TARGET = IM.Reg_NoIA_TARGET,
                    mnt = IM.MNT,
                    year_covered = IM.YEAR_COVERED,
                    as_of = IM.AS_OF,
                    Reg_NoMember = IM.Reg_NoMember,
                    Reg_NoMember_Target = IM.Reg_NoMember_Target

                };


                db.Entry(P9).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();


                var P10 = new IMTSS_IAStrengthening()
                {

                    IDAccomp = IM.IDACCOMP,
                    idf = IM.idf,
                    Str_Area_Covered = IM.Str_Area_Covered,
                    Str_Area_Covered_TARGET = IM.Str_Area_Covered_TARGET,
                    Str_NoFB = IM.Str_NoFB,
                    Str_NoFB_TARGET = IM.Str_NoFB_TARGET,
                    Str_NoIA = IM.Str_NoIA,
                    Str_NoIA_TARGET = IM.Str_NoIA_TARGET,
                    Str_Tsag = IM.Str_Tsag,
                    Str_Tsag_TARGET = IM.Str_Tsag_TARGET,
                    mnt = IM.MNT,
                    year_covered = IM.YEAR_COVERED,
                    as_of = IM.AS_OF,
                    Str_NoMember = IM.Str_NoMember,
                    Str_NoMember_TARGET = IM.Str_NoMember_TARGET


                };

                db.Entry(P10).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();



                var P11 = new IMTSS_IASustenance()
                {

                    IDAccomp = IM.IDACCOMP,
                    idg = IM.idg,
                    Sus_Area_Covered = IM.Sus_Area_Covered,
                    Sus_Area_Covered_TARGET = IM.Sus_Area_Covered_TARGET,
                    Sus_NoFB = IM.Sus_NoFB,
                    Sus_NoFB_TARGET = IM.Sus_NoFB_TARGET,
                    Sus_NoIA = IM.Sus_NoIA,
                    Sus_NoIA_TARGET = IM.Sus_NoIA_TARGET,
                    Sus_Tsag = IM.Sus_Tsag,
                    Sus_Tsag_TARGET = IM.Sus_Tsag_TARGET,
                    mnt = IM.MNT,
                    year_covered = IM.YEAR_COVERED,
                    as_of = IM.AS_OF,
                    Sus_NoFB1 = IM.Sus_NoFB1,
                    Sus_NoFB1_TARGET = IM.Sus_NoFB1_TARGET

                };

                db.Entry(P11).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();


                var P12 = new IMTSS_IDPPersonnel()
                {

                    IDAccomp = IM.IDACCOMP,
                    idh = IM.idh,
                    dailyjob = IM.dailyjob,
                    dailyjob_TARGET = IM.dailyjob_TARGET,
                    Contractual = IM.Contractual,
                    Contractual_TARGET = IM.Contractual_TARGET,
                    COST_ACCOMP = IM.COST_ACCOMP,
                    COST_TARGET = IM.COST_TARGET,
                    mnt = IM.MNT,
                    year_covered = IM.YEAR_COVERED,
                    as_of = IM.AS_OF,
                    joborder = IM.joborder,
                    joborder_target = IM.joborder_target


                };
                db.Entry(P12).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();


                var P13 = new IMTSS_ModelOther()
                {

                    ModOther_Area_Covered = IM.ModOther_Area_Covered,
                    ModOther_Area_Covered_TARGET = IM.ModOther_Area_Covered_TARGET,
                    ModOther_lined = IM.ModOther_Area_Covered_TARGET,
                    ModOther_lined_target = IM.ModOther_Area_Covered_TARGET,
                    ModOther_NoFB = IM.ModOther_NoFB,
                    ModOther_NoFB_TARGET = IM.ModOther_NoFB_TARGET,
                    ModOther_NoIA = IM.ModOther_NoIA,
                    ModOther_NoIA_TARGET = IM.ModOther_NoIA_TARGET,
                    ModOther_NoMember = IM.ModOther_NoMember,
                    ModOther_NoMember_Target = IM.ModOther_NoMember_Target,
                    ModOther_unlined = IM.ModOther_unlined,
                    ModOther_unlined_target = IM.ModOther_unlined_target,
                    iddMod = IM.iddMod,
                    IDAccomp = IM.IDACCOMP,
                    mnt = IM.MNT,
                    year_covered = IM.YEAR_COVERED,
                    as_of = IM.AS_OF,

                };

                db.Entry(P13).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                var P14 = new IMTSS_ModContract()
                {

                    ModCon_Area_Covered = IM.ModCon_Area_Covered,
                    ModCon_Area_Covered_TARGET = IM.ModCon_Area_Covered_TARGET,
                    ModCon_lined = IM.ModCon_lined,
                    ModCon_lined_target = IM.ModCon_lined_target,
                    ModCon_NoFB = IM.ModCon_NoFB,
                    ModCon_NoFB_TARGET = IM.ModCon_NoFB_TARGET,
                    ModCon_NoIA = IM.ModCon_NoIA,
                    ModCon_NoIA_TARGET = IM.ModCon_NoIA_TARGET,
                    ModCon_NoMember = IM.ModCon_NoMember,
                    ModCon_NoMember_Target = IM.ModCon_NoMember_Target,
                    ModCon_unlined = IM.ModCon_unlined,
                    ModCon_unlined_target = IM.ModCon_unlined_target,
                    iddModCon = IM.iddModCon,
                    ModCon_FUSA = IM.ModCon_FUSA,
                    ModCon_FUSA_target = IM.ModCon_FUSA_target,
                    ModCon_Remarks = IM.ModCon_Remarks,
                    ModCon_SA = IM.ModCon_SA,
                    ModCon_SA_target = IM.ModCon_SA_target,
                    IDAccomp = IM.IDACCOMP,
                    mnt = IM.MNT,
                    year_covered = IM.YEAR_COVERED,
                    as_of = IM.AS_OF,

                };

                db.Entry(P14).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                var P15 = new IMTSS_EModContract()
                {

                    EModCon_Area_Covered = IM.EModCon_Area_Covered,
                    EModCon_Area_Covered_TARGET = IM.EModCon_Area_Covered_TARGET,
                    EModCon_lined = IM.EModCon_lined,
                    EModCon_lined_target = IM.EModCon_lined_target,
                    EModCon_NoFB = IM.EModCon_NoFB,
                    EModCon_NoFB_TARGET = IM.EModCon_NoFB_TARGET,
                    EModCon_NoIA = IM.EModCon_NoIA,
                    EModCon_NoIA_TARGET = IM.EModCon_NoIA_TARGET,
                    EModCon_NoMember = IM.EModCon_NoMember,
                    EModCon_NoMember_Target = IM.EModCon_NoMember_Target,
                    EModCon_unlined = IM.EModCon_unlined,
                    EModCon_unlined_target = IM.EModCon_unlined_target,
                    iddEModCon = IM.iddEModCon,
                    EModCon_FUSA = IM.EModCon_FUSA,
                    EModCon_FUSA_target = IM.EModCon_FUSA_target,
                    
                    EModCon_SA = IM.EModCon_SA,
                    EModCon_SA_target = IM.EModCon_SA_target,
                    IDAccomp = IM.IDACCOMP,
                    mnt = IM.MNT,
                    year_covered = IM.YEAR_COVERED,
                    as_of = IM.AS_OF,

                };

                db.Entry(P15).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                //var P13 = new IMTSS_Model1()
                //{

                //    IDAccomp = IM.IDACCOMP,
                //    idi = IM.idi,
                //    Mod1_Area_Covered = IM.Mod1_Area_Covered,
                //    Mod1_Area_Covered_TARGET = IM.Mod1_Area_Covered_TARGET,
                //    Mod1_NoFB = IM.Mod1_NoFB,
                //    Mod1_NoFB_TARGET = IM.Mod1_NoFB_TARGET,
                //    Mod1_NoIA = IM.Mod1_NoIA,
                //    Mod1_NoIA_TARGET = IM.Mod1_NoIA_TARGET,
                //    Mod1_NoMember = IM.Mod1_NoMember,
                //    Mod1_NoMember_TARGET = IM.Mod1_NoMember_TARGET,
                //    mnt = IM.MNT,
                //    year_covered = IM.YEAR_COVERED,
                //    as_of = IM.AS_OF


                //};

                //db.Entry(P13).State = System.Data.Entity.EntityState.Modified;
                //db.SaveChanges();




                //var P14 = new IMTSS_Model2()
                //{

                //    IDAccomp = IM.IDACCOMP,
                //    idj = IM.idj,
                //    Mod2_Area_Covered = IM.Mod2_Area_Covered,
                //    Mod2_Area_Covered_TARGET = IM.Mod2_Area_Covered_TARGET,
                //    Mod2_NoFB = IM.Mod2_NoFB,
                //    Mod2_NoFB_TARGET = IM.Mod2_NoFB_TARGET,
                //    Mod2_NoIA = IM.Mod2_NoIA,
                //    Mod2_NoIA_TARGET = IM.Mod2_NoIA_TARGET,
                //    Mod2_NoMember = IM.Mod2_NoMember,
                //    Mod2_NoMember_TARGET = IM.Mod2_NoMember_TARGET,
                //    mnt = IM.MNT,
                //    year_covered = IM.YEAR_COVERED,
                //    as_of = IM.AS_OF


                //};

                //db.Entry(P14).State = System.Data.Entity.EntityState.Modified;
                //db.SaveChanges();


                //var P15 = new IMTSS_Model3()
                //{

                //    IDAccomp = IM.IDACCOMP,
                //    idk = IM.idk,
                //    Mod3_Area_Covered = IM.Mod3_Area_Covered,
                //    Mod3_Area_Covered_TARGET = IM.Mod3_Area_Covered_TARGET,
                //    Mod3_NoFB = IM.Mod3_NoFB,
                //    Mod3_NoFB_TARGET = IM.Mod3_NoFB_TARGET,
                //    Mod3_NoIA = IM.Mod3_NoIA,
                //    Mod3_NoIA_TARGET = IM.Mod3_NoIA_TARGET,
                //    Mod3_NoMember = IM.Mod3_NoMember,
                //    Mod3_NoMember_TARGET = IM.Mod3_NoMember_TARGET,
                //    mnt = IM.MNT,
                //    year_covered = IM.YEAR_COVERED,
                //    as_of = IM.AS_OF


                //};
                //db.Entry(P15).State = System.Data.Entity.EntityState.Modified;
                //db.SaveChanges();


                //var P16 = new IMTSS_Model4()
                //{

                //    IDAccomp = IM.IDACCOMP,
                //    idl = IM.idl,
                //    Mod4_Area_Covered = IM.Mod4_Area_Covered,
                //    Mod4_Area_Covered_TARGET = IM.Mod4_Area_Covered_TARGET,
                //    Mod4_NoFB = IM.Mod4_NoFB,
                //    Mod4_NoFB_TARGET = IM.Mod4_NoFB_TARGET,
                //    Mod4_NoIA = IM.Mod4_NoIA,
                //    Mod4_NoIA_TARGET = IM.Mod4_NoIA_TARGET,
                //    Mod4_NoMember = IM.Mod4_NoMember,
                //    Mod4_NoMember_TARGET = IM.Mod4_NoMember_TARGET,
                //    mnt = IM.MNT,
                //    year_covered = IM.YEAR_COVERED,
                //    as_of = IM.AS_OF


                //};



                //db.Entry(P16).State = System.Data.Entity.EntityState.Modified;
                //db.SaveChanges();

                string url = Url.Action("Index", "IMTSS", new { id = IM.IDACCOMP });

                return Json(new { success = true, url = url });
                //return Json(new { suceess = true });
            }

            return PartialView("_MyEdit",IM );
        
        
        }


        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACCOMPLISHMENT imtss = db.ACCOMPLISHMENTs.Find(id);
            if (imtss == null)
            {
                return HttpNotFound();
            }
            return View(imtss);
        }


        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IMTSSViewModelPhysical imtssphysical = db.IMTSSViewModelPhysicals.Find(id);

            if (imtssphysical == null)
            {
                return HttpNotFound();

            }
            return PartialView("_Delete", imtssphysical);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {

            IMTSS_Physical imtssphysical = db.IMTSS_Physical.Find(id);
            db.IMTSS_Physical.Remove(imtssphysical);
            db.SaveChanges();

            string url = Url.Action("Index", "IMTSS", new { id = imtssphysical.IDAccomp });

            return Json(new { success = true, url = url });

        }



        //finance

        public ActionResult IndexFinance(string id)
        {

            ViewBag.IDAccomp = id;
            Session["idaccomp"] = id;
            var imtss = db.IMTSS_Finance_View.Where(a => a.IDAccomp == id);




            if (imtss == null)
            {
                return HttpNotFound();
            }
            // return View(asa);
            return PartialView("_IndexFinance", imtss);

        }



        public ActionResult MyEditFinance(int? id)
        {


            IMTSS_Finance_View imtssp = db.IMTSS_Finance_View.Find(id);


            if (imtssp == null)
            {
                return HttpNotFound();
            }


            //var PARTIULARSload = db.IMTSS_Particulars
            //           .OrderBy(r => r.ParticularsId)
            //           .Where(r => (r.categ == "Phy" || r.categ == "PhyFin"));

            //ViewBag.particularslang = new SelectList(PARTIULARSload, "ParticularsId", "Particulars", imtssp.Particulars);
            //ViewBag.subparticularslang = new SelectList(db.IMTSS_SubParticulars, "SubParticularsID", "SubParticulars", imtssp.subParticulars);
            //ViewBag.subsubparticularslang = new SelectList(db.IMTSS_SubsubParticulars, "SubSubParticularsId", "SubSubParticulars", imtssp.subsubparticulars);


            return PartialView("_MyEditFinance", imtssp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult MyEditFinance(IMTSS_Finance_View IM)
        {


            if (ModelState.IsValid)
            {

                string Constring = ConfigurationManager.ConnectionStrings["AccomplishmentEntitiesSecurity"].ConnectionString;
                using (SqlConnection CON = new SqlConnection(Constring))
                {
                    CON.Open();
                    SqlCommand cmd = new SqlCommand("insert into IMTSS_FinanceLog (userid,dateedit,timeedit,idaccomp) values (@userid,@dateedit,@timeedit,@idaccomp)", CON);
                    cmd.Parameters.AddWithValue("@userid", Session["userid"].ToString());
                    cmd.Parameters.AddWithValue("@idaccomp", IM.IDAccomp);
                    cmd.Parameters.AddWithValue("@dateedit", DateTime.Now);
                    cmd.Parameters.AddWithValue("@timeedit", DateTime.Now.TimeOfDay);
                    cmd.ExecuteNonQuery();


                }

                var P1 = new IMTSS_Finance()
                {

                    idaccomp = IM.IDAccomp,
                    mnt = IM.mnt,
                    year_covered = IM.year_covered,
                    asof = IM.asof,
                    JobOrder = IM.JobOrder,
                    Daily = IM.Daily,
                    Contractual = IM.Contractual,
                    CapacityBuildingForIA = IM.CapacityBuildingForIA,
                    CapacityBuildingForNIAStaff= IM.CapacityBuildingForNIAStaff,
                    AssistanceProgram = IM.AssistanceProgram,
                    SupervisionCost = IM.SupervisionCost,
                    OfficeSuppliesMaterials = IM.OfficeSuppliesMaterials,
                    Miscellaneous = IM.Miscellaneous,
                    FSS = IM.FSS,
                    DBMS = IM.DBMS,
                    OtherMOOE = IM.OtherMOOE,
                    remarks = IM.remarks,
                    IDIMTSS_FINANCE = IM.IDIMTSS_FINANCE,
                    JobOrderAllocation = IM.JobOrderAllocation,
                    DailyAllocation = IM.DailyAllocation,
                    ContractualAllocation = IM.ContractualAllocation,
                    CapacityBuildingForIAAllocation = IM.CapacityBuildingForIAAllocation,
                    CapacityBuildingForNIAStaffAllocation = IM.CapacityBuildingForNIAStaffAllocation,
                    AssistanceProgramAllocation = IM.AssistanceProgramAllocation,
                    SupervisionCostAllocation = IM.SupervisionCostAllocation,
                    OfficeSuppliesMaterialsAllocation = IM.OfficeSuppliesMaterialsAllocation,
                    MiscellaneousAllocation = IM.MiscellaneousAllocation,
                    FSSAllocation = IM.FSSAllocation,
                    DBMSAllocation = IM.DBMSAllocation,
                    OtherMOOEAllocation = IM.OtherMOOEAllocation


                };

                db.Entry(P1).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();


   

                //db.Entry(P16).State = System.Data.Entity.EntityState.Modified;
                //db.SaveChanges();

                string url = Url.Action("IndexFinance", "IMTSS", new { id = IM.IDAccomp });

                return Json(new { success = true, url = url });
                //return Json(new { suceess = true });
            }

            return PartialView("_MyEditFinance", IM);


        }

        public ActionResult EditFinance(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACCOMPLISHMENT imtss = db.ACCOMPLISHMENTs.Find(id);
            if (imtss == null)
            {
                return HttpNotFound();
            }
            return View(imtss);
        }



        //Financial


        public ActionResult IndexFinancial(string id)
        {

            ViewBag.IDAccomp = id;
            Session["idaccomp"] = id;
            var imtss = db.IMTSSViewModelFinancials.Where(a => a.IDAccomp == id);




            if (imtss == null)
            {
                return HttpNotFound();
            }
            // return View(asa);
            return PartialView("_IndexFinancial", imtss);

        }



        public ActionResult CreateFinancial(string Id)
        {


            var PARTIULARSload = db.IMTSS_Particulars
                           .OrderBy(r => r.ParticularsId)
                           .Where(r => (r.categ == "Fin" || r.categ == "PhyFin"));

            ViewBag.particularsTWO = new SelectList(PARTIULARSload, "ParticularsId", "Particulars");
            ViewBag.particularsONE = new SelectList(db.IMTSS_ParticularsFinancial, "FinancialParticularsId", "FinancialParticulars");

            IMTSS_Financial imtssfinancial = new IMTSS_Financial();
            imtssfinancial.IDAccomp = Id;


            return PartialView("_CreateFinancial", imtssfinancial);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFinancial([Bind(Include = "IDAccomp,mnt,year_covered,as_of,Particulars,subParticulars,Total_budget,Total_Expenses,quarter")]IMTSS_Financial imtssfinancial)
        {

            if (ModelState.IsValid)
            {
                db.IMTSS_Financial.Add(imtssfinancial);
                db.SaveChanges();

                string url = Url.Action("IndexFinancial", "IMTSS", new { id = imtssfinancial.IDAccomp });

                return Json(new { success = true, url = url });
                //return Json(new { suceess = true });
            }


            return PartialView("_CreateFinancial", imtssfinancial);

        }





        public ActionResult Update(int? id)
        {
            IMTSSViewModelFinancial imtssp = db.IMTSSViewModelFinancials.Find(id);


            if (imtssp == null)
            {
                return HttpNotFound();
            }


            var PARTIULARSload = db.IMTSS_Particulars
                       .OrderBy(r => r.ParticularsId)
                       .Where(r => (r.categ == "Fin" || r.categ == "PhyFin"));


            ViewBag.particularsTWO = new SelectList(PARTIULARSload, "ParticularsId", "Particulars", imtssp.ParticularsId);
            ViewBag.particularsONE = new SelectList(db.IMTSS_ParticularsFinancial, "FinancialParticularsId", "FinancialParticulars", imtssp.FinancialParticularsId);


            return PartialView("_Update", imtssp);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(IMTSSViewModelFinancial viewmodel)
        {
            if (ModelState.IsValid)
            {
                var IMTSSPS = new IMTSS_Financial()
                {
                    subParticulars = viewmodel.ParticularsId,
                    Particulars = viewmodel.FinancialParticularsId,
                    imtssFinancialID = viewmodel.imtssFinancialID,
                    Total_budget = viewmodel.Total_budget,
                    Total_Expenses = viewmodel.Total_Expenses,
                    quarter = viewmodel.quarter,
                    IDAccomp = viewmodel.IDAccomp,
                    mnt = viewmodel.mnt,
                    year_covered = viewmodel.year_covered,
                    as_of = viewmodel.as_of
                };



                db.Entry(IMTSSPS).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();
                string url = Url.Action("IndexFinancial", "IMTSS", new { id = viewmodel.IDAccomp });
                return Json(new { success = true, url = url });
                //return Json(new { suceess = true });



            }
            var PARTIULARSload = db.IMTSS_Particulars
                          .OrderBy(r => r.ParticularsId)
                          .Where(r => (r.categ == "Fin" || r.categ == "PhyFin"));


            ViewBag.particularslang = new SelectList(PARTIULARSload, "ParticularsId", "Particulars", viewmodel.ParticularsId);
            ViewBag.particularsfinancial = new SelectList(db.IMTSS_ParticularsFinancial, "FinancialParticularsId", "FinancialParticulars", viewmodel.FinancialParticularsId);


            return View();
        }

        public ActionResult DeleteFinancial(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IMTSSViewModelFinancial imtssfinancial = db.IMTSSViewModelFinancials.Find(id);

            if (imtssfinancial == null)
            {
                return HttpNotFound();

            }
            return PartialView("_DeleteFinancial", imtssfinancial);

        }

        [HttpPost, ActionName("DeleteFinancial")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFinancial(int id)
        {

            IMTSS_Financial imtssfinancial = db.IMTSS_Financial.Find(id);
            db.IMTSS_Financial.Remove(imtssfinancial);
            db.SaveChanges();

            string url = Url.Action("IndexFinancial", "IMTSS", new { id = imtssfinancial.IDAccomp });

            return Json(new { success = true, url = url });

        }


    }
}