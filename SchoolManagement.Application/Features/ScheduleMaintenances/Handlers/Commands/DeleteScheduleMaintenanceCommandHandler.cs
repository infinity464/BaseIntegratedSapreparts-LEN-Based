using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.ScheduleMaintenances.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ScheduleMaintenances.Handlers.Commands
{
    public class DeleteScheduleMaintenanceCommandHandler : IRequestHandler<DeleteScheduleMaintenanceCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteScheduleMaintenanceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteScheduleMaintenanceCommand request, CancellationToken cancellationToken)
        {
            var ScheduleMaintenance = await _unitOfWork.Repository<ScheduleMaintenance>().Get(request.ScheduleMaintenanceId);

            if (ScheduleMaintenance == null)
                throw new NotFoundException(nameof(ScheduleMaintenance), request.ScheduleMaintenanceId);

            await _unitOfWork.Repository<ScheduleMaintenance>().Delete(ScheduleMaintenance);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
