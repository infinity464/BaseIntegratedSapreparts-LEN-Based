using MediatR;
using SchoolManagement.Application.DTOs.SurveyAuthority;

namespace SchoolManagement.Application.Features.SurveyAuthoritys.Requests.Queries
{
    public class GetSurveyAuthorityDetailRequest : IRequest<SurveyAuthorityDto>
    {
        public int SurveyAuthorityId { get; set; }
    }
}
