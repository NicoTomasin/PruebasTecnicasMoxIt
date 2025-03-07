﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vuelos.Data;

#nullable disable

namespace Vuelos.Migrations
{
    [DbContext(typeof(VuelosDbContext))]
    [Migration("20240806190852_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("Vuelos.Models.VuelosModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Airline")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CheckIn")
                        .HasColumnType("TEXT");

                    b.Property<string>("FlightNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("delayed")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Vuelos");
                });
#pragma warning restore 612, 618
        }
    }
}
