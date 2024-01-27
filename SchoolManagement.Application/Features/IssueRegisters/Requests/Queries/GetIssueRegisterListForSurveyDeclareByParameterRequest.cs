using MediatR;
using SchoolManagement.Application.DTOs.IssueRegister;

namespace SchoolManagement.Application.Features.IssueRegisters.Requests.Queries
{

    public class GetIssueRegisterListForSurveyDeclareByParameterRequest : IRequest<object>
    {
        public int UnitId { get; set; }
        public int DepartmentNameId { get; set; }
        public int ItemCategoryId { get; set; }
        public int ItemDetailId { get; set; }
    }
}   
       