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

        [HttpGet("tracks/user/{userId}")]
        public async Task<IActionResult> GetAllTrackedMovies(string userId)
        {
            try
            {
                var userIdFromToken = this.User.GetUserId();
                if (userIdFromToken == null)
                {
                    return BadRequest(new { message = "User is not authenticated" });
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
        
        [HttpPost("tracks/movie/{movieId}")]
        public async Task<IActionResult> TrackMovie(string movieId)
        {
            try
            {
                if (movieId == null)
                {
                    return BadRequest(new { message = "Identity of the movie is missing" });
                }
                
                var userIdFromToken = this.User.GetUserId();
                if (userIdFromToken == null)
                {
                    return BadRequest(new { message = "User is not authenticated" });
                }

                var trackedMovie = new TrackedMovie { MovieId = movieId, UserId = userIdFromToken, Watched = false };
                trackedMovie.CreatedTime = DateTime.Now;
                await _trackService.TrackMovie(trackedMovie);
                
                return NoContent();      
            }
            catch (Exception err)
            {
                Log.Error(err.Message);
                return BadRequest(new { message = err.Message });
            }
        }

        // TODO: Check if movieId exists, otherwise return a BadRequest.
        // Also create a DTO and rename the method so that it makes more sense in the new workflow.
        [HttpGet("tracked/movie/{movieId}")]
        public async Task<IActionResult> IsMovieTrackedByUser(string movieId)
        {
            try
            {
                var userIdFromToken = this.User.GetUserId();
                if (userIdFromToken == null)
                {
                    return BadRequest(new { message = "User is not authenticated" });
                }

                var result = await _trackService.IsMovieTrackedByUser(userIdFromToken, movieId);
                return Ok(new { isTracked = result != null, isWatched = result != null ? result.Watched : false });
            }
            catch (Exception err)
            {
                Log.Error(err.Message);
                return BadRequest(new { message = err.Message });                
            }
        }

        [HttpPost("untrack/movie/{movieId}")]
        public async Task<IActionResult> UntrackMovie(string movieId)
        {         
            try
            {
                if (movieId == null)
                {
                    return BadRequest(new { message = "Identity of the movie is missing" });
                }
                
                var userIdFromToken = this.User.GetUserId();
                if (userIdFromToken == null)
                {
                    return BadRequest(new { message = "User is not authenticated" });
                }

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

        [HttpPost("watch/movie/{movieId}")]
        public async Task<IActionResult> ToggleMovieWatchedStatus(string movieId)
        {         
            try
            {
                if (movieId == null)
                {
                    return BadRequest(new { message = "Identity of the movie is missing" });   
                }
                
                var userIdFromToken = this.User.GetUserId();
                if (userIdFromToken == null)
                {
                    return BadRequest(new { message = "User is not authenticated" });
                }

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