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
    using System.Web;
    
    public partial class DIMEPicView
    {
        public string IDAccomp { get; set; }
        public Nullable<int> year { get; set; }
        public string region { get; set; }
        public string province { get; set; }
        public string district { get; set; }
        public string Category1 { get; set; }
        public string Category2 { get; set; }
        public string Category3 { get; set; }
        public string Category4 { get; set; }
        public string mainproject { get; set; }
        public string subproject { get; set; }
        public string subsubproject { get; set; }
        public string municipality { get; set; }
        public Nullable<double> amount { get; set; }
        public Nullable<double> Allotment { get; set; }
        public Nullable<double> Obligation { get; set; }
        public Nullable<int> newed { get; set; }
        public Nullable<int> restored { get; set; }
        public Nullable<int> rehab { get; set; }
        public Nullable<double> canals { get; set; }
        public Nullable<double> canal_lining { get; set; }
        public Nullable<double> structures { get; set; }
        public Nullable<double> roads { get; set; }
        public Nullable<int> farmer_beneficiaries { get; set; }
        public Nullable<int> jobs { get; set; }
        public string remarks { get; set; }
        public Nullable<int> new_accomp { get; set; }
        public Nullable<int> resto_accomp { get; set; }
        public Nullable<int> rehab_accomp { get; set; }
        public Nullable<double> canals_accomp { get; set; }
        public Nullable<double> canal_lining_accomp { get; set; }
        public Nullable<double> structures_accomp { get; set; }
        public Nullable<double> roads_accomp { get; set; }
        public Nullable<int> Beneficiary_accomp { get; set; }
        public Nullable<int> JobGen { get; set; }
        public Nullable<double> Physical { get; set; }
        public Nullable<double> Financial { get; set; }
        public string Remarks_accomp { get; set; }
        public string status { get; set; }
        public Nullable<double> Value_accomp { get; set; }
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
        public Nullable<System.DateTime> as_of { get; set; }
        public string SAMPLE { get; set; }
        public Nullable<double> saro { get; set; }
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
        public Nullable<double> disbursement { get; set; }
        public byte[] RowVersion { get; set; }
        public Nullable<int> mnt { get; set; }
        public Nullable<int> year_covered { get; set; }
        public Nullable<double> cash { get; set; }
        public Nullable<double> saro_region { get; set; }
        public Nullable<double> asa_region { get; set; }
        public Nullable<double> cash_region { get; set; }
        public string remarks_financial { get; set; }
        public string asano { get; set; }
        public string maindescription { get; set; }
        public string asof { get; set; }
        public string projectmonitor { get; set; }
        public string infra { get; set; }
        public Nullable<double> HDPE { get; set; }
        public Nullable<double> COCONET { get; set; }
        public Nullable<double> GRAVEL { get; set; }
        public Nullable<double> HDPEACCOMP { get; set; }
        public Nullable<double> COCONETACCOMP { get; set; }
        public Nullable<double> GRAVELACCOMP { get; set; }
        public Nullable<int> Nosystem { get; set; }
        public string lat { get; set; }
        public string longi { get; set; }
        public string imo { get; set; }
        public string @long { get; set; }
        public string financeRegion { get; set; }
        public string OperationRegion { get; set; }
        public string category5 { get; set; }
        public string category6 { get; set; }
        public int PicID { get; set; }
        public byte[] DIMEPic { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string mapLoc { get; set; }


        public IEnumerable<HttpPostedFileBase> File { get; set; }
    }
}
