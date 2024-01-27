using MediatR;
using SchoolManagement.Application.DTOs.StatusOfJob;

namespace SchoolManagement.Application.Features.StatusOfJobs.Requests.Commands
{
    public class UpdateStatusOfJobCommand : IRequest<Unit>
    { 
        public StatusOfJobDto StatusOfJobDto { get; set; }
    }
}
 