using System.ComponentModel.DataAnnotations;

namespace pathLab.Domain.Entities
{
    public class User : CommonData
    {
        [Key]
        public int UserId { get; set; }

        [Required, MaxLength(100)] public string FirstName { get; set; }
        [Required, MaxLength(100)] public string LastName { get; set; }

        [Required, MaxLength(200)] public string Email { get; set; }
        [MaxLength(20)] public string? PhoneNumber { get; set; }

        public int GenderId { get; set; }
        public Gender Gender { get; set; }

        [Required] public string PasswordHash { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<Address> Addresses { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }


        public Doctor? Doctor { get; set; }

    }

}
