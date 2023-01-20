﻿// <auto-generated />
using System;
using HitchFix.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HitchFix.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230120225205_AddDeviceTypeSvg")]
    partial class AddDeviceTypeSvg
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HitchFix.Models.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DeviceTypeId")
                        .HasColumnType("int");

                    b.Property<string>("ModelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DeviceTypeId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("HitchFix.Models.DeviceProblem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DeviceId")
                        .HasColumnType("int");

                    b.Property<double?>("DiscountPrice")
                        .HasColumnType("float");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("ProblemName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TotalPriceAfterDiscount")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.ToTable("DeviceProblems");
                });

            modelBuilder.Entity("HitchFix.Models.DeviceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DeviceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageSvg")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DeviceTypes");
                });

            modelBuilder.Entity("HitchFix.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("DiscountTotal")
                        .HasColumnType("float");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrderStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OrderTime")
                        .HasColumnType("datetime2");

                    b.Property<double>("OrderTotal")
                        .HasColumnType("float");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("HitchFix.Models.OrderProblem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double?>("DiscountPrice")
                        .HasColumnType("float");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("ProblemName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TotalPriceAfterDiscount")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderProblems");
                });

            modelBuilder.Entity("HitchFix.Models.Device", b =>
                {
                    b.HasOne("HitchFix.Models.DeviceType", "DeviceType")
                        .WithMany()
                        .HasForeignKey("DeviceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DeviceType");
                });

            modelBuilder.Entity("HitchFix.Models.DeviceProblem", b =>
                {
                    b.HasOne("HitchFix.Models.Device", "Device")
                        .WithMany("DeviceProblems")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("HitchFix.Models.OrderProblem", b =>
                {
                    b.HasOne("HitchFix.Models.Order", "Order")
                        .WithMany("OrderProblems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("HitchFix.Models.Device", b =>
                {
                    b.Navigation("DeviceProblems");
                });

            modelBuilder.Entity("HitchFix.Models.Order", b =>
                {
                    b.Navigation("OrderProblems");
                });
#pragma warning restore 612, 618
        }
    }
}
