using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccompProject.Models;
using AccompProject.Models.EntityModel;
using AccompProject.Hubs;
using System.Configuration;
using System.Data.SqlClient;
using AccompProject.Models.EntityModel.DatabaseFirstContext;
using System.Data;

namespace AccompProject.Controllers
{

    public class HomeController : Controller
    {
        private AccomplishmentEntities db = new AccomplishmentEntities();



        public ActionResult POWERBIView()
        {

            return View();
        
        }

        public ActionResult Bingo()
        {



            ViewBag.Temp = "Bingo!";
            return PartialView("_Bingo", ViewBag.Temp);
        }


        public ActionResult SendBingonoti(int empno = 0,string url  = null, string lname = null, string fname = null, string bingo = null)
   //  public ActionResult SendBingonoti(BingoUser bingo)
        {



            string numberstring = "";
            string Constring1 = ConfigurationManager.ConnectionStrings["AccomplishmentEntitiesSecurity"].ConnectionString;
            using (SqlConnection CON = new SqlConnection(Constring1))
            {
                CON.Open();


                SqlCommand cmd = new SqlCommand("Select title from bingotitle where id = @title", CON);
                int id = 2;

                cmd.Parameters.AddWithValue("@title", id);

                SqlDataReader reader;

                reader = cmd.ExecuteReader();
                if (reader.Read())
                {


                    numberstring = reader.GetString(0);
                   
                }


            }
            int userid = empno;
            string url1 =url;
            int existwinner = db.BingoWinners.Where(r => r.sixDigit == empno && r.title == numberstring).Count();


            if (existwinner <= 0)
            {

                string Constring = ConfigurationManager.ConnectionStrings["AccomplishmentEntitiesSecurity"].ConnectionString;
                using (SqlConnection CON1 = new SqlConnection(Constring))
                {
                    CON1.Open();
                    SqlCommand cmd1 = new SqlCommand("insert into bingowinner (email,url,datewin,timewin,title,sixdigit,lname,fname) values (@email,@url,@datewin,@timewin,@title,@sixdigit,@lname,@fname)", CON1);

                    cmd1.Parameters.AddWithValue("@email", userid.ToString());
                    cmd1.Parameters.AddWithValue("@datewin", DateTime.Now);
                    cmd1.Parameters.AddWithValue("@timewin", DateTime.Now.TimeOfDay);
                    cmd1.Parameters.AddWithValue("@url", url1); 
                    cmd1.Parameters.AddWithValue("@title", numberstring);
                    cmd1.Parameters.AddWithValue("@sixdigit", userid);
                    cmd1.Parameters.AddWithValue("@lname", lname);
                    cmd1.Parameters.AddWithValue("@fname", fname);
                    cmd1.ExecuteNonQuery();

                    //       return Json(new { success = true });

                }

            }


            NotificationBingoHub objNotifHub = new NotificationBingoHub();
            //     Notification objNotif = new Notification();

            objNotifHub.BingoModal(bingo,empno,lname,fname);



            string url2 = "";

            return Json(new { success = true, url = url2 });
            //return Json(new { suceess = true });





        }
        public JsonResult GetWinners()
        {
               string numberstring = "";

            string Constring1 = ConfigurationManager.ConnectionStrings["AccomplishmentEntitiesSecurity"].ConnectionString;
            using (SqlConnection CON = new SqlConnection(Constring1))
            {
                CON.Open();


                SqlCommand cmd = new SqlCommand("Select title from bingotitle where id = @title", CON);
                int id = 2;

                cmd.Parameters.AddWithValue("@title", id);

                SqlDataReader reader;

                reader = cmd.ExecuteReader();
                if (reader.Read())
                {


                    numberstring = reader.GetString(0);
                   
                }


            }
            var contacts = db.BingoWinners.Where(r=> r.title == numberstring ).ToList();

            var winner = new List<BingoWinnersPotential>();
       //    string url = Session["regiontolog"].ToString();
            foreach (var t in contacts) {

                DateTime? mydate = t.datewin;
                TimeSpan? mytime = t.timewin;

                winner.Add(new BingoWinnersPotential { 
                
                    UserID = t.email,
                    url = t.url,
                    datewinner = mydate.Value.ToString("d"),
                    timewinner = t.timewin.Value.ToString("T"),
                    empno = t.sixDigit,
                    lname = t.lname,
                    fname = t.fname
                    
                });
            
                
            
            }

          
            return Json(winner, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult CheckEntry(string entry = null,string title = null)
        {


            bool success = false;


            int checker = db.BingoNotis.Where(r => r.Title == title && r.Details == entry).Count();


            if (checker > 0) {

                success = true;
            
            }



            return Json(new { success = success });
        }


         [HttpPost]
        public JsonResult GetImage(string cate)
        {


            NotificationBingoHub objNotifHub = new NotificationBingoHub();
            
            var contacts = db.BingoImages.Where(r => r.Gametype == cate).FirstOrDefault();

            objNotifHub.ImageBingo(contacts.GameImage.ToString());
            Session["imge"] = contacts.GameImage.ToString();



            string Constring = ConfigurationManager.ConnectionStrings["AccomplishmentEntitiesSecurity"].ConnectionString;
            using (SqlConnection CON = new SqlConnection(Constring))
            {
                CON.Open();


                SqlCommand cmd = new SqlCommand("update bingotitle set title = @title, imagename = @imagename  where id = @id", CON);


                cmd.Parameters.AddWithValue("@title", contacts.Gametype.ToString());
                cmd.Parameters.AddWithValue("@imagename", contacts.GameImage.ToString());

                cmd.Parameters.AddWithValue("@id", 2);
                cmd.ExecuteNonQuery();


            }
            objNotifHub.GetNotification();
             return Json(contacts);
        }

        public ActionResult IndexBingo(int myid = 101141)
        {

            ViewBag.myid = myid;

            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "X Type", Value = "X Type" });
            items.Add(new SelectListItem { Text = "Block Out", Value = "Block Out" });

            BingoUser bingouser = new BingoUser();


            string Constring1 = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection CON = new SqlConnection(Constring1))
            {
                CON.Open();


                SqlCommand cmd = new SqlCommand("Select url from BingoEmployeeIDURL where empno = @id", CON);
               

                cmd.Parameters.AddWithValue("@id", myid);

                SqlDataReader reader;

                reader = cmd.ExecuteReader();
                if (reader.Read())
                {


                    bingouser.url = reader.GetString(0);

                }


            }

