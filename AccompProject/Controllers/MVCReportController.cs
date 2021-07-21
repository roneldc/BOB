using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccompProject.Models.EntityModel;

namespace Accomplishment.Controllers
{
    public class MVCReportController : Controller
    {
        //
        // GET: /MVCReport/


        
       
       [Authorize]
       public ActionResult ViewReport(string temp)
        {
            
            if (temp == "Regional") {
                ViewBag.kindReport = "/Accomplishment Reports/WEB/WEB Overall - Physical Accomplishment";
                //ViewBag.kindReport = "/Accomplishment Reports/WEB/WEB Overall Region";
                ViewBag.nameReport = "Regional Summary";
            }

            else if (temp == "Mainproject")
            {
                ViewBag.kindReport = "/Accomplishment Reports/WEB/WEB Mainproject - Physical Accomplishment";
                ViewBag.nameReport = "Mainproject Summary";
            }

            else if (temp == "Detail")
            {
                ViewBag.kindReport = "/Accomplishment Reports/WEB/WEB Detail - Physical Accomplishment";
                ViewBag.nameReport = "Detail Accomplishment";
            }

            else if (temp == "CSummary")
            {
                ViewBag.kindReport = "/Accomplishment Reports/WEB/WEB Contract Reports Per Project";
                ViewBag.nameReport = "Summary per Contract of Project";
            }


            else {
                 ViewBag.kindReport = "/Accomplishment Reports/WEB/WEB Detail - Physical Accomplishment";
                ViewBag.nameReport = "Detail Accomplishment";
            }     
  

           
           ViewBag.tempo = temp;

            return View();

        }

    //    IDetail

       public ActionResult SampleView() {

           return View();
       
       }






       [Authorize]
       public ActionResult Financial(string temp)
       {

           if (temp == "FList")
           {
               ViewBag.kindReport = "/Accomplishment Reports/WEB/WEB List Of ASA";
               ViewBag.nameReport = "List of ASA Release";
           }

           else if (temp == "FAR")
           {
               ViewBag.kindReport = "/Accomplishment Reports/WEB/WEB FAR";
               ViewBag.nameReport = "FAR";
           }


           else if (temp == "FDetail")
           {
               ViewBag.kindReport = "/Accomplishment Reports/WEB/WEB Detail Status of Funds Per Responsibility Center - New Current";
               ViewBag.nameReport = "Detail Financial";
           }

           else if (temp == "Monthly")
           {
               ViewBag.kindReport = "/Accomplishment Reports/WEB/WEB Status of Funds Per Responsibility Center - New Current";
               ViewBag.nameReport = "Monthly Financial";
           }

           else if (temp == "dd")
           {
              
           }
           else
           {
               ViewBag.kindReport = "/Accomplishment Reports/WEB/WEB Detail Status of Funds Per Responsibility Center - New Current";
               ViewBag.nameReport = "Detail Financial";
            //   ViewBag.kindReport = "/Accomplishment Reports/WEB/WEB Financial Detail";
            //   ViewBag.nameReport = "Detail Financial";

               //ViewBag.kindReport = "/Accomplishment Reports/WEB/WEB Overall Region";
               //ViewBag.nameReport = "Regional Summary";
           }

           ViewBag.tempo = temp;

           return View();

       }

       [Authorize]
       public ActionResult Inventory(string temp)
       {

           if (temp == "IList")
           {
               ViewBag.kindReport = "/Inventory Report/WEB/WEB INVENTORY REPORT";
               ViewBag.nameReport = "List of NIS, CIS, PIS AND OGA";
           }

           else if (temp == "IRegion")
           {
               ViewBag.kindReport = "/Inventory Report/WEB/WEB Summary Of Region";
               ViewBag.nameReport = "List of NIS, CIS, PIS AND OGA";
           }


           else if (temp == "IProvince")
           {
               ViewBag.kindReport = "/Inventory Report/WEB/WEB Summary Of Province";
               ViewBag.nameReport = "List of NIS, CIS, PIS AND OGA";
           }

           else if (temp == "ISystem")
           {
               ViewBag.kindReport = "/Inventory Report/WEB/WEB NIS Systems";
               ViewBag.nameReport = "List of NIS, CIS, PIS AND OGA";
           }

           else if (temp == "IDetail")
           {
               ViewBag.kindReport = "/Inventory Report/WEB/WEB NIS Systems";
               ViewBag.nameReport = "List of NIS, CIS, PIS AND OGA";
           }
           else if (temp == "sampleDetail")
           {
               ViewBag.kindReport = "/Inventory Report/WEB/Overall Report Per Region";
               ViewBag.nameReport = "List of NIS, CIS, PIS AND OGA";
           }


           else if (temp == "form1")
           {
               ViewBag.kindReport = "/Inventory Report/WEB/WEB FORM 1 INVENTORY REPORT - Detailed";
               ViewBag.nameReport = "List of NIS, CIS, PIS AND OGA";
           }
           else if (temp == "form2")
           {
               ViewBag.kindReport = "/Inventory Report/WEB/WEB FORM 2 INVENTORY REPORT - Detailed";
               ViewBag.nameReport = "List of NIS, CIS, PIS AND OGA";
           }

           else if (temp == "form3")
           {
               ViewBag.kindReport = "/Inventory Report/WEB/WEB FORM 3 INVENTORY REPORT - Detailed";
               ViewBag.nameReport = "List of NIS, CIS, PIS AND OGA";
           }
         
           else
           {
              // ViewBag.kindReport = "/Inventory Report/WEB/Overall Report Per Region";
              ViewBag.kindReport = "/Inventory Report/WEB/WEB INVENTORY REPORT";
               ViewBag.nameReport = "List of NIS, CIS, PIS AND OGA"; 
           }

           ViewBag.tempo = temp;

           return View();

       }


