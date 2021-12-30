using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagment.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser> 
    {
        public AppDbContext( DbContextOptions<AppDbContext> options) :base(options)
        {
                
        }
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
          modelBuilder.Seed();
            //change foreign key constraint (delete behavior) by default (cascade)
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e=>e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;

            }
            //change identity tables name and schema names
           // modelBuilder.Entity<IdentityUser>().ToTable("new table name","new schema name");
        }
    }
}
