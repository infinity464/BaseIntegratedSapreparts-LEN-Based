using MediatR;
using SchoolManagement.Application.DTOs.SpareRequiredEntry;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.SpareRequiredEntrys.Requests.Commands
{
    public class CreateSpareRequiredEntryCommand : IRequest<BaseCommandResponse>
    {
        public CreateSpareRequiredEntryDto SpareRequiredEntryDto { get; set; }
    }
}
