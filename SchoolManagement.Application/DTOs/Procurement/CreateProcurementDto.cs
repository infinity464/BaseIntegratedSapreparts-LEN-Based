using Microsoft.AspNetCore.Http;

namespace SchoolManagement.Application.DTOs.Procurement
{
    public class CreateProcurementDto : IProcurementDto
    {
        public int ProcurementId { get; set; }
        public int? UnitId { get; set; }
        public int? DemandId { get; set; }
        public int? ItemDetailId { get; set; }
        public int? ProcurementStatusId { get; set; }
        public int? ItemCategoryId { get; set; }
        public int? EquipmentNameId { get; set; }
        public int? PrincipalNameId { get; set; }
        public int? ManufactureId { get; set; }
        public int? CstTecId { get; set; }
        public int? LocalAgentId { get; set; }
        public int? ProcurementTypeId { get; set; }
        public int? SupplierId { get; set; }
        public int? SupplierAId { get; set; }
        public int? SupplierMId { get; set; }
        public int? PartOfShipmentId { get; set; }
        public int? DepartmentNameId { get; set; }
        public int? SparesCategoryId { get; set; }
        public string? TenderNumber { get; set; }
        public DateTime? WorkOrderDate { get; set; }
        public DateTime? DateOfTenderFloat { get; set; }
        public DateTime? TenderopeningDate { get; set; }
        public DateTime? TenderPublishDate { get; set; }
        public string? TenderNotice { get; set; }
        public string? TenderSpecification { get; set; }
        public string? FinancialApproval { get; set; }
        public string? WorkOrder { get; set; }
        public DateTime? DateOfDelivery { get; set; }
        public string? UnitPrice { get; set; }
        public string? Qty { get; set; }
        public int? SftQty { get; set; }
        public int? ProcurementCompleteStatus { get; set; }
        public string? Remarks { get; set; }
        public string? ProcurementDocument { get; set; }
        public int? Status { get; set; }
        public int? MenuPosition { get; set; }
        public string? LpRequisitionNo { get; set; }
        public bool IsActive { get; set; }

        public IFormFile? Doc { get; set; }
    }
}
