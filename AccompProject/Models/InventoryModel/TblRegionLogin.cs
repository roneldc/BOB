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
    
    public partial class TblRegionLogin
    {
        public int id_region { get; set; }
        public string region { get; set; }
        public string pwd { get; set; }
        public Nullable<int> cntlog { get; set; }
        public Nullable<int> SORT { get; set; }
        public string ISLAND { get; set; }
        public Nullable<double> reg_pot { get; set; }
        public Nullable<double> master_pot { get; set; }
    }
}