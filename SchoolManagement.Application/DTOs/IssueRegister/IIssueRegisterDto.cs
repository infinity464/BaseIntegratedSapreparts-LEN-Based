using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs.IssueRegister
{
    public interface IIssueRegisterDto
    {
        public int? DepartmentNameId { get; set; }
        public int? ItemDetailId { get; set; }
        public int? IssueTypeId { get; set; }
        public int? IssueStatusId { get; set; }
        public int? CorrectiveCompleteStatus { get; set; }
        public int? TotalReceivedQty { get; set; }
        public DateTime? IssueDate { get; set; }
        public string? IssuedTo { get; set; }
        public string? Reason { get; set; }
        public int? AvailableQtyBeforeIssue { get; set; }
        public int? DemandCompleteStatus { get; set; }
        public int? AvailableQtyAfterIssue { get; set; }
        public string? ReceivedPerson { get; set; }
        public string? Remarks { get; set; }
        public int? Status { get; set; }
        public bool IsActive { get; set; }
    } 
}
