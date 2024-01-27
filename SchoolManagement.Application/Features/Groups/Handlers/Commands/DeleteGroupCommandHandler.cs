using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.Groups.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Groups.Handlers.Commands
{
    public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            var Group = await _unitOfWork.Repository<Group>().Get(request.GroupId);

            if (Group == null)
                throw new NotFoundException(nameof(Group), request.GroupId);

            await _unitOfWork.Repository<Group>().Delete(Group);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
