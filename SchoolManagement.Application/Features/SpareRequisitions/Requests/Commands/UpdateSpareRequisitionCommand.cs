using MediatR;
using SchoolManagement.Application.DTOs.SpareRequisition;

namespace SchoolManagement.Application.Features.SpareRequisitions.Requests.Commands
{
    public class UpdateSpareRequisitionCommand : IRequest<Unit>
    { 
        public SpareRequisitionDto SpareRequisitionDto { get; set; }
    }
}
 