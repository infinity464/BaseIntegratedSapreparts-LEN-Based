using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.PreventiveMaintenances.Requests.Queries
{
    public class GetSelectSurveyByUnitAndDepartmentIdRequest : IRequest<List<SelectedModel>>
    {
        public int UnitId { get; set; }
        public int DepartmentNameId { get; set; }
        public int ItemCategoryId { get; set; }
        public int DemandCompleteStatus { get; set; } 
       // public int SurveyAuthorityId { get; set; }  

    }
}  
  