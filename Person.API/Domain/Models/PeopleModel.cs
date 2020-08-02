using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Person.API.Domain.Models
{
    public class PeopleModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string BirthCity { get; set; }
        public int BirthStateId { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
    }
}
