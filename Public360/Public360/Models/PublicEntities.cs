using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.EntityModel;

namespace Public360.Models
{
    public class PublicEntities : DbContext
    {

        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<UICityDataType> UICityDataTypes { get; set; }
        public DbSet<CensusCityPop> CensusCityPops { get; set; }
        public DbSet<CensusCityAge> CityAges { get; set; }
        public DbSet<DataSetCurrentYear> DataSetCurrentYears { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CensusCityPop>().HasKey(obj => new { obj.CityID, obj.Year });
            modelBuilder.Entity<CensusCityAge>().Map(m => m.ToTable("VW_CensusCityAges"));
            modelBuilder.Entity<CensusCityAge>().HasKey(obj => new { obj.CityID, obj.Gender, obj.MinAge });
            modelBuilder.Entity<DataSetCurrentYear>().Map(m => m.ToTable("DataSetCurrentYear"));
            modelBuilder.Entity<DataSetCurrentYear>().HasKey(obj => new { obj.StateID, obj.EntityType, obj.DataSet, obj.DataSetSubtype });
            //modelBuilder.Entity<State>().Map(m => m.ToTable("State"));
            //modelBuilder.Entity<City>().Map(m => m.ToTable("City"));
        }
    }
}
