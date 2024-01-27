using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Opinion.Validators;
using SchoolManagement.Application.Features.Opinions.Requests.Commands;

namespace SchoolManagement.Application.Features.Opinions.Handlers.Commands
{
    public class UpdateOpinionCommandHandler : IRequestHandler<UpdateOpinionCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateOpinionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateOpinionCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateOpinionDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.OpinionDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var Opinion = await _unitOfWork.Repository<Opinion>().Get(request.OpinionDto.OpinionId);

            if (Opinion is null)
                throw new NotFoundException(nameof(Opinion), request.OpinionDto.OpinionId);

            _mapper.Map(request.OpinionDto, Opinion);

            await _unitOfWork.Repository<Opinion>().Update(Opinion);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
