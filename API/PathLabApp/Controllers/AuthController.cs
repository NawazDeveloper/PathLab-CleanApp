using ClarmindsApp.DTOs;
using ClarmindsApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ClarmindsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserAuth _userAuth;

        public AuthController(IUserAuth userAuth)
        {
            _userAuth = userAuth;
        }
       // admin register
        //[HttpPost("admin-register")]
        // public async Task<IActionResult> AdminRegister([FromBody] RegisterationRequest request)
        // {
        //     var result = await _userAuth.RegisterAsync(request, "Admin");
        //     if (result.Contains("success", StringComparison.OrdinalIgnoreCase))
        //         return Ok(result);
        //     return BadRequest(result);
        // }


        // doctor register
        [Authorize(Roles = "Admin")]
        [HttpPost("doctor-register")]

        public async Task<IActionResult> DoctorRegister([FromBody] RegisterationRequest request)
        {
            try
            {
                var result = await _userAuth.RegisterAsync(request, "Doctor");

                if (result.Success)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RegistrationResponse
                {
                    Success = false,
                    Message = $"An error occurred while registering doctor: {ex.Message}"
                });
            }
        }

        //pharmacy register
        [Authorize(Roles = "Admin")]
        [HttpPost("pharmacy-register")]
        public async Task<IActionResult> PharmacyRegister([FromBody] RegisterationRequest request)
        {
            try
            {
                var result = await _userAuth.RegisterAsync(request, "Pharmacy");

                if (result.Success)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new RegistrationResponse
                {
                    Success = false,
                    Message = $"An error occurred while registering pharmacy: {ex.Message}"
                });
            }
        }


        // admin login
        [HttpPost("admin-login")]
        public async Task<IActionResult> AdminLogin([FromBody] LoginRequest request)
        {
            return await RoleBasedLogin(request, "Admin");
        }


        // doctor  login

        [HttpPost("doctor-login")]
        public async Task<IActionResult> DoctorLogin([FromBody] LoginRequest request)
        {
            return await RoleBasedLogin(request, "Doctor");
        }

        // pharmacy login
        [HttpPost("pharmacy-login")]
        public async Task<IActionResult> PharmacyLogin([FromBody] LoginRequest request)
        {
            return await RoleBasedLogin(request, "Pharmacy");
        }

        //  check role to login time
        private async Task<IActionResult> RoleBasedLogin(LoginRequest request, string expectedRole)
        {
            var token = await _userAuth.LoginAsync(request.Username, request.Password);
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Invalid username or password.");
            }

            // Decode JWT
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var roleClaim = jwtToken.Claims.FirstOrDefault(r => r.Type == ClaimTypes.Role)?.Value;

            if (!string.Equals(roleClaim, expectedRole, StringComparison.OrdinalIgnoreCase))
            {
                return Unauthorized($"Access denied. Not {expectedRole}");
            }

            return Ok(new { Token = token, Message = "Login successful" });
        }
       
        // sb apni profile dekh sakta hai

        [HttpGet("my-profile")]
        public async Task<IActionResult> MyProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var profile = await _userAuth.GetProfileByIdAsync(userId);
            return Ok(profile);
        }

        // Admin only - sabki profile
        [Authorize(Roles = "Admin")]
        [HttpGet("all-profiles")]
        public async Task<IActionResult> GetAllProfiles()
        {
            var profiles = await _userAuth.GetAllProfilesAsync();
            return Ok(profiles);
        }
    }
}