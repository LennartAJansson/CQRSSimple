﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeatherForecastsDb;

#nullable disable

namespace WeatherForecastsDb.Migrations
{
    [DbContext(typeof(WeatherForecastsContext))]
    [Migration("20220110145633_DecimalTemp")]
    partial class DecimalTemp
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Models.Operation", b =>
                {
                    b.Property<Guid>("OperationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Action")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("After")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Before")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("Ready")
                        .HasColumnType("bit");

                    b.Property<string>("RequestData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("WeatherForecastId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("OperationId");

                    b.ToTable("Operations");
                });

            modelBuilder.Entity("Models.WeatherForecast", b =>
                {
                    b.Property<Guid>("WeatherForecastId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Celsius")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WeatherForecastId");

                    b.ToTable("WeatherForecasts");
                });
#pragma warning restore 612, 618
        }
    }
}