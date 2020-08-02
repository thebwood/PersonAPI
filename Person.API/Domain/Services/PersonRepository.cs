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
                     //searchRequest.MovieGenreIds.Contains(m.MovieGenresId.Value) &&
                     //(m.MovieGenresId.HasValue && searchRequest.MovieGenreIds.Contains(m.MovieGenresId.Value)) &&
                     //(searchRequest.MovieRatingIds.Contains(m.MovieRatingsId))
                     )
                     select new PersonSearchResultsModel
                     {
                         Id = p.Id,
                         FirstName = p.FirstName,
                         LastName = p.LastName,
                         State = s.Name,
                         BirthState = bs.Name,
                     })
                    .OrderByDescending(a => a.Id)
                    .Take(1000)
                    .ToList();

            return results;
        }

    }
}
