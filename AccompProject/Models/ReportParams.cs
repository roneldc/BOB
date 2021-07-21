﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccompProject.Models
{
    public class ReportParams<T>
    {


        public string RptFileName { get; set; }

        public string ReportTitle { get; set; }
        public List<T> DataSource { get; set; }
        public bool IsPassParamToCr { get; set; }

    }
}