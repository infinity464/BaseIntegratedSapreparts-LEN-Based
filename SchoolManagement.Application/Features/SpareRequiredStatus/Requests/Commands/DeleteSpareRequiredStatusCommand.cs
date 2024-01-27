using MediatR;

namespace SchoolManagement.Application.Features.SpareRequiredStatuss.Requests.Commands
{
    public class DeleteSpareRequiredStatusCommand : IRequest
    {
        public int SpareRequiredStatusId { get; set; }
    }
} 
