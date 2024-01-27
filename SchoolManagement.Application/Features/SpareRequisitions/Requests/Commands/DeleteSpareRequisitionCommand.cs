using MediatR;

namespace SchoolManagement.Application.Features.SpareRequisitions.Requests.Commands
{
    public class DeleteSpareRequisitionCommand : IRequest
    {
        public int SpareRequisitionId { get; set; }
    }
} 
