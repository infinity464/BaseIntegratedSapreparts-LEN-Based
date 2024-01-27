using MediatR;
using SchoolManagement.Application.DTOs.IssueRegister;

namespace SchoolManagement.Application.Features.IssueRegisters.Requests.Queries
{

    public class GetIssueRegisterListByParameterRequest : IRequest<List<IssueRegisterDto>>
    {
        public int UnitId { get; set; }
        public int DepartmentNameId { get; set; }
        public int ItemCategoryId { get; set; }
        public int IssueTypeId { get; set; }
    }
}   
       