using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Public360.Models
{
    public class DataSetCurrentYear
    {
        public byte StateID { get; set; }
        public byte EntityType { get; set; }
        public byte DataSet { get; set; }
        public byte DataSetSubtype { get; set; }
        public byte Year { get; set; }
    }
}
