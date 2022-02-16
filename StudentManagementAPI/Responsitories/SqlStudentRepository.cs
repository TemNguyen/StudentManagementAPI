using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentManagementAPI.DataModel;
using StudentManagementAPI.Repositories;

namespace StudentManagementAPI.Responsitories
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
    }
}
