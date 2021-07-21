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
    
    public partial class FinancialNCAADA
    {
        public string IDAccomp { get; set; }
        public Nullable<int> IDFinance { get; set; }
        public int IDNCAADA { get; set; }
        [DisplayName("DATE")]
        public Nullable<System.DateTime> NCADate { get; set; }
         [DisplayName("NCA NO.")]
        public string NCANO { get; set; }
         [DisplayName("AMOUNT")]
        public Nullable<double> NCAAmount { get; set; }
         [DisplayName("DATE")]
        public Nullable<System.DateTime> NTADate { get; set; }
         [DisplayName("NTA NO.")]
        public string NTANO { get; set; }
         [DisplayName("AMOUNT")]
        public Nullable<double> NTAAmount { get; set; }
         [DisplayName("VALIDITY")]
        public Nullable<System.DateTime> Validity { get; set; }
         [DisplayName("COVERING")]
         [DataType(DataType.MultilineText)]
        public string Covering { get; set; }
         [DisplayName("ACCOUNT NO.")]
        public string AccountNo { get; set; }
         [DisplayName("DATE")]
        public Nullable<System.DateTime> ADADate { get; set; }
         [DisplayName("ADA NO.")]
        public string ADANO { get; set; }
         [DisplayName("AMOUNT")]
        public Nullable<double> ADAAmount { get; set; }
        public string blnk { get; set; }
    }
}
