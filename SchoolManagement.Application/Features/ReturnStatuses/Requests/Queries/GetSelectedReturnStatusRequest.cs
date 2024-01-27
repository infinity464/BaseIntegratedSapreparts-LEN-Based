using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ReturnStatuss.Requests.Queries
{
    public class GetSelectedReturnStatusRequest : IRequest<List<SelectedModel>>
    {
    }
} 
