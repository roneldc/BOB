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

namespace AccompProject.Controllers
{
    public class sampleUsersController : ApiController
    {
        private AccompMobileAPI db = new AccompMobileAPI();

     //    GET: api/sampleUsers
        public IQueryable<sampleUser> GetsampleUsers()
        {
            return db.sampleUsers;
        }

         

       // [HttpGet]  
     //   [Route("api/sampleUsers/username={username}/password={password}")]  
         [Route("api/sampleUsers/CheckUser")]
        public IHttpActionResult GetCheckUser(string username=null, string password=null)
        {
            // sampleUser sampleUser = db.sampleUsers.Find(id);

            var sampleUser = db.sampleUsers
                          .Where(a => a.username == username && a.passwords == password);


            if (sampleUser == null)
            {
                return NotFound();
            }

            return Ok(sampleUser);
        }

 

       //  GET: api/sampleUsers/5
        [ResponseType(typeof(sampleUser))]
       
        public IHttpActionResult GetsampleUser(int id)
        {
            sampleUser sampleUser = db.sampleUsers.Find(id);
            if (sampleUser == null)
            {
                return NotFound();
            }

            return Ok(sampleUser);
        }

     

        // PUT: api/sampleUsers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutsampleUser(int id, sampleUser sampleUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sampleUser.id)
            {
                return BadRequest();
            }

            db.Entry(sampleUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!sampleUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/sampleUsers
        [ResponseType(typeof(sampleUser))]
        public IHttpActionResult PostsampleUser(sampleUser sampleUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.sampleUsers.Add(sampleUser);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = sampleUser.id }, sampleUser);
        }

        // DELETE: api/sampleUsers/5
        [ResponseType(typeof(sampleUser))]
        public IHttpActionResult DeletesampleUser(int id)
        {
            sampleUser sampleUser = db.sampleUsers.Find(id);
            if (sampleUser == null)
            {
                return NotFound();
            }

            db.sampleUsers.Remove(sampleUser);
            db.SaveChanges();

            return Ok(sampleUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool sampleUserExists(int id)
        {
            return db.sampleUsers.Count(e => e.id == id) > 0;
        }
    }
}