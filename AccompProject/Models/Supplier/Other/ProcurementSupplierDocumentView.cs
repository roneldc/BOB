﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccompProject.Models.Supplier.Other
{
    public class ProcurementSupplierDocumentView
    {


        public int IDSupplierDocu { get; set; }
        public string userid { get; set; }
        public Nullable<int> DOcuID { get; set; }
        public string Filename { get; set; }
        public string Filetype { get; set; }
        public string  Docuname { get; set; }
    }


}