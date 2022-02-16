using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StudentManagementAPI.DataModel;

namespace StudentManagementAPI.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Student, DomainModels.Student>()
                .ReverseMap();

            CreateMap<Gender, DomainModels.Gender>()
                .ReverseMap();

            CreateMap<Address, DomainModels.Address>()
                .ReverseMap();
        }
    }
}
