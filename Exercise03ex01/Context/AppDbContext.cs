using Exercise03ex01.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Exercise03ex01.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users2 { get; set; }
        public DbSet<Printer> Printers { get; set; }
        //public DbSet<Printermakes> PrinterMakes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().ToTable("User2");
            builder.Entity<Printer>().ToTable("Printers");
            //builder.Entity<Printermakes>().ToTable("PrinterMakes");
        }
    }
}
