using MediatR;
using SchoolManagement.Application.DTOs.PmsNumberEntry;

namespace SchoolManagement.Application.Features.PmsNumberEntrys.Requests.Queries
{
    public class GetPmsNumberEntryByDepartmentRequest : IRequest<List<PmsNumberEntryDto>>
    {
        
        public int DepartmentNameId { get; set; }
    } 
}

 