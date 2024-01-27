using MediatR;
using SchoolManagement.Application.DTOs.ReturnStatus;

namespace SchoolManagement.Application.Features.ReturnStatuss.Requests.Queries
{
    public class GetReturnStatusDetailRequest : IRequest<ReturnStatusDto>
    {
        public int ReturnStatusId { get; set; }
    }
}
