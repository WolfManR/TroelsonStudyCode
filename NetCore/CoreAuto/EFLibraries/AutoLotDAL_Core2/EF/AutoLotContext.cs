using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLotDAL_Core2.EF
{
    public class AutoLotContext:DbContext
    {
        internal AutoLotContext()
        {

        }
        public AutoLotContext(DbContextOptions options):base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"server=(LocalDb)\MSSQLLocalDB;database=AutoLotCore2;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework;",
                    options => options.EnableRetryOnFailure());
            }
        }
    }
}
