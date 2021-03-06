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
    
    public partial class ClimateChangeAccompView
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
        public string maindescription { get; set; }
        public string projectmonitor { get; set; }
        public int IDClimate { get; set; }
        public string ProjectActivity { get; set; }
        public string ImplementationPeriod { get; set; }
        public Nullable<int> Target { get; set; }
        public int IDClimateAccomp { get; set; }
         [DisplayName("ACTUAL")]
        public Nullable<int> Actual { get; set; }
         [DisplayName("PHYSICAL (%)")]
        public Nullable<double> Phy { get; set; }
         [DisplayName("FINANCIAL (%)")]
        public Nullable<double> Fin { get; set; }
          [DataType(DataType.MultilineText)]
         [DisplayName("REMARKS")]
        public string remarks { get; set; }
        public Nullable<int> mnt { get; set; }
        public Nullable<int> yr { get; set; }
         [DisplayName("AS OF")]
        public string asof { get; set; }
    }
}
