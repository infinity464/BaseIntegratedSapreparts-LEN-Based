using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.Opinions.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Opinions.Handlers.Commands
{
    public class DeleteOpinionCommandHandler : IRequestHandler<DeleteOpinionCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteOpinionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteOpinionCommand request, CancellationToken cancellationToken)
        {
            var Opinion = await _unitOfWork.Repository<Opinion>().Get(request.OpinionId);

            if (Opinion == null)
                throw new NotFoundException(nameof(Opinion), request.OpinionId);

            await _unitOfWork.Repository<Opinion>().Delete(Opinion);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
