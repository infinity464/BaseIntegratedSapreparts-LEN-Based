using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.ItemDetail;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.ItemDetails.Requests.Queries
{
    public class GetItemDetailListByDepartmentIdUnitIdAndItemCategoryIdRequest : IRequest<PagedResult<ItemDetailDto>>
    {
        public QueryParams QueryParams { get; set; }
        public int DepartmentNameId { get; set; }
        public int UnitId { get; set; } 
        public int ItemCategoryId { get; set; } 
    } 
}
  
  