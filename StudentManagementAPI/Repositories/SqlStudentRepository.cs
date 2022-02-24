using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentManagementAPI.DataModel;

namespace StudentManagementAPI.Repositories
{
    public class SqlStudentRepository : IStudentRepository
    {
        private readonly StudentManagementContext _context;

        public SqlStudentRepository(StudentManagementContext _context)
        {
            this._context = _context;
        }
        public async Task<List<Student>> GetStudentsAsync()
        {
            return await _context.Students.Include(nameof(Gender)).Include(nameof(Address)).ToListAsync();
        }

        public async Task<Student> GetStudentAsync(Guid studentid)
        {
            return await _context.Students.Include(nameof(Gender)).Include(nameof(Address)).FirstOrDefaultAsync(s =>
                s.Id == studentid);
        }

        public async Task<List<Gender>> GetGendersAsync()
        {
            return await _context.Genders.ToListAsync();
        }

        public async Task<bool> Exists(Guid studentId)
        {
            return await _context.Students.AnyAsync(s => s.Id == studentId);
        }

        public async Task<Student> UpdateStudent(Guid studentId, Student request)
        {
            var existingStudent = await GetStudentAsync(studentId);
            if (existingStudent != null)
            {
                existingStudent.FirstName = request.FirstName;
                existingStudent.LastName = request.LastName;
                existingStudent.DateOfBirth = request.DateOfBirth;
                existingStudent.Email = request.Email;
                existingStudent.Mobile = request.Mobile;
                existingStudent.GenderId = request.GenderId;
                existingStudent.Address.PhysicalAddress = request.Address.PhysicalAddress;
                existingStudent.Address.PostalAddress = request.Address.PostalAddress;

                await _context.SaveChangesAsync();
                return existingStudent;
            }

            return null;
        }

        public async Task<Student> DeleteStudent(Guid studentId)
        {
            var student = await GetStudentAsync(studentId);

            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                return student;
            }

            return null;
        }

        public async Task<Student> AddStudent(Student request)
        {
           var student = await _context.Students.AddAsync(request);
           await _context.SaveChangesAsync();
           return student.Entity;
        }

        public async Task<bool> UpdateProfileImage(Guid studentId, string profileImageUrl)
        {
            var student = await GetStudentAsync(studentId);

            if (student != null)
            {
                student.ProfileImageUrl = profileImageUrl;
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
