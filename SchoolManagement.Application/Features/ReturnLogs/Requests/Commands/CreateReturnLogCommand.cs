using MediatR;
using SchoolManagement.Application.DTOs.IssueRegister;
using SchoolManagement.Application.DTOs.ReturnLogs;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.ReturnLogs.Requests.Commands
{
    public class CreateReturnLogCommand : IRequest<BaseCommandResponse>
    {
        public AddReturnLogToStoreDto ReturnLogDto { get; set; }
    }
}
