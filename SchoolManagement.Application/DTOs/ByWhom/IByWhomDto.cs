namespace SchoolManagement.Application.DTOs.ByWhom
{
    public interface IByWhomDto
    {
        public int ByWhomId { get; set; }
        public string? Name { get; set; }
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }
    }
}
