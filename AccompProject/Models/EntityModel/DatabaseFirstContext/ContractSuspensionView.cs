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
    
    public partial class ContractSuspensionView
    {
        public string IDAccomp { get; set; }
        public Nullable<int> year { get; set; }
        public string region { get; set; }
        public string province { get; set; }
        public string mainproject { get; set; }
        public string maindescription { get; set; }
        public string subproject { get; set; }
        public string municipality { get; set; }
        public Nullable<double> amount { get; set; }
        public int ContractID { get; set; }
        public string ContractName { get; set; }
        public string ContractDescription { get; set; }
        public Nullable<System.DateTime> ContractDate { get; set; }
        public Nullable<double> ContractAmount { get; set; }
        public Nullable<double> PercentofContract { get; set; }
        public string ContractorName { get; set; }
        public Nullable<System.DateTime> TargetStart { get; set; }
        public Nullable<System.DateTime> TargetEnd { get; set; }
        public Nullable<System.DateTime> ActualStart { get; set; }
        public Nullable<System.DateTime> EstimatedEnd { get; set; }
        public int idsuspension { get; set; }
        [Required(ErrorMessage = "This field is Required")]
        [DisplayName("Date of Suspension")]
        public Nullable<System.DateTime> DateSuspension { get; set; }
        [DisplayName("Date of Resumption")]
        public Nullable<System.DateTime> DateofResumption { get; set; }
        [DisplayName("Period Duration (cd)")]
        public Nullable<int> PeriodDuration { get; set; }
        [DisplayName("Reason / Justification")]
        [DataType(DataType.MultilineText)]
        public string Reason { get; set; }
       
    }
}
