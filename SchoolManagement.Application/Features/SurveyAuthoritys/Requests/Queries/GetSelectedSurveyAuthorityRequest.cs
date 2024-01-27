using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.SurveyAuthoritys.Requests.Queries
{
    public class GetSelectedSurveyAuthorityRequest : IRequest<List<SelectedModel>>
    {
    }
} 
