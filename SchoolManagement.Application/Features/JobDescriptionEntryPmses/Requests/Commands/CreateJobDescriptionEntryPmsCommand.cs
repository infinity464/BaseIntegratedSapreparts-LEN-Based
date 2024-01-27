using MediatR;
using SchoolManagement.Application.DTOs.JobDescriptionEntryPms;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.JobDescriptionEntryPmses.Requests.Commands
{
    public class CreateJobDescriptionEntryPmsCommand : IRequest<BaseCommandResponse>
    {
        public CreateJobDescriptionEntryPmsDto JobDescriptionEntryPmsDto { get; set; }
    }
}
