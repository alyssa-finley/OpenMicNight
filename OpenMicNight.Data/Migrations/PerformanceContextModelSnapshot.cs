﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OpenMicNight.Data;

#nullable disable

namespace OpenMicNight.Data.Migrations
{
    [DbContext(typeof(PerformanceContext))]
    partial class PerformanceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OpenMicNight.Domain.Music", b =>
                {
                    b.Property<int>("MusicId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MusicId"));

                    b.Property<int>("PerformancePerformerId")
                        .HasColumnType("int");

                    b.Property<int>("PerformerId")
                        .HasColumnType("int");

                    b.Property<string>("PerformerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MusicId");

                    b.HasIndex("PerformancePerformerId");

                    b.ToTable("Music");
                });

            modelBuilder.Entity("OpenMicNight.Domain.Performance", b =>
                {
                    b.Property<int>("PerformerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PerformerId"));

                    b.Property<DateTime>("Datetime")
                        .HasColumnType("datetime2");

                    b.Property<string>("PerformerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PerformerType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PerformerId");

                    b.ToTable("Performance");
                });

            modelBuilder.Entity("OpenMicNight.Domain.Song", b =>
                {
                    b.Property<int>("SongId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SongId"));

                    b.Property<bool>("IsOriginal")
                        .HasColumnType("bit");

                    b.Property<int>("MusicId")
                        .HasColumnType("int");

                    b.Property<int>("PerformancePerformerId")
                        .HasColumnType("int");

                    b.Property<int>("PerformerId")
                        .HasColumnType("int");

                    b.Property<string>("SongName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SongId");

                    b.HasIndex("MusicId");

                    b.HasIndex("PerformancePerformerId");

                    b.ToTable("Songs");
                });

            modelBuilder.Entity("OpenMicNight.Domain.Music", b =>
                {
                    b.HasOne("OpenMicNight.Domain.Performance", "Performance")
                        .WithMany()
                        .HasForeignKey("PerformancePerformerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Performance");
                });

            modelBuilder.Entity("OpenMicNight.Domain.Song", b =>
                {
                    b.HasOne("OpenMicNight.Domain.Music", "Music")
                        .WithMany("Songs")
                        .HasForeignKey("MusicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OpenMicNight.Domain.Performance", "Performance")
                        .WithMany()
                        .HasForeignKey("PerformancePerformerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Music");

                    b.Navigation("Performance");
                });

            modelBuilder.Entity("OpenMicNight.Domain.Music", b =>
                {
                    b.Navigation("Songs");
                });
#pragma warning restore 612, 618
        }
    }
}
