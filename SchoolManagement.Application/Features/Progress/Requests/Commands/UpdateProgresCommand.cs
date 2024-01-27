using MediatR;
using SchoolManagement.Application.DTOs.Progres;

namespace SchoolManagement.Application.Features.Progress.Requests.Commands
{
    public class UpdateProgresCommand : IRequest<Unit>
    { 
        public ProgresDto ProgresDto { get; set; }
    }
}
 