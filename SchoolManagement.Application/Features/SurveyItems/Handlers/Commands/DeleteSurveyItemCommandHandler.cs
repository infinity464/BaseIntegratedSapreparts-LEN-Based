using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.SurveyItems.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.SurveyItems.Handlers.Commands
{
    public class DeleteSurveyItemCommandHandler : IRequestHandler<DeleteSurveyItemCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteSurveyItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteSurveyItemCommand request, CancellationToken cancellationToken)
        {
            var SurveyItem = await _unitOfWork.Repository<SurveyItem>().Get(request.SurveyItemId);

            if (SurveyItem == null)
                throw new NotFoundException(nameof(SurveyItem), request.SurveyItemId);

            await _unitOfWork.Repository<SurveyItem>().Delete(SurveyItem);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
