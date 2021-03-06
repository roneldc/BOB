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
    
    public partial class ClimateChangeProfileView
    {
        public string IDAccomp { get; set; }
        public Nullable<int> year { get; set; }
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
          [DisplayName("AMOUNT (P'000)")]
        public Nullable<double> amount { get; set; }
        public string maindescription { get; set; }
        public string projectmonitor { get; set; }

        public int IDClimate { get; set; }
         [DataType(DataType.MultilineText)]
          [DisplayName("PROJECT ACTIVITY")]
        public string ProjectActivity { get; set; }
          [DisplayName("IMPLEMENTATION PERIOD")]
        public string ImplementationPeriod { get; set; }
          [DisplayName("TARGET")]
        public Nullable<int> Target { get; set; }
    }
}
