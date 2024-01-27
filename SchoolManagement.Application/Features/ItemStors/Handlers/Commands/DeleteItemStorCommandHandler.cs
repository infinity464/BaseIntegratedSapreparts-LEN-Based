using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.ItemStors.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ItemStors.Handlers.Commands
{
    public class DeleteItemStorCommandHandler : IRequestHandler<DeleteItemStorCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteItemStorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteItemStorCommand request, CancellationToken cancellationToken)
        {
            var ItemStor = await _unitOfWork.Repository<ItemStor>().Get(request.ItemStorId);

            if (ItemStor == null)
                throw new NotFoundException(nameof(ItemStor), request.ItemStorId);

            await _unitOfWork.Repository<ItemStor>().Delete(ItemStor);
            
                await _unitOfWork.Save();
            

            

            return Unit.Value;
        }
    }
}
