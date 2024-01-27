using MediatR;
using SchoolManagement.Application.DTOs.SurveyItem;

namespace SchoolManagement.Application.Features.SurveyItems.Requests.Commands
{
    public class UpdateSurveyItemCommand : IRequest<Unit>
    { 
        public SurveyItemDto SurveyItemDto { get; set; }
    }
}
 