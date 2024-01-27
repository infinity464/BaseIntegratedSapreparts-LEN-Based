using MediatR;
using SchoolManagement.Application.DTOs.IssueRegister;

namespace SchoolManagement.Application.Features.IssueRegisters.Requests.Commands
{
    public class AddingReturnLogRegisterCommand : IRequest<Unit>
    { 
        public AddReturnLogToStoreDto IssueRegisterDtos { get; set; }
    }
}
   