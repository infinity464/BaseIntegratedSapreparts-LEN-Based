using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.IssueRegisters.Requests.Queries
{
    public class GetSelectedEquipmentNameFromIssueRegisterByParameterRequest : IRequest<List<SelectedModel>>
    {
        public int UnitId { get; set; }
        public int DepartmentNameId { get; set; }
        public int ItemCategoryId { get; set; }
    }
}
  