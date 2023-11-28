using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CannabisPlantations.WebApi.Models;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;

namespace CannabisPlantations.WebApi.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }


    public DbSet<Agronomist> Agronomists { get; set; }
    public DbSet<AgronomistBusinessTrips> AgronomistBusinessTrips { get; set; }
    public DbSet<BusinessTrip> BusinessTrips { get; set; }
    public DbSet<Tasting> Tastings { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<CustomerTastings> CustomerTastings { get; set; }
    public DbSet<CannabisType> CannabisTypes { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Harvest> Harvests { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductStorage> ProductStorage { get; set; }
    public DbSet<Return> Returns { get; set; }
    public DbSet<ReturnDetail> ReturnDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agronomist>()
        .Property(e => e.IsAvailable)
        .HasDefaultValue(true);
    }

}
