using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Public360.Models
{
    public class UICityDataType
    {
        [Key]
        public Int32 CityDataTypeID { get; set; }
        public byte SortOrder { get; set; }
        public bool Active { get; set; }
        public string DisplayText { get; set; }
        public byte DataLevel { get; set; }
        public Int32? ParentCityDataTypeID { get; set; }
        public bool IsLeaf { get; set; }
        public string Source { get; set; }
    }
}
