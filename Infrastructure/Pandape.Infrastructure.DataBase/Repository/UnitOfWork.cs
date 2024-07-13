using Microsoft.EntityFrameworkCore.ChangeTracking;
using Pandape.Infrastructure.Domain.Dto;

namespace Pandape.Infrastructure.DataBase;

public class UnitOfWork : IUnitOfWork
{
    #region context
        protected PandaContext _context {  get; set; }
        
        public UnitOfWork(PandaContext context) 
        {
            _context = context;
        }

        private bool disposed = false;
        
        protected virtual void Dispose(bool disposed) 
        {
            if (!this.disposed)
            {
                if (disposed)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Rollback()
        {
            foreach (EntityEntry item in _context.ChangeTracker.Entries().ToList())
            {
                item.State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                
            }
        }

        #endregion

    #region EFPrivates

    private IRepository<Candidate>? _cadidates;

    #endregion

    #region EFPublics

    public IRepository<Candidate> Cadidates => _cadidates ??= new EFRepository<Candidate>(_context);

    #endregion
}