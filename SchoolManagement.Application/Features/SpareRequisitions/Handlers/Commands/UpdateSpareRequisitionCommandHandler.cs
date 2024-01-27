using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.SpareRequisitions.Requests.Commands;
using SchoolManagement.Application.DTOs.SpareRequisition.Validators;

namespace SchoolManagement.Application.Features.SpareRequisitions.Handlers.Commands
{
    public class UpdateSpareRequisitionCommandHandler : IRequestHandler<UpdateSpareRequisitionCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateSpareRequisitionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateSpareRequisitionCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateSpareRequisitionDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.SpareRequisitionDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var SpareRequisition = await _unitOfWork.Repository<SpareRequisition>().Get(request.SpareRequisitionDto.SpareRequisitionId);

            if (SpareRequisition is null)
                throw new NotFoundException(nameof(SpareRequisition), request.SpareRequisitionDto.SpareRequisitionId);

            _mapper.Map(request.SpareRequisitionDto, SpareRequisition);

            await _unitOfWork.Repository<SpareRequisition>().Update(SpareRequisition);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
