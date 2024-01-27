using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Opinions.Requests.Queries
{
    public class GetSelectedOpinionRequest : IRequest<List<SelectedModel>>
    {
    }
} 
