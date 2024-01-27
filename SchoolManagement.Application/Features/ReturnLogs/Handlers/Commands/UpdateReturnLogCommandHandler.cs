using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ReturnLogs.Validators;
using SchoolManagement.Application.Features.ReturnLogs.Requests.Commands;

namespace SchoolManagement.Application.Features.ReturnLogs.Handlers.Commands
{
    public class UpdateReturnLogCommandHandler : IRequestHandler<UpdateReturnLogCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateReturnLogCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateReturnLogCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateReturnLogDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.ReturnLogDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var ReturnLog = await _unitOfWork.Repository<ReturnLog>().Get(request.ReturnLogDto.ReturnLogId);

            if (ReturnLog is null)
                throw new NotFoundException(nameof(ReturnLog), request.ReturnLogDto.ReturnLogId);

            _mapper.Map(request.ReturnLogDto, ReturnLog);

            await _unitOfWork.Repository<ReturnLog>().Update(ReturnLog);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
