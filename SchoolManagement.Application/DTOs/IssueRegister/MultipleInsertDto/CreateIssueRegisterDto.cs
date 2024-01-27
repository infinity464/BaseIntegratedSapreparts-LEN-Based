namespace SchoolManagement.Application.DTOs.IssueRegister.MultipleInsertDto
{
    public class CreateIssueRegisterDto : IIssueRegisterDto
    {
        public int? UnitId { get; set; }
        public int? DepartmentNameId { get; set; }
        public int? EquipmentNameId { get; set; } 
        public int? ItemCategoryId { get; set; }
        public DateTime? IssueDate { get; set; }
        public string? IssuedTo { get; set; }
        public string? IssuedBy { get; set; }
        public string? Reason { get; set; }
        public int? CorrectiveCompleteStatus { get; set; }
        public int? PreventiveCompleteStatus { get; set; }
        public int? ScheduleCompleteStatus { get; set; }
        public int? DemandCompleteStatus { get; set; }
        public int? IssueRegisterId { get; set; }
        public int? SparesCategoryId { get; set; }
        public int? ItemDetailId { get; set; }
        public int? IssueTypeId { get; set; }
        public int? IssueStatusId { get; set; }
        public int? TotalReceivedQty { get; set; }
        public int? SurveyStatus { get; set; }
        public int? TrainingCrewId { get; set; }
        public string? Remarks { get; set; }
        public int? AvailableQtyBeforeIssue { get; set; }
        public int? AvailableQtyAfterIssue { get; set; }
        public string? ReceivedPerson { get; set; }
        public int? Status { get; set; }
        public bool IsActive { get; set; }
        public List<ItemStoreList>? ItemStoreList { get; set; }
    }
}
