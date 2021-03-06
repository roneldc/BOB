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
    
    public partial class FSDEPhysicalView
    {
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
        public string maindescription { get; set; }
        public string IDAccomp { get; set; }
        public int IdFSDE { get; set; }
          [DisplayName("Type of Study")]
        public string typeofStudy { get; set; }
          [DisplayName("Mode of Implementation")]
        public string modeImplementation { get; set; }
          [DisplayName("Date of NTP")]
        public Nullable<System.DateTime> dateNTP { get; set; }
          [DisplayName("Activity Start")]
        public Nullable<System.DateTime> ActivityStart { get; set; }
          [DisplayName("Activity FInish")]
        public Nullable<System.DateTime> ActivityFinish { get; set; }
        public Nullable<int> mnt { get; set; }
        public Nullable<int> yr { get; set; }
          [DisplayName("As of")]
        public string asof { get; set; }
          [DisplayName("Status / Remarks")]
        public string remarks { get; set; }
          [DisplayName("Start of Implementaion / Construction (Year)")]
        public Nullable<int> startConstruction { get; set; }
        public double valueaccompfs { get; set; }
        public double valueaccompde { get; set; }
        public double expendituresfs { get; set; }
        public double expendituresde { get; set; }
          [DisplayName("FS (%)")]
        public Nullable<double> physicalfs { get; set; }
          [DisplayName("DE (%)")]
        public Nullable<double> physicalde { get; set; }
          [DisplayName("FS")]
        public Nullable<double> financialfs { get; set; }
          [DisplayName("DE")]
        public Nullable<double> financialde { get; set; }
        public double TotalValueAccomp { get; set; }
        public double TotalExpenditure { get; set; }
          [DisplayName("Physical (%)")]
        public Nullable<double> PhysicalPercent { get; set; }
          [DisplayName("Financial (%)")]
        public Nullable<double> FinancialPercent { get; set; }
        [DisplayName("FS")]  
        public Nullable<double> fsobligation { get; set; }
        [DisplayName("DE")]
        public Nullable<double> deobligation { get; set; }
    }
}
