using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.MaintenanceSubCategory
{
    public class CreateMaintenanceSubCategoryDto : IMaintenanceSubCategoryDto
    {
        public int MaintenanceSubCategoryId { get; set; }
        public int? MaintenanceCategoryId { get; set; }
        public int? DepartmentNameId { get; set; }
        public string? SubCategoryName { get; set; }
        public string? AllowedExtension { get; set; }
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }
    }
}
