using Microsoft.Extensions.Configuration;

namespace Pandape.Application;

public static class OurServiceLocator
{
    private static IConfigurationRoot? _configurationRoot;

    public static IConfigurationRoot ConfigurationRoot =>
        _configurationRoot ?? new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
        .Build();


    public static IUnitOfWork GetUnitOfWork()
    {
        string? dbConnectionString = ConfigurationRoot.GetConnectionString("PandaContext");
        if (string.IsNullOrWhiteSpace(dbConnectionString))
            throw new Exception("Erro connection string");
        var entities = new IEntityConfiguration[]
        {
            new CandidateConfiguration(),
        };
        var pandapeContext = new PandaContext(dbConnectionString, entities);
        return new UnitOfWork(pandapeContext);
    }
}