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
    
    public partial class InstrumentReadingView
    {
        public string IDDam { get; set; }
        public string DamName { get; set; }
        public string TypeDam { get; set; }
        public string Location { get; set; }
        public string DamLongitude { get; set; }
        public string Damlatitude { get; set; }
        public Nullable<System.DateTime> CompletionDate { get; set; }
        public Nullable<double> ConstructionCost { get; set; }
        public string WaterSource { get; set; }
        public string Region { get; set; }
        public string Province { get; set; }
        public string Municipality { get; set; }
        public string Description { get; set; }
        public int IDInstrument { get; set; }
        public Nullable<double> ElevationTip { get; set; }
        public Nullable<double> ElevationUpper { get; set; }
        public Nullable<double> ElevationLower { get; set; }
        public string Latitude { get; set; }
        public string Instrument { get; set; }
        public string WaterLocation { get; set; }
        public int IDReading { get; set; }
        public Nullable<System.DateTime> DateRead { get; set; }
        public Nullable<System.TimeSpan> TimeRead { get; set; }
        public Nullable<double> DataRead { get; set; }
        public string DataNo { get; set; }
        public Nullable<double> DataX { get; set; }
        public Nullable<double> DataY { get; set; }
        public byte[] UploadedFile { get; set; }
        public Nullable<int> filetype { get; set; }
        public string contenttype { get; set; }
        public Nullable<int> YearCovered { get; set; }
    }
}
