using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.SurveyAuthority.Validators;
using SchoolManagement.Application.Features.SurveyAuthoritys.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.SurveyAuthoritys.Handlers.Commands
{
    public class CreateSurveyAuthorityCommandHandler : IRequestHandler<CreateSurveyAuthorityCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateSurveyAuthorityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateSurveyAuthorityCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateSurveyAuthorityDtoValidator();
            var validationResult = await validator.ValidateAsync(request.SurveyAuthorityDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var SurveyAuthority = _mapper.Map<SurveyAuthority>(request.SurveyAuthorityDto);

                SurveyAuthority = await _unitOfWork.Repository<SurveyAuthority>().Add(SurveyAuthority);

                try
                {
                    await _unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex);
                }


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = SurveyAuthority.SurveyAuthorityId;
            }

            return response;
        }
    }
}
