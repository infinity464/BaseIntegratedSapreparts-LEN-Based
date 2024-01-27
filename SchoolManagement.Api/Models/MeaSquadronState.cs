using System;
using System.Collections.Generic;

namespace SchoolManagement.Api.Models
{
    public partial class MeaSquadronState
    {
        public int MeaSquadronStateId { get; set; }
        public int? DepartmentNameId { get; set; }
        public int? PresentStateId { get; set; }
        public string SerNo { get; set; }
        public string WorkOrderReceived { get; set; }
        public DateTime? WorkOrderDate { get; set; }
        public string WorkshopName { get; set; }
        public string Remarks { get; set; }
        public int? MenuPosition { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual BaseSchoolName DepartmentName { get; set; }
        public virtual PresentState PresentState { get; set; }
    }
}
