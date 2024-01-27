using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.SpareRequiredEntrys.Requests.Queries
{
    public class GetSelectedSpareRequiredEntryRequest : IRequest<List<SelectedModel>>
    {
    }
} 
