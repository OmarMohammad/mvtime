﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieTime.Web.Auth;
using MovieTime.Web.TrackedMovies.Models;
using Serilog;
using System.Linq;
using System.Collections;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;

namespace MovieTime.Web.TrackedMovies
{
    [Route("api")]
    public class TrackController : Controller
    {
        private readonly ITrackService _trackService;
        private readonly IMapper _mapper;

        public TrackController(ITrackService trackService, IMapper mapper)
        {
            _trackService = trackService;
            _mapper = mapper;
        }

        /* Return a list of all tracked movies of the specified user. */
        [HttpGet("tracks/user/{userId}")]
        public async Task<IActionResult> GetAllTrackedMoviesByUserId(string userId)
        {
            try
            {
                var userExist = await _trackService.UserExist(userId);
                if (!userExist)
                {
                    return NotFound();
                }

                var trackedMovies = await _trackService.GetTrackedMoviesByUser(userId);
                var trackedMoviesDto = _mapper.Map<ICollection<TrackedMovie>, ICollection<TrackedMoviesGetDto>>(trackedMovies);

                return Ok(trackedMoviesDto);
            }
            catch (Exception err)
            {
                Log.Error(err.Message);
                return BadRequest(new { message = err.Message });
            }
        }

        /* Track a specified movie with the userId (retrieved from the request header). */
        [Authorize]
        [HttpPost("tracks/movie/{movieId}")]
        public async Task<IActionResult> TrackMovie(string movieId)
        {
            try
            {
                var movieExists = await _trackService.MovieExist(movieId);
                if (!movieExists)
                {
                    return NotFound();
                }
                
                var userIdFromToken = this.User.GetUserId();
                var trackedMovie = new TrackedMovie { MovieId = movieId, UserId = userIdFromToken, Watched = false };
                trackedMovie.CreatedTime = DateTime.Now; // CreatedTime is used to sort the trackedMovies in the DTO.
                await _trackService.TrackMovie(trackedMovie);
                
                return NoContent();      
            }
            catch (Exception err)
            {
                Log.Error(err.Message);
                return BadRequest(new { message = err.Message });
            }
        }

        /* Check whether the current movie is tracked by the user. */
        [Authorize]
        [HttpGet("tracked/movie/{movieId}")]
        public async Task<IActionResult> IsMovieTrackedByUser(string movieId)
        {
            try
            {
                var movieExists = await _trackService.MovieExist(movieId);
                if (!movieExists)
                {
                    return NotFound();
                }

                var userIdFromToken = this.User.GetUserId();
                var result = await _trackService.IsMovieTrackedByUser(userIdFromToken, movieId);
                return Ok(new { isTracked = result != null, isWatched = result != null ? result.Watched : false });
            }
            catch (Exception err)
            {
                Log.Error(err.Message);
                return BadRequest(new { message = err.Message });                
            }
        }

        /* Untrack a specified movie with the userId (retrieved from the request header). */
        [Authorize]
        [HttpPost("untrack/movie/{movieId}")]
        public async Task<IActionResult> UntrackMovie(string movieId)
        {         
            try
            {
                var movieExists = await _trackService.MovieExist(movieId);
                if (!movieExists)
                {
                    return NotFound();
                }
                
                var userIdFromToken = this.User.GetUserId();
                var trackedMovie = new TrackedMovie { MovieId = movieId, UserId = userIdFromToken };
                await _trackService.UntrackMovie(trackedMovie); 

                return Ok();
            }
            catch (Exception err)
            {
                Log.Error(err.Message);
                return BadRequest(new { message = err.Message });
            }
        }  

        /* Toggle the watch status with the specified movie and userId (retrieved from the request header). */
        [Authorize]
        [HttpPost("watch/movie/{movieId}")]
        public async Task<IActionResult> ToggleMovieWatchedStatus(string movieId)
        {         
            try
            {
                var movieExists = await _trackService.MovieExist(movieId);
                if (!movieExists)
                {
                    return NotFound();
                }
                
                var userIdFromToken = this.User.GetUserId();
                var result = await _trackService.ToggleMovieWatchedStatus(movieId, userIdFromToken);
                if (result == null) 
                {
                    return BadRequest(new { message = "User is not tracking the current movie" });
                }

                var response = _mapper.Map<TrackedMovie, TrackedMovieGetDto>(result);
                return Ok(response);
            }
            catch (Exception err)
            {
                Log.Error(err.Message);
                return BadRequest(new { message = err.Message });
            }
        }  
    }
}