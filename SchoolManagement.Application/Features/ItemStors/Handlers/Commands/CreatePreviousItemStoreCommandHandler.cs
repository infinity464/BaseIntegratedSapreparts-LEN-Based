using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ItemStor.Validators;
using SchoolManagement.Application.Features.ItemStors.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ItemStors.Handlers.Commands
{
    public class CreatePreviousItemStoreCommandHandler : IRequestHandler<CreatePreviousItemStoreCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreatePreviousItemStoreCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreatePreviousItemStoreCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateItemStorDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ItemStorDto);

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

                if (request.ItemStorDto.Doc != null)
                {
                    var fileName = Path.GetFileName(request.ItemStorDto.Doc.FileName);
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                    var a = Directory.GetCurrentDirectory();
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\item-stores", uniqueFileName);
                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        await request.ItemStorDto.Doc.CopyToAsync(fileSteam);
                    }
                }
                var ItemStor = _mapper.Map<ItemStor>(request.ItemStorDto);
                ItemStor.OtherDoc = request.ItemStorDto.OtherDoc ?? "files/item-stores/" + uniqueFileName;
                ItemStor.AvailableQty = ItemStor.StoreQty;
                ItemStor.TotalReceivedQty = ItemStor.StoreQty;
                ItemStor = await _unitOfWork.Repository<ItemStor>().Add(ItemStor);
                ItemStor.ItemReceivedDate = ItemStor.ItemReceivedDate.Value.AddDays(1.0);
                ItemStor.DateOfExpiry = ItemStor.DateOfExpiry.Value.AddDays(1.0);

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
                response.Id = ItemStor.ItemStorId;
            }

            return response;
        }
    }
}
