using MediatR;
using SchoolManagement.Application.DTOs.SurveyItem;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.SurveyItems.Requests.Commands
{
    public class CreateSurveyItemCommand : IRequest<BaseCommandResponse>
    {
        public CreateSurveyItemDto SurveyItemDto { get; set; }
    }
}
