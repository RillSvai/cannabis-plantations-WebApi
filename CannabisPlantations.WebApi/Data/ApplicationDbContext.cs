using System;
using System.Collections.Generic;
using CannabisPlantations.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CannabisPlantations.WebApi.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Agronomist> Agronomists { get; set; }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<ApplicationCredential> ApplicationCredentials { get; set; }

    public virtual DbSet<BusinessTrip> BusinessTrips { get; set; }

    public virtual DbSet<CannabisType> CannabisTypes { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Harvest> Harvests { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductStorage> ProductStorages { get; set; }

    public virtual DbSet<Return> Returns { get; set; }

    public virtual DbSet<ReturnDetail> ReturnDetails { get; set; }

    public virtual DbSet<Scope> Scopes { get; set; }

    public virtual DbSet<Tasting> Tastings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:Kiril");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agronomist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Agronomi__3214EC07A55AD8E7");

            entity.HasIndex(e => e.Name, "UQ__Agronomi__737584F63DC65F9E").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasMany(d => d.BusinessTrips).WithMany(p => p.Agronomists)
                .UsingEntity<Dictionary<string, object>>(
                    "BusinessTripAgronomist",
                    r => r.HasOne<BusinessTrip>().WithMany()
                        .HasForeignKey("BusinessTripId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__BusinessT__Busin__5441852A"),
                    l => l.HasOne<Agronomist>().WithMany()
                        .HasForeignKey("AgronomistId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__BusinessT__Agron__44FF419A"),
                    j =>
                    {
                        j.HasKey("AgronomistId", "BusinessTripId").HasName("PK__Business__B2C1A236EAEEEEF7");
                        j.ToTable("BusinessTripAgronomists");
                        j.HasIndex(new[] { "AgronomistId" }, "idx_AgronomistId");
                    });
        });

        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Applicat__3214EC07EFBF25C1");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<ApplicationCredential>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Applicat__3214EC07170F979D");

            entity.HasIndex(e => e.Secret, "UQ_Secret").IsUnique();

            entity.Property(e => e.Id).HasMaxLength(255);
            entity.Property(e => e.Secret).HasMaxLength(255);

            entity.HasOne(d => d.Application).WithMany(p => p.ApplicationCredentials)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Applicati__Appli__60A75C0F");
        });

        modelBuilder.Entity<BusinessTrip>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Business__3214EC07B94F5A1B");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<CannabisType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cannabis__3214EC07D6CC9167");

            entity.HasIndex(e => e.Name, "UQ__Cannabis__737584F6527CBB50").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC072C779624");

            entity.HasIndex(e => e.Name, "UQ__Customer__737584F6EF9194F2").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasMany(d => d.Tastings).WithMany(p => p.Customers)
                .UsingEntity<Dictionary<string, object>>(
                    "TastingCustomer",
                    r => r.HasOne<Tasting>().WithMany()
                        .HasForeignKey("TastingId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__TastingCu__Tasti__534D60F1"),
                    l => l.HasOne<Customer>().WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__TastingCu__Custo__49C3F6B7"),
                    j =>
                    {
                        j.HasKey("CustomerId", "TastingId").HasName("PK__TastingC__4EF05ECA74ADE805");
                        j.ToTable("TastingCustomers");
                        j.HasIndex(new[] { "CustomerId" }, "idx_CustomerId");
                    });
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Feedback__3214EC077A9455CB");

            entity.ToTable("Feedback");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Text).HasMaxLength(255);

            entity.HasOne(d => d.Customer).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Feedback__Custom__4BAC3F29");
        });

        modelBuilder.Entity<Harvest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Harvest__3214EC07BD39CE95");

            entity.ToTable("Harvest");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.HasOne(d => d.Agronomist).WithMany(p => p.Harvests)
                .HasForeignKey(d => d.AgronomistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Harvest__Agronom__46E78A0C");

            entity.HasOne(d => d.CannabisType).WithMany(p => p.Harvests)
                .HasForeignKey(d => d.CannabisTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Harvest__Cannabi__5165187F");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC074183CBE2");

            entity.HasIndex(e => e.CustomerId, "idx_CustomerId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.HasOne(d => d.Agronomist).WithMany(p => p.Orders)
                .HasForeignKey(d => d.AgronomistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__Agronomi__48CFD27E");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__Customer__4CA06362");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.OrderId }).HasName("PK__OrderDet__5835C37179EEE31C");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__Order__5535A963");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__Produ__4F7CD00D");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC075B649FFA");

            entity.HasIndex(e => e.CannabisTypeId, "UQ__Products__115623557941556F").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Agronomist).WithMany(p => p.Products)
                .HasForeignKey(d => d.AgronomistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__Agrono__47DBAE45");

            entity.HasOne(d => d.CannabisType).WithOne(p => p.Product)
                .HasForeignKey<Product>(d => d.CannabisTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__Cannab__52593CB8");
        });

        modelBuilder.Entity<ProductStorage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductS__3214EC07177E5FEF");

            entity.ToTable("ProductStorage");

            entity.HasIndex(e => e.ProductId, "UQ__ProductS__B40CC6CC9825AA36").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Product).WithOne(p => p.ProductStorage)
                .HasForeignKey<ProductStorage>(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductSt__Produ__4E88ABD4");
        });

        modelBuilder.Entity<Return>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Returns__3214EC07CFA7A054");

            entity.HasIndex(e => e.CustomerId, "idx_CustomerId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.HasOne(d => d.Agronomist).WithMany(p => p.Returns)
                .HasForeignKey(d => d.AgronomistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Returns__Agronom__45F365D3");

            entity.HasOne(d => d.Customer).WithMany(p => p.Returns)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Returns__Custome__4AB81AF0");
        });

        modelBuilder.Entity<ReturnDetail>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.ReturnId }).HasName("PK__ReturnDe__2B4898572FB78776");

            entity.HasOne(d => d.Product).WithMany(p => p.ReturnDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReturnDet__Produ__5070F446");

            entity.HasOne(d => d.Return).WithMany(p => p.ReturnDetails)
                .HasForeignKey(d => d.ReturnId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReturnDet__Retur__5629CD9C");
        });

        modelBuilder.Entity<Scope>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Scopes__3214EC07B3FD58E6");

            entity.HasIndex(e => e.Name, "UQ__Scopes__737584F68222FCAD").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(30);

            entity.HasMany(d => d.ApplicationCredentials).WithMany(p => p.Scopes)
                .UsingEntity<Dictionary<string, object>>(
                    "ScopeApplicationCredential",
                    r => r.HasOne<ApplicationCredential>().WithMany()
                        .HasForeignKey("ApplicationCredentialId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ScopeAppl__Appli__6FE99F9F"),
                    l => l.HasOne<Scope>().WithMany()
                        .HasForeignKey("ScopeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ScopeAppl__Scope__6E01572D"),
                    j =>
                    {
                        j.HasKey("ScopeId", "ApplicationCredentialId").HasName("PK__ScopeApp__4FAE9254E9E84959");
                        j.ToTable("ScopeApplicationCredentials");
                        j.IndexerProperty<string>("ApplicationCredentialId").HasMaxLength(255);
                    });
        });

        modelBuilder.Entity<Tasting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tastings__3214EC07D41C760C");

            entity.HasIndex(e => e.AgronomistId, "idx_AgronomistId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.HasOne(d => d.Agronomist).WithMany(p => p.Tastings)
                .HasForeignKey(d => d.AgronomistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tastings__Agrono__440B1D61");

            entity.HasOne(d => d.Product).WithMany(p => p.Tastings)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tastings__Produc__4D94879B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
