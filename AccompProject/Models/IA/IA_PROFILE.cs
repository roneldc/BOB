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
    using System.ComponentModel.DataAnnotations;
    
    public partial class IA_PROFILE
    {
        public Nullable<int> YEAR_COVERED { get; set; }
        public Nullable<int> REGION { get; set; }
        public Nullable<int> CATEGORY { get; set; }
        public string SYSTEM_NAME { get; set; }
        public string IA_NAME { get; set; }
        public string ADDRESS { get; set; }
        public string FEDERATION { get; set; }
        public string IMO { get; set; }
        public string ZONE { get; set; }
        public string WRFT { get; set; }
        public string TECHNICIAN { get; set; }
        public string CONTRACT { get; set; }
          [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]

        public Nullable<System.DateTime> CONTRACT_DATE_ORIGINAL { get; set; }
          [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]

        public Nullable<System.DateTime> DATE_ORGANIZED { get; set; }
        public string SEC { get; set; }
          [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]

        public Nullable<System.DateTime> DATE_REGISTERED { get; set; }
        public string TIN { get; set; }
        public string IA_STATUS { get; set; }
        public byte[] LOGO { get; set; }
        public Nullable<double> INCOME { get; set; }
        public Nullable<double> EXPENSES { get; set; }
        public Nullable<double> EXPENSES_DIRECT { get; set; }
        public Nullable<double> VI { get; set; }
        public string ADJECTIVE_RATING { get; set; }
        public Nullable<double> NUMERICAL_RATING { get; set; }
        public string BUSINESS { get; set; }
        public string ADJECTIVE_RATING_IMT { get; set; }
        public Nullable<double> NUMERICAL_RATING_IMT { get; set; }
        public Nullable<double> FIXED_ASSETS { get; set; }
        public Nullable<double> CURRENT_ASSETS { get; set; }
        public Nullable<int> SHARING_NIA { get; set; }
        public Nullable<int> SHARING_IA { get; set; }
        public string AMORTIZING { get; set; }
        public Nullable<int> NOSECTOR { get; set; }
        public string LOC_SOURCE { get; set; }
        public Nullable<double> IA_LOAN { get; set; }
        public Nullable<double> ANNUAL_AMORT { get; set; }
        public Nullable<double> CURRENT_FORTHEYEAR { get; set; }
        public Nullable<double> BACK_FORTHEYEAR { get; set; }
        public Nullable<double> PAYMENT_TODATE { get; set; }
        public Nullable<double> BALANCE_LOAN { get; set; }
        public string IA_CONTRACTING { get; set; }
        public Nullable<int> MNT { get; set; }
        public string coopno { get; set; }
        public Nullable<System.DateTime> coopreg { get; set; }
        public string CIA { get; set; }
        public Nullable<double> SHARING_CIA { get; set; }
        public Nullable<double> SHARING_CIA_IA { get; set; }
        public Nullable<double> SHARING_FIA { get; set; }
        public Nullable<double> SHARING_FIA_IA { get; set; }
        public string ADJECTIVE_RATING_IMT_WET { get; set; }
        public Nullable<double> NUMERICAL_RATING_IMT_WET { get; set; }
        public string ADJECTIVE_RATING_IMT_DRY { get; set; }
        public Nullable<double> NUMERICAL_RATING_IMT_DRY { get; set; }
        public Nullable<double> RENTAL_FEE { get; set; }
        public Nullable<double> AMOUNT_NIA_IA { get; set; }
        public Nullable<double> AMOUNT_NIA_FIA { get; set; }
        public Nullable<double> AMOUNT_NIA_CIA { get; set; }
        public Nullable<double> AMOUNT_CIAFIA_IA { get; set; }
        public Nullable<double> AMOUNT_IA_TSAG { get; set; }
        public Nullable<double> PROJECT_COST { get; set; }
        public Nullable<System.DateTime> DATE_TURNOVER { get; set; }
        public string NIA_ASSISTED { get; set; }
        public string TYPE_AMORTIZING { get; set; }
        public Nullable<int> YEARS_TO_PAY { get; set; }
        public string DONOR { get; set; }
        public string ORIG_FUND { get; set; }
        public string ADJECTIVE_RATING_IMT_ANNUAL { get; set; }
        public Nullable<double> NUMERICAL_RATING_IMT_ANNUAL { get; set; }
        public Nullable<double> AMOUNTEQUITY { get; set; }
        public Nullable<System.DateTime> MOASIGNED { get; set; }
        public string IAID { get; set; }
        public int IAPROFILEID { get; set; }
    }
}
