using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using AccompProject.Models;
using AccompProject.Models.EntityModel;
using System.Configuration;
using System.Data.SqlClient;
using AccompProject.Services;
using AccompProject.Helpers;


namespace AccompProject.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {
        private AccomplishmentEntities db = new AccomplishmentEntities();
        private InventoryEntities db2 = new InventoryEntities();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {

                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {


                case SignInStatus.Success:

                    var typeuser = db.AspNetUsers
                    .Where(a => a.Email.Equals(model.Email)).FirstOrDefault();

                    Session["regiontolog"] = typeuser.TypeUser.ToString();
                    Session["mnttoedit"] = typeuser.mnt.ToString();
                    Session["yrtoedit"] = typeuser.yr.ToString();
                    Session["userid"] = typeuser.Id.ToString();
                    Session["email"] = typeuser.Email.ToString();

                    var typeuserRole = db.UsersProfiles.Where(a => a.UserName.Equals(model.Email)).FirstOrDefault();


                    //save audit trail

                    string Constring = ConfigurationManager.ConnectionStrings["AccomplishmentEntitiesSecurity"].ConnectionString;
                    using (SqlConnection CON = new SqlConnection(Constring))
                    {
                        CON.Open();
                        SqlCommand cmd = new SqlCommand("insert into userlogintrail (userid,datelogin,timelogin) values (@userid,@datelogin,@timelogin)", CON);

                        cmd.Parameters.AddWithValue("@userid", typeuser.Id.ToString());
                        cmd.Parameters.AddWithValue("@datelogin", DateTime.Now);
                        cmd.Parameters.AddWithValue("@timelogin", DateTime.Now.TimeOfDay);
                        cmd.ExecuteNonQuery();

                        //       return Json(new { success = true });

                    }

                    //location
                    //           string Constring11 = ConfigurationManager.ConnectionStrings["AccomplishmentEntitiesSecurity"].ConnectionString;
                    //using (SqlConnection CON11 = new SqlConnection(Constring11))
                    //{
                    //    CON11.Open();
                    //    SqlCommand cmd = new SqlCommand("delete from maplocuser where userid = @userid" , CON11);

                    //    cmd.Parameters.AddWithValue("@userid", Session["userid"].ToString());



                    //    cmd.ExecuteNonQuery();

                    //    //       return Json(new { success = true });

                    //}
                    //if (!string.IsNullOrWhiteSpace(model.longi)) { 

                    //     string Constring1 = ConfigurationManager.ConnectionStrings["AccomplishmentEntitiesSecurity"].ConnectionString;
                    //        using (SqlConnection CON1 = new SqlConnection(Constring1))
                    //        {
                    //            CON1.Open();
                    //            SqlCommand cmd = new SqlCommand("insert into maplocuser (username,lati,longi,status,userid) values (@username,@lati,@longi,@status,@userid)", CON1);

                    //            cmd.Parameters.AddWithValue("@userid",typeuser.Id.ToString());
                    //            cmd.Parameters.AddWithValue("@lati", model.lati);
                    //            cmd.Parameters.AddWithValue("@longi", model.longi);
                    //            cmd.Parameters.AddWithValue("@username", typeuser.Email.ToString());
                    //            cmd.Parameters.AddWithValue("@status", "Online");



                    //            cmd.ExecuteNonQuery();

                    //     //       return Json(new { success = true });

                    //        }

                    //}
                    ////  sms
                    //var msg = "User name " + typeuser.Email.ToString() + " is now Log In";
                    //var smsnum = db.SMS
                    //     .Where(a => a.projectmonitor == "ALL").ToList();

                    //SMS sSMS = new SMS();

                    //if (smsnum.Count != 0)
                    //{

                    //    for (int i = 0; i < smsnum.Count; i++)
                    //    {


                    //        sSMS.sendSMS(smsnum[i].SMSnumber.ToString(), msg);
                    //        System.Threading.Thread.Sleep(3000);


                    //    }

                    //}



                    if (typeuserRole.RoleName.ToString() == "Personnel")
                    {

                        return RedirectToAction("IndexEmployee", "InventoryMapping", "");

                    }

                    if (typeuserRole.RoleName.ToString() == "Bingo")
                    {

                        return RedirectToAction("IndexBingo", "Home", "");

                    }


                    return RedirectToLocal(returnUrl);

                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };






                var result = await UserManager.CreateAsync(user, model.Password);



                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }




