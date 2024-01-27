using MediatR;
using SchoolManagement.Application.DTOs.ReturnStatus;

namespace SchoolManagement.Application.Features.ReturnStatuss.Requests.Commands
{
    public class UpdateReturnStatusCommand : IRequest<Unit>
    { 
        public ReturnStatusDto ReturnStatusDto { get; set; }
    }
}
