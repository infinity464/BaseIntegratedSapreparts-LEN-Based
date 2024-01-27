using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ScheduleMaintenenceTypes.Validators;
using SchoolManagement.Application.Features.ScheduleMaintenenceTypes.Requests.Commands;

namespace SchoolManagement.Application.Features.ScheduleMaintenenceTypes.Handlers.Commands
{
    public class UpdateScheduleMaintenenceTypeCommandHandler : IRequestHandler<UpdateScheduleMaintenenceTypeCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateScheduleMaintenenceTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateScheduleMaintenenceTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateScheduleMaintenenceTypeDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.ScheduleMaintenenceTypeDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var ScheduleMaintenenceType = await _unitOfWork.Repository<ScheduleMaintenenceType>().Get(request.ScheduleMaintenenceTypeDto.ScheduleMaintenenceTypeId);

            if (ScheduleMaintenenceType is null)
                throw new NotFoundException(nameof(ScheduleMaintenenceType), request.ScheduleMaintenenceTypeDto.ScheduleMaintenenceTypeId);

            _mapper.Map(request.ScheduleMaintenenceTypeDto, ScheduleMaintenenceType);

            await _unitOfWork.Repository<ScheduleMaintenenceType>().Update(ScheduleMaintenenceType);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
