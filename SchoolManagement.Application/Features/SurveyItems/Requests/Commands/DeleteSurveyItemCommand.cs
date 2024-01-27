using MediatR;

namespace SchoolManagement.Application.Features.SurveyItems.Requests.Commands
{
    public class DeleteSurveyItemCommand : IRequest
    {
        public int SurveyItemId { get; set; }
    }
} 
