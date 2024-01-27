using MediatR;
using SchoolManagement.Application.DTOs.SpareRequisition;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.SpareRequisitions.Requests.Commands
{
    public class CreateSpareRequisitionCommand : IRequest<BaseCommandResponse>
    {
        public CreateSpareRequisitionDto SpareRequisitionDto { get; set; }
    }
}
