using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccompProject.Models
{
    public class Polygon
    {

        public string IDgeo { get; set; }
        public string DrawingType { get; set; }
        public string geometry { get; set; }
        public string Farmerlot { get; set; }
        public string Farmername { get; set; }
        public string idsystem { get; set; }
        public int overlayid { get; set; }
        public Nullable<double> PolygonArea { get; set; }
        public string tempdata { get; set; }
        public int profileid { get; set; }


    }
}