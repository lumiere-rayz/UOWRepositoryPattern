using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UOW_101.Models;
using UOW_101.UnitOfWorks;

namespace UOW_101.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : Controller  
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _unitOfWork.Course.All();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourse(Guid id)
        {
            var item = await _unitOfWork.Course.GetById(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                course.Id = Guid.NewGuid();
                await _unitOfWork.Course.Add(course);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("CreateCourse", new { course.Id }, course);
            }

            return new JsonResult("Somethign Went wrong") { StatusCode = 500 };
        }

        [HttpPut]
        public async Task<IActionResult>UpdateCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                course.Id = Guid.NewGuid();
                await _unitOfWork.Course.Upsert(course);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("CreateCourse", new { course.Id }, course);
            }

            return new JsonResult("Somethign Went wrong") { StatusCode = 500 };
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            var item = await _unitOfWork.Student.GetById(id);

            if (item == null)
                return BadRequest();

            await _unitOfWork.Course.Delete(id);
            await _unitOfWork.CompleteAsync();

            return Ok(item);
        }
    }
}
