using Azure.Core;
using ClarmindsApp.DTOs;
using ClarmindsApp.Entities;
using ClarmindsApp.Interfaces;
using ClarmindsApp.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClarmindsApp.Services
{
    public class UserAuthService : IUserAuth
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UserAuthService(ApplicationDbContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        // REGISTER
        public async Task<RegistrationResponse> RegisterAsync(RegisterationRequest request, string roleName)
        {
            if (request == null)
                return new RegistrationResponse { Success = false, Message = "Invalid payload" };

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == roleName);
            if (role == null)
                return new RegistrationResponse { Success = false, Message = "Role not found" };

           
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
                return new RegistrationResponse { Success = false, Message = "Email already registered" };

           
            if (request.Password != request.ConfirmPassword)
                return new RegistrationResponse { Success = false, Message = "Passwords do not match" };

           
            var user = new User
            {
                FirstName = request.FirstName.Trim(),
                LastName = request.LastName.Trim(),
                Email = request.Email.Trim(),
                PhoneNumber = request.PhoneNumber?.Trim(),
                PasswordHash = HashPassword(request.Password),
                CreatedAt = DateTime.UtcNow,
                IsActive = true,
                GenderId = request.GenderId
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

         
            _context.UserRoles.Add(new UserRole
            {
                UserId = user.UserId,
                RoleId = role.RoleId
            });

          
            if (roleName.ToLower() == "doctor")
            {
                if (request.DesignationId <= 0 || string.IsNullOrEmpty(request.Education))
                {
                    return new RegistrationResponse
                    {
                        Success = false,
                        Message = "Designation and Education are required for doctor"
                    };
                }

                var doctor = new Doctor
                {
                    UserId = user.UserId,
                    DesignationId = request.DesignationId,
                    Dob = request.Dob,
                    Education = request.Education
                };
                _context.Doctors.Add(doctor);
            }

            await _context.SaveChangesAsync();

            return new RegistrationResponse
            {
                Success = true,
                Message = "Registered successfully",
                UserId = user.UserId,
                Role = roleName
            };
        }


        // LOGIN -> returns JWT token string or error text
        public async Task<string> LoginAsync(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return "Invalid email or password";

            string pwdHash = HashPassword(password);

            
            var user = await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == pwdHash);

            if (user == null || !user.IsActive)
                return "Invalid email or password";

            
            await _context.SaveChangesAsync();

           
            var roleName = user.UserRoles?.FirstOrDefault()?.Role?.RoleName ?? "User";

            // Claims
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Role, roleName)
        };

            // JWT
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        
        private string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password ?? ""));
            return BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
        }



        /// get user profile by id
        public async Task<UserProfileDto> GetProfileByIdAsync(string userId)
        {
            
            var user = await _context.Users.Where(u => u.UserId.ToString() == userId)
                .Select(u => new UserProfileDto
                {
                    UserId = u.UserId,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber ,
                    

                    Role = u.UserRoles.FirstOrDefault().Role.RoleName
                })
                .FirstOrDefaultAsync();
            return user;
        }

        public Task<IEnumerable<UserProfileDto>> GetAllProfilesAsync()
        {

            return _context.Users
                .Select(u => new UserProfileDto
                {
                    UserId = u.UserId,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                   
                    Role = u.UserRoles.FirstOrDefault().Role.RoleName,
                     PhoneNumber = u.PhoneNumber,
                    
                })
                .ToListAsync()
                .ContinueWith(task => (IEnumerable<UserProfileDto>)task.Result);
        }
    }
    
}
