using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.DTOs.ItemStor.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.ItemStors.Requests.Commands;
using SchoolManagement.Application.Contracts.Persistence;

namespace SchoolManagement.Application.Features.ItemStors.Handlers.Commands
{
    public class UpdateItemStorCommandHandler : IRequestHandler<UpdateItemStorCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateItemStorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateItemStorCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateItemStorDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ItemStorDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var ItemStor = await _unitOfWork.Repository<ItemStor>().Get(request.ItemStorDto.ItemStorId);

            if (ItemStor is null)
                throw new NotFoundException(nameof(ItemStor), request.ItemStorDto.ItemStorId);

            /////// Image Upload //////////
            ///
            string uniqueFileName = null;
            //CreateItemStorDto
            if (request.ItemStorDto.Doc != null)
            {

                var fileName = Path.GetFileName(request.ItemStorDto.Doc.FileName);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                var a = Directory.GetCurrentDirectory();
                //var filePath = Path.Combine(_hostingEnv.WebRootPath, "Content\\images\\profile", uniqueFileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\item-stores", uniqueFileName);

                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await request.ItemStorDto.Doc.CopyToAsync(fileSteam);
                }


            }
            _mapper.Map(request.ItemStorDto, ItemStor);
            ItemStor.OtherDoc = request.ItemStorDto.Doc != null ? "files/item-stores/" + uniqueFileName : ItemStor.OtherDoc.Replace("https://localhost:44395/Content/", String.Empty);
            await _unitOfWork.Repository<ItemStor>().Update(ItemStor);
            await _unitOfWork.Save();

            return Unit.Value;
            //var validator = new UpdateItemStorDtoValidator(); 
            // var validationResult = await validator.ValidateAsync(request.ItemStorDto);

            //if (validationResult.IsValid == false)
            //    throw new ValidationException(validationResult);

            //var ItemStor = await _unitOfWork.Repository<ItemStor>().Get(request.ItemStorDto.ItemStorId);

            //if (ItemStor is null)
            //    throw new NotFoundException(nameof(ItemStor), request.ItemStorDto.ItemStorId);

            ///////// File Upload //////////
            /////
            //string uniqueFileName = null;

            //if (request.ItemStorDto.Doc != null)
            //{

            //    var fileName = Path.GetFileName(request.ItemStorDto.Doc.FileName);
            //    uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
            //    var a = Directory.GetCurrentDirectory();
            //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\item-stores", uniqueFileName);

            //    using (var fileSteam = new FileStream(filePath, FileMode.Create))
            //    {
            //        await request.ItemStorDto.Doc.CopyToAsync(fileSteam);
            //    }
            //}
            //_mapper.Map(request.ItemStorDto, ItemStor);
            //ItemStor.ItemReceivedDate = ItemStor.ItemReceivedDate.Value.AddDays(1.0);
            //ItemStor.DateOfExpiry = ItemStor.DateOfExpiry.Value.AddDays(1.0);
            //ItemStor.OtherDoc = request.ItemStorDto.OtherDoc != null ? "files/item-stores/" + uniqueFileName : ItemStor.OtherDoc.Replace("https://localhost:44395/Content/", String.Empty);
            //await _unitOfWork.Repository<ItemStor>().Update(ItemStor);
            //await _unitOfWork.Save();

            //return Unit.Value;
        }
    }
}
