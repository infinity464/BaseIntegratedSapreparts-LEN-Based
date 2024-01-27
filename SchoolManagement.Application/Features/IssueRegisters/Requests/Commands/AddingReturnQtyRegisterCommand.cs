using MediatR;
using SchoolManagement.Application.DTOs.IssueRegister;

namespace SchoolManagement.Application.Features.IssueRegisters.Requests.Commands
{
    public class AddingReturnQtyRegisterCommand : IRequest<Unit>
    {
        public AddReturnQtyToStoreDto IssueRegisterDto { get; set; }
    }
}
  