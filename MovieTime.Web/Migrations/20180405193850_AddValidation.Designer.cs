﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using MovieTime.Web.Database;
using System;

namespace MovieTime.Web.Migrations
{
    [DbContext(typeof(MovieContext))]
    [Migration("20180405193850_AddValidation")]
    partial class AddValidation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MovieTime.Web.Comments.Models.Comment", b =>
                {
                    b.Property<string>("CommentId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("MovieId")
                        .IsRequired();

                    b.Property<string>("UserId");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(2000);

                    b.HasKey("CommentId");

                    b.HasIndex("MovieId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("MovieTime.Web.Genres.GenreModels.Genre", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Name");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("MovieTime.Web.Genres.MovieGenreModels.MovieGenre", b =>
                {
                    b.Property<string>("MovieId");

                    b.Property<string>("GenreId");

                    b.HasKey("MovieId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("MovieGenre");
                });

            modelBuilder.Entity("MovieTime.Web.Movies.Models.Movie", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("Actors")
                        .IsRequired()
                        .HasMaxLength(2000);

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasMaxLength(2000);

                    b.Property<string>("ImdbRating");

                    b.Property<string>("Plot")
                        .IsRequired()
                        .HasMaxLength(4000);

                    b.Property<string>("Poster")
                        .IsRequired();

                    b.Property<int>("RunTimeInMinutes");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(2000);

                    b.Property<string>("Writer")
                        .IsRequired()
                        .HasMaxLength(2000);

                    b.Property<DateTime>("Year");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("MovieTime.Web.Reviews.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDateTime");

                    b.Property<DateTime>("EditedDateTime");

                    b.Property<bool>("IsConcept");

                    b.Property<string>("MovieId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.HasIndex("UserId");

                    b.ToTable("Review");
                });

            modelBuilder.Entity("MovieTime.Web.TrackedMovies.Models.TrackedMovie", b =>
                {
                    b.Property<string>("MovieId");

                    b.Property<string>("UserId");

                    b.Property<DateTime>("CreatedTime");

                    b.Property<bool>("Watched");

                    b.HasKey("MovieId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("TrackedMovies");
                });

            modelBuilder.Entity("MovieTime.Web.Users.User", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(35);

                    b.Property<string>("ImageUrl");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(35);

                    b.Property<string>("UserName")
                        .HasMaxLength(35);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MovieTime.Web.Comments.Models.Comment", b =>
                {
                    b.HasOne("MovieTime.Web.Movies.Models.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MovieTime.Web.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("MovieTime.Web.Genres.MovieGenreModels.MovieGenre", b =>
                {
                    b.HasOne("MovieTime.Web.Genres.GenreModels.Genre", "Genre")
                        .WithMany("Movies")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MovieTime.Web.Movies.Models.Movie", "Movie")
                        .WithMany("Genres")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MovieTime.Web.Reviews.Models.Review", b =>
                {
                    b.HasOne("MovieTime.Web.Movies.Models.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieId");

                    b.HasOne("MovieTime.Web.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("MovieTime.Web.TrackedMovies.Models.TrackedMovie", b =>
                {
                    b.HasOne("MovieTime.Web.Movies.Models.Movie", "Movie")
                        .WithMany("TrackedMovies")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MovieTime.Web.Users.User", "User")
                        .WithMany("TrackedMovies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
