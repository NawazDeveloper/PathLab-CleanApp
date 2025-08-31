namespace pathLab.Application.DTOs.RequestDto
{
    public class CreateCbcTestDto
    {
        public string PatientName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }

        public decimal Hemoglobin { get; set; }
        public decimal RBC { get; set; }
        public decimal WBC { get; set; }
        public decimal Platelets { get; set; }
        public decimal Hematocrit { get; set; }
        public decimal MCV { get; set; }
        public decimal MCH { get; set; }
        public decimal MCHC { get; set; }
    }
}
