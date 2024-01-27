using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.Opinion;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.Opinions.Requests.Queries
{
    public class GetOpinionListRequest : IRequest<PagedResult<OpinionDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
