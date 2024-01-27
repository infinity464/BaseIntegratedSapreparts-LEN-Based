using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.SurveyItems.Requests.Commands;
using SchoolManagement.Application.DTOs.SurveyItem.Validators;

namespace SchoolManagement.Application.Features.SurveyItems.Handlers.Commands
{
    public class UpdateSurveyItemCommandHandler : IRequestHandler<UpdateSurveyItemCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateSurveyItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateSurveyItemCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateSurveyItemDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.SurveyItemDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var SurveyItem = await _unitOfWork.Repository<SurveyItem>().Get(request.SurveyItemDto.SurveyItemId);

            if (SurveyItem is null)
                throw new NotFoundException(nameof(SurveyItem), request.SurveyItemDto.SurveyItemId);

            _mapper.Map(request.SurveyItemDto, SurveyItem);

            await _unitOfWork.Repository<SurveyItem>().Update(SurveyItem);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
