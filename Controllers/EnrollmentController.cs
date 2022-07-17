using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UOW_101.Models;
using UOW_101.UnitOfWorks;

namespace UOW_101.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnrollmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public EnrollmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _unitOfWork.Enrollment.All();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEnrollment(Guid id)
        {
            var item = await _unitOfWork.Enrollment.GetById(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEnrollment(Enrollment Enrollment)
        {
            if (ModelState.IsValid)
            {
                Enrollment.Id = Guid.NewGuid();

                await _unitOfWork.Enrollment.Upsert(Enrollment);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetEnrollment", new { Enrollment.Id }, Enrollment);
            }

            return new JsonResult("Somethign Went wrong") { StatusCode = 500 };
        }


        [HttpPut]
        public async Task<IActionResult> UpdateEnrollment(Enrollment Enrollment)
        {
            if (ModelState.IsValid)
            {
                Enrollment.Id = Guid.NewGuid();

                await _unitOfWork.Enrollment.Update(Enrollment);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetEnrollment", new { Enrollment.Id }, Enrollment);
            }

            return new JsonResult("Somethign Went wrong") { StatusCode = 500 };
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnrollment(Guid id)
        {
            var item = await _unitOfWork.Enrollment.GetById(id);

            if (item == null)
                return BadRequest();

            await _unitOfWork.Enrollment.Delete(id);
            await _unitOfWork.CompleteAsync();

            return Ok(item);
        }
    }
}
