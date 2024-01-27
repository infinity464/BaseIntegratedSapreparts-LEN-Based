using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.EquipmentPrioritys.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.EquipmentPrioritys.Handlers.Commands
{
    public class DeleteEquipmentPriorityCommandHandler : IRequestHandler<DeleteEquipmentPriorityCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteEquipmentPriorityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteEquipmentPriorityCommand request, CancellationToken cancellationToken)
        {
            var EquipmentPriority = await _unitOfWork.Repository<EquipmentPriority>().Get(request.EquipmentPriorityId);

            if (EquipmentPriority == null)
                throw new NotFoundException(nameof(EquipmentPriority), request.EquipmentPriorityId);

            await _unitOfWork.Repository<EquipmentPriority>().Delete(EquipmentPriority);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
