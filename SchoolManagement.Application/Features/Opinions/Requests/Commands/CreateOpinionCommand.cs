using MediatR;
using SchoolManagement.Application.DTOs.Opinion;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.Opinions.Requests.Commands
{
    public class CreateOpinionCommand : IRequest<BaseCommandResponse>
    {
        public CreateOpinionDto OpinionDto { get; set; }
    }
}
