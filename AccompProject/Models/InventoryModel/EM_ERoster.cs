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
    
    public partial class EM_ERoster
    {
        public int num { get; set; }
      //  [Required]
        public string empno { get; set; }
        public string newid { get; set; }
        public string fname { get; set; }
        public string mname { get; set; }
        public string lname { get; set; }
        public string suffix { get; set; }
        public string abrev_currdept { get; set; }
        public string office { get; set; }
    }
}
