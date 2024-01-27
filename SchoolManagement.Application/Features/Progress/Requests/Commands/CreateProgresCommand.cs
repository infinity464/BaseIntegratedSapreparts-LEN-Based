using MediatR;
using SchoolManagement.Application.DTOs.Progres;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.Progress.Requests.Commands
{
    public class CreateProgresCommand : IRequest<BaseCommandResponse>
    {
        public CreateProgresDto ProgresDto { get; set; }
    }
}
