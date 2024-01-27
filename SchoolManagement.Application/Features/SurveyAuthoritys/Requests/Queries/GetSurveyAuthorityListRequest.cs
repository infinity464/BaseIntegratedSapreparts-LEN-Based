using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.SurveyAuthority;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.SurveyAuthoritys.Requests.Queries
{
    public class GetSurveyAuthorityListRequest : IRequest<PagedResult<SurveyAuthorityDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
