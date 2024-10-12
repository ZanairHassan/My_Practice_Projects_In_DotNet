﻿// <auto-generated />
using System;
using DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(GoThrowDbContext))]
    partial class GoThrowDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DAL.Models.Airport", b =>
                {
                    b.Property<int>("AirportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AirportId"));

                    b.Property<string>("AirportCity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AirportCountry")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("AirportCreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("AirportDeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("AirportName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AirportType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("AirportUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("AirportId");

                    b.ToTable("Airports");
                });
#pragma warning restore 612, 618
        }
    }
}
