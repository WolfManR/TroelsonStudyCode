using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AutoLotDAL_Core2.EF
{
    public class AutoLotContextFactory : IDesignTimeDbContextFactory<AutoLotContext>
    {
        public AutoLotContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AutoLotContext>();
            optionsBuilder.UseSqlServer(@"server=(LocalDb)\MSSQLLocalDB;database=AutoLotCore2;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework;",
                    options => options.EnableRetryOnFailure());
            return new AutoLotContext(optionsBuilder.Options);
        }
    }
}
