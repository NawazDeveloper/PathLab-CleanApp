namespace ClarmindsApp.DTOs
{
    public class UpdateDoctorResponseDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
      
        public string PhoneNumber { get; set; }
        public string Designation { get; set; }
        public string Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string Education { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
    }
}
