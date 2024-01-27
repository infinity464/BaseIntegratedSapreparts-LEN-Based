using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.PreventiveMaintenance.Validators;
using SchoolManagement.Application.Features.PreventiveMaintenances.Requests.Commands;

namespace SchoolManagement.Application.Features.PreventiveMaintenances.Handlers.Commands
{
    public class UpdatePreventiveMaintenanceCommandHandler : IRequestHandler<UpdatePreventiveMaintenanceCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdatePreventiveMaintenanceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdatePreventiveMaintenanceCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdatePreventiveMaintenanceDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.PreventiveMaintenanceDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var PreventiveMaintenance = await _unitOfWork.Repository<PreventiveMaintenance>().Get(request.PreventiveMaintenanceDto.PreventiveMaintenanceId);

            if (PreventiveMaintenance is null)
                throw new NotFoundException(nameof(PreventiveMaintenance), request.PreventiveMaintenanceDto.PreventiveMaintenanceId);

            _mapper.Map(request.PreventiveMaintenanceDto, PreventiveMaintenance);

            await _unitOfWork.Repository<PreventiveMaintenance>().Update(PreventiveMaintenance);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
