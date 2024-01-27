using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.PreventiveMaintenances.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.PreventiveMaintenances.Handlers.Commands
{
    public class DeletePreventiveMaintenanceCommandHandler : IRequestHandler<DeletePreventiveMaintenanceCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeletePreventiveMaintenanceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeletePreventiveMaintenanceCommand request, CancellationToken cancellationToken)
        {
            var PreventiveMaintenance = await _unitOfWork.Repository<PreventiveMaintenance>().Get(request.PreventiveMaintenanceId);

            if (PreventiveMaintenance == null)
                throw new NotFoundException(nameof(PreventiveMaintenance), request.PreventiveMaintenanceId);

            await _unitOfWork.Repository<PreventiveMaintenance>().Delete(PreventiveMaintenance);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
