using MediatR;
using SchoolManagement.Application.DTOs.SpareRequiredEntry;

namespace SchoolManagement.Application.Features.SpareRequiredEntrys.Requests.Queries
{
    public class GetSpareRequiredEntryByJobDescRequest : IRequest<List<SpareRequiredEntryDto>>
    {
        
        public int JobDescriptionEntryPmsId { get; set; }
    } 
}

 