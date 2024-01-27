using MediatR;

namespace SchoolManagement.Application.Features.PmsNumberEntrys.Requests.Commands
{
    public class DeletePmsNumberEntryCommand : IRequest
    {
        public int PmsNumberEntryId { get; set; }
    }
} 
