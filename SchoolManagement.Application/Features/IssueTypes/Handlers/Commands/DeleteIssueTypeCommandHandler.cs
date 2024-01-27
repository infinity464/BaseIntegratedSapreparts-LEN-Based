using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.IssueTypes.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.IssueTypes.Handlers.Commands
{
    public class DeleteIssueTypeCommandHandler : IRequestHandler<DeleteIssueTypeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteIssueTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteIssueTypeCommand request, CancellationToken cancellationToken)
        {
            var IssueType = await _unitOfWork.Repository<IssueType>().Get(request.IssueTypeId);

            if (IssueType == null)
                throw new NotFoundException(nameof(IssueType), request.IssueTypeId);

            await _unitOfWork.Repository<IssueType>().Delete(IssueType);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
