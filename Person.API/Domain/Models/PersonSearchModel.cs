using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Person.API.Domain.Models
{
    public class PersonSearchModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? BirthStateId { get; set; }
        public int? StateId { get; set; }

    }
}
