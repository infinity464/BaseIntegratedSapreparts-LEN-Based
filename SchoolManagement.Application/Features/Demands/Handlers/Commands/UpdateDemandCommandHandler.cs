using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Demands.Validators;
using SchoolManagement.Application.Features.Demands.Requests.Commands;

namespace SchoolManagement.Application.Features.Demands.Handlers.Commands
{
    public class UpdateDemandCommandHandler : IRequestHandler<UpdateDemandCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateDemandCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateDemandCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateDemandDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CreateDemandDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var Demand = await _unitOfWork.Repository<Demand>().Get(request.CreateDemandDto.DemandId);

            if (Demand is null)
                throw new NotFoundException(nameof(Demand), request.CreateDemandDto.DemandId);

            /////// Image Upload //////////
            ///
            string uniqueFileName = null;
            //CreateDemandDto
            if (request.CreateDemandDto.Doc != null)
            {

                var fileName = Path.GetFileName(request.CreateDemandDto.Doc.FileName);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                var a = Directory.GetCurrentDirectory();
                //var filePath = Path.Combine(_hostingEnv.WebRootPath, "Content\\images\\profile", uniqueFileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\demand", uniqueFileName);

                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await request.CreateDemandDto.Doc.CopyToAsync(fileSteam);
                }


            }
            _mapper.Map(request.CreateDemandDto, Demand);

            Demand.ProcurementStatus = 0;
            Demand.StoreStatus = 0;
            Demand.SftStatus = 0;
            Demand.DemandCompleteStatus = 0;

            Demand.SpecDoc = request.CreateDemandDto.Doc != null ? "files/demand/" + uniqueFileName : Demand.SpecDoc.Replace("https://localhost:44395/Content/", String.Empty);
            await _unitOfWork.Repository<Demand>().Update(Demand);
            await _unitOfWork.Save();

            return Unit.Value;

            //var validator = new CreateDemandDtoValidator(); 
            // var validationResult = await validator.ValidateAsync(request.CreateDemandDto);

            //if (validationResult.IsValid == false)
            //    throw new ValidationException(validationResult);

            //var Demand = await _unitOfWork.Repository<Demand>().Get(request.CreateDemandDto.DemandId);

            //if (Demand is null)
            //    throw new NotFoundException(nameof(Demand), request.CreateDemandDto.DemandId);

            ///////// File Upload //////////
            /////
            //string uniqueFileName = null;

            //if (request.CreateDemandDto.Doc != null)
            //{

            //    var fileName = Path.GetFileName(request.CreateDemandDto.Doc.FileName);
            //    uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
            //    var a = Directory.GetCurrentDirectory();
            //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\demand", uniqueFileName);

            //    using (var fileSteam = new FileStream(filePath, FileMode.Create))
            //    {
            //        await request.CreateDemandDto.Doc.CopyToAsync(fileSteam);
            //    }

            //}

            //_mapper.Map(request.CreateDemandDto, Demand);
            //Demand.DemandDate = Demand.DemandDate.Value.AddDays(1.0);
            ////Demand.DemandCompleteStatus = 0;

            //Demand.SpecDoc = request.CreateDemandDto.SpecDoc != null ? "files/demand/" + uniqueFileName : Demand.SpecDoc.Replace("https://localhost:44395/Content/", String.Empty);


            //await _unitOfWork.Repository<Demand>().Update(Demand);

            //await _unitOfWork.Save();

            //return Unit.Value;
        }
    }
}
