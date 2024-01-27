using MediatR;

namespace SchoolManagement.Application.Features.StatusOfJobs.Requests.Commands
{
    public class DeleteStatusOfJobCommand : IRequest
    {
        public int StatusOfJobId { get; set; }
    }
} 
