using Microsoft.Extensions.Configuration;
using Pandape.Infrastructure.DataBase;

namespace Pandape.Application;

[SetUpFixture]
public class SetUp
{
    [OneTimeSetUp]
    public void RunBeforeAnyTest() 
    {
        string? dbConnectionString = OurServiceLocator.ConfigurationRoot.GetConnectionString("PandaContext");
        if (string.IsNullOrEmpty(dbConnectionString))
            throw new ArgumentNullException(nameof(dbConnectionString));
        var entities = new IEntityConfiguration[]
        {
            new CandidateConfiguration(),
        };
        var pandapeContext = new PandaContext(dbConnectionString, entities);
        pandapeContext.Database.EnsureCreated();
    }
}
