using System.ComponentModel.DataAnnotations;

namespace ClarmindsApp.Entities
{
    public class Role
    {
        [Key]
           
        public int RoleId { get; set; }
        [Required, MaxLength(50)]
        public string RoleName { get; set; } 
           

            // Navigation
            public ICollection<UserRole> UserRoles { get; set; }
        
    }
}
