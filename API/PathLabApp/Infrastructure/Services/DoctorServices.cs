
using Microsoft.EntityFrameworkCore;
using pathLab.Application.DTOs;
using pathLab.Application.Interfaces;
using pathLab.Infrastructure.Repositories;

namespace pathLab.Infrastructure.Services
{
    public class DoctorServices : IDoctor
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public DoctorServices(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        // delete doctor by id 
        public Task DeleteDoctorByIdAsync(int id)
        {
            var doctor = _context.Users
                  .Include(u => u.UserRoles)
                  .ThenInclude(ur => ur.Role)
                  .FirstOrDefaultAsync(u => u.UserId == id && u.UserRoles.Any(ur => ur.Role.RoleName == "Doctor"));
            if (doctor == null)
            {
                throw new KeyNotFoundException("Doctor not found");
            }
            _context.Users.Remove(doctor.Result);
            return _context.SaveChangesAsync();
        }

        // Get  doctor by Id
        public async Task<DoctorProfileDto?> GetDoctorByIdAsync(int id)
        {
            var doctor = await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .Include(u => u.Doctor)
                    .ThenInclude(d => d.Designation)
                .Where(u => u.UserId == id && u.UserRoles.Any(ur => ur.Role.RoleName == "Doctor"))
                .Select(u => new DoctorProfileDto
                {
                    UserId = u.UserId,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Role = u.UserRoles.FirstOrDefault().Role.RoleName,
                    IsActive = u.IsActive,


                    Gender = u.GenderId != null ? u.GenderId.ToString() : null,



                    Designation = u.Doctor != null ? u.Doctor.Designation.DesignationName : null,
                    Education = u.Doctor != null ? u.Doctor.Education : null,
                    Dob = u.Doctor != null ? u.Doctor.Dob : null
                })
                .FirstOrDefaultAsync();

            return doctor;
        }


        // get all doctors
        public async Task<List<DoctorProfileDto>> GetAllDoctorAsync()
        {
            var doctors = await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .Include(u => u.Doctor)
                    .ThenInclude(d => d.Designation)
                .Where(u => u.UserRoles.Any(ur => ur.Role.RoleName == "Doctor"))
                .Select(u => new DoctorProfileDto
                {
                    UserId = u.UserId,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,

                    Role = u.UserRoles.FirstOrDefault().Role.RoleName,
                    IsActive = u.IsActive,



                    Gender = u.GenderId != null ? u.GenderId.ToString() : null,



                    Designation = u.Doctor != null ? u.Doctor.Designation.DesignationName : null,
                    Education = u.Doctor != null ? u.Doctor.Education : null,
                    Dob = u.Doctor != null ? u.Doctor.Dob : null,


                })
                .ToListAsync();

            return doctors;
        }



        // Update doctor by id 

        public async Task<UpdateDoctorResponseDto?> UpdateDoctorAsync(int id, UpdateDoctorRequestDto updateDoctor)
        {
            var doctorToUpdate = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.UserId == id && u.UserRoles.Any(ur => ur.Role.RoleName == "Doctor"));

            if (doctorToUpdate == null)
            {
                return null;
            }


            doctorToUpdate.FirstName = updateDoctor.FirstName ?? doctorToUpdate.FirstName;
            doctorToUpdate.LastName = updateDoctor.LastName ?? doctorToUpdate.LastName;
            doctorToUpdate.Email = updateDoctor.Email ?? doctorToUpdate.Email;

            doctorToUpdate.PhoneNumber = updateDoctor.PhoneNumber ?? doctorToUpdate.PhoneNumber;

            doctorToUpdate.IsActive = updateDoctor.IsActive;

            _context.Users.Update(doctorToUpdate);
            await _context.SaveChangesAsync();

            return new UpdateDoctorResponseDto
            {
                UserId = doctorToUpdate.UserId,
                FirstName = doctorToUpdate.FirstName,
                LastName = doctorToUpdate.LastName,
                Email = doctorToUpdate.Email,

                PhoneNumber = doctorToUpdate.PhoneNumber,

                Role = doctorToUpdate.UserRoles.FirstOrDefault()?.Role?.RoleName,
                IsActive = doctorToUpdate.IsActive
            };
        }



    }
}
