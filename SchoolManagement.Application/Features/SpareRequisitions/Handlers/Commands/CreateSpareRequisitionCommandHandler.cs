using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.SpareRequisition.Validators;
using SchoolManagement.Application.Features.SpareRequisitions.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.SpareRequisitions.Handlers.Commands
{
    public class CreateSpareRequisitionCommandHandler : IRequestHandler<CreateSpareRequisitionCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateSpareRequisitionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateSpareRequisitionCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateSpareRequisitionDtoValidator();
            var validationResult = await validator.ValidateAsync(request.SpareRequisitionDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var SpareRequisition = _mapper.Map<SpareRequisition>(request.SpareRequisitionDto);

                SpareRequisition = await _unitOfWork.Repository<SpareRequisition>().Add(SpareRequisition);

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
                response.Id = SpareRequisition.SpareRequisitionId;
            }

            return response;
        }
    }
}
