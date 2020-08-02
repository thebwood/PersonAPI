using Person.API.Domain.Models;
using System.Collections.Generic;

namespace Person.API.Domain.Services
{
    public interface IPersonService
    {
        List<PersonSearchResultsModel> SearchPeople(PersonSearchModel searchRequest);
    }
}