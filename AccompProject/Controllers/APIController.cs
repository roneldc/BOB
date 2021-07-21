using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AccompProject.Models;
using AccompProject.Models.EntityModel;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace AccompProject.Controllers
{
    public class APIController : ApiController
    {
        private AccomplishmentEntities db = new AccomplishmentEntities();
        private AccompMobileAPI db1 = new AccompMobileAPI();

        // GET: api/API
        public IEnumerable<ImplementationAndCoordinate> Get()
        {
            return db.ImplementationAndCoordinates
                       .OrderByDescending(r => r.year)
                       .Where(r => r.mnt == 7 && r.year == 2019).ToList();
                       



        }



      //  [System.Web.Mvc.Route("api/api/{myid}")]
      //  public IHttpActionResult getaccompmobile(int myid)
      //  {
      //      //var str = string.Format("mnt:{0},yr:{1}", mnt, yr);
      //      //return Ok(str);

      //      var aCCOMPLISHMENT = db1.sampleUsers
      //                .OrderByDescending(r => r.id)
      //                .Where(r => r.id == myid).ToList();


      //      if (aCCOMPLISHMENT == null)
      //      {
      //          return NotFound();
      //      }

      //      return Ok(aCCOMPLISHMENT);
      //  }
      ////  [System.Web.Mvc.Route("api/api/{myid}")]
      
        [System.Web.Mvc.Route("api/api/{mnt}/{yr}")]
        public IHttpActionResult getaccomp(int mnt, int yr)
        {
            //var str = string.Format("mnt:{0},yr:{1}", mnt, yr);
            //return Ok(str);

            var aCCOMPLISHMENT = db.ImplementationAndCoordinates
                      .OrderByDescending(r => r.year)
                      .Where(r => r.mnt == mnt && r.year == yr).ToList();


            if (aCCOMPLISHMENT == null)
            {
                return NotFound();
            }

            return Ok(aCCOMPLISHMENT);
        }
  
        // GET: api/API/5
        //[ResponseType(typeof(ImplementationAndCoordinate))]
        //public IHttpActionResult Multi(int yr, int mnt)
        //{
        //    var aCCOMPLISHMENT = db.ImplementationAndCoordinates
        //               .OrderByDescending(r => r.year)
        //               .Where(r => r.mnt == mnt && r.year == yr).ToList();


        //    if (aCCOMPLISHMENT == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(aCCOMPLISHMENT);
        //}

        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ACCOMPLISHMENTExists(string id)
        {
            return db.ACCOMPLISHMENTs.Count(e => e.IDAccomp == id) > 0;
        }
    }
}