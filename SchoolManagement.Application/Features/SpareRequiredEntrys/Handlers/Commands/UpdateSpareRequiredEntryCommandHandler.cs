using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.SpareRequiredEntry.Validators;
using SchoolManagement.Application.Features.SpareRequiredEntrys.Requests.Commands;

namespace SchoolManagement.Application.Features.SpareRequiredEntrys.Handlers.Commands
{
    public class UpdateSpareRequiredEntryCommandHandler : IRequestHandler<UpdateSpareRequiredEntryCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateSpareRequiredEntryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateSpareRequiredEntryCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateSpareRequiredEntryDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.SpareRequiredEntryDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var SpareRequiredEntry = await _unitOfWork.Repository<SpareRequiredEntry>().Get(request.SpareRequiredEntryDto.SpareRequiredEntryId);

            if (SpareRequiredEntry is null)
                throw new NotFoundException(nameof(SpareRequiredEntry), request.SpareRequiredEntryDto.SpareRequiredEntryId);

            _mapper.Map(request.SpareRequiredEntryDto, SpareRequiredEntry);

            await _unitOfWork.Repository<SpareRequiredEntry>().Update(SpareRequiredEntry);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
