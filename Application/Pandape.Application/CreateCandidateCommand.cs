using FluentValidation;
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

public class CreateCandidateValidate : AbstractValidator<CreateCandidateCommand>
{
    public CreateCandidateValidate() 
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is requiered.");
        RuleFor(x => x.Surname).NotEmpty().WithMessage("Name is requiered.");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("A valid email is required.")
            .EmailAddress().WithMessage("Name is requiered.");
        RuleFor(x => x.Birthdate)
            .NotEmpty()
            .NotEqual(default(DateTime)).WithMessage("Birthdate is required.")
            .Must(BeAtLeast16YearsOld).WithMessage("Candidate must be at least 16 years old.");
    }
    private bool BeAtLeast16YearsOld(DateTime birthdate)
    {
        var age = DateTime.Today.Year - birthdate.Year;
        if (birthdate > DateTime.Today.AddYears(-age)) age--;
        return age >= 16;
    }
}
