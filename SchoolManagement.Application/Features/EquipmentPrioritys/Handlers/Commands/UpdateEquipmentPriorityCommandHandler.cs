using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.EquipmentPrioritys.Validators;
using SchoolManagement.Application.Features.EquipmentPrioritys.Requests.Commands;

namespace SchoolManagement.Application.Features.EquipmentPrioritys.Handlers.Commands
{
    public class UpdateEquipmentPriorityCommandHandler : IRequestHandler<UpdateEquipmentPriorityCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEquipmentPriorityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateEquipmentPriorityCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateEquipmentPriorityDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.EquipmentPriorityDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var EquipmentPriority = await _unitOfWork.Repository<EquipmentPriority>().Get(request.EquipmentPriorityDto.EquipmentPriorityId);

            if (EquipmentPriority is null)
                throw new NotFoundException(nameof(EquipmentPriority), request.EquipmentPriorityDto.EquipmentPriorityId);

            _mapper.Map(request.EquipmentPriorityDto, EquipmentPriority);

            await _unitOfWork.Repository<EquipmentPriority>().Update(EquipmentPriority);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
