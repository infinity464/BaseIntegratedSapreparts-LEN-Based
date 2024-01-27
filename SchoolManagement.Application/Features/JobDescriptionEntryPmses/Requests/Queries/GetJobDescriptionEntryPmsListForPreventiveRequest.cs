using MediatR;
using SchoolManagement.Application.DTOs.JobDescriptionEntryPms;

namespace SchoolManagement.Application.Features.JobDescriptionEntryPmses.Requests.Queries
{
    public class GetJobDescriptionEntryPmsListForPreventiveRequest : IRequest<List<JobDescriptionEntryPmsDto>>
    {
        public int UnitId { get; set; }
        public int DepartmentNameId { get; set; }
        public int MaintenanceTypeId { get; set; }
        public int MaintenanceCategoryId { get; set; }
        public int ItemDetailId { get; set; }
    } 
}

 