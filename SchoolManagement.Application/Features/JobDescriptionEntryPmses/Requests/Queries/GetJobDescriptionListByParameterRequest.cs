using MediatR;
using SchoolManagement.Application.DTOs.JobDescriptionEntryPms;

namespace SchoolManagement.Application.Features.JobDescriptionEntryPmses.Requests.Queries
{
    public class GetJobDescriptionListByParameterRequest : IRequest<List<JobDescriptionEntryPmsDto>>
    {
        public int UnitId { get; set; } 
        public int DepartmentNameId { get; set; }
        public int ItemDetailId { get; set; }
    }
}   
       