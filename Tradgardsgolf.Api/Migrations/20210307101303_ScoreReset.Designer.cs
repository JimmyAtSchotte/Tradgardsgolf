﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Tradgardsgolf.Infrastructure;
using Tradgardsgolf.Infrastructure.Database;

namespace Tradgardsgolf.Api.Migrations
{
    [DbContext(typeof(TradgardsgolfContext))]
    [Migration("20210307101303_ScoreReset")]
    partial class ScoreReset
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Tradgardsgolf.Application.Core.Entities.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("dtmCreated");

                    b.Property<int>("CreatedById")
                        .HasColumnType("integer")
                        .HasColumnName("intCreatedBy");

                    b.Property<int>("Holes")
                        .HasColumnType("integer")
                        .HasColumnName("intHoles");

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision")
                        .HasColumnName("dblLatitude");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision")
                        .HasColumnName("dblLongitude");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("strName");

                    b.Property<DateTime?>("ScoreReset")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("dtmScoreReset");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("course");
                });

            modelBuilder.Entity("Tradgardsgolf.Application.Core.Entities.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("dtmCreated");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("strEmail");

                    b.Property<string>("Key")
                        .HasColumnType("text")
                        .HasColumnName("strKey");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("strName");

                    b.Property<string>("Password")
                        .HasColumnType("text")
                        .HasColumnName("strPassword");

                    b.HasKey("Id");

                    b.ToTable("player");
                });

            modelBuilder.Entity("Tradgardsgolf.Application.Core.Entities.Round", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CourseId")
                        .HasColumnType("integer")
                        .HasColumnName("intCourseId");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("dtmDate");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("round");
                });

            modelBuilder.Entity("Tradgardsgolf.Application.Core.Entities.RoundScore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Hole")
                        .HasColumnType("integer")
                        .HasColumnName("intHole");

                    b.Property<int>("PlayerId")
                        .HasColumnType("integer")
                        .HasColumnName("intPlayerId");

                    b.Property<int>("RoundId")
                        .HasColumnType("integer")
                        .HasColumnName("intRoundId");

                    b.Property<int>("Score")
                        .HasColumnType("integer")
                        .HasColumnName("intScore");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("RoundId");

                    b.ToTable("roundscore");
                });

            modelBuilder.Entity("Tradgardsgolf.Application.Core.Entities.Course", b =>
                {
                    b.HasOne("Tradgardsgolf.Application.Core.Entities.Player", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("Tradgardsgolf.Application.Core.Entities.Round", b =>
                {
                    b.HasOne("Tradgardsgolf.Application.Core.Entities.Course", "Course")
                        .WithMany("Rounds")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Tradgardsgolf.Application.Core.Entities.RoundScore", b =>
                {
                    b.HasOne("Tradgardsgolf.Application.Core.Entities.Player", "Player")
                        .WithMany("RoundScores")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tradgardsgolf.Application.Core.Entities.Round", "Round")
                        .WithMany("RoundScores")
                        .HasForeignKey("RoundId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");

                    b.Navigation("Round");
                });

            modelBuilder.Entity("Tradgardsgolf.Application.Core.Entities.Course", b =>
                {
                    b.Navigation("Rounds");
                });

            modelBuilder.Entity("Tradgardsgolf.Application.Core.Entities.Player", b =>
                {
                    b.Navigation("RoundScores");
                });

            modelBuilder.Entity("Tradgardsgolf.Application.Core.Entities.Round", b =>
                {
                    b.Navigation("RoundScores");
                });
#pragma warning restore 612, 618
        }
    }
}
