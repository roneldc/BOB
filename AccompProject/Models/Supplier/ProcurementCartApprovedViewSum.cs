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
    
    public partial class ProcurementCartApprovedViewSum
    {
        public long ID { get; set; }
        [DisplayName("PROCUREMENT CODE")]
        public string ProcurementID { get; set; }
        [DisplayName("QUANTITY")]
        public Nullable<double> qty { get; set; }
        [DisplayName("PROCUREMENT MODE")]
        public string procurementmode { get; set; }
        public string user_id { get; set; }
         [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]  
        public Nullable<System.DateTime> deadline { get; set; }
         public string quotationno { get; set; }
         public string requisitionno { get; set; }
         [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
         public Nullable<System.DateTime> openingdate { get; set; }
        [DisplayFormat(DataFormatString = "{0:t}")]
         public Nullable<System.TimeSpan> openingtime { get; set; }
         public string philgeps { get; set; }
         public string notes { get; set; }
         public Nullable<int> validityPeriod { get; set; }
         public Nullable<int> deliveryPeriod { get; set; }
         public string article { get; set; }
         public Nullable<double> total_price { get; set; }
    }
}
