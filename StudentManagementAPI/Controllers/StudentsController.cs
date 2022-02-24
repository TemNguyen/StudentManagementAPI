using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using StudentManagementAPI.Repositories;
using StudentManagementAPI.DomainModels;

namespace StudentManagementAPI.Controllers
{
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly IImageRepository _imageRepository;

        public StudentsController(IStudentRepository studentRepository, IMapper mapper, IImageRepository imageRepository)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _imageRepository = imageRepository;
        }
        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetStudentsAsync();
            var domainModelStudents = _mapper.Map<List<Student>>(students);
            

            return Ok(domainModelStudents);
        }

        [HttpGet]
        [Route("[controller]/{studentId:guid}"), ActionName("GetStudentAsync")]
        public async Task<IActionResult> GetStudentAsync([FromRoute] Guid studentId)
        {
            var student = await _studentRepository.GetStudentAsync(studentId);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Student>(student));
        }

        [HttpPut]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> UpdateStudentAsync([FromRoute] Guid studentId, [FromBody] UpdateStudentRequest request)
        {
            var updatedStudent =
                await _studentRepository.UpdateStudent(studentId, _mapper.Map<DataModel.Student>(request));

            if (await _studentRepository.Exists(studentId))
            {
                return Ok(_mapper.Map<DomainModels.Student>(updatedStudent));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> DeleteStudentAsync([FromRoute] Guid studentId)
        {
            if (await _studentRepository.Exists(studentId))
            {
                var student =  await _studentRepository.DeleteStudent(studentId);
                return Ok(_mapper.Map<DomainModels.Student>(student));
            }

            return NotFound();
        }

        [HttpPost]
        [Route("[controller]/Add")]
        public async Task<IActionResult> AddStudentAsync([FromBody] AddStudentRequest request)
        {
            var student = await _studentRepository.AddStudent(_mapper.Map<DataModel.Student>(request));
            return CreatedAtAction(nameof(GetStudentAsync), new { studentId = student.Id },
                _mapper.Map<Student>(student));
        }

        [HttpPost]
        [Route("[controller]/{studentId:guid}/upload-image")]
        public async Task<IActionResult> UploadImageAsync([FromRoute] Guid studentId, IFormFile profileImage)
        {
            var validExtension = new List<string>
            {
                ".jpeg",
                ".png",
                ".gif",
                ".jpg"
            };
            if (profileImage != null && profileImage.Length > 0)
            {
                var extension = Path.GetExtension(profileImage.FileName);
                if (validExtension.Contains(extension))
                {
                    if (await _studentRepository.Exists(studentId))
                    {
                        var fileName = Guid.NewGuid() + Path.GetExtension(profileImage.FileName);
                        var fileImagePath = await _imageRepository.Upload(profileImage, fileName);
                        if (await _studentRepository.UpdateProfileImage(studentId, fileImagePath))
                        {
                            return Ok(fileImagePath);
                        }

                        return StatusCode(StatusCodes.Status500InternalServerError, "Error when uploading image");
                    }
                }

                return BadRequest("This is not a valid Image format");
            }

            

            return NotFound();
        }
    }
}
