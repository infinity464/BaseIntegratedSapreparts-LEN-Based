using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.AirCraftFlying.Validators;
using SchoolManagement.Application.Features.AirCraftFlyings.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.AirCraftFlyings.Handlers.Commands
{
    public class CreateAirCraftFlyingCommandHandler : IRequestHandler<CreateAirCraftFlyingCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAirCraftFlyingCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateAirCraftFlyingCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateAirCraftFlyingDtoValidator();
            var validationResult = await validator.ValidateAsync(request.AirCraftFlyingDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var AirCraftFlyings = _mapper.Map<AirCraftFlying>(request.AirCraftFlyingDto);
                //ClassRoutines.Date = ClassRoutines.Date.Value.AddDays(1.0);
                AirCraftFlyings.Date = AirCraftFlyings.Date.Value.AddDays(1.0);
                AirCraftFlyings = await _unitOfWork.Repository<AirCraftFlying>().Add(AirCraftFlyings);
               
                    await _unitOfWork.Save();
                
               


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = AirCraftFlyings.AirCraftFlyingId;
            }

            return response;
        }
    }
}
