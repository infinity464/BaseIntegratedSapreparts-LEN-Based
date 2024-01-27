namespace SchoolManagement.Application.DTOs.MeaSquadronState
{
    public interface IMeaSquadronStateDto
    {
        public int MeaSquadronStateId { get; set; }
        public int? DepartmentNameId { get; set; }
        public int? PresentStateId { get; set; }
        public string? SerNo { get; set; }
        public string? WorkOrderReceived { get; set; }
        public DateTime? WorkOrderDate { get; set; }
        public string? WorkshopName { get; set; }
        public string? Remarks { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
