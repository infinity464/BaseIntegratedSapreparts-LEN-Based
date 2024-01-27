using MediatR;
using SchoolManagement.Application.DTOs.JobDescriptionEntryPms;

namespace SchoolManagement.Application.Features.JobDescriptionEntryPmses.Requests.Queries
{
    public class GetJobDescriptionEntryPmsDetailRequest : IRequest<JobDescriptionEntryPmsDto>
    {
        public int JobDescriptionEntryPmsId { get; set; }
    }
}
