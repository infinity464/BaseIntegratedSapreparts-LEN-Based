namespace SchoolManagement.Application.DTOs.SurveyAuthority
{
    public interface ISurveyAuthorityDto
    {
        public int SurveyAuthorityId { get; set; }
        public string? SurveyAuthorityName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
