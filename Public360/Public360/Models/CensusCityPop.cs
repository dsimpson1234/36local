using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Public360.Models
{
    public class CensusCityPop
    {
        public int CityID { get; set; }
        public byte Year { get; set; }
        public int? Population {get; set; }
        public int? HousingUnits {get; set; }
        public int? Households { get; set; }
    }
}