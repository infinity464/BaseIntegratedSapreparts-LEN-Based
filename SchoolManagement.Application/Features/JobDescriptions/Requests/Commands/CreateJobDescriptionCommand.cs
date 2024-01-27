using MediatR;
using SchoolManagement.Application.DTOs.JobDescription;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.JobDescriptions.Requests.Commands
{
    public class CreateJobDescriptionCommand : IRequest<BaseCommandResponse>
    {
        public CreateJobDescriptionDto JobDescriptionDto { get; set; }
    }
}
