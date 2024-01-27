using MediatR;
using SchoolManagement.Application.DTOs.PmsNumberEntry;

namespace SchoolManagement.Application.Features.PmsNumberEntrys.Requests.Queries
{
    public class GetPmsNumberEntryDetailRequest : IRequest<PmsNumberEntryDto>
    {
        public int PmsNumberEntryId { get; set; }
    }
}
