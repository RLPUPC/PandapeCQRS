using NUnit.Framework.Legacy;

namespace Pandape.Application.CreateCandidate;

[TestFixture]
public class CreateCandidateHandlerTests: BaseCreate
{
    public DateTime dt;
    [SetUp]
    public void SetUp(){
        dt = new DateTime(2024,12,22);
        BaseSetUp();
        if(_clockManager == null)
            throw new ArgumentNullException(nameof(_clockManager));
        _clockManager.SetCurrentUtc(dt);
    }

    [TearDown]
    public void TearDown() 
    {
        ClearCandidateDDBB(); 
    }

    [Test]
    public void CreateCandidateDDBB(){
        var candidateNew = new CreateCandidateCommand {
            Name = "Ricardo",
            Surname = "Lopez",
            Email = "rlp@gmail.com",
            Birthdate = new DateTime(1996,05,30),
        };
        if(_createCommand == null)
            throw new ArgumentNullException(nameof(_createCommand));
        var candidteInt = _createCommand.Handle(candidateNew, CancellationToken.None);
        ClassicAssert.IsNotNull(candidteInt);
        using(var toCheck = OurServiceLocator.GetUnitOfWork()){
            var candidate = toCheck.Cadidates.GetAll().Where(x => x.Email == "rlp@gmail.com");
            ClassicAssert.IsNotNull(candidate);
        }
    }

}
