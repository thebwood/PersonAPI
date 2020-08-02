using Person.API.Data;
using Person.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Person.API.Domain.Services
{
    public class PersonRepository : IPersonRepository
    {
        private PeopleContext _context;

        public PersonRepository(PeopleContext context) => _context = context;

        public List<States> GetStates() => _context.States.ToList();
        public People GetPerson(long personId) => _context.People.Where(x => x.Id == personId).SingleOrDefault();
        public void SaveDetail(People person)
        {
            if (person.Id > 0)
                _context.People.Update(person);
            else
                _context.People.Add(person);
            _context.SaveChanges();
        }

    public List<PersonSearchResultsModel> SearchPeople(PersonSearchModel searchRequest)
        {
            var results =
                    (from p in _context.People
                     join s in _context.States on p.StateId equals s.Id into ps
                     from s in ps.DefaultIfEmpty()
                     join bs in _context.States on p.BirthStateId equals bs.Id into pbs
                     from bs in pbs.DefaultIfEmpty()
                     where ((string.IsNullOrWhiteSpace(searchRequest.FirstName) || p.FirstName.Contains(searchRequest.FirstName)) &&
                     (string.IsNullOrWhiteSpace(searchRequest.LastName) || p.LastName.Contains(searchRequest.LastName)) &&
                     (searchRequest.BirthStateId == null || p.BirthStateId == searchRequest.BirthStateId) &&
                     (searchRequest.StateId == null || p.StateId == searchRequest.StateId)
                     )
                     select new PersonSearchResultsModel
                     {
                         Id = p.Id,
                         FirstName = p.FirstName,
                         LastName = p.LastName,
                         DateOfBirth = p.DateOfBirth,
                         City = p.City,
                         State = s.Name,
                         BirthCity = p.BirthCity,
                         BirthState = bs.Name,
                     })
                    .OrderByDescending(a => a.Id)
                    .Take(1000)
                    .ToList();

            return results;
        }

    }
}
