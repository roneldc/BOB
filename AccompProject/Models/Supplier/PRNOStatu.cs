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
    
    public partial class PRNOStatu
    {
        public int IDProcStatus { get; set; }
        public string ProcurementID { get; set; }
        public string PRNO { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> dateLog { get; set; }
        public Nullable<System.TimeSpan> timeLog { get; set; }
        public Nullable<int> id_key { get; set; }
        public string note { get; set; }
    }
}