using MediatR;

namespace SchoolManagement.Application.Features.JobDescriptionEntryPmses.Requests.Commands
{
    public class DeleteJobDescriptionEntryPmsCommand : IRequest
    {
        public int JobDescriptionEntryPmsId { get; set; }
    }
} 
