using Microsoft.AspNet.SignalR;
using AccompProject.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AccompProject.Models.EntityModel.DatabaseFirstContext;
using AccompProject.Models.EntityModel;

namespace AccompProject.Hubs
{
    //[Authorize]
    public class NotificationBingoHub : Hub
    {
        private static readonly ConcurrentDictionary<string, UserHubModels> Users =
            new ConcurrentDictionary<string, UserHubModels>(StringComparer.InvariantCultureIgnoreCase);

        private AccomplishmentEntities context = new AccomplishmentEntities();


        //Logged Use Call
        public void GetNotification()
        {
            try
            {
                // string loggedUser = Context.User.Identity.Name;

                //Get TotalNotification

                string numberstring = "";
                string numberstring1 = "";
                string Constring = ConfigurationManager.ConnectionStrings["AccomplishmentEntitiesSecurity"].ConnectionString;
                using (SqlConnection CON = new SqlConnection(Constring))
                {
                    CON.Open();


                    SqlCommand cmd = new SqlCommand("Select title,imagename from bingotitle where id = @title", CON);
                    int id = 2;

                    cmd.Parameters.AddWithValue("@title", id);

                    SqlDataReader reader;

                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {


                        numberstring = reader.GetString(0);
                        numberstring1 = reader.GetString(1);
                        //string totalNotif = LoadNotifData(numberstring,LTTR);

                        ////Send To
                        ////UserHubModels receiver;
                        ////if (Users.TryGetValue(loggedUser, out receiver))
                        ////{
                        ////    var cid = receiver.ConnectionIds.FirstOrDefault();
                        //var context = GlobalHost.ConnectionManager.GetHubContext<NotificationBingoHub>();
                        //context.Clients.All.broadcaastNotif(totalNotif, numberstring,lttr);

                    }


                }

                int cnt = 0;
                string lttr = "";
                for (cnt = 0; cnt < 5; cnt++)
                {
                    if (cnt == 0)
                    {
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

                    string totalNotif = LoadNotifData(numberstring, lttr);

                    //Send To
                    //UserHubModels receiver;
                    //if (Users.TryGetValue(loggedUser, out receiver))
                    //{
                    //    var cid = receiver.ConnectionIds.FirstOrDefault();
                    var context = GlobalHost.ConnectionManager.GetHubContext<NotificationBingoHub>();
                    context.Clients.All.broadcaastNotif(totalNotif, numberstring, lttr);

                   ImageBingo(numberstring1);
                }
                //}
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        //Specific User Call
        //public void SendNotification(string SentTo)
        //{
        //    try
        //    {
        //        //Get TotalNotification
        //        string totalNotif = LoadNotifData(SentTo);

        //        //Send To
        //        UserHubModels receiver;
        //        if (Users.TryGetValue(SentTo, out receiver))
        //        {
        //            var cid = receiver.ConnectionIds.FirstOrDefault();
        //            var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
        //            context.Clients.Client(cid).broadcaastNotif(totalNotif);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //    }
        //}
        public void SendNotification(string SentTo, string lttr,string localletter)
        {
            try
            {
                //Get TotalNotification
                string totalNotif = LoadNotifData(SentTo, lttr);

                //Send To
                //UserHubModels receiver;
                //if (Users.TryGetValue(SentTo, out receiver))
                //{
                //    var cid = receiver.ConnectionIds.FirstOrDefault();
                var context = GlobalHost.ConnectionManager.GetHubContext<NotificationBingoHub>();
                context.Clients.All.broadcaastNotif(totalNotif, SentTo, lttr, localletter);
                //}
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        public void ImageBingo(string imgurl)
        {
            try
            {
                //Get TotalNotification

                //Send To
                //UserHubModels receiver;
                //if (Users.TryGetValue(SentTo, out receiver))
                //{
                //    var cid = receiver.ConnectionIds.FirstOrDefault();
                var context = GlobalHost.ConnectionManager.GetHubContext<NotificationBingoHub>();
                context.Clients.All.imageBingo(imgurl);
                //}
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        public void BingoModal(string url, int userid, string lname , string fname)
        {
            try
            {
                //Get TotalNotification

                //Send To
                //UserHubModels receiver;
                //if (Users.TryGetValue(SentTo, out receiver))
                //{
                //    var cid = receiver.ConnectionIds.FirstOrDefault();
                var context = GlobalHost.ConnectionManager.GetHubContext<NotificationBingoHub>();
                context.Clients.All.bingoModal(url, userid, lname, fname);
                //}
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        private string LoadNotifData(string title, string lttr)
        {
            //  var total = 0;
            //  var query = (from t in context.BingoNotis
            //               where t.Title == userId
            //               select t)
            //              .ToList();
            //var  total = query.FirstOrDefault();



            string numberstring = "";
            string Constring = ConfigurationManager.ConnectionStrings["AccomplishmentEntitiesSecurity"].ConnectionString;
            using (SqlConnection CON = new SqlConnection(Constring))
            {
                CON.Open();


                SqlCommand cmd = new SqlCommand("Select bingonumber from Bingonotiview where title = @title AND notificationtype = @LTTR ", CON);


                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@LTTR", lttr);

                SqlDataReader reader;

                reader = cmd.ExecuteReader();
                if (reader.Read())
                {


                    numberstring = reader.GetString(0);

                }


            }


            return numberstring.ToString();

        }

        //private string LoadNotifData(string userId)
        //{
        //    int total = 0;
        //    var query = (from t in context.Notifications
        //                 where t.SentTo == userId
        //                 select t)
        //                .ToList();
        //    total = query.Count;
        //    return total.ToString();
        //}

        public override Task OnConnected()
        {
            string userName = Context.User.Identity.Name;
            string connectionId = Context.ConnectionId;

            var user = Users.GetOrAdd(userName, _ => new UserHubModels
            {
                UserName = userName,
                ConnectionIds = new HashSet<string>()
            });

            lock (user.ConnectionIds)
            {
                user.ConnectionIds.Add(connectionId);
                if (user.ConnectionIds.Count == 1)
                {
                    Clients.Others.userConnected(userName);
                }
            }

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string userName = Context.User.Identity.Name;
            string connectionId = Context.ConnectionId;

            UserHubModels user;
            Users.TryGetValue(userName, out user);

            if (user != null)
            {
                lock (user.ConnectionIds)
                {
                    user.ConnectionIds.RemoveWhere(cid => cid.Equals(connectionId));
                    if (!user.ConnectionIds.Any())
                    {
                        UserHubModels removedUser;
                        Users.TryRemove(userName, out removedUser);
                        Clients.Others.userDisconnected(userName);
                    }
                }
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}