            string Constring11 = ConfigurationManager.ConnectionStrings["HRMIS"].ConnectionString;
            using (SqlConnection CON1 = new SqlConnection(Constring11))
            {
                CON1.Open();


                SqlCommand cmd1 = new SqlCommand("Select LNAME,FNAME from id_factory where NEXTID = @id", CON1);


                cmd1.Parameters.AddWithValue("@id", myid);

                SqlDataReader reader1;

                reader1 = cmd1.ExecuteReader();
                if (reader1.Read())
                {


                    bingouser.lname = reader1.GetString(0);
                    bingouser.fname = reader1.GetString(1);

                }


            }






            bingouser.empno = myid;




            ViewBag.typeBingo = items;

            return View(bingouser);
        }

        [HttpPost]
        public ActionResult IndexBingo(string url, string myid)
        {
            string constr = ConfigurationManager.ConnectionStrings["AccomplishmentEntitiesSecurity"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);

            try
            {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("spUpdateUrl", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@url", url);
                    cmd.Parameters.AddWithValue("@myid", myid);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            ViewBag.myid = myid;

            return IndexBingo(Convert.ToInt32(myid));
        }

        public ActionResult Entry()
        {

           

            ViewBag.typeBingo = new SelectList(db.BingoImages, "GameCode", "Gametype");


          
            return View();
        }


        public ActionResult Index()
        {
            ViewBag.kindReport = "/Accomplishment Reports/WEB/WEB Central Office - Region Project";
            ViewBag.nameReport = "FINANCIAL REGION";


           
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult POWERBI()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Map()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult IDD()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Chatter()
        {

            return View();
        }
        [Authorize]
        public ActionResult ChatRoom()
        {

            return View();
        }




        public FileContentResult Download(int id = 1)
        {
            //declare byte array to get file content from database and string to store file name

            byte[] fileData;
            string fileName;
            //create object of LINQ to SQL class
            //  DBContext dataContext = new DBContext();
            //using LINQ expression to get record from database for given id value
            var record = from p in db.FileAccomps
                         where p.FileType == id
                         select p;  
            //only one record will be returned from database as expression uses condtion on primary field
            //so get first record from returned values and retrive file content (binary) and filename
            fileData = (byte[])record.FirstOrDefault().Content.ToArray();
            fileName = record.FirstOrDefault().FileName;
            //return file and provide byte file content and file name
            return File(fileData, "text", fileName);

        }
    }
}