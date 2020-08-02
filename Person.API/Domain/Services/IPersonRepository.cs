using Person.API.Domain.Models;
using System.Collections.Generic;

namespace Person.API.Domain.Services
{
    public interface IPersonRepository
    {
        List<PersonSearchResultsModel> SearchPeople(PersonSearchModel searchRequest);
    }
}