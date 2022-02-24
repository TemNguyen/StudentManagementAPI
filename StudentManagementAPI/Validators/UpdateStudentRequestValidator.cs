using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using StudentManagementAPI.DomainModels;
using StudentManagementAPI.Repositories;

namespace StudentManagementAPI.Validators
{
    public class UpdateStudentRequestValidator : AbstractValidator<UpdateStudentRequest>
    {
        public UpdateStudentRequestValidator(IStudentRepository studentRepository)
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.DateOfBirth).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Mobile).NotEmpty().LessThan(1000000000).GreaterThan(99999);
            RuleFor(x => x.GenderId).NotEmpty().Must(id =>
            {
                var gender = studentRepository.GetGendersAsync().Result.ToList().FirstOrDefault(x
                    => x.Id == id);

                if (gender != null)
                {
                    return true;
                }

                return false;
            }).WithMessage("Please enter a valid Gender");
            RuleFor(x => x.PhysicalAddress).NotEmpty();
            RuleFor(x => x.PostalAddress).NotEmpty();

        }
    }
}
