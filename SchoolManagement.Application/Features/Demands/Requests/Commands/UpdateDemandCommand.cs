using MediatR;
using SchoolManagement.Application.DTOs.Demands;

namespace SchoolManagement.Application.Features.Demands.Requests.Commands
{
    public class UpdateDemandCommand : IRequest<Unit>
    { 
        public CreateDemandDto CreateDemandDto { get; set; }
    }
} 
 
