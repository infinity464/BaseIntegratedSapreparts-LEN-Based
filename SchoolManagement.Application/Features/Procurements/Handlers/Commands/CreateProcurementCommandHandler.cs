using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Procurement.Validators;
using SchoolManagement.Application.Features.Procurements.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Procurements.Handlers.Commands
{
    public class CreateProcurementCommandHandler : IRequestHandler<CreateProcurementCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProcurementCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateProcurementCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateProcurementDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ProcurementDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                /////// File Upload //////////
                string uniqueFileName = null;

                if (request.ProcurementDto.Doc != null)
                {

                    var fileName = Path.GetFileName(request.ProcurementDto.Doc.FileName);
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                    var a = Directory.GetCurrentDirectory();
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\procurement", uniqueFileName);
                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        await request.ProcurementDto.Doc.CopyToAsync(fileSteam);
                    }
                }
                
                
                
                var Procurement = _mapper.Map<Procurement>(request.ProcurementDto);
                Procurement.ProcurementDocument = request.ProcurementDto.ProcurementDocument ?? "files/procurement/" + uniqueFileName;
                Procurement = await _unitOfWork.Repository<Procurement>().Add(Procurement);
                //Procurement.DateOfTenderFloat = Procurement.DateOfTenderFloat.Value.AddDays(1.0);
                Procurement.TenderopeningDate = Procurement.TenderopeningDate.Value.AddDays(1.0);
                Procurement.DateOfDelivery = Procurement.DateOfDelivery.Value.AddDays(1.0);
                await _unitOfWork.Save();


                var demands = await _unitOfWork.Repository<Demand>().Get((int)request.ProcurementDto.DemandId);

                //
                demands.ProcurementStatus = 1;
                demands.DemandCompleteStatus = 1;
                demands.ItemStatusId = 7;

                await _unitOfWork.Repository<Demand>().Update(demands);
                
                await _unitOfWork.Save();



                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = Procurement.ProcurementId;
            }

            return response;
        }
    }
}
