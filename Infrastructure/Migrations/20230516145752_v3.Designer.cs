﻿// <auto-generated />
using System;
using Infrastructure.MySQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(WebshopDbContext))]
    [Migration("20230516145752_v3")]
    partial class v3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.3.23174.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Domain.Address", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ID");

                    b.ToTable("Address");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            City = "New York",
                            Country = "USA",
                            PostalCode = "10001",
                            Street = "123 Main St"
                        },
                        new
                        {
                            ID = 2,
                            City = "San Francisco",
                            Country = "USA",
                            PostalCode = "94105",
                            Street = "456 Main St"
                        });
                });

            modelBuilder.Entity("Domain.Customer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ID");

                    b.HasIndex("AddressId");

                    b.ToTable("Customer");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            AddressId = 1,
                            Email = "johndoe@example.com",
                            Name = "John Doe"
                        },
                        new
                        {
                            ID = 2,
                            AddressId = 2,
                            Email = "janesmith@example.com",
                            Name = "Jane Smith"
                        });
                });

            modelBuilder.Entity("Domain.Merchant", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ID");

                    b.ToTable("Merchant");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Email = "bal@bal.com"
                        });
                });

            modelBuilder.Entity("Domain.Order", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Psp")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CustomerId");

                    b.ToTable("Order");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            CustomerId = 1,
                            OrderId = new Guid("f7a23d59-6433-4046-8626-af1cae7abc8b"),
                            Psp = 3
                        },
                        new
                        {
                            ID = 2,
                            CustomerId = 2,
                            OrderId = new Guid("443faf6d-066c-40fc-8e08-e63f02181a52"),
                            Psp = 2
                        },
                        new
                        {
                            ID = 3,
                            CustomerId = 2,
                            OrderId = new Guid("80cfdc98-6003-43be-acf0-cb62838f5ffe"),
                            Psp = 1
                        });
                });

            modelBuilder.Entity("Domain.OrderProduct", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ID")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "OrderId");

                    b.HasIndex("OrderId");

                    b.ToTable("Order_Product");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            OrderId = 1,
                            ID = 1
                        },
                        new
                        {
                            ProductId = 2,
                            OrderId = 1,
                            ID = 2
                        },
                        new
                        {
                            ProductId = 1,
                            OrderId = 2,
                            ID = 3
                        });
                });

            modelBuilder.Entity("Domain.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("MerchantId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("MerchantId");

                    b.ToTable("Product");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Description = "This is product 1",
                            MerchantId = 1,
                            Name = "Product 1",
                            Price = 10.99m,
                            Stock = 100
                        },
                        new
                        {
                            ID = 2,
                            Description = "This is product 2",
                            MerchantId = 1,
                            Name = "Product 2",
                            Price = 15.99m,
                            Stock = 50
                        },
                        new
                        {
                            ID = 3,
                            Description = "This is product 3",
                            MerchantId = 1,
                            Name = "Product 3",
                            Price = 8.99m,
                            Stock = 200
                        },
                        new
                        {
                            ID = 4,
                            Description = "This is product 4",
                            MerchantId = 1,
                            Name = "Product 4",
                            Price = 20.99m,
                            Stock = 75
                        });
                });

            modelBuilder.Entity("Domain.Customer", b =>
                {
                    b.HasOne("Domain.Address", "Address")
                        .WithMany("Customers")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Domain.Order", b =>
                {
                    b.HasOne("Domain.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Domain.OrderProduct", b =>
                {
                    b.HasOne("Domain.Order", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Product", "Product")
                        .WithMany("OrderProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Domain.Product", b =>
                {
                    b.HasOne("Domain.Merchant", "Merchant")
                        .WithMany("Products")
                        .HasForeignKey("MerchantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Merchant");
                });

            modelBuilder.Entity("Domain.Address", b =>
                {
                    b.Navigation("Customers");
                });

            modelBuilder.Entity("Domain.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Domain.Merchant", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Domain.Order", b =>
                {
                    b.Navigation("OrderProducts");
                });

            modelBuilder.Entity("Domain.Product", b =>
                {
                    b.Navigation("OrderProducts");
                });
#pragma warning restore 612, 618
        }
    }
}