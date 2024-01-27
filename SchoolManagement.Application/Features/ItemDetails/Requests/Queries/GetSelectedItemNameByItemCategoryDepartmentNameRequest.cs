using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ItemDetails.Requests.Queries
{
    public class GetSelectedItemNameByItemCategoryDepartmentNameRequest : IRequest<List<SelectedModel>>
    {
        public int DepartmentNameId { get; set; } 
    }
}   
   