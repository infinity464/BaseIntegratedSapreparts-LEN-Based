using MediatR;

namespace SchoolManagement.Application.Features.Opinions.Requests.Commands
{
    public class DeleteOpinionCommand : IRequest
    {
        public int OpinionId { get; set; }
    }
} 
