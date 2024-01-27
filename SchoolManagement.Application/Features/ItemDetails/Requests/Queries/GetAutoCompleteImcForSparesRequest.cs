using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ItemDetails.Requests.Queries
{
    public class GetAutoCompleteImcForSparesRequest : IRequest<List<SelectedModel>>
    { 
        public string Imc { get; set; } 
        public int DepartmentNameId { get; set; } 
        public int UnitId { get; set; }  
        public int ItemCategoryId { get; set; } 
    }
}
   