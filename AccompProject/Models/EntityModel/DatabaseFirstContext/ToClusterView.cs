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
    
    public partial class ToClusterView
    {
        public string IDAccomp { get; set; }
        public Nullable<System.DateTime> DateSelected { get; set; }
        public Nullable<System.TimeSpan> TimeSelected { get; set; }
        public int IDCluster { get; set; }
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
        public string subsubproject { get; set; }
        public string municipality { get; set; }
        public Nullable<double> amount { get; set; }
        public Nullable<double> Allotment { get; set; }
        public Nullable<double> Obligation { get; set; }
        public Nullable<int> newed { get; set; }
        public Nullable<int> restored { get; set; }
        public Nullable<int> rehab { get; set; }
        public Nullable<double> canals { get; set; }
        public string maindescription { get; set; }
    }
}
