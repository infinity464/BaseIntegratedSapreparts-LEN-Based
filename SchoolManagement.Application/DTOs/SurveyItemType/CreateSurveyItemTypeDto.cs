namespace SchoolManagement.Application.DTOs.SurveyItemType
{
    public class CreateSurveyItemTypeDto : ISurveyItemTypeDto
    {
        public int SurveyItemTypeId { get; set; }
        public string? SurveyItemTypeName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
 