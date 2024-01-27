using MediatR;
using SchoolManagement.Application.DTOs.ReturnStatus;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.ReturnStatuss.Requests.Commands
{
    public class CreateReturnStatusCommand : IRequest<BaseCommandResponse>
    {
        public CreateReturnStatusDto ReturnStatusDto { get; set; }
    }
}
