using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.SurveyItemTypes.Requests.Queries
{
    public class GetSelectedSurveyItemTypeRequest : IRequest<List<SelectedModel>>
    {
    }
} 
