﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using MovieTime.Web.Movie.Persistance;
using System;

namespace MovieTime.Web.Migrations
{
    [DbContext(typeof(MovieContext))]
    partial class MovieContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("MovieTime.Web.Movie.Persistance.Database.DbGenre", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("CustomField");

                    b.HasKey("Name");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("MovieTime.Web.Movie.Persistance.Database.DbMovie", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Actors")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Plot")
                        .IsRequired()
                        .HasMaxLength(600);

                    b.Property<string>("Poster")
                        .IsRequired();

                    b.Property<string>("Rated")
                        .IsRequired();

                    b.Property<double>("Rating");

                    b.Property<int>("RunTimeInMinutes")
                        .HasMaxLength(1440);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<string>("Writer")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("Year");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("MovieTime.Web.Movie.Persistance.Database.DbMovieGenre", b =>
                {
                    b.Property<string>("DbMovieId");

                    b.Property<string>("DbGenreId");

                    b.HasKey("DbMovieId", "DbGenreId");

                    b.HasIndex("DbGenreId");

                    b.ToTable("MovieGenre");
                });

            modelBuilder.Entity("MovieTime.Web.Users.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MovieTime.Web.Movie.Persistance.Database.DbMovieGenre", b =>
                {
                    b.HasOne("MovieTime.Web.Movie.Persistance.Database.DbGenre", "Genre")
                        .WithMany("Movies")
                        .HasForeignKey("DbGenreId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MovieTime.Web.Movie.Persistance.Database.DbMovie", "Movie")
                        .WithMany("Genres")
                        .HasForeignKey("DbMovieId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
