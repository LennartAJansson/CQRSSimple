﻿// <auto-generated />
using System;
using CQRS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CQRS.Migrations
{
    [DbContext(typeof(WeatherForecastsContext))]
    [Migration("20211216142046_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CQRS.Model.Operation", b =>
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

                    b.Property<string>("Data")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("WeatherForecastId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("OperationId");

                    b.HasIndex("WeatherForecastId");

                    b.ToTable("Operations");
                });

            modelBuilder.Entity("CQRS.Model.WeatherForecast", b =>
                {
                    b.Property<Guid>("WeatherForecastId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TemperatureC")
                        .HasColumnType("int");

                    b.HasKey("WeatherForecastId");

                    b.ToTable("WeatherForecasts");
                });

            modelBuilder.Entity("CQRS.Model.Operation", b =>
                {
                    b.HasOne("CQRS.Model.WeatherForecast", "WeatherForecast")
                        .WithMany("Operations")
                        .HasForeignKey("WeatherForecastId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WeatherForecast");
                });

            modelBuilder.Entity("CQRS.Model.WeatherForecast", b =>
                {
                    b.Navigation("Operations");
                });
#pragma warning restore 612, 618
        }
    }
}