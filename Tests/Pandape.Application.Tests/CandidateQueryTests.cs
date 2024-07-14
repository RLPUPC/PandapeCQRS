using Pandape.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pandape.Application;

[TestFixture]
public class CandidateQueryTests: BaseUowTests
{
    private GetCandidateQueryHandler _candidateQuery;
    private DateTime dt;

    [SetUp]
    public void SetUp() 
    {
        dt = new DateTime(2022,04,04);
        _candidateQuery = new GetCandidateQueryHandler(OurServiceLocator.GetUnitOfWork());
    }

    [TearDown]
    public void TearDown()
    {
        ClearCandidateDDBB();
    }


    [Test]
    public async Task EmptyCandidates() 
    {
        var query = new CandidateQuery();
        var candidates = await _candidateQuery.Handle(query, CancellationToken.None);
        Assert.That(candidates, Is.Empty);
    }

    [Test]
    public async Task AllCandidates()
    {
        var idCAndidate1 = CreateCandidate("Name1", "Surname1", new DateTime(2000, 01, 01),"Name1@Surname1", dt);
        var idCAndidate2 = CreateCandidate("Name2", "Surname2", new DateTime(2000, 01, 01),"Name2@Surname2", dt);
        var idCAndidate3 = CreateCandidate("Name3", "Surname3", new DateTime(2000, 01, 01),"Name3@Surname3", dt);
        var idCAndidate4 = CreateCandidate("Name4", "Surname4", new DateTime(2000, 01, 01), "Name4@Surname4", dt);
        var query = new CandidateQuery();
        var candidates = await _candidateQuery.Handle(query, CancellationToken.None);
        var candidatesExpected = new CandidateDto[] 
        {
            new CandidateDto
            {
                IdCandidate = idCAndidate1,
                Name = "Name1",
                Surname = "Surname1",
                Email = "Name1@Surname1",
                Birthdate = new DateTime(2000, 01, 01),
                InsertDate = dt
            },
            new CandidateDto
            {
                IdCandidate = idCAndidate2,
                Name = "Name2",
                Surname = "Surname2",
                Email = "Name2@Surname2",
                Birthdate = new DateTime(2000, 01, 01),
                InsertDate = dt
            },
            new CandidateDto
            {
                IdCandidate = idCAndidate3,
                Name = "Name3",
                Surname = "Surname3",
                Email = "Name3@Surname3",
                Birthdate = new DateTime(2000, 01, 01),
                InsertDate = dt
            },
            new CandidateDto
            {
                IdCandidate = idCAndidate4,
                Name = "Name4",
                Surname = "Surname4",
                Email = "Name4@Surname4",
                Birthdate = new DateTime(2000, 01, 01),
                InsertDate = dt
            },
        };
        Assert.That(candidates, Is.EqualTo(candidatesExpected));
    }

}
