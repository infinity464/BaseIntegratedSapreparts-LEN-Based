using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.PmsNumberEntry;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.PmsNumberEntrys.Requests.Queries
{
    public class GetPmsNumberEntryListRequest : IRequest<PagedResult<PmsNumberEntryDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
