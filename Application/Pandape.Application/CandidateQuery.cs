using MediatR;
using Pandape.Domain;
using Pandape.Infrastructure.DataBase;

namespace Pandape.Application;

public class CandidateQuery: IRequest<IEnumerable<CandidateDto>> 
{
    
}

public class GetCandidateQueryHandler : IRequestHandler<CandidateQuery, IEnumerable<CandidateDto>>
{
    public readonly IUnitOfWork _uow;

    public GetCandidateQueryHandler(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public Task<IEnumerable<CandidateDto>> Handle(CandidateQuery request, CancellationToken cancellationToken)
    {
        var candidates = _uow.Cadidates.GetAll()
            .AsEnumerable()
            .Select(x =>
            {
                return new CandidateDto
                {
                    IdCandidate = x.IdCandidate,
                    Name = x.Name,
                    Surname = x.Surname,
                    Email = x.Email,
                    Birthdate = x.Birthdate,
                    InsertDate = x.InsertDate,
                    ModifyDate = x.ModifyDate,
                };
            }).AsEnumerable();

        return Task.FromResult<IEnumerable<CandidateDto>>(candidates);
    }
}
