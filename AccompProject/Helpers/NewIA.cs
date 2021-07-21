using AccompProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AccompProject.Helpers
{
    public class NewIA
    {

        IAEntities db1 = new IAEntities();

        public async Task SaveIAProfileNew(string systemname = null, string iaid = null, string ianame = null)
        {

            try
            {
                int yr = Convert.ToInt32(db1.YearReferences.Select(n => n.year).First());


                var iaprofile = new IA_PROFILE()
                {

                    YEAR_COVERED = yr,
                    SYSTEM_NAME = systemname,
                    IAID = iaid,
                    IA_NAME = ianame

                };


                db1.IA_PROFILE.Add(iaprofile);
                await db1.SaveChangesAsync();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }


        }

        public async Task UpdateIAProfileNew(IAListView i)
        {

            try
            {
                 int yr = Convert.ToInt32(db1.YearReferences.Select(n => n.year).First());
                 var D = db1.IA_PROFILE.Where(r => r.IAID == i.IDIA).FirstOrDefault();

            
                    D.YEAR_COVERED = yr;
                    D.SYSTEM_NAME = i.systemname;
                    
                    D.IA_NAME = i.IAname;
                   

           
                await db1.SaveChangesAsync();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }


        }


        public int YearReference()
        {
            int yearReference = Convert.ToInt32(db1.YearReferences.Where(r => r.idyear == 1).Select(z => z.year).Single());

            return yearReference;
        }

    }
}