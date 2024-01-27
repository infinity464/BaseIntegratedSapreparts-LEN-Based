using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.CorrectiveMaintenence
{
    public class CreateCorrectiveMaintenenceDto : ICorrectiveMaintenenceDto
    {
        public int CorrectiveMaintenenceId { get; set; }
        public int? UnitId { get; set; }
        public int? DepartmentNameId { get; set; }
        public int? EquipmentNameId { get; set; }
        public int? PriorityId { get; set; }
        public int? MaintenanceAuthorityId { get; set; }
        public int? StatusOfJobId { get; set; }
        public int? ByWhomId { get; set; }
        public DateTime? DateOfDefect { get; set; }
        public string? DescriptionOfDefect { get; set; }
        public string? WorkOrderNo { get; set; }
        public DateTime? WorkOrderDate { get; set; }
        public DateTime? WorkStartDate { get; set; }
        public string? RefDocument { get; set; }
        public int? SpareRequisitionId { get; set; }
       // public int? SpareRequisition { get; set; }
        public DateTime? WorkCompletedDate { get; set; }
        public int? CompletedStatus { get; set; }
        public bool IsActive { get; set; }
        public int? IssueRegisterId { get; set; }
        public int? ItemCategoryId { get; set; }

        //public int? IssueTypeId { get; set; }
        //public int? ItemCategoryId { get; set; }
        public int? ItemDetailId { get; set; }
        public IFormFile? Doc { get; set; }
    }
}
