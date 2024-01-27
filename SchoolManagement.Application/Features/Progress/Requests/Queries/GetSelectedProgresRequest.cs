using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Progress.Requests.Queries
{
    public class GetSelectedProgresRequest : IRequest<List<SelectedModel>>
    {
    }
} 
