namespace Pandape.Application;

public class BaseCreate: BaseUowTests
{
    protected CreateCandidateCommandHandle? _createCommand;
    public void BaseSetUp() {

        _clockManager = new MockClockManager();
        
        _createCommand = new CreateCandidateCommandHandle(OurServiceLocator.GetUnitOfWork(),_clockManager);
    }
}
