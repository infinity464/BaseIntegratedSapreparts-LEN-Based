using MediatR;

namespace SchoolManagement.Application.Features.SurveyItemTypes.Requests.Commands
{
    public class DeleteSurveyItemTypeCommand : IRequest
    {
        public int SurveyItemTypeId { get; set; }
    }
} 
