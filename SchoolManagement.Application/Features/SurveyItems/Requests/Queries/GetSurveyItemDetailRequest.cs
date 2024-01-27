using MediatR;
using SchoolManagement.Application.DTOs.SurveyItem;

namespace SchoolManagement.Application.Features.SurveyItems.Requests.Queries
{
    public class GetSurveyItemDetailRequest : IRequest<SurveyItemDto>
    {
        public int SurveyItemId { get; set; }
    }
}
