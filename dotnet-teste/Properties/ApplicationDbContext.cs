using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using dotnet_teste.Models;
using System.Reflection.Emit;

namespace dotnet_teste.Properties
{

    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public ApplicationDbContext()
        {
        }

        public DbSet<Books>? Books { get; set; }
        public DbSet<Loan>? Loans { get; set; }
        public DbSet<IdentityUser> aspNetUsers { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

