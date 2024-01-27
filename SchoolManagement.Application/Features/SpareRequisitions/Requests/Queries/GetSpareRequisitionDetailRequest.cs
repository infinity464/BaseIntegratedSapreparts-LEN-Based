using MediatR;
using SchoolManagement.Application.DTOs.SpareRequisition;

namespace SchoolManagement.Application.Features.SpareRequisitions.Requests.Queries
{
    public class GetSpareRequisitionDetailRequest : IRequest<SpareRequisitionDto>
    {
        public int SpareRequisitionId { get; set; }
    }
}
