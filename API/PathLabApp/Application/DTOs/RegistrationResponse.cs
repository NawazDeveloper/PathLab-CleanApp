namespace pathLab.Application.DTOs
{
    public class RegistrationResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int? UserId { get; set; }
        public string Role { get; set; }

    }
}
