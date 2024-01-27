using MediatR;
using SchoolManagement.Application.DTOs.Group;

namespace SchoolManagement.Application.Features.Groups.Requests.Queries
{
    public class GetGroupDetailRequest : IRequest<GroupDto>
    {
        public int GroupId { get; set; }
    }
}
