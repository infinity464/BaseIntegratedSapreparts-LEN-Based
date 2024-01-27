using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ByWhoms.Requests.Queries
{
    public class GetSelectedByWhomRequest : IRequest<List<SelectedModel>>
    {
    }
} 
