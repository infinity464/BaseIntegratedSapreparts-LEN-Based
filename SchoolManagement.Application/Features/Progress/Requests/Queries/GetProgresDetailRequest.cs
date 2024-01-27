using MediatR;
using SchoolManagement.Application.DTOs.Progres;

namespace SchoolManagement.Application.Features.Progress.Requests.Queries
{
    public class GetProgresDetailRequest : IRequest<ProgresDto>
    {
        public int ProgresId { get; set; }
    }
}
