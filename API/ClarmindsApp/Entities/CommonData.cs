namespace ClarmindsApp.Entities
{
    public class CommonData
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }     
        public int? UpdatedBy { get; set; }
    }
}
