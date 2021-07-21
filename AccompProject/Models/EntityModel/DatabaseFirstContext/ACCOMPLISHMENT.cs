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
    using System.Web;


    public partial class ACCOMPLISHMENT 
        //: IValidatableObject
    {
        [DisplayName("ID")]
        public string IDAccomp { get; set; }
        [DisplayName("YEAR")]
        public Nullable<int> year { get; set; }
        [DisplayName("REGION")]
        public string region { get; set; }
        [DisplayName("PROVINCE")]
        public string province { get; set; }
        public string district { get; set; }
        public string Category1 { get; set; }
        public string Category2 { get; set; }
        public string Category3 { get; set; }
        public string Category4 { get; set; }
        [DisplayName("MAINPROJECT")]
        public string mainproject { get; set; }
        [DisplayName("SUBPROJECT")]
        public string subproject { get; set; }
        public string subsubproject { get; set; }
        [DisplayName("MUNICIPALITY")]
        public string municipality { get; set; }
        [DisplayName("ALLOCATION (P'000)")]
        public Nullable<double> amount { get; set; }
        public Nullable<double> Allotment { get; set; }
        [DisplayName("OBLIGATION")]
        public Nullable<double> Obligation { get; set; }
        [DisplayName("NEW")]
        public Nullable<double> newed { get; set; }
        [DisplayName("RESTORE")]
        public Nullable<double> restored { get; set; }
        [DisplayName("REHAB")]
        public Nullable<double> rehab { get; set; }
        [DisplayName("EARTH CANAL")]
        public Nullable<double> canals { get; set; }
        [DisplayName("CONCRETE CANAL LINING")]
        public Nullable<double> canal_lining { get; set; }
        [DisplayName("STRUCTURE")]
        public Nullable<double> structures { get; set; }
        [DisplayName("ROAD")]
        public Nullable<double> roads { get; set; }
        [DisplayName("FARMER BENEFICIARIES")]
        public Nullable<int> farmer_beneficiaries { get; set; }
        [DisplayName("JOBS")]
        public Nullable<int> jobs { get; set; }
      //  [Required]
         [DisplayName("Brief Description of Attachment")]
        public string remarks { get; set; }
        [DisplayName("NEW")]
        public Nullable<int> new_accomp { get; set; }
        [DisplayName("RESTORE")]
        public Nullable<int> resto_accomp { get; set; }
        [DisplayName("REHAB")]
        public Nullable<int> rehab_accomp { get; set; }
        [DisplayName("CANAL (km.)")]
        public Nullable<double> canals_accomp { get; set; }
        [DisplayName("CANAL LINING (km.)")]
        public Nullable<double> canal_lining_accomp { get; set; }
        [DisplayName("STRUCTURES")]
        public Nullable<double> structures_accomp { get; set; }
        [DisplayName("ROADS")]
        public Nullable<double> roads_accomp { get; set; }
        [DisplayName("FARMER BENEFICIARY")]
        public Nullable<int> Beneficiary_accomp { get; set; }
        [DisplayName("JOBS")]
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
        public Nullable<System.DateTime> as_of { get; set; }
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
        [DisplayName("CASH")]
        public Nullable<double> cash { get; set; }
        [DisplayName("SARO")]
        public Nullable<double> saro_region { get; set; }
        [DisplayName("ASA")]
        public Nullable<double> asa_region { get; set; }
        [DisplayName("CASH")]
        public Nullable<double> cash_region { get; set; }
        [DataType(DataType.MultilineText)]
        [DisplayName("REMARKS")]
        public string remarks_financial { get; set; }
        [DisplayName("ASA NO.")]
        public string asano { get; set; }
        public string maindescription { get; set; }
        [DisplayName("MONTH / YEAR")]
       // [Required(ErrorMessage = "AS Of Date is required")]
        public string asof { get; set; }
        public string projectmonitor { get; set; }
        public string infra { get; set; }
         [DisplayName("HDPE (km.)")]
        public Nullable<double> HDPE { get; set; }
           [DisplayName("COCONET (sq. m.)")]
        public Nullable<double> COCONET { get; set; }
          [DisplayName("GRAVEL (km.)")]
        public Nullable<double> GRAVEL { get; set; }
         [DisplayName("HDPE (km.)")]
        public Nullable<double> HDPEACCOMP { get; set; }
         [DisplayName("COCONET (sq. m.)")]
        public Nullable<double> COCONETACCOMP { get; set; }
         [DisplayName("GRAVEL (km.)")]
        public Nullable<double> GRAVELACCOMP { get; set; }
         public Nullable<int> Nosystem { get; set; }
         [DisplayName("LATITUDE")]
        
         public string lat { get; set; }
          [DisplayName("LONGITUDE")]
         public string longi { get; set; }
         public string imo { get; set; }
         public string @long { get; set; }
         public string financeRegion { get; set; }
         public string OperationRegion { get; set; }
         public string category5 { get; set; }
         public string category6 { get; set; }
         public string powerbiregion { get; set; }
         public string powerbifund { get; set; }




         public HttpPostedFileBase File { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    //if (new_accomp > newed)
        //    //{
        //    //    yield return new ValidationResult("Accomplishment is greater than target : NEW!");
        //    //}
        //    //if (resto_accomp > restored)
        //    //{
        //    //    yield return new ValidationResult("Accomplishment is greater than target : RESTO!");
        //    //}
        //    //if (rehab_accomp > rehab)
        //    //{
        //    //    yield return new ValidationResult("Accomplishment is greater than target : REHAB!");
        //    //}

        //    //if (canals_accomp > canals)
        //    //{
        //    //    if (canals != 0)
        //    //    {
        //    //        yield return new ValidationResult("Accomplishment is greater than target : CANALS!");
        //    //    }
        //    //}

        //    //if (canal_lining_accomp > canal_lining)
        //    //{
        //    //    if (canal_lining != 0)
        //    //    {
        //    //        yield return new ValidationResult("Accomplishment is greater than target : CANAL LINING!");
        //    //    }
        //    //}

        //    //if (structures_accomp > structures)
        //    //{
        //    //    if (structures != 0)
        //    //    {
        //    //        yield return new ValidationResult("Accomplishment is greater than target : STRUCTURES!");
        //    //    }
        //    //}


        //}
    }
}
