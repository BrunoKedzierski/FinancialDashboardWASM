using FInDashboardWASM.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace FInDashboardWASM.Server
{
    public class MssqDBContext : DbContext
    {
        public MssqDBContext(DbContextOptions options) : base(options)
        {
        }

        protected MssqDBContext()
        {
        }

        public DbSet<CompanyData> companyData { get; set; }
        public DbSet<OHLC> ohlc { get; set; }
        public DbSet<User> user { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyData>()
                .Property(b => b.LogoLink);


            modelBuilder.Entity<CompanyData>()
               .Property(b => b.QueryDate);
        }



    }
}
