using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.JobDescriptionEntryPmses.Requests.Queries
{
    public class GetJICByUnitAndDepartmentAndItemDetailIdRequest : IRequest<List<SelectedModel>>
    {
        public int UnitId { get; set; }
        public int DepartmentNameId { get; set; }
        public int ItemDetailId { get; set; }
    }
}  
  