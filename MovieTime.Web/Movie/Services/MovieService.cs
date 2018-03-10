﻿using System;
using AutoMapper;
using MovieTime.Web.Movie.Persistance.Database;
using MovieTime.Web.Movie.Persistance.Omdb;
using MovieTime.Web.Movie.Persistance.ViewModels;
using MovieTime.Web.Movie.Repositories;

namespace MovieTime.Web.Movie.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMapper _mapper;
        private readonly IMovieRepository _movieRepository;
        private readonly IDatabaseMovieRespository _databaseMovieRespository;

        public MovieService(IMapper mapper, IMovieRepository movieRepository, IDatabaseMovieRespository databaseMovieRespository)
        {
            _mapper = mapper;
            _movieRepository = movieRepository;
            _databaseMovieRespository = databaseMovieRespository;
        }

        public MovieDetailsViewModel GetMovieDetailsById(string id)
        {
            var movieModel = _databaseMovieRespository.GetMovieById(id);
            if (movieModel == null)
            {
                movieModel = _movieRepository.GetMovieById(id);
            }

            var movieDetailsVm = _mapper.Map<DbMovie, MovieDetailsViewModel>(movieModel);

            return movieDetailsVm;
        }

        public MovieDetailsViewModel GetMovieDetailsByTitle(string title)
        {
            var movieModel = _databaseMovieRespository.GetMovieByTitle(title);

            if (movieModel == null)
            {
                movieModel = _movieRepository.GetMovieByTitle(title);
            }

            var movieDetailsVm = _mapper.Map<DbMovie, MovieDetailsViewModel>(movieModel); //todo test null

            return movieDetailsVm;
        }

        public SearchResultsModel GetMoviesByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public void AddMovie(MovieForCreationDto movie)
        {
            throw new NotImplementedException();
        }
    }
}