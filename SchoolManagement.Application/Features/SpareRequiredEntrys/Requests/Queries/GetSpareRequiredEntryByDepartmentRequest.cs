using MediatR;
using SchoolManagement.Application.DTOs.SpareRequiredEntry;

namespace SchoolManagement.Application.Features.SpareRequiredEntrys.Requests.Queries
{
    public class GetSpareRequiredEntryByDepartmentRequest : IRequest<List<SpareRequiredEntryDto>>
    {
        
        public int DepartmentNameId { get; set; }
    } 
}

 