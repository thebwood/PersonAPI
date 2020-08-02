using AutoMapper;
using Person.API.Data;
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

        public List<PersonSearchResultsModel> SearchPeople(PersonSearchModel searchRequest) => _personRepository.SearchPeople(searchRequest);
        public List<States> GetStates() => _personRepository.GetStates();
        public People GetPerson(long personId) => _personRepository.GetPerson(personId);
        public List<string> SaveDetail(PeopleModel person)
        {
            var errors = ValidatePerson(person);
            if (errors.Count == 0)
            {
                var existingPerson = new People();
                if (person.Id > 0)
                    existingPerson = _personRepository.GetPerson(person.Id);

                _mapper.Map<PeopleModel, People>(person, existingPerson);
                _personRepository.SaveDetail(existingPerson);
            }

            return errors;
        }

        private List<string> ValidatePerson(PeopleModel person)
        {
            var errors = new List<string>();

            if (String.IsNullOrWhiteSpace(person.FirstName))
            {
                errors.Add("First Name is required");
            }
            if (String.IsNullOrWhiteSpace(person.LastName))
            {
                errors.Add("Last Name is required");
            }


            return errors;
        }
    }
}
