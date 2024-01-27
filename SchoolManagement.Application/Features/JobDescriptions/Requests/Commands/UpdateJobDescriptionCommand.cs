using MediatR;
using SchoolManagement.Application.DTOs.JobDescription;

namespace SchoolManagement.Application.Features.JobDescriptions.Requests.Commands
{
    public class UpdateJobDescriptionCommand : IRequest<Unit>
    { 
        public JobDescriptionDto JobDescriptionDto { get; set; }
    }
}
 