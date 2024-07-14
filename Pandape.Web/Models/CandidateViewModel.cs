namespace Pandape.Web.Models;

public class CandidateViewModel
{
    public int IdCandidate { get; set; }
    public string Name { get; set; } = default!;
    public string Surname { get; set; } = default!;
    public string Email { get; set; } = default!;
    public DateTime Birthdate { get; set; }
}
