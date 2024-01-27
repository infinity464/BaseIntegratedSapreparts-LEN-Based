using MediatR;
using SchoolManagement.Application.DTOs.IssueRegister;

namespace SchoolManagement.Application.Features.IssueRegisters.Requests.Commands
{
    public class UpdateIssueRegisterEqupNonOPLStatusCommand : IRequest
    {
        public int IssueRegisterId { get; set; }
        public int Status { get; set; }
    }
}
