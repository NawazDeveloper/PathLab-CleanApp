using pathLab.Application.DTOs;

namespace pathLab.Application.Interfaces
{
    public interface IDoctor
    {
        // Get all doctor names
        Task<List<DoctorProfileDto>> GetAllDoctorAsync();

        // Get a single doctor by ID
        Task<DoctorProfileDto> GetDoctorByIdAsync(int id);

        // Update doctor information
        Task<UpdateDoctorResponseDto> UpdateDoctorAsync(int id, UpdateDoctorRequestDto updateDoctor);

        // Delete a doctor by ID
        Task DeleteDoctorByIdAsync(int id);
    }
}
