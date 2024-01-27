using MediatR;
using SchoolManagement.Application.DTOs.ReturnLogs;

namespace SchoolManagement.Application.Features.ReturnLogs.Requests.Commands
{
    public class UpdateReturnLogCommand : IRequest<Unit>
    { 
        public ReturnLogDto ReturnLogDto { get; set; }
    }
}
