﻿// <auto-generated />
using System;
using CannabisPlantations.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CannabisPlantations.WebApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231127191326_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BusinessTripAgronomist", b =>
                {
                    b.Property<int>("AgronomistId")
                        .HasColumnType("int");

                    b.Property<int>("BusinessTripId")
                        .HasColumnType("int");

                    b.HasKey("AgronomistId", "BusinessTripId")
                        .HasName("PK__Business__B2C1A236EAEEEEF7");

                    b.HasIndex("BusinessTripId");

                    b.HasIndex(new[] { "AgronomistId" }, "idx_AgronomistId");

                    b.ToTable("BusinessTripAgronomists", (string)null);
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.Agronomist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id")
                        .HasName("PK__Agronomi__3214EC07A55AD8E7");

                    b.HasIndex(new[] { "Name" }, "UQ__Agronomi__737584F63DC65F9E")
                        .IsUnique();

                    b.ToTable("Agronomists");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.Application", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id")
                        .HasName("PK__Applicat__3214EC07EFBF25C1");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.ApplicationCredential", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("ApplicationId")
                        .HasColumnType("int");

                    b.Property<string>("Secret")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id")
                        .HasName("PK__Applicat__3214EC07170F979D");

                    b.HasIndex("ApplicationId");

                    b.HasIndex(new[] { "Secret" }, "UQ_Secret")
                        .IsUnique();

                    b.ToTable("ApplicationCredentials");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.BusinessTrip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime");

                    b.HasKey("Id")
                        .HasName("PK__Business__3214EC07B94F5A1B");

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
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id")
                        .HasName("PK__Cannabis__3214EC07D6CC9167");

                    b.HasIndex(new[] { "Name" }, "UQ__Cannabis__737584F6527CBB50")
                        .IsUnique();

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
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id")
                        .HasName("PK__Customer__3214EC072C779624");

                    b.HasIndex(new[] { "Name" }, "UQ__Customer__737584F6EF9194F2")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime");

                    b.Property<string>("Text")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id")
                        .HasName("PK__Feedback__3214EC077A9455CB");

                    b.HasIndex("CustomerId");

                    b.ToTable("Feedback", (string)null);
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

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Harvest__3214EC07BD39CE95");

                    b.HasIndex("AgronomistId");

                    b.HasIndex("CannabisTypeId");

                    b.ToTable("Harvest", (string)null);
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

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime");

                    b.HasKey("Id")
                        .HasName("PK__Orders__3214EC074183CBE2");

                    b.HasIndex("AgronomistId");

                    b.HasIndex(new[] { "CustomerId" }, "idx_CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.OrderDetail", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "OrderId")
                        .HasName("PK__OrderDet__5835C37179EEE31C");

                    b.HasIndex("OrderId");

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

                    b.Property<int?>("Price")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Products__3214EC075B649FFA");

                    b.HasIndex("AgronomistId");

                    b.HasIndex("CannabisTypeId")
                        .IsUnique();

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

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__ProductS__3214EC07177E5FEF");

                    b.HasIndex(new[] { "ProductId" }, "UQ__ProductS__B40CC6CC9825AA36")
                        .IsUnique();

                    b.ToTable("ProductStorage", (string)null);
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

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime");

                    b.HasKey("Id")
                        .HasName("PK__Returns__3214EC07CFA7A054");

                    b.HasIndex("AgronomistId");

                    b.HasIndex(new[] { "CustomerId" }, "idx_CustomerId");

                    b.ToTable("Returns");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.ReturnDetail", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("ReturnId")
                        .HasColumnType("int");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "ReturnId")
                        .HasName("PK__ReturnDe__2B4898572FB78776");

                    b.HasIndex("ReturnId");

                    b.ToTable("ReturnDetails");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.Scope", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id")
                        .HasName("PK__Scopes__3214EC07B3FD58E6");

                    b.HasIndex(new[] { "Name" }, "UQ__Scopes__737584F68222FCAD")
                        .IsUnique();

                    b.ToTable("Scopes");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.Tasting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AgronomistId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Tastings__3214EC07D41C760C");

                    b.HasIndex("ProductId");

                    b.HasIndex(new[] { "AgronomistId" }, "idx_AgronomistId");

                    b.ToTable("Tastings");
                });

            modelBuilder.Entity("ScopeApplicationCredential", b =>
                {
                    b.Property<int>("ScopeId")
                        .HasColumnType("int");

                    b.Property<string>("ApplicationCredentialId")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("ScopeId", "ApplicationCredentialId")
                        .HasName("PK__ScopeApp__4FAE9254E9E84959");

                    b.HasIndex("ApplicationCredentialId");

                    b.ToTable("ScopeApplicationCredentials", (string)null);
                });

            modelBuilder.Entity("TastingCustomer", b =>
                {
                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("TastingId")
                        .HasColumnType("int");

                    b.HasKey("CustomerId", "TastingId")
                        .HasName("PK__TastingC__4EF05ECA74ADE805");

                    b.HasIndex("TastingId");

                    b.HasIndex(new[] { "CustomerId" }, "idx_CustomerId");

                    b.ToTable("TastingCustomers", (string)null);
                });

            modelBuilder.Entity("BusinessTripAgronomist", b =>
                {
                    b.HasOne("CannabisPlantations.WebApi.Models.Agronomist", null)
                        .WithMany()
                        .HasForeignKey("AgronomistId")
                        .IsRequired()
                        .HasConstraintName("FK__BusinessT__Agron__44FF419A");

                    b.HasOne("CannabisPlantations.WebApi.Models.BusinessTrip", null)
                        .WithMany()
                        .HasForeignKey("BusinessTripId")
                        .IsRequired()
                        .HasConstraintName("FK__BusinessT__Busin__5441852A");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.ApplicationCredential", b =>
                {
                    b.HasOne("CannabisPlantations.WebApi.Models.Application", "Application")
                        .WithMany("ApplicationCredentials")
                        .HasForeignKey("ApplicationId")
                        .IsRequired()
                        .HasConstraintName("FK__Applicati__Appli__60A75C0F");

                    b.Navigation("Application");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.Feedback", b =>
                {
                    b.HasOne("CannabisPlantations.WebApi.Models.Customer", "Customer")
                        .WithMany("Feedbacks")
                        .HasForeignKey("CustomerId")
                        .IsRequired()
                        .HasConstraintName("FK__Feedback__Custom__4BAC3F29");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.Harvest", b =>
                {
                    b.HasOne("CannabisPlantations.WebApi.Models.Agronomist", "Agronomist")
                        .WithMany("Harvests")
                        .HasForeignKey("AgronomistId")
                        .IsRequired()
                        .HasConstraintName("FK__Harvest__Agronom__46E78A0C");

                    b.HasOne("CannabisPlantations.WebApi.Models.CannabisType", "CannabisType")
                        .WithMany("Harvests")
                        .HasForeignKey("CannabisTypeId")
                        .IsRequired()
                        .HasConstraintName("FK__Harvest__Cannabi__5165187F");

                    b.Navigation("Agronomist");

                    b.Navigation("CannabisType");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.Order", b =>
                {
                    b.HasOne("CannabisPlantations.WebApi.Models.Agronomist", "Agronomist")
                        .WithMany("Orders")
                        .HasForeignKey("AgronomistId")
                        .IsRequired()
                        .HasConstraintName("FK__Orders__Agronomi__48CFD27E");

                    b.HasOne("CannabisPlantations.WebApi.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .IsRequired()
                        .HasConstraintName("FK__Orders__Customer__4CA06362");

                    b.Navigation("Agronomist");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.OrderDetail", b =>
                {
                    b.HasOne("CannabisPlantations.WebApi.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .IsRequired()
                        .HasConstraintName("FK__OrderDeta__Order__5535A963");

                    b.HasOne("CannabisPlantations.WebApi.Models.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK__OrderDeta__Produ__4F7CD00D");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.Product", b =>
                {
                    b.HasOne("CannabisPlantations.WebApi.Models.Agronomist", "Agronomist")
                        .WithMany("Products")
                        .HasForeignKey("AgronomistId")
                        .IsRequired()
                        .HasConstraintName("FK__Products__Agrono__47DBAE45");

                    b.HasOne("CannabisPlantations.WebApi.Models.CannabisType", "CannabisType")
                        .WithOne("Product")
                        .HasForeignKey("CannabisPlantations.WebApi.Models.Product", "CannabisTypeId")
                        .IsRequired()
                        .HasConstraintName("FK__Products__Cannab__52593CB8");

                    b.Navigation("Agronomist");

                    b.Navigation("CannabisType");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.ProductStorage", b =>
                {
                    b.HasOne("CannabisPlantations.WebApi.Models.Product", "Product")
                        .WithOne("ProductStorage")
                        .HasForeignKey("CannabisPlantations.WebApi.Models.ProductStorage", "ProductId")
                        .IsRequired()
                        .HasConstraintName("FK__ProductSt__Produ__4E88ABD4");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.Return", b =>
                {
                    b.HasOne("CannabisPlantations.WebApi.Models.Agronomist", "Agronomist")
                        .WithMany("Returns")
                        .HasForeignKey("AgronomistId")
                        .IsRequired()
                        .HasConstraintName("FK__Returns__Agronom__45F365D3");

                    b.HasOne("CannabisPlantations.WebApi.Models.Customer", "Customer")
                        .WithMany("Returns")
                        .HasForeignKey("CustomerId")
                        .IsRequired()
                        .HasConstraintName("FK__Returns__Custome__4AB81AF0");

                    b.Navigation("Agronomist");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.ReturnDetail", b =>
                {
                    b.HasOne("CannabisPlantations.WebApi.Models.Product", "Product")
                        .WithMany("ReturnDetails")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK__ReturnDet__Produ__5070F446");

                    b.HasOne("CannabisPlantations.WebApi.Models.Return", "Return")
                        .WithMany("ReturnDetails")
                        .HasForeignKey("ReturnId")
                        .IsRequired()
                        .HasConstraintName("FK__ReturnDet__Retur__5629CD9C");

                    b.Navigation("Product");

                    b.Navigation("Return");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.Tasting", b =>
                {
                    b.HasOne("CannabisPlantations.WebApi.Models.Agronomist", "Agronomist")
                        .WithMany("Tastings")
                        .HasForeignKey("AgronomistId")
                        .IsRequired()
                        .HasConstraintName("FK__Tastings__Agrono__440B1D61");

                    b.HasOne("CannabisPlantations.WebApi.Models.Product", "Product")
                        .WithMany("Tastings")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK__Tastings__Produc__4D94879B");

                    b.Navigation("Agronomist");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ScopeApplicationCredential", b =>
                {
                    b.HasOne("CannabisPlantations.WebApi.Models.ApplicationCredential", null)
                        .WithMany()
                        .HasForeignKey("ApplicationCredentialId")
                        .IsRequired()
                        .HasConstraintName("FK__ScopeAppl__Appli__6FE99F9F");

                    b.HasOne("CannabisPlantations.WebApi.Models.Scope", null)
                        .WithMany()
                        .HasForeignKey("ScopeId")
                        .IsRequired()
                        .HasConstraintName("FK__ScopeAppl__Scope__6E01572D");
                });

            modelBuilder.Entity("TastingCustomer", b =>
                {
                    b.HasOne("CannabisPlantations.WebApi.Models.Customer", null)
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .IsRequired()
                        .HasConstraintName("FK__TastingCu__Custo__49C3F6B7");

                    b.HasOne("CannabisPlantations.WebApi.Models.Tasting", null)
                        .WithMany()
                        .HasForeignKey("TastingId")
                        .IsRequired()
                        .HasConstraintName("FK__TastingCu__Tasti__534D60F1");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.Agronomist", b =>
                {
                    b.Navigation("Harvests");

                    b.Navigation("Orders");

                    b.Navigation("Products");

                    b.Navigation("Returns");

                    b.Navigation("Tastings");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.Application", b =>
                {
                    b.Navigation("ApplicationCredentials");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.CannabisType", b =>
                {
                    b.Navigation("Harvests");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CannabisPlantations.WebApi.Models.Customer", b =>
                {
                    b.Navigation("Feedbacks");

                    b.Navigation("Orders");

                    b.Navigation("Returns");
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
#pragma warning restore 612, 618
        }
    }
}
