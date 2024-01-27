using MediatR;

namespace SchoolManagement.Application.Features.JobDescriptions.Requests.Commands
{
    public class DeleteJobDescriptionCommand : IRequest
    {
        public int JobDescriptionId { get; set; }
    }
} 
