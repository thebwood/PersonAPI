using System;
using System.Collections.Generic;

namespace Person.API.Data
{
    public partial class Owners
    {
        public long Id { get; set; }
        public long? PersonId { get; set; }
    }
}
