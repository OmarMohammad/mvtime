﻿using Microsoft.EntityFrameworkCore;
using MovieTime.Web.Database;

namespace MovieTime.Web.TrackedMovies.Models
{
    public class TrackedMovieModelBuildingConfig : IEntityModelBuildingConfig
    {
        public void Map(ModelBuilder builder)
        {
            MapProperties(builder);
            MapRelations(builder);
        }

        public void MapRelations(ModelBuilder builder)
        {
        }

        public void MapProperties(ModelBuilder builder)
        {
            builder.Entity<TrackedMovie>()
                .HasKey(t => new { t.MovieId, t.UserId });
        }

    }
}