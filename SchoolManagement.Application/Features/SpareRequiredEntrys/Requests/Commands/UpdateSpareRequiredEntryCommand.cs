using MediatR;
using SchoolManagement.Application.DTOs.SpareRequiredEntry;

namespace SchoolManagement.Application.Features.SpareRequiredEntrys.Requests.Commands
{
    public class UpdateSpareRequiredEntryCommand : IRequest<Unit>
    { 
        public SpareRequiredEntryDto SpareRequiredEntryDto { get; set; }
    }
}
 