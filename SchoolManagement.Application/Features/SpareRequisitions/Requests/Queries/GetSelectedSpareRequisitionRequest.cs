using MediatR;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.SpareRequisitions.Requests.Queries
{
    public class GetSelectedSpareRequisitionRequest : IRequest<List<SelectedModel>>
    {
    }
} 
