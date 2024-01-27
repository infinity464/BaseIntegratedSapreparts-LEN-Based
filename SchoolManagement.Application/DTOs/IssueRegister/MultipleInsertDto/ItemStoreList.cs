using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.IssueRegister.MultipleInsertDto
{
    public class ItemStoreList 
    { 
        public string? Deno { get; set; }
        public string? ItemDetail { get; set; }
        public string? PartNo { get; set; }
        public string? ItemSerNo { get; set; }
        public int? IssuedQty { get; set; }
        public int? SurveyStatus { get; set; }
        public string? SurveyNumber { get; set; }
        public DateTime? SurveyDate { get; set; }
        public int? SurveyAuthorityId { get; set; }
        public int? SurveyItemTypeId { get; set; }
        public string? ByWhom { get; set; }
        public int? Status { get; set; }
        public int? ItemDetailId { get; set; }
        public int? IssueTypeId { get; set; }
        public int? ItemStorId { get; set; }
        public int? DepartmentNameId { get; set; }
        public int? SparesCategoryId { get; set; }
        public bool IsRefundable { get; set; }
        public int? TotalReceivedQty { get; set; }
        public int? DemandCompleteStatus { get; set; }
        public int? ToolsLocationId { get; set; }
        public bool? IsChecked { get; set; }
        public int? AvailableQty { get; set; }
        public int? ReturnQty { get; set; }
        public DateTime? ItemReceivedDate { get; set; }
        public string? NameOfItem { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
        public DateTime? DateOfExpiry { get; set; }
        public object? AcctStore { get; set; }

    }
}
