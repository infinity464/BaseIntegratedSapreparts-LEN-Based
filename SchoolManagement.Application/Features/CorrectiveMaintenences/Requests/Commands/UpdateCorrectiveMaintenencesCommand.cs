using MediatR;
using SchoolManagement.Application.DTOs.CorrectiveMaintenence;

namespace SchoolManagement.Application.Features.CorrectiveMaintenences.Requests.Commands
{
    public class UpdateCorrectiveMaintenenceCommand : IRequest<Unit>
    { 
        public CreateCorrectiveMaintenenceDto CorrectiveMaintenenceDto { get; set; }
    }
}
