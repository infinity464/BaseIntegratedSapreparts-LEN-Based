using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Progres.Validators;
using SchoolManagement.Application.Features.Progress.Requests.Commands;

namespace SchoolManagement.Application.Features.Progress.Handlers.Commands
{
    public class UpdateProgresCommandHandler : IRequestHandler<UpdateProgresCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProgresCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateProgresCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProgresDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.ProgresDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var Progres = await _unitOfWork.Repository<Progres>().Get(request.ProgresDto.ProgresId);

            if (Progres is null)
                throw new NotFoundException(nameof(Progres), request.ProgresDto.ProgresId);

            _mapper.Map(request.ProgresDto, Progres);

            await _unitOfWork.Repository<Progres>().Update(Progres);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
