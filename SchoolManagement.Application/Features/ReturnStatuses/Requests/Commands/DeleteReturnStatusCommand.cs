using MediatR;

namespace SchoolManagement.Application.Features.ReturnStatuss.Requests.Commands
{
    public class DeleteReturnStatusCommand : IRequest
    {
        public int ReturnStatusId { get; set; }
    }
} 
