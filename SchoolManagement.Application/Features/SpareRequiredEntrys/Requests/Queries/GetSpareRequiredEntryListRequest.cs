using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.SpareRequiredEntry;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.SpareRequiredEntrys.Requests.Queries
{
    public class GetSpareRequiredEntryListRequest : IRequest<PagedResult<SpareRequiredEntryDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
