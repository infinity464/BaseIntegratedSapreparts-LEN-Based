using MediatR;

namespace SchoolManagement.Application.Features.Groups.Requests.Commands
{
    public class DeleteGroupCommand : IRequest
    {
        public int GroupId { get; set; }
    }
} 
