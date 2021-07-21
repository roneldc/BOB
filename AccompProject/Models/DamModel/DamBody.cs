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

    public partial class DamBody
    {
        public string IDDam { get; set; }
        public int IDDamBody { get; set; }
        public Nullable<double> Structural { get; set; }
        public Nullable<double> CrestLength { get; set; }
        public Nullable<double> RiverbedElevation { get; set; }
        public Nullable<double> CrestWidth { get; set; }
        public Nullable<double> ReservoirCapacity { get; set; }
        public string SlopeUpstream { get; set; }
        public string SlopeDownstream { get; set; }
        public string DamSize { get; set; }
        public string DamHazardRisk { get; set; }
        public Nullable<int> YearCovered { get; set; }
        public Nullable<double> ElevationLowestStreambed { get; set; }
        public Nullable<double> HeightLowestStreambed { get; set; }
        public Nullable<double> DamCrest { get; set; }
        public Nullable<double> NormalReservoir { get; set; }
        public Nullable<double> TopInactive { get; set; }
        public Nullable<double> MaximumReservoir { get; set; }
    }
}