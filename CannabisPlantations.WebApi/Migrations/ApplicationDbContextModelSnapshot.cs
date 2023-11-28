﻿// <auto-generated />
using System;
using CannabisPlantations.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CannabisPlantations.WebApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.Agronomist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsAvailable")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Agronomists");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.AgronomistBusinessTrips", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AgronomistId")
                        .HasColumnType("int");

                    b.Property<int>("BusinessTripId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AgronomistId");

                    b.HasIndex("BusinessTripId");

                    b.ToTable("AgronomistBusinessTrips");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.BusinessTrip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("BusinessTrips");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.CannabisType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CannabisTypes");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.CustomerTastings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("TastingId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("TastingId");

                    b.ToTable("CustomerTastings");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.Harvest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AgronomistId")
                        .HasColumnType("int");

                    b.Property<int>("CannabisTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AgronomistId");

                    b.HasIndex("CannabisTypeId");

                    b.ToTable("Harvests");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AgronomistId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AgronomistId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AgronomistId")
                        .HasColumnType("int");

                    b.Property<int>("CannabisTypeId")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AgronomistId");

                    b.HasIndex("CannabisTypeId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.ProductStorage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("ProductStorage");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.Return", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AgronomistId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AgronomistId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Returns");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.ReturnDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int?>("ReturnId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("ReturnId");

                    b.ToTable("ReturnDetails");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.Tasting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AgronomistId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AgronomistId");

                    b.HasIndex("ProductId");

                    b.ToTable("Tastings");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.AgronomistBusinessTrips", b =>
                {
                    b.HasOne("CannabisPlantations.WebApi.Models.Agronomist", "Agronomist")
                        .WithMany("AgronomistBusinessTrips")
                        .HasForeignKey("AgronomistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CannabisPlantations.WebApi.Models.BusinessTrip", "BusinessTrip")
                        .WithMany("AgronomistBusinessTrips")
                        .HasForeignKey("BusinessTripId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Agronomist");

                    b.Navigation("BusinessTrip");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.CustomerTastings", b =>
                {
                    b.HasOne("CannabisPlantations.WebApi.Models.Customer", "Customer")
                        .WithMany("Tastings")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CannabisPlantations.WebApi.Models.Tasting", "Tasting")
                        .WithMany("CustomerTastings")
                        .HasForeignKey("TastingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Tasting");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.Feedback", b =>
                {
                    b.HasOne("CannabisPlantations.WebApi.Models.Customer", "Customer")
                        .WithMany("Feedbacks")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.Harvest", b =>
                {
                    b.HasOne("CannabisPlantations.WebApi.Models.Agronomist", "Agronomist")
                        .WithMany("Harvests")
                        .HasForeignKey("AgronomistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CannabisPlantations.WebApi.Models.CannabisType", "CannabisType")
                        .WithMany("Harvests")
                        .HasForeignKey("CannabisTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Agronomist");

                    b.Navigation("CannabisType");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.Order", b =>
                {
                    b.HasOne("CannabisPlantations.WebApi.Models.Agronomist", "Agronomist")
                        .WithMany("Orders")
                        .HasForeignKey("AgronomistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CannabisPlantations.WebApi.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Agronomist");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.OrderDetail", b =>
                {
                    b.HasOne("CannabisPlantations.WebApi.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CannabisPlantations.WebApi.Models.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.Product", b =>
                {
                    b.HasOne("CannabisPlantations.WebApi.Models.Agronomist", "Agronomist")
                        .WithMany("Products")
                        .HasForeignKey("AgronomistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CannabisPlantations.WebApi.Models.CannabisType", "CannabisType")
                        .WithMany()
                        .HasForeignKey("CannabisTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Agronomist");

                    b.Navigation("CannabisType");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.ProductStorage", b =>
                {
                    b.HasOne("CannabisPlantations.WebApi.Models.Product", "Product")
                        .WithOne("ProductStorage")
                        .HasForeignKey("CannabisPlantations.WebApi.Models.ProductStorage", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.Return", b =>
                {
                    b.HasOne("CannabisPlantations.WebApi.Models.Agronomist", "Agronomist")
                        .WithMany("Returns")
                        .HasForeignKey("AgronomistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CannabisPlantations.WebApi.Models.Customer", "Customer")
                        .WithMany("Returns")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Agronomist");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.ReturnDetail", b =>
                {
                    b.HasOne("CannabisPlantations.WebApi.Models.Product", "Product")
                        .WithMany("ReturnDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CannabisPlantations.WebApi.Models.Return", "Return")
                        .WithMany("ReturnDetails")
                        .HasForeignKey("ReturnId");

                    b.Navigation("Product");

                    b.Navigation("Return");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.Tasting", b =>
                {
                    b.HasOne("CannabisPlantations.WebApi.Models.Agronomist", "Agronomist")
                        .WithMany("Tastings")
                        .HasForeignKey("AgronomistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CannabisPlantations.WebApi.Models.Product", "Product")
                        .WithMany("Tastings")
                        .HasForeignKey("ProductId");

                    b.Navigation("Agronomist");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.Agronomist", b =>
                {
                    b.Navigation("AgronomistBusinessTrips");

                    b.Navigation("Harvests");

                    b.Navigation("Orders");

                    b.Navigation("Products");

                    b.Navigation("Returns");

                    b.Navigation("Tastings");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.BusinessTrip", b =>
                {
                    b.Navigation("AgronomistBusinessTrips");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.CannabisType", b =>
                {
                    b.Navigation("Harvests");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.Customer", b =>
                {
                    b.Navigation("Feedbacks");

                    b.Navigation("Orders");

                    b.Navigation("Returns");

                    b.Navigation("Tastings");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.Product", b =>
                {
                    b.Navigation("OrderDetails");

                    b.Navigation("ProductStorage");

                    b.Navigation("ReturnDetails");

                    b.Navigation("Tastings");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.Return", b =>
                {
                    b.Navigation("ReturnDetails");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.Tasting", b =>
                {
                    b.Navigation("CustomerTastings");
                });
#pragma warning restore 612, 618
        }
    }
}
