namespace SchoolManagement.Application.DTOs.Progres
{
    public interface IProgresDto
    {
        public int ProgresId { get; set; }
        public string? Name { get; set; }
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }
    }
}
