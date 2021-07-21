using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AccompProject.Models.Others
{
    public class TransferCurrentToMonthly
    {
         [DisplayName("MONTH IN NUMBER")]
         [Required(ErrorMessage = "Month is Required")]
        public Nullable<int> month { get; set; }
         [DisplayName("YEAR")]
         [Required(ErrorMessage = "Year is Required")]
         public Nullable<int> year { get; set; }
         [DisplayName("AS OF")]
         [Required(ErrorMessage = "AS Of Date is required")]
        public string asof { get; set; }
         [DisplayName("CUT-OFF DATE")]
         [Required(ErrorMessage = "Date is required")]
        public string dateod { get; set; }
         [DisplayName("AS OF NOT INCLUDED")]
         [Required(ErrorMessage = "Filed is required")]
         public string asofnot { get; set; }

         [DisplayName("GAA YEAR")]
         [Required(ErrorMessage = "YEAR is Required")]
         public Nullable<int> GAA { get; set; }
    }
}