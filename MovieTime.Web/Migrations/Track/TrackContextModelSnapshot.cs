﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using MovieTime.Web.Track;
using System;

namespace MovieTime.Web.Migrations.Track
{
    [DbContext(typeof(TrackContext))]
    partial class TrackContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("MovieTime.Web.Track.TrackModel", b =>
                {
                    b.Property<string>("MovieId");

                    b.Property<string>("UserId");

                    b.HasKey("MovieId", "UserId");

                    b.ToTable("Track");
                });
#pragma warning restore 612, 618
        }
    }
}
