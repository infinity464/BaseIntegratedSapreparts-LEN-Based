using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.CorrectiveMaintenence;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.CorrectiveMaintenences.Requests.Queries
{
    public class GetCorrectiveMaintenenceListRequest : IRequest<PagedResult<CorrectiveMaintenenceDto>>
    {
        public int UnitId { get; set; }
        public int DepartmentNameId { get; set; }
        public int IssueRegisterId { get; set; }

        public QueryParams QueryParams { get; set; }
    }
}
