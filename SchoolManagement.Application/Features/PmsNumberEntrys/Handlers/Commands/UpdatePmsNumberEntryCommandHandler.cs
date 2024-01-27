using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.PmsNumberEntry.Validators;
using SchoolManagement.Application.Features.PmsNumberEntrys.Requests.Commands;

namespace SchoolManagement.Application.Features.PmsNumberEntrys.Handlers.Commands
{
    public class UpdatePmsNumberEntryCommandHandler : IRequestHandler<UpdatePmsNumberEntryCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdatePmsNumberEntryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdatePmsNumberEntryCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdatePmsNumberEntryDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.PmsNumberEntryDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var PmsNumberEntry = await _unitOfWork.Repository<PmsNumberEntry>().Get(request.PmsNumberEntryDto.PmsNumberEntryId);

            if (PmsNumberEntry is null)
                throw new NotFoundException(nameof(PmsNumberEntry), request.PmsNumberEntryDto.PmsNumberEntryId);

            _mapper.Map(request.PmsNumberEntryDto, PmsNumberEntry);

            await _unitOfWork.Repository<PmsNumberEntry>().Update(PmsNumberEntry);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
