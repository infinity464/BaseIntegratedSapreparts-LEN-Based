namespace SchoolManagement.Application.DTOs.Opinion
{
    public interface IOpinionDto
    {
        public int OpinionId { get; set; }
        public string? Name { get; set; }
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }
    }
}
