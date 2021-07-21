//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AccompProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;


    public partial class FinancialDetail :IValidatableObject
    {
        public string IDAccomp { get; set; }
        public Nullable<System.DateTime> as_of { get; set; }
        public int IDFinance { get; set; }
        [DisplayName("NO.")]
        public string sarono { get; set; }
        [DisplayName("AMOUNT")]
        public Nullable<double> saroamount { get; set; }
        [DisplayName("NO.")]
        public string asano { get; set; }
        [DisplayName("AMOUNT")]
        public Nullable<double> asaamount { get; set; }
        [DisplayName("NO.")]
        public string disbursement_co_no { get; set; }
        [DisplayName("DISBURSEMENT (AMOUNT)")]
        public Nullable<double> disbursement_co { get; set; }
        [DisplayName("NO.")]
        public string disbursement_region_no { get; set; }
        [DisplayName("DISBURSEMENT (AMOUNT)")]
        public Nullable<double> disbursement_region { get; set; }
        [DisplayName("NO.")]
        public string obligation_co_no { get; set; }
        [DisplayName("AMOUNT")]
        public Nullable<double> obligation_co { get; set; }
        [DisplayName("NO.")]
        public string obligation_region_no { get; set; }
        [DisplayName("AMOUNT")]
        public Nullable<double> obligation_region { get; set; }
        [DisplayName("NO.")]
        public string ncano { get; set; }
        [DisplayName("AMOUNT")]
        public Nullable<double> ncaamount { get; set; }
        [DisplayName("DUE AND DEMANDABLE NO.")]
        public string ddno { get; set; }
        [DisplayName("DUE AND DEMANDABLE (AMOUNT)")]
        public Nullable<double> ddamount { get; set; }
        [DisplayName("NO.")]
        public string ntano { get; set; }
        [DisplayName("AMOUNT")]
        public Nullable<double> ntaamount { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("JEV DATE")]
        public Nullable<System.DateTime> jevdate { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("TRANSACTION DATE")]
        public Nullable<System.DateTime> transactiondate { get; set; }
        [DataType(DataType.MultilineText)]
        [DisplayName("REMARKS")]
        public string remarksfinancial { get; set; }
        public int mnt { get; set; }
        public int yr { get; set; }
        //   [DataType(DataType.Date)]
        [DisplayName("DATE")]
        public Nullable<System.DateTime> asadate { get; set; }
      //  [Required]
        [DisplayName("FUND CODE")]
        public string fundCode { get; set; }
        [DisplayName("NO.")]
        public string adano { get; set; }
        [DisplayName("AMOUNT")]
        public Nullable<double> adaamount { get; set; }
        [DisplayName("NO.")]
        public string burno { get; set; }
        [DisplayName("AMOUNT")]
        public Nullable<double> buramount { get; set; }
        [DisplayName("TYPE")]
        public string typefinance { get; set; }
    //   [Required(ErrorMessage = "This can't be blank")]
        [Required]
        [DisplayName("PARTICULARS")]
        public string particulars { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (particulars == "")
            {
                yield return new ValidationResult("Pariculars field can't be blank");
            }
         

        }
  

    }
}