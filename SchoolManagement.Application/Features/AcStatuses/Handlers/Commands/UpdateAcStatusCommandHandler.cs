using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.AcStatuses.Requests.Commands;
using SchoolManagement.Application.DTOs.AcStatus.Validators;

namespace SchoolManagement.Application.Features.AcStatuses.Handlers.Commands
{
    public class UpdateAcStatusCommandHandler : IRequestHandler<UpdateAcStatusCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateAcStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateAcStatusCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateAcStatusDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.AcStatusDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var AcStatus = await _unitOfWork.Repository<AcStatus>().Get(request.AcStatusDto.AcStatusId);

            if (AcStatus is null)
                throw new NotFoundException(nameof(AcStatus), request.AcStatusDto.AcStatusId);

            _mapper.Map(request.AcStatusDto, AcStatus);

            await _unitOfWork.Repository<AcStatus>().Update(AcStatus);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
