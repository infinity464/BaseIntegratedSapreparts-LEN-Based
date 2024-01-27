using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.SpareRequiredStatuss.Requests.Queries
{
    public class GetSelectedSpareRequiredStatusRequest : IRequest<List<SelectedModel>>
    {
    }
} 
