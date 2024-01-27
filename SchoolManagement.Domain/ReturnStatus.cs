using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public class ReturnStatus : BaseDomainEntity
    {
        public ReturnStatus()
        {
            ReturnLogs = new HashSet<ReturnLog>();
        }

        public int ReturnStatusId { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<ReturnLog> ReturnLogs { get; set; }
    }
}
