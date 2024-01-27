using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ByWhom.Validators;
using SchoolManagement.Application.Features.ByWhoms.Requests.Commands;

namespace SchoolManagement.Application.Features.ByWhoms.Handlers.Commands
{
    public class UpdateByWhomCommandHandler : IRequestHandler<UpdateByWhomCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateByWhomCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateByWhomCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateByWhomDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.ByWhomDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var ByWhom = await _unitOfWork.Repository<ByWhom>().Get(request.ByWhomDto.ByWhomId);

            if (ByWhom is null)
                throw new NotFoundException(nameof(ByWhom), request.ByWhomDto.ByWhomId);

            _mapper.Map(request.ByWhomDto, ByWhom);

            await _unitOfWork.Repository<ByWhom>().Update(ByWhom);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
