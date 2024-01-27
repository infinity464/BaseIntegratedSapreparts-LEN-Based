using MediatR;
using SchoolManagement.Application.DTOs.CorrectiveMaintenence;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.CorrectiveMaintenences.Requests.Commands
{
    public class CreateCorrectiveMaintenenceCommand : IRequest<BaseCommandResponse>
    {
        public CreateCorrectiveMaintenenceDto CorrectiveMaintenenceDto { get; set; }
    }
}
