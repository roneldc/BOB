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
    
    public partial class RegionalFinancialViewForOD
    {
        public string IDAccomp { get; set; }
        public Nullable<int> year { get; set; }
        public string region { get; set; }
        public string province { get; set; }
        public string district { get; set; }
        public string Category1 { get; set; }
        public string Category2 { get; set; }
        public string Category3 { get; set; }
        public string Category4 { get; set; }
        public string mainproject { get; set; }
        public string subproject { get; set; }
        public string municipality { get; set; }
        public Nullable<double> amount { get; set; }
        public string sarono { get; set; }
        public Nullable<double> saroamount { get; set; }
        public string asano { get; set; }
        public Nullable<double> asaamount { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public Nullable<double> Obligationamount { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public Nullable<double> Disbursement { get; set; }
        public string maindescription { get; set; }
        public int IDFinance { get; set; }
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        [DisplayName("CASH")]
        public Nullable<double> cash { get; set; }
    }
}
