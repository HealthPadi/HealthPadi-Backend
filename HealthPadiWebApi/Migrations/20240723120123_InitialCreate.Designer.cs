﻿// <auto-generated />
using System;
using HealthPadiWebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HealthPadiWebApi.Migrations
{
    [DbContext(typeof(HealthPadiDataContext))]
    [Migration("20240723120123_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HealthPadiBackend.Models.Feed", b =>
                {
                    b.Property<Guid>("FeedId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FeedContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FeedId");

                    b.ToTable("Feeds");
                });

            modelBuilder.Entity("HealthPadiBackend.Models.FeedMedia", b =>
                {
                    b.Property<Guid>("FeedMediaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Extension")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("FeedId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MediaUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MimeType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FeedMediaId");

                    b.HasIndex("FeedId");

                    b.ToTable("FeedMedias");
                });

            modelBuilder.Entity("HealthPadiBackend.Models.HealthUpdate", b =>
                {
                    b.Property<Guid>("HealthUpdateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HealthUpdateId");

                    b.ToTable("HealthUpdates");
                });

            modelBuilder.Entity("HealthPadiBackend.Models.Report", b =>
                {
                    b.Property<Guid>("ReportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ReportId");

                    b.HasIndex("UserId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("HealthPadiBackend.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HealthPadiBackend.Models.FeedMedia", b =>
                {
                    b.HasOne("HealthPadiBackend.Models.Feed", "Feed")
                        .WithMany("FeedMedias")
                        .HasForeignKey("FeedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Feed");
                });

            modelBuilder.Entity("HealthPadiBackend.Models.Report", b =>
                {
                    b.HasOne("HealthPadiBackend.Models.User", "User")
                        .WithMany("Reports")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("HealthPadiBackend.Models.Feed", b =>
                {
                    b.Navigation("FeedMedias");
                });

            modelBuilder.Entity("HealthPadiBackend.Models.User", b =>
                {
                    b.Navigation("Reports");
                });
#pragma warning restore 612, 618
        }
    }
}
