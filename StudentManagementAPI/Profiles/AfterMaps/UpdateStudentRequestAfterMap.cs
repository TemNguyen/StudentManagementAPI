using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StudentManagementAPI.DataModel;
using StudentManagementAPI.DomainModels;
using Student = StudentManagementAPI.DataModel.Student;

namespace StudentManagementAPI.Profiles.AfterMaps
{
    public class UpdateStudentRequestAfterMap : IMappingAction<UpdateStudentRequest, DataModel.Student>
    {
        public void Process(UpdateStudentRequest source, Student destination, ResolutionContext context)
        {
            destination.Address = new DataModel.Address()
            {
                PhysicalAddress = source.PhysicalAddress,
                PostalAddress = source.PostalAddress
            };
        }
    }
}
