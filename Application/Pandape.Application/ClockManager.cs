namespace Pandape.Application;

public class ClockManager: IClockManager
{
    public ClockManager() { }
    
    public DateTime GetCurrentUtc() { return DateTime.UtcNow; }
}
