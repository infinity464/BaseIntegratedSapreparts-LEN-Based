using MediatR;
using SchoolManagement.Application.DTOs.ItemStor;
using SchoolManagement.Application.Responses;

namespace SchoolManagement.Application.Features.ItemStors.Requests.Commands
{
    public class CreatePreviousItemStoreCommand : IRequest<BaseCommandResponse>
    {
        public CreateItemStorDto ItemStorDto { get; set; }
    }
}
