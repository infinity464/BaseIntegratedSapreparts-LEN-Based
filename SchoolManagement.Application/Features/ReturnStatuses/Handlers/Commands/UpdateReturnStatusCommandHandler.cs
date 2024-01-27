using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.ReturnStatuss.Requests.Commands;
using SchoolManagement.Application.DTOs.ReturnStatus.Validators;

namespace SchoolManagement.Application.Features.ReturnStatuss.Handlers.Commands
{
    public class UpdateReturnStatusCommandHandler : IRequestHandler<UpdateReturnStatusCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateReturnStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateReturnStatusCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateReturnStatusDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.ReturnStatusDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var ReturnStatus = await _unitOfWork.Repository<ReturnStatus>().Get(request.ReturnStatusDto.ReturnStatusId);

            if (ReturnStatus is null)
                throw new NotFoundException(nameof(ReturnStatus), request.ReturnStatusDto.ReturnStatusId);

            _mapper.Map(request.ReturnStatusDto, ReturnStatus);

            await _unitOfWork.Repository<ReturnStatus>().Update(ReturnStatus);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
