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

    public partial class Appurtenance
    {
        public string IDDam { get; set; }
        public int IDAppurtenance { get; set; }
        public string SpillwayType { get; set; }
        public string SpillwayDimension { get; set; }
        public string SpillwayIDF { get; set; }
        public string OutletType { get; set; }
        public string OutletDimension { get; set; }
        public string OutletIDF { get; set; }
        public Nullable<int> YearCovered { get; set; }
        public Nullable<double> SpillwayCapacity { get; set; }
        public Nullable<double> SpillwayCrestlength { get; set; }
        public Nullable<double> SpillwayCrestLevel { get; set; }
        public Nullable<double> OutletDesignDischarge { get; set; }
        public Nullable<double> OutletConduitSize { get; set; }
        public Nullable<double> OutletConduitLength { get; set; }
        public string DiversionType { get; set; }
        public Nullable<double> DiversionDesignCapacity { get; set; }
        public Nullable<double> DiversionConduitDiameter { get; set; }
        public Nullable<double> DiversionConduitLength { get; set; }
        public string status { get; set; }
        
    }
}
