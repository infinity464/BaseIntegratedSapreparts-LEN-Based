using MediatR;
using SchoolManagement.Application.DTOs.Opinion;

namespace SchoolManagement.Application.Features.Opinions.Requests.Commands
{
    public class UpdateOpinionCommand : IRequest<Unit>
    { 
        public OpinionDto OpinionDto { get; set; }
    }
}
 