using MediatR;

namespace SchoolManagement.Application.Features.ByWhoms.Requests.Commands
{
    public class DeleteByWhomCommand : IRequest
    {
        public int ByWhomId { get; set; }
    }
} 
