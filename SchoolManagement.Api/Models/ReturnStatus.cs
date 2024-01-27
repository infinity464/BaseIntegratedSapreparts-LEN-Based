using System;
using System.Collections.Generic;

namespace SchoolManagement.Api.Models
{
    public partial class ReturnStatus
    {
        public ReturnStatus()
        {
            ReturnLogs = new HashSet<ReturnLog>();
        }

        public int ReturnStatusId { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<ReturnLog> ReturnLogs { get; set; }
    }
}
