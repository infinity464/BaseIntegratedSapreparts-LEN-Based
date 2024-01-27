using MediatR;
using SchoolManagement.Application.DTOs.PmsNumberEntry;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.PmsNumberEntrys.Requests.Commands
{
    public class CreatePmsNumberEntryCommand : IRequest<BaseCommandResponse>
    {
        public CreatePmsNumberEntryDto PmsNumberEntryDto { get; set; }
    }
}
