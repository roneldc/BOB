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
    
    public partial class ContractSuspension
    {
        public string IDAccomp { get; set; }
        public int idsuspension { get; set; }
        public Nullable<System.DateTime> DateSuspension { get; set; }
        public Nullable<System.DateTime> DateofResumption { get; set; }
        public Nullable<int> PeriodDuration { get; set; }
        public string Reason { get; set; }
        public Nullable<int> ContractID { get; set; }
    }
}
