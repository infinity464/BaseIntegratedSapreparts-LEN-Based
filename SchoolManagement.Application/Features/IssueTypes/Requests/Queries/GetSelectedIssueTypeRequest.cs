using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.IssueTypes.Requests.Queries
{
    public class GetSelectedIssueTypeRequest : IRequest<List<SelectedModel>>
    {
    }
} 
