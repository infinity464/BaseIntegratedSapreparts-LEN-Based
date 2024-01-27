using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.DTOs.Procurement.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.Procurements.Requests.Commands;
using SchoolManagement.Application.Contracts.Persistence;

namespace SchoolManagement.Application.Features.Procurements.Handlers.Commands
{
    public class UpdateProcurementCommandHandler : IRequestHandler<UpdateProcurementCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProcurementCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateProcurementCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateProcurementDtoValidator();
            var validationResult = await validator.ValidateAsync(request.UpdateProcurementDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var Procurement = await _unitOfWork.Repository<Procurement>().Get(request.UpdateProcurementDto.ProcurementId);

            if (Procurement is null)
                throw new NotFoundException(nameof(Procurement), request.UpdateProcurementDto.ProcurementId);

            /////// Image Upload //////////
            ///
            string uniqueFileName = null;
            //CreateProcurementDto
            if (request.UpdateProcurementDto.Doc != null)
            {

                var fileName = Path.GetFileName(request.UpdateProcurementDto.Doc.FileName);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                var a = Directory.GetCurrentDirectory();
                //var filePath = Path.Combine(_hostingEnv.WebRootPath, "Content\\images\\profile", uniqueFileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\procurement", uniqueFileName);

                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await request.UpdateProcurementDto.Doc.CopyToAsync(fileSteam);
                }


            }
            _mapper.Map(request.UpdateProcurementDto, Procurement);
            Procurement.ProcurementDocument = request.UpdateProcurementDto.Doc != null ? "files/procurement/" + uniqueFileName : Procurement.ProcurementDocument.Replace("https://localhost:44395/Content/", String.Empty);
            await _unitOfWork.Repository<Procurement>().Update(Procurement);
            await _unitOfWork.Save();

            return Unit.Value;

            //var validator = new UpdateProcurementDtoValidator(); 
            // var validationResult = await validator.ValidateAsync(request.UpdateProcurementDto);

            //if (validationResult.IsValid == false)
            //    throw new ValidationException(validationResult);

            //var Procurement = await _unitOfWork.Repository<Procurement>().Get(request.UpdateProcurementDto.ProcurementId);

            //if (Procurement is null)
            //    throw new NotFoundException(nameof(Procurement), request.UpdateProcurementDto.ProcurementId);

            ///////// File Upload //////////
            //string uniqueFileName = null;

            //if (request.UpdateProcurementDto.Doc != null)
            //{

            //    var fileName = Path.GetFileName(request.UpdateProcurementDto.Doc.FileName);
            //    uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
            //    var a = Directory.GetCurrentDirectory();
            //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\procurement", uniqueFileName);

            //    using (var fileSteam = new FileStream(filePath, FileMode.Create))
            //    {
            //        await request.UpdateProcurementDto.Doc.CopyToAsync(fileSteam);
            //    }
            //}



            //_mapper.Map(request.UpdateProcurementDto, Procurement);
            ////Procurement.DateOfTenderFloat = Procurement.DateOfTenderFloat.Value.AddDays(1.0);
            //Procurement.TenderopeningDate = Procurement.TenderopeningDate.Value.AddDays(1.0);
            //Procurement.DateOfDelivery = Procurement.DateOfDelivery.Value.AddDays(1.0);

            //Procurement.ProcurementDocument = request.UpdateProcurementDto.ProcurementDocument != null ? "files/procurement/" + uniqueFileName : Procurement.ProcurementDocument.Replace("https://localhost:44395/Content/", String.Empty);
            //await _unitOfWork.Repository<Procurement>().Update(Procurement);
            //await _unitOfWork.Save();

            //return Unit.Value;
        }
    }
}
