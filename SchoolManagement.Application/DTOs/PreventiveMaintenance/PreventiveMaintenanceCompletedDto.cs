using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.PreventiveMaintenances
{
    public class PreventiveMaintenanceCompletedDto 
    {
        public int PreventiveMaintenanceId { get; set; }
        public int? UnitId { get; set; }
        public int? DepartmentNameId { get; set; }
        public int? IssueRegisterId { get; set; }
        public int? ItemDetailId { get; set; }
        public string? Pmsno { get; set; }
        public int? MaintenanceSubCategoryId { get; set; }
        public int? MaintenanceCategoryId { get; set; }
        public int? JobDescriptionEntryPmsId { get; set; }
        public int? MaintenanceTypeId { get; set; }
        public DateTime? CompletedDate { get; set; }
        public int? CompletedStatus { get; set; }
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }
    }
}
