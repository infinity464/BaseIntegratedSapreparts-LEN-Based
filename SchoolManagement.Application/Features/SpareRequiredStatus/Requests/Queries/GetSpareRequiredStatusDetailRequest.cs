using MediatR;
using SchoolManagement.Application.DTOs.SpareRequiredStatus;

namespace SchoolManagement.Application.Features.SpareRequiredStatuss.Requests.Queries
{
    public class GetSpareRequiredStatusDetailRequest : IRequest<SpareRequiredStatusDto>
    {
        public int SpareRequiredStatusId { get; set; }
    }
}
