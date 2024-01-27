using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.SpareRequiredStatus.Validators;
using SchoolManagement.Application.Features.SpareRequiredStatuss.Requests.Commands;

namespace SchoolManagement.Application.Features.SpareRequiredStatuss.Handlers.Commands
{
    public class UpdateSpareRequiredStatusCommandHandler : IRequestHandler<UpdateSpareRequiredStatusCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateSpareRequiredStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateSpareRequiredStatusCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateSpareRequiredStatusDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.SpareRequiredStatusDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var SpareRequiredStatus = await _unitOfWork.Repository<SpareRequiredStatus>().Get(request.SpareRequiredStatusDto.SpareRequiredStatusId);

            if (SpareRequiredStatus is null)
                throw new NotFoundException(nameof(SpareRequiredStatus), request.SpareRequiredStatusDto.SpareRequiredStatusId);

            _mapper.Map(request.SpareRequiredStatusDto, SpareRequiredStatus);

            await _unitOfWork.Repository<SpareRequiredStatus>().Update(SpareRequiredStatus);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
