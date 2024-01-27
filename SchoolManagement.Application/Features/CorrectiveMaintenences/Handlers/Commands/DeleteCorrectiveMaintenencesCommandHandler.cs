using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.CorrectiveMaintenences.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.CorrectiveMaintenences.Handlers.Commands
{
    public class DeleteCorrectiveMaintenenceCommandHandler : IRequestHandler<DeleteCorrectiveMaintenenceCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteCorrectiveMaintenenceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteCorrectiveMaintenenceCommand request, CancellationToken cancellationToken)
        {
            var CorrectiveMaintenence = await _unitOfWork.Repository<CorrectiveMaintenence>().Get(request.CorrectiveMaintenenceId);

            if (CorrectiveMaintenence == null)
                throw new NotFoundException(nameof(CorrectiveMaintenence), request.CorrectiveMaintenenceId);

            await _unitOfWork.Repository<CorrectiveMaintenence>().Delete(CorrectiveMaintenence);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
