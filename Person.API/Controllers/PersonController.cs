using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Person.API.Domain.Models;
using Person.API.Domain.Services;

namespace Person.API.Controllers
{
    [EnableCors("SiteCorsPolicy")]
    [Route("api/people")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private IPersonService _service;
        private readonly IMapper _mapper;
        public PersonController(IPersonService service, IMapper mapper)
        {
            _service = service ??
                throw new ArgumentNullException(nameof(service));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<PeopleModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<PeopleModel>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(List<PeopleModel>), (int)HttpStatusCode.InternalServerError)]
        public IActionResult GetPeople()
        {
            try
            {
                var data = _service.GetMovies();

                var retVal = _mapper.Map<IEnumerable<PeopleModel>>(data);

                if (retVal != null)
                {
                    return Ok(retVal);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "A problem happened while handling your request.");
            }
        }

        [HttpPost("search")]
        [ProducesResponseType(typeof(List<PersonSearchResultsModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<PersonSearchResultsModel>), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(List<PersonSearchResultsModel>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(List<PersonSearchResultsModel>), (int)HttpStatusCode.InternalServerError)]
        public IActionResult SearchMovies([FromBody] PersonSearchModel searchRequest)
        {
            try
            {
                var searchResults = _service.SearchPeople(searchRequest);

                return Ok(searchResults);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "A problem happened while handling your request.");
            }

        }

        [HttpGet("{movieId}")]
        [ProducesResponseType(typeof(MoviesModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(MoviesModel), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(MoviesModel), (int)HttpStatusCode.InternalServerError)]
        public IActionResult GetMovie(int movieId)
        {
            try
            {
                var data = _service.GetMovie(movieId);

                var retVal = _mapper.Map<MoviesModel>(data);

                if (retVal != null)
                {
                    return Ok(retVal);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "A problem happened while handling your request.");
            }
        }


        [HttpGet("ratings")]
        [ProducesResponseType(typeof(MovieRatingsModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(MovieRatingsModel), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(MovieRatingsModel), (int)HttpStatusCode.InternalServerError)]
        public IActionResult GetMovieRatings()
        {
            try
            {
                var data = _service.GetMovieRatings();

                var retVal = _mapper.Map<IEnumerable<MovieRatingsModel>>(data);

                if (retVal.Count() > 0)
                {
                    return Ok(retVal);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "A problem happened while handling your request.");
            }
        }

        [HttpGet("genres")]
        [ProducesResponseType(typeof(MovieGenresModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(MovieGenresModel), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(MovieGenresModel), (int)HttpStatusCode.InternalServerError)]
        public IActionResult GetMovieGenres()
        {
            try
            {
                var data = _service.GetMovieGenres();

                var retVal = _mapper.Map<IEnumerable<MovieGenresModel>>(data);

                if (retVal.Count() > 0)
                {
                    return Ok(retVal);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "A problem happened while handling your request.");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(List<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(List<string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateMovie([FromBody] MoviesModel movie)
        {
            var errorList = new List<string>();

            try
            {

                errorList = _service.SaveDetail(movie);
                if (errorList.Count > 0)
                {
                    return BadRequest(errorList);
                }
            }
            catch (Exception ex)
            {
                errorList = new List<string>() { "Error in saving" };
                return BadRequest(errorList);
            }

            return Ok(errorList);
        }

    }
}
