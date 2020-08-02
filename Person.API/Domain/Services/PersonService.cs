using AutoMapper;
using Person.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Person.API.Domain.Services
{
    public class PersonService : IPersonService
    {
        private IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonService() { }
        public PersonService(IPersonRepository repo, IMapper mapper)
        {
            _personRepository = repo;
            _mapper = mapper;
        }

        List<PersonSearchResultsModel> SearchPeople(PersonSearchModel searchRequest) => _personRepository.SearchPeople(searchRequest);

    }
}
