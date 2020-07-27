using AutoMapper;
using Person.API.Data;
using Person.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Person.API.Domain.Profiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<PeopleModel, People>();
            CreateMap<People, PeopleModel>();
        }
    }
}
