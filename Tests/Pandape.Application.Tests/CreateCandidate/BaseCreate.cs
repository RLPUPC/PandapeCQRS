namespace Pandape.Application;

public class BaseCreate
{
    protected CreateCandidateCommandHandle? _createCommand;
    protected MockClockManager? _clockManager;
    public void BaseSetUp() {

        _clockManager = new MockClockManager();
        
        _createCommand = new CreateCandidateCommandHandle(OurServiceLocator.GetUnitOfWork(),_clockManager);
    }
}
