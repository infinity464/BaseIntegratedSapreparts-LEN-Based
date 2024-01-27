using System;
using System.Collections.Generic;

namespace SchoolManagement.Api.Models
{
    public partial class ToolsLocation
    {
        public ToolsLocation()
        {
            IssueRegisters = new HashSet<IssueRegister>();
            ItemStors = new HashSet<ItemStor>();
            PreviousItemStores = new HashSet<PreviousItemStore>();
        }

        public int ToolsLocationId { get; set; }
        public string ToolsLocationName { get; set; }
        public string Remarks { get; set; }
        public bool? Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<IssueRegister> IssueRegisters { get; set; }
        public virtual ICollection<ItemStor> ItemStors { get; set; }
        public virtual ICollection<PreviousItemStore> PreviousItemStores { get; set; }
    }
}
