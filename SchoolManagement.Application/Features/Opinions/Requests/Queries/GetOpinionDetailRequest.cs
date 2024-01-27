using MediatR;
using SchoolManagement.Application.DTOs.Opinion;

namespace SchoolManagement.Application.Features.Opinions.Requests.Queries
{
    public class GetOpinionDetailRequest : IRequest<OpinionDto>
    {
        public int OpinionId { get; set; }
    }
}
