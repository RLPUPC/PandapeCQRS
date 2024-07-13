using Microsoft.EntityFrameworkCore;

namespace Pandape.Infrastructure.DataBase;

public interface IEntityConfiguration
{
    void Addconfiguration(ModelBuilder modelBuilder);
}
