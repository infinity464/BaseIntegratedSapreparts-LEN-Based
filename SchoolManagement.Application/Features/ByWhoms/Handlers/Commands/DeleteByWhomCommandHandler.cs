using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.ByWhoms.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ByWhoms.Handlers.Commands
{
    public class DeleteByWhomCommandHandler : IRequestHandler<DeleteByWhomCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteByWhomCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteByWhomCommand request, CancellationToken cancellationToken)
        {
            var ByWhom = await _unitOfWork.Repository<ByWhom>().Get(request.ByWhomId);

            if (ByWhom == null)
                throw new NotFoundException(nameof(ByWhom), request.ByWhomId);

            await _unitOfWork.Repository<ByWhom>().Delete(ByWhom);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
