using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccompProject.Models.EntityModel.DatabaseFirstContext
{
    public class BingoUser
    {
        public int empno { get; set; }
        public string url { get; set; }
        public string lname { get; set; }
        public string fname { get; set; }
        public string bingo { get; set; }
    }
}