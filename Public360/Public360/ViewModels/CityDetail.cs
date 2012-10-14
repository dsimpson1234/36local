using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Public360.Models;

namespace Public360.ViewModels
{
    public class CityDetail
    {
        public City City { get; set; }
        public IList<UICityDataType> UICityDataTypes {get; set;}
        public Int32 UICityDataTypeSize { get; set; }
        public IList<City> ComparisonCities { get; set; }
        public int? CityPopulation { get; set; }
    }
}