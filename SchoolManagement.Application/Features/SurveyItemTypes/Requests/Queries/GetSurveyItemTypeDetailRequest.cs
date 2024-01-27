using MediatR;
using SchoolManagement.Application.DTOs.SurveyItemType;

namespace SchoolManagement.Application.Features.SurveyItemTypes.Requests.Queries
{
    public class GetSurveyItemTypeDetailRequest : IRequest<SurveyItemTypeDto>
    {
        public int SurveyItemTypeId { get; set; }
    }
}
