using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.PmsNumberEntrys.Requests.Queries
{
    public class GetSelectedPmsNumberEntryRequest : IRequest<List<SelectedModel>>
    {
    }
} 
