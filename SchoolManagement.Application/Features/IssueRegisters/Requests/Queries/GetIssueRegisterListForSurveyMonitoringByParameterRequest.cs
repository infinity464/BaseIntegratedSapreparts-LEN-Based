using MediatR;
using SchoolManagement.Application.DTOs.IssueRegister;

namespace SchoolManagement.Application.Features.IssueRegisters.Requests.Queries
{

    public class GetIssueRegisterListForSurveyMonitoringByParameterRequest : IRequest<object>
    {
        public int UnitId { get; set; }
        public int DepartmentNameId { get; set; }
        public int ItemCategoryId { get; set; }
        public int EquipmentNameId { get; set; }
    }
}   
       