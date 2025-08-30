using System.ComponentModel.DataAnnotations;

namespace pathLab.Domain.Entities
{
    public class Gender
    {
        [Key]
        public int GenderId { get; set; }
        [Required, MaxLength(50)]
        public string GenderName { get; set; }
    }
}