       [Authorize]
       public ActionResult DBMPOW(string temp)
       {

           if (temp == "IList")
           {
               ViewBag.kindReport = "/Inventory Report/WEB/WEB INVENTORY REPORT";
               ViewBag.nameReport = "List of NIS, CIS, PIS AND OGA";
           }

           else if (temp == "IRegion")
           {

           }


           else if (temp == "IProvince")
           {

           }

           else if (temp == "ISystem")
           {

           }
           else
           {
               ViewBag.kindReport = "/Annual Budget/WEB List Of Subprojects with Mode of Implementation";
               ViewBag.nameReport = "List Of Subprojects with Mode of Implementation";
           }

           ViewBag.tempo = temp;

           return View();

       }


       [Authorize]
       public ActionResult Presentation(string temp)
       {

           if (temp == "IList")
           {
        //       ViewBag.kindReport = "/Inventory Report/WEB/WEB INVENTORY REPORT";
          //    ViewBag.nameReport = "List of NIS, CIS, PIS AND OGA";
           }

           else if (temp == "IRegion")
           {

           }


           else if (temp == "IProvince")
           {

           }

           else if (temp == "ISystem")
           {

           }
           else
           {
               ViewBag.kindReport = "/Inventory Report/WEB/Overall Report Per Region";
               ViewBag.nameReport = "List Of Subprojects with Mode of Implementation";
           }

           ViewBag.tempo = temp;

           return View();

       }



       [Authorize]
       public ActionResult IMTSSFINANCIAL(string temp)
       {

          
               ViewBag.kindReport = "/Accomplishment Reports/WEB/IMTSS FINANCIAL REGION";
               ViewBag.nameReport = "IMTSS FINANCIAL REGION";
                

           ViewBag.tempo = temp;

           return View();

       }

       [Authorize]
       public ActionResult IMTSSPHYSICAL(string temp)
       {


          


       
           if (temp == "IMTSSList")
           {
               ViewBag.kindReport = "/Accomplishment Reports/WEB/IDD per System IMTSS";
               ViewBag.nameReport = "IDD PHYSICAL REGION IMTSS";
           }

           else if (temp == "IMTSSMONTHLY")
           {
               ViewBag.kindReport = "/Accomplishment Reports/WEB/IDD Per System Monthly Accomplishment IMTSS";
               ViewBag.nameReport = "IDD PHYSICAL REGION IMTSS";
              
           }


           else
           {
               ViewBag.kindReport = "/Accomplishment Reports/WEB/IDD per System IMTSS";
               ViewBag.nameReport = "IDD PHYSICAL REGION IMTSS";
           }

           ViewBag.tempo = temp;

           return View();


       }
       public ActionResult NISRIPPHYSICAL(string temp)
       {


           if (temp == "NISRIPList")
           {
               ViewBag.kindReport = "/Accomplishment Reports/WEB/IDD per System NISRIP";
               ViewBag.nameReport = "IDD PHYSICAL REGION NISRIP";
           }

           else if (temp == "NISRIPMONTHLY")
           {
               ViewBag.kindReport = "/Accomplishment Reports/WEB/IDD Per System Monthly Accomplishment NISRIP";
               ViewBag.nameReport = "IDD PHYSICAL REGION NISRIP";

           }


           else
           {
               ViewBag.kindReport = "/Accomplishment Reports/WEB/IDD per System NISRIP";
               ViewBag.nameReport = "IDD PHYSICAL REGION NISRIP";
           }

           ViewBag.tempo = temp;

           return View();





       }




       public ActionResult FINANCIALONLINE(string temp)
       {



           ViewBag.kindReport = "/Accomplishment Reports/WEB/WEB Central Office - Region Project";
               ViewBag.nameReport = "FINANCIAL REGION";
         

           ViewBag.tempo = temp;

           return View();





       }
    }
}
