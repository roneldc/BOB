using AccompProject.Models;
using AccompProject.Models.EntityModel;
using AccompProject.Models.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccompProject.Helpers
{
    public class AccompChanges
    {
        private AccomplishmentEntities db = new AccomplishmentEntities();

        public void SaveChanges(AChanges aa)
        
        {

            var x = db.PhysicalAccompSummaryLogs.AsNoTracking().Where(a => a.IDPhysical == aa.Idphysical && a.FieldName == aa.fname).FirstOrDefault();


            var j = new PhysicalAccompSummaryLog
            {

                IDAccomp = aa.idaccomp,
                Comp = aa.comp,
                FieldName = aa.fname,
                IDPhysical = aa.Idphysical,
                datelog = DateTime.Now,
                timelog = DateTime.Now.TimeOfDay,
                type = "Actual"

            };



            if (aa.comp != "EQUAL")
            {
                if (x == null)
                {
                    db.PhysicalAccompSummaryLogs.Add(j);
                    db.SaveChanges();

                }
                else
                {
                    j.IDSum = x.IDSum;
                    db.Entry(j).State = System.Data.Entity.EntityState.Modified;


                    db.SaveChanges();
                }


            }else
            {
                if (x != null)
                {
                    using (var context = new AccomplishmentEntities())
                    {
                        PhysicalAccompSummaryLog aCCOMPLISHMENT = context.PhysicalAccompSummaryLogs.AsNoTracking().Where(a => a.IDPhysical == aa.Idphysical && a.FieldName == aa.fname).FirstOrDefault();
                        context.PhysicalAccompSummaryLogs.Attach(aCCOMPLISHMENT);
                        context.PhysicalAccompSummaryLogs.Remove(aCCOMPLISHMENT);

                        context.SaveChanges();
                    }
                }

            }
        
        
        }

        public void SaveChangesTarget(AChanges aa)
        {

            var x = db.PhysicalAccompSummaryLogs.AsNoTracking().Where(a => a.IDAccomp == aa.idaccomp && a.FieldName == aa.fname).FirstOrDefault();


            var j = new PhysicalAccompSummaryLog
            {

                IDAccomp = aa.idaccomp,
                Comp = aa.comp,
                FieldName = aa.fname,
                IDPhysical = aa.Idphysical,
                datelog = DateTime.Now,
                timelog = DateTime.Now.TimeOfDay,
                type = "Target"

            };



            if (aa.comp != "EQUAL")
            {
                if (x == null)
                {
                    db.PhysicalAccompSummaryLogs.Add(j);
                    db.SaveChanges();

                }
                else
                {
                    j.IDSum = x.IDSum;
                    db.Entry(j).State = System.Data.Entity.EntityState.Modified;


                    db.SaveChanges();
                }


            }
            else
            {
                if (x != null)
                {
                    using (var context = new AccomplishmentEntities())
                    {
                        PhysicalAccompSummaryLog aCCOMPLISHMENT = context.PhysicalAccompSummaryLogs.AsNoTracking().Where(a => a.IDAccomp == aa.idaccomp && a.FieldName == aa.fname).FirstOrDefault();
                        context.PhysicalAccompSummaryLogs.Attach(aCCOMPLISHMENT);
                        context.PhysicalAccompSummaryLogs.Remove(aCCOMPLISHMENT);

                        context.SaveChanges();
                    }
                }

            }


        }

    }
}