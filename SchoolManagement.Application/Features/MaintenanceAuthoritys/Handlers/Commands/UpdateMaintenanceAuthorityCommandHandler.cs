using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.MaintenanceAuthority.Validators;
using SchoolManagement.Application.Features.MaintenanceAuthoritys.Requests.Commands;

namespace SchoolManagement.Application.Features.MaintenanceAuthoritys.Handlers.Commands
{
    public class UpdateMaintenanceAuthorityCommandHandler : IRequestHandler<UpdateMaintenanceAuthorityCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateMaintenanceAuthorityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateMaintenanceAuthorityCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateMaintenanceAuthorityDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.MaintenanceAuthorityDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var MaintenanceAuthority = await _unitOfWork.Repository<MaintenanceAuthority>().Get(request.MaintenanceAuthorityDto.MaintenanceAuthorityId);

            if (MaintenanceAuthority is null)
                throw new NotFoundException(nameof(MaintenanceAuthority), request.MaintenanceAuthorityDto.MaintenanceAuthorityId);

            _mapper.Map(request.MaintenanceAuthorityDto, MaintenanceAuthority);

            await _unitOfWork.Repository<MaintenanceAuthority>().Update(MaintenanceAuthority);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
