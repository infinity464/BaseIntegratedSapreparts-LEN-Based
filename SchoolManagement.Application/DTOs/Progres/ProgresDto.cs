namespace SchoolManagement.Application.DTOs.Progres
{
    public class ProgresDto : IProgresDto
    {
        public int ProgresId { get; set; }
        public string? Name { get; set; }
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }
    }
}
