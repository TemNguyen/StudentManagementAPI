using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentManagementAPI.DataModel;

namespace StudentManagementAPI.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudentsAsync();
        Task<Student> GetStudentAsync(Guid studentId);

        Task<List<Gender>> GetGendersAsync();
        Task<bool> Exists(Guid studentId);
        Task<Student> UpdateStudent(Guid studentId, Student request);
        Task<Student> DeleteStudent(Guid studentId);
        Task<Student> AddStudent(Student request);
        Task<bool> UpdateProfileImage(Guid studentId, string profileNameUrl);
    }
}
