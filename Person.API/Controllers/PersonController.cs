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

        [HttpGet("{personId}")]
        [ProducesResponseType(typeof(PeopleModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(PeopleModel), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(PeopleModel), (int)HttpStatusCode.InternalServerError)]
        public IActionResult GetPerson(long personId)
        {
            try
            {
                var data = _service.GetPerson(personId);

                var retVal = _mapper.Map<PeopleModel>(data);

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


        [HttpGet("states")]
        [ProducesResponseType(typeof(StatesModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(StatesModel), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(StatesModel), (int)HttpStatusCode.InternalServerError)]
        public IActionResult GetMovieRatings()
        {
            try
            {
                var data = _service.GetStates();

                var retVal = _mapper.Map<IEnumerable<StatesModel>>(data);

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
        public IActionResult UpdatePerson([FromBody] PeopleModel person)
        {
            var errorList = new List<string>();

            try
            {

                errorList = _service.SaveDetail(person);
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
