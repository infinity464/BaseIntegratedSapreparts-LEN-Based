using MediatR;

namespace SchoolManagement.Application.Features.SpareRequiredEntrys.Requests.Commands
{
    public class DeleteSpareRequiredEntryCommand : IRequest
    {
        public int SpareRequiredEntryId { get; set; }
    }
} 
