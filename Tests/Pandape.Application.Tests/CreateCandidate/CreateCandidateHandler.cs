using System.Data.Common;

namespace Pandape.Application.Tests;

[TestFixture]
public class CreateCandidateHandler: BaseCreate
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
        Assert.IsNotNull(candidteInt);
        using(var toCheck = OurServiceLocator.GetUnitOfWork()){
            var candidate = toCheck.Cadidates.GetAll().Where(x => x.Email == "rlp@gmail.com");
            Assert.IsNotNull(candidate);
            Console.WriteLine(candidate);
        }
    }

}
