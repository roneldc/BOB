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
    
    public partial class FileAccomp
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public int FileType { get; set; }
        public string IDAccomp { get; set; }
        public Nullable<int> mnt { get; set; }
        public Nullable<int> yr { get; set; }
        public string status { get; set; }
        public string email { get; set; }
        public string userid { get; set; }
        public string remarks { get; set; }
        
    }
}
