using System.ComponentModel.DataAnnotations;

namespace pathLab.Domain.Entities
{
    public class CbcTest
    {
        [Key]
        public int Id { get; set; }
        public int? PatientId { get; set; }
        public string? PatientName { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }

        // CBC Test Parameters
        public decimal Hemoglobin { get; set; }
        public decimal RBC { get; set; }
        public decimal WBC { get; set; }
        public decimal Platelets { get; set; }
        public decimal Hematocrit { get; set; }
        public decimal MCV { get; set; }
        public decimal MCH { get; set; }
        public decimal MCHC { get; set; }

        public DateTime TestDate { get; set; }
    }
}
