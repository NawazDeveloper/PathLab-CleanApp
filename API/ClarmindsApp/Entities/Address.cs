using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClarmindsApp.Entities
{
    public class Address : CommonData
    {
        [Key]
        public int AddressId { get; set; }

        [Required, MaxLength(200)] 
        public string Street { get; set; }
        [MaxLength(100)]
        public string? City { get; set; }
        [MaxLength(100)] 
        public string? State { get; set; }
        [MaxLength(100)] 
        public string? Country { get; set; }
        [MaxLength(20)] 
        public string? ZipCode { get; set; }

        public bool IsPrimary { get; set; } = false;

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
