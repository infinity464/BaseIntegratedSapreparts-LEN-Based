using MediatR;
using SchoolManagement.Application.DTOs.StatusOfJob;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.StatusOfJobs.Requests.Commands
{
    public class CreateStatusOfJobCommand : IRequest<BaseCommandResponse>
    {
        public CreateStatusOfJobDto StatusOfJobDto { get; set; }
    }
}
