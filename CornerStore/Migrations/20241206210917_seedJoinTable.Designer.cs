﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CornerStore.Migrations
{
    [DbContext(typeof(CornerStoreDbContext))]
    [Migration("20241206210917_seedJoinTable")]
    partial class seedJoinTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CornerStore.Models.Cashier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Cashiers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "John",
                            LastName = "Doe"
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "Jane",
                            LastName = "Smith"
                        },
                        new
                        {
                            Id = 3,
                            FirstName = "Paul",
                            LastName = "Johnson"
                        },
                        new
                        {
                            Id = 4,
                            FirstName = "Anna",
                            LastName = "Brown"
                        },
                        new
                        {
                            Id = 5,
                            FirstName = "Emily",
                            LastName = "Davis"
                        },
                        new
                        {
                            Id = 6,
                            FirstName = "Mark",
                            LastName = "Wilson"
                        },
                        new
                        {
                            Id = 7,
                            FirstName = "Linda",
                            LastName = "Martinez"
                        });
                });

            modelBuilder.Entity("CornerStore.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoryName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryName = "Electronics"
                        },
                        new
                        {
                            Id = 2,
                            CategoryName = "Clothing"
                        },
                        new
                        {
                            Id = 3,
                            CategoryName = "Home Appliances"
                        });
                });

            modelBuilder.Entity("CornerStore.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CashierId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("PaidOnDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CashierId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CashierId = 1,
                            PaidOnDate = new DateTime(2024, 12, 6, 15, 9, 17, 393, DateTimeKind.Local).AddTicks(2230)
                        },
                        new
                        {
                            Id = 2,
                            CashierId = 2,
                            PaidOnDate = new DateTime(2024, 12, 6, 14, 39, 17, 405, DateTimeKind.Local).AddTicks(3230)
                        },
                        new
                        {
                            Id = 3,
                            CashierId = 3,
                            PaidOnDate = new DateTime(2024, 12, 6, 14, 9, 17, 405, DateTimeKind.Local).AddTicks(3260)
                        },
                        new
                        {
                            Id = 4,
                            CashierId = 4,
                            PaidOnDate = new DateTime(2024, 12, 6, 13, 39, 17, 405, DateTimeKind.Local).AddTicks(3270)
                        },
                        new
                        {
                            Id = 5,
                            CashierId = 5,
                            PaidOnDate = new DateTime(2024, 12, 6, 13, 9, 17, 405, DateTimeKind.Local).AddTicks(3270)
                        },
                        new
                        {
                            Id = 6,
                            CashierId = 6,
                            PaidOnDate = new DateTime(2024, 12, 6, 12, 39, 17, 405, DateTimeKind.Local).AddTicks(3270)
                        },
                        new
                        {
                            Id = 7,
                            CashierId = 7,
                            PaidOnDate = new DateTime(2024, 12, 6, 12, 9, 17, 405, DateTimeKind.Local).AddTicks(3270)
                        });
                });

            modelBuilder.Entity("CornerStore.Models.OrderProduct", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProducts");

                    b.HasData(
                        new
                        {
                            OrderId = 1,
                            ProductId = 1,
                            Quantity = 2
                        },
                        new
                        {
                            OrderId = 1,
                            ProductId = 2,
                            Quantity = 1
                        },
                        new
                        {
                            OrderId = 2,
                            ProductId = 3,
                            Quantity = 3
                        },
                        new
                        {
                            OrderId = 2,
                            ProductId = 4,
                            Quantity = 2
                        },
                        new
                        {
                            OrderId = 3,
                            ProductId = 5,
                            Quantity = 1
                        },
                        new
                        {
                            OrderId = 3,
                            ProductId = 6,
                            Quantity = 1
                        },
                        new
                        {
                            OrderId = 4,
                            ProductId = 9,
                            Quantity = 1
                        },
                        new
                        {
                            OrderId = 4,
                            ProductId = 10,
                            Quantity = 1
                        });
                });

            modelBuilder.Entity("CornerStore.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Brand")
                        .HasColumnType("text");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("ProductName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Brand = "BrandA",
                            CategoryId = 1,
                            Price = 999.99m,
                            ProductName = "Laptop"
                        },
                        new
                        {
                            Id = 2,
                            Brand = "BrandB",
                            CategoryId = 1,
                            Price = 799.99m,
                            ProductName = "Smartphone"
                        },
                        new
                        {
                            Id = 3,
                            Brand = "BrandC",
                            CategoryId = 2,
                            Price = 19.99m,
                            ProductName = "T-Shirt"
                        },
                        new
                        {
                            Id = 4,
                            Brand = "BrandD",
                            CategoryId = 2,
                            Price = 39.99m,
                            ProductName = "Jeans"
                        },
                        new
                        {
                            Id = 5,
                            Brand = "BrandE",
                            CategoryId = 3,
                            Price = 49.99m,
                            ProductName = "Blender"
                        },
                        new
                        {
                            Id = 6,
                            Brand = "BrandF",
                            CategoryId = 3,
                            Price = 89.99m,
                            ProductName = "Microwave"
                        },
                        new
                        {
                            Id = 7,
                            Brand = "BrandG",
                            CategoryId = 1,
                            Price = 399.99m,
                            ProductName = "Tablet"
                        },
                        new
                        {
                            Id = 8,
                            Brand = "BrandH",
                            CategoryId = 2,
                            Price = 29.99m,
                            ProductName = "Sweater"
                        },
                        new
                        {
                            Id = 9,
                            Brand = "BrandI",
                            CategoryId = 1,
                            Price = 149.99m,
                            ProductName = "Headphones"
                        },
                        new
                        {
                            Id = 10,
                            Brand = "BrandJ",
                            CategoryId = 3,
                            Price = 129.99m,
                            ProductName = "Vacuum Cleaner"
                        });
                });

            modelBuilder.Entity("CornerStore.Models.Order", b =>
                {
                    b.HasOne("CornerStore.Models.Cashier", "Cashier")
                        .WithMany("Orders")
                        .HasForeignKey("CashierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cashier");
                });

            modelBuilder.Entity("CornerStore.Models.OrderProduct", b =>
                {
                    b.HasOne("CornerStore.Models.Order", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CornerStore.Models.Product", "Product")
                        .WithMany("OrderProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CornerStore.Models.Product", b =>
                {
                    b.HasOne("CornerStore.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("CornerStore.Models.Cashier", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("CornerStore.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("CornerStore.Models.Order", b =>
                {
                    b.Navigation("OrderProducts");
                });

            modelBuilder.Entity("CornerStore.Models.Product", b =>
                {
                    b.Navigation("OrderProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
