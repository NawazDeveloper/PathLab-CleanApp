using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClarmindsApp.Entities
{
    public class Doctor : CommonData
    {
        [Key] 
        public int DoctorId { get; set; }
        
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime Dob { get; set; }
        [Required, MaxLength(200)] 
        public string Education { get; set; }

        public int DesignationId { get; set; }
        public Designation Designation { get; set; }



       
    }
}
