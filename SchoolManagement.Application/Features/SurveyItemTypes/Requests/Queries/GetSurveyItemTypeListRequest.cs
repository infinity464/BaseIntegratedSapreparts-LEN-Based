using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.SurveyItemType;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.SurveyItemTypes.Requests.Queries
{
    public class GetSurveyItemTypeListRequest : IRequest<PagedResult<SurveyItemTypeDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
