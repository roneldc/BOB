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
    
    public partial class ContractAmountHistoryView
    {
        public int ContractAmountID { get; set; }
        public Nullable<int> mnt { get; set; }
        public Nullable<int> yr { get; set; }
          [DisplayName("Contract Amount")]
        public Nullable<double> AmountBilled { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> DateApplied { get; set; }
          [DisplayName("Date Applied")]
        public Nullable<System.DateTime> DateReceived { get; set; }
        public Nullable<System.DateTime> DateEncode { get; set; }
        public string IDAccomp { get; set; }
        public Nullable<int> year { get; set; }
        public string region { get; set; }
        public string province { get; set; }
        public string mainproject { get; set; }
        public string maindescription { get; set; }
        public string subproject { get; set; }
        public string municipality { get; set; }
        public Nullable<double> amount { get; set; }
        public string ContractDescription { get; set; }
        public string ContractName { get; set; }
        public Nullable<System.DateTime> ContractDate { get; set; }
        public Nullable<double> ContractAmount { get; set; }
        public Nullable<double> PercentofContract { get; set; }
        public string ContractorName { get; set; }
        public Nullable<System.DateTime> TargetStart { get; set; }
        public Nullable<System.DateTime> TargetEnd { get; set; }
        public Nullable<System.DateTime> ActualStart { get; set; }
        public Nullable<System.DateTime> EstimatedEnd { get; set; }
        public string infra { get; set; }
        public Nullable<int> ContractDuration { get; set; }
        public Nullable<System.DateTime> RevisedExpiryDate { get; set; }
        public int ContractID { get; set; }
        public Nullable<double> ABCAmount { get; set; }
    }
}
