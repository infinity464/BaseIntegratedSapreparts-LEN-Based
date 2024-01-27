using MediatR;
using SchoolManagement.Application.DTOs.SurveyItemType;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.SurveyItemTypes.Requests.Commands
{
    public class CreateSurveyItemTypeCommand : IRequest<BaseCommandResponse>
    {
        public CreateSurveyItemTypeDto SurveyItemTypeDto { get; set; }
    }
}
