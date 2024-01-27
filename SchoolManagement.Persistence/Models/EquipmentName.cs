using System;
using System.Collections.Generic;

namespace SchoolManagement.Persistence.Models
{
    public partial class EquipmentName
    {
        public EquipmentName()
        {
            Demands = new HashSet<Demand>();
            ItemDetails = new HashSet<ItemDetail>();
        }

        public int EquipmentNameId { get; set; }
        public int? UnitId { get; set; }
        public int? DepartmentNameId { get; set; }
        public int? SparesCategoryId { get; set; }
        public string? Name { get; set; }
        public string? Remarks { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual BaseSchoolName? DepartmentName { get; set; }
        public virtual SparesCategory? SparesCategory { get; set; }
        public virtual ICollection<Demand> Demands { get; set; }
        public virtual ICollection<ItemDetail> ItemDetails { get; set; }
    }
}
