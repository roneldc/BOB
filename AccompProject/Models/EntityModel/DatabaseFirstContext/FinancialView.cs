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
    
    public partial class FinancialView
    {
        public string IDAccomp { get; set; }
        [DisplayName("YEAR")]
        public Nullable<int> year { get; set; }
        [DisplayName("REGION")]
        public string region { get; set; }
        [DisplayName("PROVINCE")]
        public string province { get; set; }
        public string district { get; set; }
        public string Category1 { get; set; }
        public string Category2 { get; set; }
        public string Category3 { get; set; }
        public string Category4 { get; set; }
        [DisplayName("MAINPROJECT")]
        public string mainproject { get; set; }
        [DisplayName("SUBPROJECT")]
        public string subproject { get; set; }
        [DisplayName("MUNICIPALITY")]
        public string municipality { get; set; }
        [DisplayName("ALLOCATION (P'000)")]
        public Nullable<double> amount { get; set; }
        public Nullable<System.DateTime> as_of { get; set; }
        [Key]
        public int IDFinance { get; set; }
        [DisplayName("SARO NO.")]
        public string sarono { get; set; }
        [DisplayName("SARO AMOUNT")]
        public Nullable<double> saroamount { get; set; }
        [DisplayName("ASA NO.")]
        public string asano { get; set; }
        [DisplayName("ASA AMOUNT")]
        public Nullable<double> asaamount { get; set; }
        [DisplayName("DISBURSEMENT NO.")]
        public string disbursement_co_no { get; set; }
        [DisplayName("DISBURSEMENT AMOUNT")]
        public Nullable<double> disbursement_co { get; set; }
        [DisplayName("DISBURSEMENT NO.")]
        public string disbursement_region_no { get; set; }
        [DisplayName("DISBURSEMENT AMOUNT")]
        public Nullable<double> disbursement_region { get; set; }
        [DisplayName("OBLIGATION NO.")]
        public string obligation_co_no { get; set; }
        [DisplayName("OBLIGATION AMOUNT")]
        public Nullable<double> obligation_co { get; set; }
        [DisplayName("OBLIGATION NO.")]
        public string obligation_region_no { get; set; }
        [DisplayName("OBLIGATION AMOUNT")]
        public Nullable<double> obligation_region { get; set; }
        [DisplayName("NCA NO.")]
        public string ncano { get; set; }
        [DisplayName("NCA AMOUNT")]
        public Nullable<double> ncaamount { get; set; }
          [DisplayName("DUE AND DEMANDABLE NO.")]
        public string ddno { get; set; }
          [DisplayName("DUE AND DEMANDABLE AMOUNT")]
        public Nullable<double> ddamount { get; set; }
        [DisplayName("NTA NO.")]
        public string ntano { get; set; }
        [DisplayName("NTA AMOUNT")]
        public Nullable<double> ntaamount { get; set; }
        [DisplayName("JEV DATE")]
        [Required(ErrorMessage = "JEV Date is required")]
        public Nullable<System.DateTime> jevdate { get; set; }
        [DisplayName("TRANSACTION DATE")]
        [Required(ErrorMessage="Transaction Date is required")]
        public Nullable<System.DateTime> transactiondate { get; set; }
        [DisplayName("REMARKS")]
        public string remarksfinancial { get; set; }
        public Nullable<int> yr { get; set; }
        public Nullable<int> mnt { get; set; }
        // [DataType(DataType.Date)]
        [DisplayName("DATE")]
        public Nullable<System.DateTime> asadate { get; set; }
        public string fundCode { get; set; }
        [DisplayName("ADA NO.")]
        public string adano { get; set; }
        [DisplayName("ADA AMOUNT")]
        public Nullable<double> adaamount { get; set; }
        public string burno { get; set; }
        [DisplayName("BUR AMOUNT")]
        public Nullable<double> buramount { get; set; }
        [DisplayName("TYPE OF RELEASE")]
        public string TYPERELEASE { get; set; }
        [DisplayName("REFERENCE NO.")]
        public string REFERENCENO { get; set; }
        public string asof { get; set; }
        public string maindescription { get; set; }
        [DisplayName("TYPE")]
        public string typefinance { get; set; }
        [DisplayName("PARTICULARS")]
        public string particulars { get; set; }
        public string financeregion { get; set; }
        public string OPERATIONREGION { get; set; }

    }
}