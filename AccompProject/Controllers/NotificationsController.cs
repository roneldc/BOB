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
    public class NotificationsController : Controller
    {
        private AccomplishmentEntities db = new AccomplishmentEntities();

      
        public ActionResult Read(int id, string sentto)
        {
            // return View(db.NotificationsViews.ToList());


            string Constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection CON = new SqlConnection(Constring))
            {
                CON.Open();
                SqlCommand cmd = new SqlCommand("update notification set isread = 'True' where id = " + id, CON);


                cmd.ExecuteNonQuery();
                return RedirectToAction("Index", "Notifications", new { sentto = sentto});
                // return Json(new { success = true });

            }


          

        }


        public ActionResult Index(string sentto)
        {
           // return View(db.NotificationsViews.ToList());






            var noti = db.NotificationsViews
               .OrderBy(a => a.IsRead).ThenByDescending(a => a.Date)
               .Where(a => a.SentTo == sentto);



            if (noti == null)
            {
                return HttpNotFound();
            }
            return View(noti);

        }

        // GET: Notifications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = db.Notifications.Find(id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            return View(notification);
        }

        // POST: Notifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Notification notification = db.Notifications.Find(id);
            db.Notifications.Remove(notification);
            db.SaveChanges();
            return RedirectToAction("Index", new { sentto = notification.SentTo});
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
