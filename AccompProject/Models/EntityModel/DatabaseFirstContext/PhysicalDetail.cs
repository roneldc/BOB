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
    
    public partial class PhysicalDetail
    {
        public string IDAccomp { get; set; }
        public Nullable<System.DateTime> as_of { get; set; }
        public int IDPhysical { get; set; }
        public Nullable<int> newed { get; set; }
        public Nullable<int> restored { get; set; }
        public Nullable<int> rehab { get; set; }
        public Nullable<double> canals { get; set; }
        public Nullable<double> canal_lining { get; set; }
        public Nullable<int> structures { get; set; }
        public Nullable<double> roads { get; set; }
        public Nullable<int> farmer_beneficiaries { get; set; }
        public Nullable<int> jobs { get; set; }
        public string remarks { get; set; }
        public Nullable<int> new_accomp { get; set; }
        public Nullable<int> resto_accomp { get; set; }
        public Nullable<int> rehab_accomp { get; set; }
        public Nullable<double> canals_accomp { get; set; }
        public Nullable<double> canal_lining_accomp { get; set; }
        public Nullable<int> structures_accomp { get; set; }
        public Nullable<double> roads_accomp { get; set; }
        public Nullable<int> Beneficiary_accomp { get; set; }
        public Nullable<int> JobGen { get; set; }
        [DisplayName("Physical Percentage (%)")]
        public Nullable<double> Physical { get; set; }
        [DisplayName("Financial Percentage (%)")]
        public Nullable<double> Financial { get; set; }
        [DataType(DataType.MultilineText)]
        [DisplayName("REMARKS")]
        public string Remarks_accomp { get; set; }
        public string status { get; set; }
        [DisplayName("VALUE ACCOMPLISHMENT")]
        public Nullable<double> Value_accomp { get; set; }
        [DisplayName("EXPENDITURES")]
        public Nullable<double> Expenditures { get; set; }
        public Nullable<double> Phy { get; set; }
        public Nullable<double> Fin { get; set; }
        public Nullable<double> FUSA { get; set; }
        public string OK { get; set; }
        public string MONITORING1 { get; set; }
        public string MONITORING2 { get; set; }
        public Nullable<double> PC { get; set; }
        public string VAL { get; set; }
        public Nullable<double> EXP { get; set; }
        public string SAMPLE { get; set; }
        [DisplayName("SARO")]
        public Nullable<double> saro { get; set; }
        [DisplayName("ASA")]
        public Nullable<double> asa { get; set; }
        public Nullable<int> p_new { get; set; }
        public Nullable<int> p_resto { get; set; }
        public Nullable<int> p_rehab { get; set; }
        public Nullable<double> p_canal { get; set; }
        public Nullable<double> p_canal_lining { get; set; }
        public Nullable<double> p_structure { get; set; }
        public Nullable<double> p_road { get; set; }
        public Nullable<int> p_job { get; set; }
        public Nullable<int> p_fb { get; set; }
        [DisplayName("DISBURSEMENT")]
        public Nullable<double> disbursement { get; set; }
        [ConcurrencyCheck]
        public byte[] RowVersion { get; set; }
        public Nullable<int> mnt { get; set; }
        public Nullable<int> year_covered { get; set; }
        public string mainproject { get; set; }
        public Nullable<double> amount { get; set; }
        public string subproject { get; set; }
        public string municipality { get; set; }
        public string province { get; set; }
    }
}
