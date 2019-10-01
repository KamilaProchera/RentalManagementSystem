using Microsoft.EntityFrameworkCore;
using RentalSystem.Persistence.Models;
using System;

namespace RentalSystem.Persistence
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Scooter> Scooters { get; set; }
        public DbSet<Defect> Defects { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<RentalHistory> RentalHistory { get; set; }
    }
}
