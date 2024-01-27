using MediatR;
using SchoolManagement.Application.DTOs.SurveyAuthority;

namespace SchoolManagement.Application.Features.SurveyAuthoritys.Requests.Commands
{
    public class UpdateSurveyAuthorityCommand : IRequest<Unit>
    { 
        public SurveyAuthorityDto SurveyAuthorityDto { get; set; }
    }
}
 