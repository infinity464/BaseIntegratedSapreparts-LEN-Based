using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Progres;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.Progress.Requests.Queries
{
    public class GetProgresListRequest : IRequest<PagedResult<ProgresDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
