using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ItemDetails.Requests.Queries
{
    public class GetSelectedItemNameByParametersForPreventiveRequest : IRequest<List<SelectedModel>>
    {
        public int UnitId { get; set; } 
        public int DepartmentNameId { get; set; } 
        public int ItemCategoryId { get; set; } 
    }
}   
   