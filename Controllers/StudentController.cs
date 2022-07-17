using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UOW_101.Models;
using UOW_101.UnitOfWorks;

namespace UOW_101.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        
        private readonly IUnitOfWork _unitOfWork;

        public StudentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _unitOfWork.Student.All();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(Guid id)
        {
            var item = await _unitOfWork.Student.GetById(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                student.Id = Guid.NewGuid();

                await _unitOfWork.Student.Add(student);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetStudent", new { student.Id }, student);
            }

            return new JsonResult("Somethign Went wrong") { StatusCode = 500 };
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                student.Id = Guid.NewGuid();

                await _unitOfWork.Student.Upsert(student);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetStudent", new { student.Id }, student);
            }

            return new JsonResult("Somethign Went wrong") { StatusCode = 500 };
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var item = await _unitOfWork.Student.GetById(id);

            if (item == null)
                return BadRequest();

            await _unitOfWork.Student.Delete(id);
            await _unitOfWork.CompleteAsync();

            return Ok(item);
        }
    }
}
