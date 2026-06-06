using HallManagement.Core.Interfaces;
using HallManagement.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace HallManagement.Repositories.GenericRepository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly BangamataHallContext _context;
        public RepositoryBase(BangamataHallContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAll() => _context.Set<T>().AsTracking();

        public async Task<T> GetById(int id) => await _context.Set<T>().FindAsync(id);

        public bool Create(T entity)
        {
            _context.Set<T>().Add(entity);
            return _context.SaveChanges() > 0;
        }

        public bool Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return _context.SaveChanges() > 0;
        }

        public bool Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return _context.SaveChanges() > 0;
        }
    }
}
