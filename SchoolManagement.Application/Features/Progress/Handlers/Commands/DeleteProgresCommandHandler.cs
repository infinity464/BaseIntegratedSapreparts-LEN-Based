using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.Progress.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Progress.Handlers.Commands
{
    public class DeleteProgresCommandHandler : IRequestHandler<DeleteProgresCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteProgresCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteProgresCommand request, CancellationToken cancellationToken)
        {
            var Progres = await _unitOfWork.Repository<Progres>().Get(request.ProgresId);

            if (Progres == null)
                throw new NotFoundException(nameof(Progres), request.ProgresId);

            await _unitOfWork.Repository<Progres>().Delete(Progres);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
