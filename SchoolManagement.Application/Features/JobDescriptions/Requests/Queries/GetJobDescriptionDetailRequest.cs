using MediatR;
using SchoolManagement.Application.DTOs.JobDescription;

namespace SchoolManagement.Application.Features.JobDescriptions.Requests.Queries
{
    public class GetJobDescriptionDetailRequest : IRequest<JobDescriptionDto>
    {
        public int JobDescriptionId { get; set; }
    }
}
