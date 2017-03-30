using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SearchNameCodeFirst.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SearchNameCodeFirst.DAL
{
    public class SearchContext : DbContext
    {
        public SearchContext() : base("SearchContext")
        {
        }

        public DbSet<People> People { get; set;}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
