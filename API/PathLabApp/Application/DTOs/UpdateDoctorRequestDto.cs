namespace pathLab.Application.DTOs
{
    public class UpdateDoctorRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string Role { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Designation { get; set; }
        public DateTime? Dob { get; set; }
        public string? Education { get; set; }
        public bool IsActive { get; set; }
        public string Gender { get; set; }

    }
}
