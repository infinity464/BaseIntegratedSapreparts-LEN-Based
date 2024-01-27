using MediatR;
using SchoolManagement.Application.DTOs.Acceptances;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Courses;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.Courses.Requests.Queries
{
    public class GetAcceptancesListRequest : IRequest<PagedResult<AcceptanceDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
  