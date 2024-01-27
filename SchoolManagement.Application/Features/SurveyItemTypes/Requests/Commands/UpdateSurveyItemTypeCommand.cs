using MediatR;
using SchoolManagement.Application.DTOs.SurveyItemType;

namespace SchoolManagement.Application.Features.SurveyItemTypes.Requests.Commands
{
    public class UpdateSurveyItemTypeCommand : IRequest<Unit>
    { 
        public SurveyItemTypeDto SurveyItemTypeDto { get; set; }
    }
}
 