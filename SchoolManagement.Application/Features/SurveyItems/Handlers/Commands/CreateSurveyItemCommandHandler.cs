using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.SurveyItem.Validators;
using SchoolManagement.Application.Features.SurveyItems.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.SurveyItems.Handlers.Commands
{
    public class CreateSurveyItemCommandHandler : IRequestHandler<CreateSurveyItemCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateSurveyItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateSurveyItemCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateSurveyItemDtoValidator();
            var validationResult = await validator.ValidateAsync(request.SurveyItemDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var SurveyItem = _mapper.Map<SurveyItem>(request.SurveyItemDto);

                SurveyItem = await _unitOfWork.Repository<SurveyItem>().Add(SurveyItem);

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
                response.Id = SurveyItem.SurveyItemId;
            }

            return response;
        }
    }
}
