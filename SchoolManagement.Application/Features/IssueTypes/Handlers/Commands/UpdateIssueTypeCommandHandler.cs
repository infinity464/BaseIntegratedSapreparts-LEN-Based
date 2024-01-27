using SchoolManagement.Domain;
using AutoMapper;
using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.IssueType.Validators;
using SchoolManagement.Application.Features.IssueTypes.Requests.Commands;

namespace SchoolManagement.Application.Features.IssueTypes.Handlers.Commands
{
    public class UpdateIssueTypeCommandHandler : IRequestHandler<UpdateIssueTypeCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateIssueTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateIssueTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateIssueTypeDtoValidator(); 
             var validationResult = await validator.ValidateAsync(request.IssueTypeDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var IssueType = await _unitOfWork.Repository<IssueType>().Get(request.IssueTypeDto.IssueTypeId);

            if (IssueType is null)
                throw new NotFoundException(nameof(IssueType), request.IssueTypeDto.IssueTypeId);

            _mapper.Map(request.IssueTypeDto, IssueType);

            await _unitOfWork.Repository<IssueType>().Update(IssueType);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
