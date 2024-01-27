using MediatR;
using SchoolManagement.Application.DTOs.JobDescriptionEntryPms;

namespace SchoolManagement.Application.Features.JobDescriptionEntryPmses.Requests.Queries
{
    public class GetJobDescriptionEntryPmsByDepartmentRequest : IRequest<List<JobDescriptionEntryPmsDto>>
    {
        
        public int DepartmentNameId { get; set; }
    } 
}

 