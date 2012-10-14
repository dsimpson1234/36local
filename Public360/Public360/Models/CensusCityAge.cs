using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Public360.Models
{
    public class CensusCityAge
    {
        public Int32 CityID { get; set; }
        public bool Gender { get; set; } //false is male
        public string GenderDescription { get; set; }
        public byte MinAge { get; set; }
        public byte? MaxAge { get; set; }
        public Int32 Population { get; set; }
    }
}
