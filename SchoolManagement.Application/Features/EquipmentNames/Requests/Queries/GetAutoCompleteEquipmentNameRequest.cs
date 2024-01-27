using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ItemDetails.Requests.Queries
{
    public class GetAutoCompleteEquipmentNameRequest : IRequest<List<SelectedModel>>
    {  
        public string Name { get; set; } 
        //public int DepartmentNameId { get; set; } 
        //public int UnitId { get; set; } 
        //public int ItemCategoryId { get; set; } 
    } 
}
   