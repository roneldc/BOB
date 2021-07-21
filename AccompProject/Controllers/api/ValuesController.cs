using AccompProject.Hubs;
using AccompProject.Models;
using AccompProject.Models.EntityModel;
using AccompProject.Models.EntityModel.DatabaseFirstContext;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using NotifSystem.Web.Models;

namespace AccompProject.Controllers.api
{
    public class ValuesController : ApiController
    {
        private AccomplishmentEntities context = new AccomplishmentEntities();

        [HttpPost]
        public HttpResponseMessage SendNotification(NotifModels obj)
        {
            NotificationBingoHub objNotifHub = new NotificationBingoHub();
      
            BingoNoti Bnoti = new BingoNoti();

            int nia = context.BingoNotis.Where(r => r.Title == obj.cate).Count();
          
          

            Bnoti.Details = obj.Message;
            Bnoti.Title = obj.cate;
            Bnoti.NotificationType = obj.lettr;
          
       

            string Constring = ConfigurationManager.ConnectionStrings["AccomplishmentEntitiesSecurity"].ConnectionString;
            using (SqlConnection CON = new SqlConnection(Constring))
            {
                CON.Open();


                SqlCommand cmd = new SqlCommand("update bingotitle set title = @title, lttr = @lttr  where id = @id", CON);

      
                cmd.Parameters.AddWithValue("@title", obj.cate);
                cmd.Parameters.AddWithValue("@lttr", obj.lettr);
              
              
                cmd.Parameters.AddWithValue("@id", 2);
                cmd.ExecuteNonQuery();


            }
            //    //context.Entry(Bnoti).State = EntityState.Modified;
            //    //context.SaveChanges();

            //}
            //else
            //{


                context.Configuration.ProxyCreationEnabled = false;
                context.BingoNotis.Add(Bnoti);
                context.SaveChanges();

          //  }

            int cnt = 0;
            string lttr = "";
            for (cnt = 0; cnt <= 4; cnt++)
            {
                if(cnt ==0){
                    lttr = "B";

                }
                if (cnt == 1)
                {
                    lttr = "I";

                }
                if (cnt == 2)
                {
                    lttr = "N";

                }
                if (cnt == 3)
                {
                    lttr = "G";

                }
                if (cnt == 4)
                {
                    lttr = "O";

                }
                objNotifHub.SendNotification(Bnoti.Title, lttr, obj.lettr);
            }



            var query = (from t in context.BingoNotis
                         where t.Title == Bnoti.Title
                         select t).ToList();

          

            return Request.CreateResponse(HttpStatusCode.OK, new { query });
        }

        //[HttpPost]
        //public HttpResponseMessage BingoNoti(string bingo)
        //{
        //    NotificationHub objNotifHub = new NotificationHub();
        //    //     Notification objNotif = new Notification();
          
        //    objNotifHub.BingoModal(bingo);

          


        //    return Request.CreateResponse(HttpStatusCode.OK);
        //}
    }
}