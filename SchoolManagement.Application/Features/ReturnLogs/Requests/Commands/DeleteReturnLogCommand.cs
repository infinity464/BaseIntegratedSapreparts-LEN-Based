using MediatR;

namespace SchoolManagement.Application.Features.ReturnLogs.Requests.Commands
{
    public class DeleteReturnLogCommand : IRequest
    {
        public int ReturnLogId { get; set; }
    }
} 
