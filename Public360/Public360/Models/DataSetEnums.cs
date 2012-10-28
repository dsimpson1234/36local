using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Public360.Models
{
    public enum EntityType : byte
    {
        None = 0,
        State = 1,
        County = 2,
        City = 3,
        School = 11
    }

    public enum DataSet : byte
    {
        None = 0,
        USCensus = 1,
        ACS = 2,
        FBI = 3,
        Financial = 101
    }

    public enum DataSetSubtype : byte
    {
        None = 0,
        Primary = 1,
        DP02 = 2,
        DP03 = 3,
        DP04 = 4,
        DP05 = 5
    }
}