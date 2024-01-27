using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ReturnLogs.Requests.Queries
{
    public class GetSelectedReturnLogRequest : IRequest<List<SelectedModel>>
    {
    }
} 
