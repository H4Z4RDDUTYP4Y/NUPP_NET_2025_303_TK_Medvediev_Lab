﻿// <auto-generated />
using System;
using Guitar.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Guitar.Infrastructure.Migrations
{
    [DbContext(typeof(GuitarContext))]
    partial class GuitarContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.4");

            modelBuilder.Entity("Guitar.Infrastructure.Models.GuitarModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("GuitarType")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("TEXT");

                    b.Property<float>("Price")
                        .HasColumnType("REAL");

                    b.Property<int>("ScaleLength")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StringCount")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Guitars");

                    b.HasDiscriminator<string>("GuitarType").HasValue("GuitarModel");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Guitar.Infrastructure.Models.PlayerModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("YearsExperience")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Guitar.Infrastructure.Models.AcousticModel", b =>
                {
                    b.HasBaseType("Guitar.Infrastructure.Models.GuitarModel");

                    b.Property<bool>("HasPiezo")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StringType")
                        .HasColumnType("INTEGER");

                    b.HasDiscriminator().HasValue("Acoustic");
                });

            modelBuilder.Entity("Guitar.Infrastructure.Models.ElectricModel", b =>
                {
                    b.HasBaseType("Guitar.Infrastructure.Models.GuitarModel");

                    b.Property<int>("PickupCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("VibratoSystem")
                        .HasColumnType("INTEGER");

                    b.HasDiscriminator().HasValue("Electric");
                });

            modelBuilder.Entity("Guitar.Infrastructure.Models.GuitarModel", b =>
                {
                    b.HasOne("Guitar.Infrastructure.Models.PlayerModel", "Player")
                        .WithMany("Guitars")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("Guitar.Infrastructure.Models.PlayerModel", b =>
                {
                    b.Navigation("Guitars");
                });
#pragma warning restore 612, 618
        }
    }
}
