using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Public360.Models
{
    public class State
    {
        public Byte StateID {get; set; }
        public string StateName { get; set; }
        public string StateAbrv { get; set; }
        public decimal Lattitude { get; set; }
        public decimal Longitude { get; set; }
        public decimal LandSqMiles { get; set; }
        public decimal WaterSqMiles { get; set; }
        public Int64 LandSqMeters { get; set; }
        public Int64 WaterSqMeters { get; set; }
        public Int32 GNISID { get; set; }
        public bool ActiveCounties { get; set; }
        public bool ActiveCities { get; set; }
        public bool ActiveSchools { get; set; }

    }
}
