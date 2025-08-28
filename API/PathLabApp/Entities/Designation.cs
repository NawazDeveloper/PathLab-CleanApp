using System.ComponentModel.DataAnnotations;

namespace ClarmindsApp.Entities
{
    public class Designation
    {
        [Key] 
        public int DesignationId { get; set; }
        [Required, MaxLength(100)] 
        public string DesignationName { get; set; }
    }
}
