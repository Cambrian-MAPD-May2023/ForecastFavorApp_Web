﻿// <auto-generated />
using System;
using ForecastFavorLib.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ForecastFavorApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231207024514_UpdatePreferencesModel")]
    partial class UpdatePreferencesModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.25");

            modelBuilder.Entity("ForecastFavorLib.Models.CalendarEvent", b =>
                {
                    b.Property<int>("EventID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EventDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("EventLocation")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("EventTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("EventTitle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("EventID");

                    b.HasIndex("UserID");

                    b.ToTable("CalendarEvents");
                });

            modelBuilder.Entity("ForecastFavorLib.Models.Preferences", b =>
                {
                    b.Property<int>("PreferencesID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("NotifyOnClouds")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("NotifyOnRain")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("NotifyOnSnow")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("NotifyOnSun")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PreferredLocations")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("PreferencesID");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("Preferences");
                });

            modelBuilder.Entity("ForecastFavorLib.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ForecastFavorLib.Models.WeatherHistory", b =>
                {
                    b.Property<int>("HistoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<double>("Precipitation")
                        .HasColumnType("REAL");

                    b.Property<double>("Temperature")
                        .HasColumnType("REAL");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("HistoryID");

                    b.HasIndex("UserID");

                    b.ToTable("WeatherHistories");
                });

            modelBuilder.Entity("ForecastFavorLib.Models.CalendarEvent", b =>
                {
                    b.HasOne("ForecastFavorLib.Models.User", "User")
                        .WithMany("CalendarEvents")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ForecastFavorLib.Models.Preferences", b =>
                {
                    b.HasOne("ForecastFavorLib.Models.User", "User")
                        .WithOne("Preferences")
                        .HasForeignKey("ForecastFavorLib.Models.Preferences", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ForecastFavorLib.Models.WeatherHistory", b =>
                {
                    b.HasOne("ForecastFavorLib.Models.User", "User")
                        .WithMany("WeatherHistories")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ForecastFavorLib.Models.User", b =>
                {
                    b.Navigation("CalendarEvents");

                    b.Navigation("Preferences")
                        .IsRequired();

                    b.Navigation("WeatherHistories");
                });
#pragma warning restore 612, 618
        }
    }
}
