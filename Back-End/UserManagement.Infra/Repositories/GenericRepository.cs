using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Interfaces;
using UserManagement.Infra.Data;

namespace UserManagement.Infra.Repositories
{

    public class GenericRepository<T> : IGeneric<T>, IDisposable where T : class
    {
        private readonly Context _context;
        private bool _disposed = false;

        public GenericRepository(Context context)
        {
            _context = context;
        }

        public async Task Add(T objeto)
        {
            await _context.Set<T>().AddAsync(objeto);
            await _context.SaveChangesAsync();
        }
        public async Task Update(T objeto)
        {
            _context.Set<T>().Update(objeto);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(T objeto)
        {
            _context.Set<T>().Remove(objeto);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetEntityById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> List()
        {
            return await _context.Set<T>().ToListAsync();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }

}
