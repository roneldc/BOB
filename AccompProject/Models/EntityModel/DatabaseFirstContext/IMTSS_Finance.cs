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
    
    public partial class IMTSS_Finance
    {
        public int IDIMTSS_FINANCE { get; set; }
        public string idaccomp { get; set; }
        public string asof { get; set; }
        public Nullable<int> mnt { get; set; }
        public Nullable<int> year_covered { get; set; }
        public Nullable<double> CapacityBuildingForIA { get; set; }
        public Nullable<double> CapacityBuildingForNIAStaff { get; set; }
        public Nullable<double> AssistanceProgram { get; set; }
        public Nullable<double> SupervisionCost { get; set; }
        public Nullable<double> OfficeSuppliesMaterials { get; set; }
        public Nullable<double> Miscellaneous { get; set; }
        public Nullable<double> FSS { get; set; }
        public Nullable<double> DBMS { get; set; }
        public Nullable<double> OtherMOOE { get; set; }
        public Nullable<double> JobOrder { get; set; }
        public Nullable<double> Daily { get; set; }
        public Nullable<double> Contractual { get; set; }
        [DataType(DataType.MultilineText)]
        [DisplayName("Remarks")]
        public string remarks { get; set; }
        public Nullable<double> CapacityBuildingForIAAllocation { get; set; }
        public Nullable<double> CapacityBuildingForNIAStaffAllocation { get; set; }
        public Nullable<double> AssistanceProgramAllocation { get; set; }
        public Nullable<double> SupervisionCostAllocation { get; set; }
        public Nullable<double> OfficeSuppliesMaterialsAllocation { get; set; }
        public Nullable<double> MiscellaneousAllocation { get; set; }
        public Nullable<double> FSSAllocation { get; set; }
        public Nullable<double> DBMSAllocation { get; set; }
        public Nullable<double> OtherMOOEAllocation { get; set; }
        public Nullable<double> JobOrderAllocation { get; set; }
        public Nullable<double> DailyAllocation { get; set; }
        public Nullable<double> ContractualAllocation { get; set; }
        
    }
}
