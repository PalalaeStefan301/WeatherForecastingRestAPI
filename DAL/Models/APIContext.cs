using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Tempo>().HasOne(typeof(Project));

            //modelBuilder.Entity<Project>().HasMany(typeof(Tempo));

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<City> Cities { get; set; }
    }
}
