using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.SurveyAuthoritys.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.SurveyAuthoritys.Handlers.Commands
{
    public class DeleteSurveyAuthorityCommandHandler : IRequestHandler<DeleteSurveyAuthorityCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteSurveyAuthorityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteSurveyAuthorityCommand request, CancellationToken cancellationToken)
        {
            var SurveyAuthority = await _unitOfWork.Repository<SurveyAuthority>().Get(request.SurveyAuthorityId);

            if (SurveyAuthority == null)
                throw new NotFoundException(nameof(SurveyAuthority), request.SurveyAuthorityId);

            await _unitOfWork.Repository<SurveyAuthority>().Delete(SurveyAuthority);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}
