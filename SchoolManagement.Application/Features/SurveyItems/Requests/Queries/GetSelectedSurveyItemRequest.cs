using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.SurveyItems.Requests.Queries
{
    public class GetSelectedSurveyItemRequest : IRequest<List<SelectedModel>>
    {
    }
} 
