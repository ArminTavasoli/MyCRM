﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyCRM.Data.DbContexts;

#nullable disable

namespace MyCRM.Data.Migrations
{
    [DbContext(typeof(CrmContext))]
    partial class CrmContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MyCRM.Domain.Entities.Account.Customer", b =>
                {
                    b.Property<long>("UserID")
                        .HasColumnType("bigint");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Job")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("UserID");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("MyCRM.Domain.Entities.Account.Marketer", b =>
                {
                    b.Property<long>("UserID")
                        .HasColumnType("bigint");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("FieldStudy")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("IrCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("MarketerEducation")
                        .HasColumnType("int");

                    b.HasKey("UserID");

                    b.ToTable("Marketers");
                });

            modelBuilder.Entity("MyCRM.Domain.Entities.Account.User", b =>
                {
                    b.Property<long>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("UserID"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("IntroduceName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("MobilePhone")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("UserGender")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MyCRM.Domain.Entities.Orders.Order", b =>
                {
                    b.Property<long>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("OrderId"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("CustomerId")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFinish")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSale")
                        .HasColumnType("bit");

                    b.Property<int>("OrderType")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("MyCRM.Domain.Entities.Orders.OrderSelectedMarketer", b =>
                {
                    b.Property<long>("OrderId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<long>("MarketerId")
                        .HasColumnType("bigint");

                    b.Property<long>("ModifyUserId")
                        .HasColumnType("bigint");

                    b.HasKey("OrderId");

                    b.HasIndex("MarketerId");

                    b.HasIndex("ModifyUserId");

                    b.ToTable("OrderSelectedMarketers");
                });

            modelBuilder.Entity("MyCRM.Domain.Entities.Account.Customer", b =>
                {
                    b.HasOne("MyCRM.Domain.Entities.Account.User", "User")
                        .WithOne("Customer")
                        .HasForeignKey("MyCRM.Domain.Entities.Account.Customer", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyCRM.Domain.Entities.Account.Marketer", b =>
                {
                    b.HasOne("MyCRM.Domain.Entities.Account.User", "User")
                        .WithOne("Marketer")
                        .HasForeignKey("MyCRM.Domain.Entities.Account.Marketer", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyCRM.Domain.Entities.Orders.Order", b =>
                {
                    b.HasOne("MyCRM.Domain.Entities.Account.Customer", "Customer")
                        .WithMany("OrderCollection")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("MyCRM.Domain.Entities.Orders.OrderSelectedMarketer", b =>
                {
                    b.HasOne("MyCRM.Domain.Entities.Account.Marketer", "Marketer")
                        .WithMany("OrderSelectedMarketers")
                        .HasForeignKey("MarketerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MyCRM.Domain.Entities.Account.User", "ModifyUser")
                        .WithMany("OrderSelectedMarketers")
                        .HasForeignKey("ModifyUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MyCRM.Domain.Entities.Orders.Order", "Order")
                        .WithOne("OrderSelectedMarketer")
                        .HasForeignKey("MyCRM.Domain.Entities.Orders.OrderSelectedMarketer", "OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Marketer");

                    b.Navigation("ModifyUser");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("MyCRM.Domain.Entities.Account.Customer", b =>
                {
                    b.Navigation("OrderCollection");
                });

            modelBuilder.Entity("MyCRM.Domain.Entities.Account.Marketer", b =>
                {
                    b.Navigation("OrderSelectedMarketers");
                });

            modelBuilder.Entity("MyCRM.Domain.Entities.Account.User", b =>
                {
                    b.Navigation("Customer")
                        .IsRequired();

                    b.Navigation("Marketer")
                        .IsRequired();

                    b.Navigation("OrderSelectedMarketers");
                });

            modelBuilder.Entity("MyCRM.Domain.Entities.Orders.Order", b =>
                {
                    b.Navigation("OrderSelectedMarketer")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
