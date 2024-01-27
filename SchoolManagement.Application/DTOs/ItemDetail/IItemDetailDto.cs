using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.ItemDetail
{
    public interface IItemDetailDto
    {
        public int ItemDetailId { get; set; }
        public int? UnitId { get; set; }
        public int? EquipmentNameId { get; set; }
        //public int? EquipmentPriority { get; set; }
        public int? EquipmentPriorityId { get; set; }

        public int? DepartmentNameId { get; set; }
        public string? PartNo { get; set; }
        public string? ImcNumber { get; set; }
        public string? SerialNo { get; set; }
        public string? Model { get; set; }
        public string? Brand { get; set; }
        public string? NameOfItem { get; set; }
        public int? ItemCategoryId { get; set; }
        public int? ItemCategoryTypeId { get; set; }
        public int? SparesCategoryId { get; set; }
        public int? ItemTypeId { get; set; }
        public string? AlternatiovePrartNo { get; set; }
        public string? MinimumStock { get; set; }
        public int? TradeId { get; set; }
        public string? Spec { get; set; }
        public string? PhotoUp { get; set; }
        public string? Remarks { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    } 
}
