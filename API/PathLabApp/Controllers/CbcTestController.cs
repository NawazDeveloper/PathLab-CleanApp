using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pathLab.Application.DTOs;
using pathLab.Application.Interfaces;

namespace pathLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CbcTestController : ControllerBase
    {

        private readonly ICbcService _service;

        public CbcTestController(ICbcService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tests = await _service.GetAllAsync();
            return Ok(tests);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var test = await _service.GetByIdAsync(id);
            if (test == null) return NotFound();
            return Ok(test);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CbcTestDto dto)
        {
            await _service.AddAsync(dto);
            return Ok("CBC Test Added Successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CbcTestDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return Ok("CBC Test Updated Successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok("CBC Test Deleted Successfully");
        }
    }
}

