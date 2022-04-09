using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ManicureAndPedicureSalon.Data;

namespace ManicureAndPedicureSalon.Data
{
    public class ApplicationDbContext : IdentityDbContext<Client>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Appoitment> Appoitments { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}
