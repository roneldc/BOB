using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccompProject.Models.EntityModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using AccompProject.Models;
using Microsoft.AspNet.Identity.Owin;
//using Microsoft.AspNet.Identity; // Maybe this one
using AccompProject.Controllers;
using System.Threading.Tasks;
using AccompProject;
using System.Net;
using System.Data.Entity;
using System.Data.Entity.Validation;
using AccompProject.Models.Others;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


namespace MVCInBuiltFeatures.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        AccomplishmentEntities db = new AccomplishmentEntities();
        //
        // GET: /Roles/
        public ActionResult Index()
        {
            //            var roles = context.Users.ToList();



            if (User.IsInRole("AdminFinance"))
            {
                return View(db.UsersProfiles.Where(r => r.RoleName == "Financial Region" || r.RoleName == "Financial").ToList());
            }

            return View(db.UsersProfiles.ToList());
        }

        public ActionResult ListOfRoles()
        {

            var roles = context.Roles.ToList();
            return View(roles);
        }



        public   ActionResult UpdateMntYr(int mnt = 0, int yr = 0)
        {

            var items =  db.UsersProfiles.AsNoTracking().Where(r => r.RoleName == "Financial Region" || r.RoleName == "Financial").ToList();
            int ii = 0;
            foreach (var item in items)
            {

                var i = db.AspNetUsers.First(x => x.Id == item.UserId);

                i.mnt = mnt;
                i.yr = yr;
                db.SaveChanges();
                ii++; 
            
            }




            string url = Url.Action("Index", "Roles", "");

            return Json(new { success = true, url = ii });
        
        }











        //
        // GET: /Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Roles/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                {

                    Name = collection["RoleName"]
                });
                context.SaveChanges();
                ViewBag.ResultMessage = "Role created successfully !";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Roles/Edit/5
        public ActionResult Edit(string roleName)
        {
            var thisRole = context.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            return View(thisRole);
        }

        //
        // POST: /Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Microsoft.AspNet.Identity.EntityFramework.IdentityRole role)
        {
            try
            {
                context.Entry(role).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Roles/Delete/5
        public ActionResult Delete(string RoleName)
        {
            var thisRole = context.Roles.Where(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            context.Roles.Remove(thisRole);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ManageUserMonth(string id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspnetuser = db.AspNetUsers.Find(id);
            if (aspnetuser == null)
            {
                return HttpNotFound();
            }


            //var myregion = db.TypeUsers.OrderBy(r => r.TypeUser1).ToList().Select(rr => new SelectListItem { Value = rr.TypeUser1.ToString(), Text = rr.TypeUser1 }).ToList();
            //ViewBag.tuser = myregion;


            return View(aspnetuser);

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageUserMonth([Bind(Include = "ID, Email, EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled, LockoutEndDateUtc, LockoutEnabled,AccessFailedCount,UserName, TypeUser,mnt,yr")] AspNetUser aspnetuser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(aspnetuser).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {

                            System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }

            }
            var myregion = db.TypeUsers.OrderBy(r => r.TypeUser1).ToList().Select(rr => new SelectListItem { Value = rr.TypeUser1.ToString(), Text = rr.TypeUser1 }).ToList();
            ViewBag.tuser = myregion;

            return View(aspnetuser);
        }


        public ActionResult ManageUserType(string id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspnetuser = db.AspNetUsers.Find(id);
            if (aspnetuser == null)
            {
                return HttpNotFound();
            }


            var myregion = db.TypeUsers.OrderBy(r => r.TypeUser1).ToList().Select(rr => new SelectListItem { Value = rr.TypeUser1.ToString(), Text = rr.TypeUser1 }).ToList();
            ViewBag.tuser = myregion;


            return View(aspnetuser);

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageUserType([Bind(Include = "ID, Email, EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled, LockoutEndDateUtc, LockoutEnabled,AccessFailedCount,UserName, TypeUser,mnt,yr")] AspNetUser aspnetuser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(aspnetuser).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {

                            System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }

            }
            var myregion = db.TypeUsers.OrderBy(r => r.TypeUser1).ToList().Select(rr => new SelectListItem { Value = rr.TypeUser1.ToString(), Text = rr.TypeUser1 }).ToList();
            ViewBag.tuser = myregion;

            return View(aspnetuser);
        }



        public ActionResult ManageUserRoles(string id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersProfile userProfile = db.UsersProfiles.Find(id);
            if (userProfile == null)
            {
                return HttpNotFound();
            }
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;


            return View(userProfile);


        }






        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageUserRoles(string UserName, string RoleName)
        {
            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            //var account = new AccountController();
            //account.UserManager.AddToRole(user.Id, RoleName);

            //Delete existing



            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);


            if (Session["prevRole"].ToString() == "none")
            {

                userManager.AddToRole(user.Id, RoleName);


            }
            else
            {

                userManager.RemoveFromRole(user.Id, Session["prevRole"].ToString());
                userManager.AddToRole(user.Id, RoleName);


            }




            ViewBag.ResultMessage = "Role created successfully !";

            // prepopulat roles for the view dropdown
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GetRoles(string UserName)
        {
            if (!string.IsNullOrWhiteSpace(UserName))
            {

                ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                //var account = new AccountController();
                ApplicationUserManager UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                //this.UserManager.GetRoles(user.Id);

                ViewBag.RolesForThisUser = await UserManager.GetRolesAsync(user.Id);
                // prepopulat roles for the view dropdown
                var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = list;


            }

            return View("ManageUserRoles");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleForUser(string UserName, string RoleName)
        {
            var account = new AccountController();
            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            if (account.UserManager.IsInRole(user.Id, RoleName))
            {
                account.UserManager.RemoveFromRole(user.Id, RoleName);
                ViewBag.ResultMessage = "Role removed from this user successfully !";
            }
            else
            {
                ViewBag.ResultMessage = "This user doesn't belong to selected role.";
            }
            // prepopulat roles for the view dropdown
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            return View("ManageUserRoles");
        }









        #region Finance Current to Monthly


        public ActionResult CurrentToMonthly()
        {


            TransferCurrentToMonthly tcm = new TransferCurrentToMonthly();
            tcm.GAA = 0;


            return View(tcm);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<ActionResult> CurrentToMonthly(TransferCurrentToMonthly tcm)
        {

            if (ModelState.IsValid)
            {

                string Constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection CON = new SqlConnection(Constring))
                {
                    CON.Open();
                    SqlCommand cmd = new SqlCommand("TransferCurrentToMonthly", CON);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@month", tcm.month));
                    cmd.Parameters.Add(new SqlParameter("@year", tcm.year));
                    cmd.Parameters.Add(new SqlParameter("@asof", tcm.asof));
                    cmd.Parameters.Add(new SqlParameter("@dateod", tcm.dateod));
                    cmd.Parameters.Add(new SqlParameter("@asofnot", tcm.asofnot));

               //   await Task.Run(() => cmd.ExecuteNonQuery());
                    cmd.ExecuteNonQuery();
                  return RedirectToAction("Index", "Home", "");
                    // return Json(new { success = true });

                }


            }







            return View(tcm);

        }


        public ActionResult CurrentToMonthlyLeftBehind()
        {


            TransferCurrentToMonthly tcm = new TransferCurrentToMonthly();
            tcm.asofnot = "00/3000";
            tcm.month = 0;
            tcm.dateod = "1";

            return View(tcm);

        }









        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CurrentToMonthlyLeftBehind(TransferCurrentToMonthly tcm)
        {

            if (ModelState.IsValid)
            {
                string str = "";
                string STRHONEY = "";
                string Constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection CON = new SqlConnection(Constring))
                {
                    CON.Open();


                    STRHONEY = "select idaccomp from report_physical WHERE amount > 0  and YEAR = " + Convert.ToInt32(tcm.GAA) + " and asof = '" + tcm.asof + "'";


                    SqlCommand cmd = new SqlCommand(STRHONEY, CON);
                    SqlDataReader sqlReader;
                    sqlReader = await Task.Run(() => cmd.ExecuteReader());

                    while (sqlReader.Read())
                    {
                        STRHONEY = "SELECT * FROM FINANCIALDETAILASOF WHERE ASOF = '" + tcm.asof + "'" + " AND IDACCOMP = @idaccomp";

                        using (SqlConnection CON1 = new SqlConnection(Constring))
                        {
                            CON1.Open();




                            SqlCommand cmd1 = new SqlCommand(STRHONEY, CON1);


                            cmd1.Parameters.AddWithValue("@idaccomp", sqlReader.GetValue(0));
                            SqlDataReader sqlReader1;
                            sqlReader1 = await Task.Run(() => cmd1.ExecuteReader());

                            if (sqlReader1.HasRows)
                            {
                                str = "Yes";
                            }
                            else
                            {
                                str = "No";

                            }
                        }







                        if (str == "No")
                        {

                            using (SqlConnection CON123 = new SqlConnection(Constring))
                            {
                                CON123.Open();


                                //  STRHONEY = "select * from report_physical WHERE amount > 0  and YEAR = " + Convert.ToInt32(tcm.year) + " and asof = '" + tcm.asof + "'";
                                STRHONEY = "Insert into financialdetailasof (idaccomp,mnt,yr,asof,saroamount,asaamount,buramount,asadate,sarono) values (@idaccomp,@mnt,@yr,@asof,@saroamount,@asaamount,@buramount,@asadate,@sarono)";


                                SqlCommand cmd123 = new SqlCommand(STRHONEY, CON123);



                                cmd123.Parameters.AddWithValue("@idaccomp", sqlReader.GetValue(0));
                                cmd123.Parameters.AddWithValue("@mnt", tcm.month);
                                cmd123.Parameters.AddWithValue("@yr", tcm.year);
                                cmd123.Parameters.AddWithValue("@asof", tcm.asof);
                                cmd123.Parameters.AddWithValue("@saroamount", Convert.ToInt32(0));
                                cmd123.Parameters.AddWithValue("@asaamount", Convert.ToInt32(0));
                                cmd123.Parameters.AddWithValue("@buramount", Convert.ToInt32(0));
                                cmd123.Parameters.AddWithValue("@asadate", DateTime.Now.ToShortDateString());
                                cmd123.Parameters.AddWithValue("@sarono", "jtv");



                                await Task.Run(() => cmd123.ExecuteNonQuery());
                                cmd123.Parameters.Clear();



                                cmd123.Dispose();




                            }




                        }
                    }
                }
                return RedirectToAction("Index", "Home", "");
            }







            return View(tcm);

        }
        #endregion

    }
}
