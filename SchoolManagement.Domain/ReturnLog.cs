using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public class ReturnLog: BaseDomainEntity
    {
        public int ReturnLogId { get; set; }
        public int? IssueRegisterId { get; set; }
        public int? ReturnStatusId { get; set; }
        public int? IssueQty { get; set; }
        public int? ReceiveQty { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string? ReturnBy { get; set; }
        public bool IsActive { get; set; }

        public virtual IssueRegister? IssueRegister { get; set; }
        public virtual ReturnStatus? ReturnStatus { get; set; }
    }
}
