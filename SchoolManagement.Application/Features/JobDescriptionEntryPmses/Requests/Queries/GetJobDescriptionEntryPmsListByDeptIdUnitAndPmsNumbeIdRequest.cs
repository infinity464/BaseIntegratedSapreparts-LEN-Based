using MediatR;
using SchoolManagement.Application.DTOs.Common;
using SchoolManagement.Application.DTOs.JobDescriptionEntryPms;
using SchoolManagement.Application.Models;

namespace SchoolManagement.Application.Features.JobDescriptionEntryPmses.Requests.Queries
{
    public class GetJobDescriptionEntryPmsListByDeptIdUnitAndPmsNumbeIdRequest : IRequest<PagedResult<JobDescriptionEntryPmsDto>>
    { 
        public QueryParams QueryParams { get; set; }
        public int? UnitId { get; set; }
        public int? DepartmentNameId { get; set; }
        public int? PmsNumberEntryId { get; set; }
    }
} 
