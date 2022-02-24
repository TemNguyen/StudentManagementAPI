using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using StudentManagementAPI.DomainModels;
using StudentManagementAPI.Repositories;

namespace StudentManagementAPI.Controllers
{
    [ApiController]
    public class GendersController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public GendersController(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllGenders()
        {
            var genderList = await _studentRepository.GetGendersAsync();

            if (genderList == null || !genderList.Any())
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<Gender>>(genderList));
        }
    }
}
