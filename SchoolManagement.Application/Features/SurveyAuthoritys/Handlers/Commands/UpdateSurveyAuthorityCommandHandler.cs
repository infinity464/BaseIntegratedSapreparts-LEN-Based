using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.SurveyAuthority.Validators;
using SchoolManagement.Application.Features.SurveyAuthoritys.Requests.Commands;

namespace SchoolManagement.Application.Features.SurveyAuthoritys.Handlers.Commands
{
    public class UpdateSurveyAuthorityCommandHandler : IRequestHandler<UpdateSurveyAuthorityCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateSurveyAuthorityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateSurveyAuthorityCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateSurveyAuthorityDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.SurveyAuthorityDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var SurveyAuthority = await _unitOfWork.Repository<SurveyAuthority>().Get(request.SurveyAuthorityDto.SurveyAuthorityId);

            if (SurveyAuthority is null)
                throw new NotFoundException(nameof(SurveyAuthority), request.SurveyAuthorityDto.SurveyAuthorityId);

            _mapper.Map(request.SurveyAuthorityDto, SurveyAuthority);

            await _unitOfWork.Repository<SurveyAuthority>().Update(SurveyAuthority);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
