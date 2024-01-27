using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.IssueRegister
{
    public class AddReturnQtyToStoreDto
    { 
        public int IssueRegisterId { get; set; }
        public string? Deno { get; set; }
        public string? ItemDetail { get; set; }
        public string? PartNo { get; set; }
        public string? Part { get; set; }
        public string? StoreQty { get; set; }
        public int? CorrectiveCompleteStatus { get; set; }
        public int? DemandCompleteStatus { get; set; }
        public string? ItemSerNo { get; set; }
        public string? IssuedQty { get; set; }
        public int? ToolsLocationId { get; set; }
        public object? Status { get; set; }
        public int? ItemDetailId { get; set; }
        public int? ItemStoreId { get; set; }
        public int? DepartmentNameId { get; set; }
        public int? ReturnQty { get; set; }
        public int? ReturnQtyUpdate { get; set; }
        public object? SparesCategoryId { get; set; }
        public bool IsRefundable { get; set; }
        public bool IsChecked { get; set; }
        public int? TotalReceivedQty { get; set; }
        public string AvailableQty { get; set; }
        public string? ItemReceivedDate { get; set; }
        public int? IssueTypeId { get; set; }
        public string? NameOfItem { get; set; }
        public object? WarrantyEndDate { get; set; }
        public string? DateOfExpiry { get; set; }
        public string? AcctStore { get; set; }
        public string? ReturnBy { get; set; }
        public int? IssueQty { get; set; }
        public int? SurveyStatus { get; set; }
        public string? SurveyNumber { get; set; }
        public DateTime? SurveyDate { get; set; }
        public int? SurveyAuthorityId { get; set; }
        public int? SurveyItemTypeId { get; set; }
        public string? ByWhom { get; set; }
        public DateTime? IssueDate { get; set; }
        public string? ItemName { get; set; }
        public string? ReturnDate { get; set; }
    }
}
