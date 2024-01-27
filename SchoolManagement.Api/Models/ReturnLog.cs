using System;
using System.Collections.Generic;

namespace SchoolManagement.Api.Models
{
    public partial class ReturnLog
    {
        public int ReturnLogId { get; set; }
        public int? IssueRegisterId { get; set; }
        public int? ReturnStatusId { get; set; }
        public int? IssueQty { get; set; }
        public int? ReceiveQty { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string ReturnBy { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual IssueRegister IssueRegister { get; set; }
        public virtual ReturnStatus ReturnStatus { get; set; }
    }
}
