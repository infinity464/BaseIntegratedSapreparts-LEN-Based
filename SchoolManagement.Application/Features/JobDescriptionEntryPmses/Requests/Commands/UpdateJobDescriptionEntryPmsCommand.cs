using MediatR;
using SchoolManagement.Application.DTOs.JobDescriptionEntryPms;

namespace SchoolManagement.Application.Features.JobDescriptionEntryPmses.Requests.Commands
{
    public class UpdateJobDescriptionEntryPmsCommand : IRequest<Unit>
    { 
        public JobDescriptionEntryPmsDto JobDescriptionEntryPmsDto { get; set; }
    }
}
 