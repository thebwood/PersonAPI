using System;
using System.Collections.Generic;

namespace Person.API.Data
{
    public partial class People
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
