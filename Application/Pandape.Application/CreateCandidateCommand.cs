using MediatR;
using Pandape.Infrastructure.DataBase;
using Pandape.Infrastructure.Domain.Dto;

namespace Pandape.Application;

public record CreateCandidateCommand: IRequest<int>
{
    public string Name {get;set;} = default!;
    public string Surname {get;set;} = default!;
    public string Email {get;set;} = default!;
    public DateTime Birthdate {get;set;}
}

public class CreateCandidateCommandHandle : IRequestHandler<CreateCandidateCommand, int>
{
    private IUnitOfWork _uow;
    private IClockManager _clockManager;
    public CreateCandidateCommandHandle(IUnitOfWork uof, IClockManager clockManager){
        _uow = uof;
        _clockManager = clockManager;
    }

    public Task<int> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
    {
        var candidate = _uow.Cadidates.Add(new Candidate{
            Name = request.Name,
            Surname = request.Surname,
            Email = request.Email,
            Birthdate = request.Birthdate,
            InsertDate = _clockManager.GetCurrentUtc()
        });
        _uow.Commit();
        return Task.FromResult(candidate.IdCandidate);
    }
}
