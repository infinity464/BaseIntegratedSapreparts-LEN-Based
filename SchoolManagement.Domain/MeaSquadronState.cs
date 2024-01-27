using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain
{
    public  class MeaSquadronState : BaseDomainEntity
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


        public virtual BaseSchoolName? DepartmentName { get; set; }
        //public virtual PresentState? PresentState { get; set; }

        public virtual PresentState? PresentState { get; set; }

    }
}
