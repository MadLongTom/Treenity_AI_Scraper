﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Treenity_AI_Scraper.Services;

#nullable disable

namespace Treenity_AI_Scraper.Migrations
{
    [DbContext(typeof(ProgramDbContext))]
    [Migration("20240425083048_SqlServerDefault")]
    partial class SqlServerDefault
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Treenity_AI_Scraper.Models.Database.Answer", b =>
                {
                    b.Property<long>("questionId")
                        .HasColumnType("bigint");

                    b.Property<string>("answers")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("questionId");

                    b.ToTable("AnswerStores");
                });

            modelBuilder.Entity("Treenity_AI_Scraper.Models.Database.AppRuntimeConfig", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("lastGetTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("AppRuntimeConfig");
                });

            modelBuilder.Entity("Treenity_AI_Scraper.Models.Database.Channel", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<string>("coursdIds")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("courseIds")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Channels");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            coursdIds = "[4000001593]",
                            courseIds = "[4000001593]"
                        },
                        new
                        {
                            Id = 2L,
                            coursdIds = "[4000001588,4000001589]",
                            courseIds = "[4000001588,4000001589]"
                        },
                        new
                        {
                            Id = 3L,
                            coursdIds = "[4000001598]",
                            courseIds = "[4000001598]"
                        });
                });

            modelBuilder.Entity("Treenity_AI_Scraper.Models.Database.EntityStore", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("CookieExpired")
                        .HasColumnType("datetime2");

                    b.Property<string>("cookie")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Entities");
                });

            modelBuilder.Entity("Treenity_AI_Scraper.Models.Database.TicketStore", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("channel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("entityStoreId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("finishTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("finished")
                        .HasColumnType("bit");

                    b.Property<DateTime>("orderTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("startTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("entityStoreId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("Treenity_AI_Scraper.Models.Database.TicketStore", b =>
                {
                    b.HasOne("Treenity_AI_Scraper.Models.Database.EntityStore", "entityStore")
                        .WithMany()
                        .HasForeignKey("entityStoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("entityStore");
                });
#pragma warning restore 612, 618
        }
    }
}
