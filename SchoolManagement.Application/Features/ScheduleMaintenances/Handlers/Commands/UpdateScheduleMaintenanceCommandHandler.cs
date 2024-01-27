using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ScheduleMaintenances.Validators;
using SchoolManagement.Application.Features.ScheduleMaintenances.Requests.Commands;

namespace SchoolManagement.Application.Features.ScheduleMaintenances.Handlers.Commands
{
    public class UpdateScheduleMaintenanceCommandHandler : IRequestHandler<UpdateScheduleMaintenanceCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateScheduleMaintenanceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateScheduleMaintenanceCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateScheduleMaintenanceDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.ScheduleMaintenanceDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var ScheduleMaintenance = await _unitOfWork.Repository<ScheduleMaintenance>().Get(request.ScheduleMaintenanceDto.ScheduleMaintenanceId);

            if (ScheduleMaintenance is null)
                throw new NotFoundException(nameof(ScheduleMaintenance), request.ScheduleMaintenanceDto.ScheduleMaintenanceId);

            _mapper.Map(request.ScheduleMaintenanceDto, ScheduleMaintenance);

            await _unitOfWork.Repository<ScheduleMaintenance>().Update(ScheduleMaintenance);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
