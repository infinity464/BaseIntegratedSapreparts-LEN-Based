using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.IssueRegisters.Requests.Queries
{
    public class GetSelectedEquipmentNameForSurveyByParameterRequest : IRequest<List<SelectedModel>>
    {
        public int UnitId { get; set; }
        public int DepartmentNameId { get; set; }
    }
}
