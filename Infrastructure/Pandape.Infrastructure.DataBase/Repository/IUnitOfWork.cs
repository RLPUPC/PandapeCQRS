using Pandape.Infrastructure.Domain.Dto;

namespace Pandape.Infrastructure.DataBase;

public interface IUnitOfWork: IDisposable
{
    void Commit();
    void Rollback();
    IRepository<Candidate> Cadidates { get; }
}
