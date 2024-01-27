using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public class EquipmentPriority :BaseDomainEntity
    {
        public EquipmentPriority()
        {
            ItemDetails = new HashSet<ItemDetail>();
        }

        public int EquipmentPriorityId { get; set; }
        public string? Name { get; set; }
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<ItemDetail> ItemDetails { get; set; }
    }
}
