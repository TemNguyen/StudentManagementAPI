using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StudentManagementAPI.DomainModels;
using Student = StudentManagementAPI.DataModel.Student;

namespace StudentManagementAPI.Profiles.AfterMaps
{
    public class AddStudentRequestAfterMap : IMappingAction<AddStudentRequest, DataModel.Student>
    {
        public void Process(AddStudentRequest source, Student destination, ResolutionContext context)
        {
            destination.Id = Guid.NewGuid();
            destination.Address = new DataModel.Address()
            {
                Id = Guid.NewGuid(),
                PhysicalAddress = source.PhysicalAddress,
                PostalAddress = source.PostalAddress
            };
        }
    }
}
