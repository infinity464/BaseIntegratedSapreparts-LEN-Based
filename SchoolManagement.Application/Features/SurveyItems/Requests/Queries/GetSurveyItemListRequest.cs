using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.SurveyItem;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.SurveyItems.Requests.Queries
{
    public class GetSurveyItemListRequest : IRequest<PagedResult<SurveyItemDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
