using Person.API.Data;
using Person.API.Domain.Models;
using System.Collections.Generic;

namespace Person.API.Domain.Services
{
    public interface IPersonService
    {
        List<PersonSearchResultsModel> SearchPeople(PersonSearchModel searchRequest);
        List<States> GetStates();
        People GetPerson(long personId);
        List<string> SaveDetail(PeopleModel person);
    }
}