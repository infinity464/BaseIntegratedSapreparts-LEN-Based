using MediatR;
using SchoolManagement.Application.DTOs.SpareRequiredEntry;

namespace SchoolManagement.Application.Features.SpareRequiredEntrys.Requests.Queries
{
    public class GetSpareRequiredEntryDetailRequest : IRequest<SpareRequiredEntryDto>
    {
        public int SpareRequiredEntryId { get; set; }
    }
}
