using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Public360.Models
{
    public class City
    {
        public Int32 CityID { get; set; }
        public string CityName { get; set; }
        public string CityUniqueName { get; set; }
        public byte StateID { get; set; }
        public decimal Lattitude { get; set; }
        public decimal Longitude { get; set; }
        public decimal LandSqMiles { get; set; }
        public decimal WaterSqMiles { get; set; }
        public Int64 LandSqMeters { get; set; }
        public Int64 WaterSqMeters { get; set; }
        public string LSADCode { get; set; }
        public Int32? GNISID { get; set; }
        public bool CensusPlace { get; set; }
        public bool CensusCountySub { get; set; }
        public bool Active { get; set; }
    }
}
