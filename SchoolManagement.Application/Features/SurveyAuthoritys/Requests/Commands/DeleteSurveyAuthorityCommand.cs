using MediatR;

namespace SchoolManagement.Application.Features.SurveyAuthoritys.Requests.Commands
{
    public class DeleteSurveyAuthorityCommand : IRequest
    {
        public int SurveyAuthorityId { get; set; }
    }
} 
