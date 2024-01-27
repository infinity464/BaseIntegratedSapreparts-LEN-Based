using MediatR;

namespace SchoolManagement.Application.Features.Progress.Requests.Commands
{
    public class DeleteProgresCommand : IRequest
    {
        public int ProgresId { get; set; }
    }
} 
