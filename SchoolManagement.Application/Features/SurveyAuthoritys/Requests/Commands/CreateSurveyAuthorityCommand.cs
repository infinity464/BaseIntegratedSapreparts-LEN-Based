using MediatR;
using SchoolManagement.Application.DTOs.SurveyAuthority;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.SurveyAuthoritys.Requests.Commands
{
    public class CreateSurveyAuthorityCommand : IRequest<BaseCommandResponse>
    {
        public CreateSurveyAuthorityDto SurveyAuthorityDto { get; set; }
    }
}
