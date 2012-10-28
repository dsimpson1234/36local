using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Public360.ViewModels
{
    public class CityNameList
    {
        public Int32 CityID { get; set; }
        public string CityText { get; set; }
        public Int32? CityPopulation { get; set; }
        public string CityUniqueName { get; set; }
        public byte CityStateID { get; set; }
    }
}
