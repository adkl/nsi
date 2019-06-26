using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Entities
{
    class UnitOfWork : IUnitOfWork
    {
        private readonly NsiContext _context;

        public UnitOfWork(NsiContext context)
        {
            _context = context;
        }

        public IUserInfoRepository UserInfoRepository
        {
            get { return new UserInfoRepository(_context); }
        }

        public void Commit()
        {
            _context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            _context.SaveChanges();
        }

        public Task CommitAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void RegisterClean<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Attach(entity);
        }

        public void RegisterDeleted<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void SetModified<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void RegisterDirty<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void RegisterNew<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Add(entity);
        }

    }
}
