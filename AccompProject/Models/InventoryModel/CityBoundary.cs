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
    
    public partial class CityBoundary
    {
        public int CityID { get; set; }
        public string Cityname { get; set; }
        public string CityBoundariesname { get; set; }
        public Nullable<int> CovidPositive { get; set; }
        public Nullable<int> CovidDied { get; set; }
        public Nullable<int> CovidRecovered { get; set; }
        public string dateasof { get; set; }
    }
}
