using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.IssueRegister
{
    public class AddReturnLogToStoreDto
    {
        public int ReturnLogId { get; set; }
        public int? IssueRegisterId { get; set; }
        public int? ReturnStatusId { get; set; }
        public int? IssueQty { get; set; } 
        public int? ReturnQtyUpdate { get; set; }
        public int? ReceiveQty { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string? ReturnBy { get; set; }
        public bool IsActive { get; set; }
    }
}
