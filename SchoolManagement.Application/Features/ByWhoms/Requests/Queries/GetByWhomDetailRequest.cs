using MediatR;
using SchoolManagement.Application.DTOs.ByWhom;

namespace SchoolManagement.Application.Features.ByWhoms.Requests.Queries
{
    public class GetByWhomDetailRequest : IRequest<ByWhomDto>
    {
        public int ByWhomId { get; set; }
    }
}
