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
    
    public partial class DamRehabilitation
    {
        public string IDDam { get; set; }
        public string DamName { get; set; }
        public string Location { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> CompletionDate { get; set; }
        public Nullable<double> ConstructionCost { get; set; }
        public string WaterSource { get; set; }
        public string Region { get; set; }
        public string Province { get; set; }
        public string Municipality { get; set; }
        public string Description { get; set; }
        [DisplayName("YEAR")]
        public Nullable<int> YearCovered { get; set; }
         [DisplayName("DATE")]
        public Nullable<System.DateTime> Daterehab { get; set; }
         [DisplayName("NATURE OF REVISION")]
        public string NatureOfRevision { get; set; }
        public int IDRehab { get; set; }
    }
}
