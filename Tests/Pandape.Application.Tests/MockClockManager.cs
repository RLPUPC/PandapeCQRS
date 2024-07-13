
namespace Pandape.Application.Tests;

public class MockClockManager : IClockManager
{
    public DateTime _dt;

    public DateTime GetCurrentUtc()
    {
        return _dt;
    }

    public void SetCurrentUtc(DateTime dt){
        _dt = dt;
    }
}
