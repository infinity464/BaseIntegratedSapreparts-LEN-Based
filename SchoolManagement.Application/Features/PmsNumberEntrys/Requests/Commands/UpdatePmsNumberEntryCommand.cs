using MediatR;
using SchoolManagement.Application.DTOs.PmsNumberEntry;

namespace SchoolManagement.Application.Features.PmsNumberEntrys.Requests.Commands
{
    public class UpdatePmsNumberEntryCommand : IRequest<Unit>
    { 
        public PmsNumberEntryDto PmsNumberEntryDto { get; set; }
    }
}
 