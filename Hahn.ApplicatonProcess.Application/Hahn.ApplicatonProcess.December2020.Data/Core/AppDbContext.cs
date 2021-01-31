using Hahn.ApplicatonProcess.December2020.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicatonProcess.December2020.Data.Core
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = applicants.db");
            base.OnConfiguring(optionsBuilder);
        }


        public DbSet<Applicant> Applicants { get; set; }
    }
}
