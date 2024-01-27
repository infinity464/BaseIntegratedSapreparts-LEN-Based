using MediatR;
using SchoolManagement.Application.DTOs.IssueRegister;

namespace SchoolManagement.Application.Features.IssueRegisters.Requests.Queries
{

    public class GetIssueRegisterListForEquipmentNonOparatonalByParameterRequest : IRequest<object>
    {
        public int UnitId { get; set; }
        public int DepartmentNameId { get; set; }
    }
}   
       