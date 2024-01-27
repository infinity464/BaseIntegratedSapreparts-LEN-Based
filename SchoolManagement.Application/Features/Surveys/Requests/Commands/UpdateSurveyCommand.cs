using MediatR;
using SchoolManagement.Application.DTOs.Survey;

namespace SchoolManagement.Application.Features.Surveys.Requests.Commands
{
    public class UpdateSurveyCommand : IRequest<Unit>
    { 
        public SurveyDto SurveyDto { get; set; }
    }
}
 