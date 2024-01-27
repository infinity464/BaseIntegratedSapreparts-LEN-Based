using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Groups.Requests.Queries
{
    public class GetSelectedGroupRequest : IRequest<List<SelectedModel>>
    {
    }
} 
