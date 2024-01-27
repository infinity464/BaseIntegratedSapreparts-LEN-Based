using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.SurveyItemType.Validators;
using SchoolManagement.Application.Features.SurveyItemTypes.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.SurveyItemTypes.Handlers.Commands
{
    public class CreateSurveyItemTypeCommandHandler : IRequestHandler<CreateSurveyItemTypeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateSurveyItemTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateSurveyItemTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateSurveyItemTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.SurveyItemTypeDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var SurveyItemType = _mapper.Map<SurveyItemType>(request.SurveyItemTypeDto);

                SurveyItemType = await _unitOfWork.Repository<SurveyItemType>().Add(SurveyItemType);

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
                response.Id = SurveyItemType.SurveyItemTypeId;
            }

            return response;
        }
    }
}
