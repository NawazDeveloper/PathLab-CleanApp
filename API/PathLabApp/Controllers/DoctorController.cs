using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using pathLab.Application.DTOs;
using pathLab.Application.Interfaces;

namespace pathLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctor _doctor;

        public DoctorController(IDoctor doctor)
        {
            _doctor = doctor;
        }
        // GET: api/doctor
        [Authorize(Roles = "Admin")]
        [HttpGet("all-doctor")]
        public async Task<IActionResult> GetAllDoctors()
        {
            try
            {
                var doctors = await _doctor.GetAllDoctorAsync();
                return Ok(doctors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving doctors: {ex.Message}");
            }

           
                        
        }
        // get doctor by id
        [Authorize(Roles = "Admin")]
        [HttpGet("getDoctorById/{id}")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            try
            {
                var doctor = await _doctor.GetDoctorByIdAsync(id);

                if (doctor == null)
                    return NotFound(new { message = "Doctor not found" });

                return Ok(new
                {
                    success = true,
                    data = doctor
                });
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(new { message = knfEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = $"Error retrieving doctor: {ex.Message}"
                });
            }
        }


        // delete doctor by id
        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteDoctorById/{id}")]
        public async Task<IActionResult> DeleteDoctorById(int id)
        {
            try
            {
                await _doctor.DeleteDoctorByIdAsync(id);

               
                return Ok(new
                {
                    success = true,
                    message = $"Doctor with ID {id} deleted successfully.",
                    deletedId = id
                });
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(new
                {
                    success = false,
                    message = knfEx.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    message = $"Error deleting doctor: {ex.Message}"
                });
            }
        }

        // Update doctor by Id
        [Authorize(Roles = "Admin")]
        [HttpPut("updateDoctorById/{id}")]
        public async Task<IActionResult> UpdateDoctorById(int id, [FromBody] UpdateDoctorRequestDto doctor)
        {
            if (doctor == null)
            {
                return BadRequest("Invalid doctor data.");
            }

            try
            {
                var updatedDoctor = await _doctor.UpdateDoctorAsync(id, doctor);

                if (updatedDoctor == null)
                {
                    return NotFound($"Doctor with ID {id} not found.");
                }

                return Ok(updatedDoctor);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error updating doctor: {ex.Message}");
            }
        }



    }
}
