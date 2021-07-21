using AccompProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AccompProject.Helpers
{
    public class PRLogClassHelper
    {
        private procurementss_onlinejervyEntities db = new procurementss_onlinejervyEntities();
        public async Task PRLOGS(int id_key = 0, string procid = null, string note = null, string status = null)
        {

            var abc = db.vwPRNODetailsJs.FirstOrDefault(r => r.id_key == id_key);

            var pronolog = new PRNOStatu()
            {

                dateLog = DateTime.Now,
                ProcurementID = procid,
                id_key = id_key,
                PRNO = abc.pr_no,
                timeLog = DateTime.Now.TimeOfDay,
                note = note,
                Status = status


            };

            db.PRNOStatus.Add(pronolog);
            db.SaveChanges();

        }


    }

}