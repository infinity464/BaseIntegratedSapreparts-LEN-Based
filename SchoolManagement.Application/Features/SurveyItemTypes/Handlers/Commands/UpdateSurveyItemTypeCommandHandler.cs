using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.SurveyItemType.Validators;
using SchoolManagement.Application.Features.SurveyItemTypes.Requests.Commands;

namespace SchoolManagement.Application.Features.SurveyItemTypes.Handlers.Commands
{
    public class UpdateSurveyItemTypeCommandHandler : IRequestHandler<UpdateSurveyItemTypeCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateSurveyItemTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateSurveyItemTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateSurveyItemTypeDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.SurveyItemTypeDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var SurveyItemType = await _unitOfWork.Repository<SurveyItemType>().Get(request.SurveyItemTypeDto.SurveyItemTypeId);

            if (SurveyItemType is null)
                throw new NotFoundException(nameof(SurveyItemType), request.SurveyItemTypeDto.SurveyItemTypeId);

            _mapper.Map(request.SurveyItemTypeDto, SurveyItemType);

            await _unitOfWork.Repository<SurveyItemType>().Update(SurveyItemType);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
