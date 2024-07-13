using Microsoft.EntityFrameworkCore;

namespace Pandape.Infrastructure.DataBase;

public class PandaContext: DbContext
{
    public string? _connectionString;
    public IEnumerable<IEntityConfiguration> _entities;
    
    public PandaContext(DbContextOptions<PandaContext> options, IEnumerable<IEntityConfiguration> entities){
        _entities = entities;
    }

    public PandaContext(string connectionString, IEnumerable<IEntityConfiguration> entities){
        _connectionString = connectionString;
        _entities = entities;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
        if(!optionsBuilder.IsConfigured){
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach(var entitie in _entities){
            entitie.Addconfiguration(modelBuilder);
        }
    }

}
