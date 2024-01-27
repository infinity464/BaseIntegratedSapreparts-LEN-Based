using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.IssueRegisters.Requests.Commands;
using SchoolManagement.Application.Features.ItemStors.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ItemStors.Handlers.Commands
{
    public class AddingReturnLogRegisterCommandHandler : IRequestHandler<AddingReturnLogRegisterCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; 

        public AddingReturnLogRegisterCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
         
        public async Task<Unit> Handle(AddingReturnLogRegisterCommand request, CancellationToken cancellationToken)
        {
            var IssueStatus = _mapper.Map<ReturnLog>(request.IssueRegisterDtos);

            IssueStatus = await _unitOfWork.Repository<ReturnLog>().Add(IssueStatus);

            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
