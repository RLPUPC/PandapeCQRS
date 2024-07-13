namespace Pandape.Infrastructure.Domain.Dto;

public class Candidate
{
    public int IdCandidate {get;set;}
    public string Name {get;set;} = default!;
    public string Surname {get;set;} = default!;
    public string Email {get;set;} = default!;
    public DateTime Birthdate {get;set;}
    public DateTime InsertDate {get;set;}
    public DateTime? ModifyDate {get;set;}
}
