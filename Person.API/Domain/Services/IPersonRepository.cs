using Person.API.Data;
using Person.API.Domain.Models;
using System.Collections.Generic;

namespace Person.API.Domain.Services
{
    public interface IPersonRepository
    {
        List<PersonSearchResultsModel> SearchPeople(PersonSearchModel searchRequest);
        List<States> GetStates();
        People GetPerson(long personId);
        void SaveDetail(People person);
    }
}