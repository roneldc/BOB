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
    
    public partial class DBMView
    {
        public string IDAccomp { get; set; }
        public Nullable<int> year { get; set; }
        [Display(Name = "Region")]
        public string region { get; set; }
        [Display(Name = "Province")]
        public string province { get; set; }
        [Display(Name = "District")]
        public string district { get; set; }
        public string Category1 { get; set; }
        public string Category2 { get; set; }
        public string Category3 { get; set; }
        public string Category4 { get; set; }
        [Display(Name = "Mainproject")]
        public string mainproject { get; set; }
        [Display(Name = "SubProject")]
        public string subproject { get; set; }
        [Display(Name = "Municipality")]
        public string municipality { get; set; }
        [Display(Name = "Amount (P,000)")]
        public Nullable<double> amount { get; set; }
        public Nullable<int> IDDBM { get; set; }
        [Display(Name = "BIDDED (please put a check if the project is already bidded)")]
        public bool CW_Bidded { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Schedule for bidding (if project is not yet bidded)")]
        public Nullable<System.DateTime> CW_Schedule { get; set; }
        [Display(Name = "Force Account Works (please put a check if the project is under FAW)")]
        public bool FAW { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Supplies bidded (put the date of bidding)")]
        public Nullable<System.DateTime> FAW_Schedule { get; set; }
        [Display(Name = "Remarks")]
        [DataType(DataType.MultilineText)]
        public string remarks { get; set; }
        public string Mngr { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Bidded")]
        public Nullable<System.DateTime> bid_date { get; set; }
        public Nullable<int> FileId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public Nullable<int> FileType { get; set; }
        public Nullable<int> SORT { get; set; }
        public string RemarksDes { get; set; }
        public string maindescription { get; set; }
        public Nullable<bool> POW { get; set; }
        public int cntBidded { get; set; }
        public int cntfaw { get; set; }
        public int cntpow { get; set; }
        public Nullable<int> mnt { get; set; }
        public Nullable<int> yr { get; set; }
        [Display(Name = "AS OF")]
        public string asof { get; set; }
           [Display(Name = "No. of POW")]
        public Nullable<int> noofpow { get; set; }
           [Display(Name = "Bidded Amount")]
        public Nullable<double> amountbidded { get; set; }
           [Display(Name = "Amount under FAW")]
        public Nullable<double> amountfaw { get; set; }

           [Display(Name = "No. of POW")]
           public Nullable<int> UnbiddedNoOfPOW { get; set; }
           [Display(Name = "Unbidded Amount")]
           public Nullable<double> UnbiddedAmount { get; set; }
     

    }
}