        // GET: /Account/RegisterSupplier
        [AllowAnonymous]
        public ActionResult RegisterSupplier()
        {
            ViewBag.TypeOfRegion = new SelectList(db2.TblRegionLogins.OrderBy(r => r.SORT), "id_region", "region");
         
            return View();
        }

        //
        // POST: /Account/RegisterSupplier
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterSupplier(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };




                var result = await UserManager.CreateAsync(user, model.Password);



            


                if (result.Succeeded)
                {









                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    int idreg = Convert.ToInt32(model.region);
                    var region = db2.TblRegionLogins.Where(r => r.id_region == idreg).FirstOrDefault();

                    //Supplier Profile

                    procurementss_onlinejervyEntities db1 = new procurementss_onlinejervyEntities();
                    var supplier = new ProcurementSupplierProfile()
                    {
                        SupplierID = Guid.NewGuid().ToString(),
                        ContactNo = model.cpnumber,
                        SupplierAddress = model.Address,
                        SupplierName = model.SupplierName,
                        userid = user.Id,
                        status = "new",
                        region = region.region



                    };

                    db1.ProcurementSupplierProfiles.Add(supplier);
                    db1.SaveChanges();

                    #region sms
                    //sms

                    var msg = "";

                    msg = msg + " New Supplier " + model.SupplierName;
                    msg = msg + " Please Don't reply this is Auto Generated.";

                    SMS sSMS = new SMS();

                    var smsnum = db.SMS
                   .Where(a => a.monitoredarea == region.region && a.projectmonitor == "PPD" && a.position == "CANVASSER").ToList();




                    if (smsnum.Count != 0)
                    {
                        for (int i = 0; i < smsnum.Count; i++)
                        {
                            if (smsnum[i].status.ToString() == "YES")
                            {


                                sSMS.sendSMS(smsnum[i].SMSnumber.ToString(), msg);
                                //  System.Threading.Thread.Sleep(3000);
                            }


                        }
                    }





                    #endregion

                    #region email

                    EMAIL myhelper = new EMAIL();

                    string emailMsg = "We Receive your registration, Please wait while our Procurement Property Division check your registration!";
                    string emailSubject = "NIA Email Notification";
              //      string emailMsgSMS = "DDD";
                    // Sending Email.  
                 //   await myhelper.SendEmailAsync(model.Email.ToString(), emailMsg, emailSubject);

                    #endregion




                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
                ViewBag.TypeOfRegion = new SelectList(db2.TblRegionLogins.OrderBy(r => r.SORT), "id_region", "region",model.region);
         
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                //  if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                if (user == null)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("Invalid");
                    // return View("ResetPassword");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                //string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                //var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                //await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                Session["em"] = user.Email;
                return RedirectToAction("ResetPassword", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {

            //    return code == null ? View("Error") : View();


            return View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            var code = UserManager.GeneratePasswordResetToken(user.Id);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);

            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();

            if (Session["userid"] == null)
            {

                return RedirectToAction("Login", "Account");
            }



            string Constring1 = ConfigurationManager.ConnectionStrings["AccomplishmentEntitiesSecurity"].ConnectionString;
            using (SqlConnection CON1 = new SqlConnection(Constring1))
            {
                CON1.Open();
                SqlCommand cmd = new SqlCommand("delete from maplocuser where userid = @userid", CON1);

                
                cmd.Parameters.AddWithValue("@userid", Session["userid"].ToString());



                cmd.ExecuteNonQuery();

                //       return Json(new { success = true });

            }



            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}