namespace Netby.Todo.Persistence.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Netby.Todo.Application.Services.Repositories;
    using Netby.Todo.Persistence.Contexts;
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly NetbyDbContext _netbyDbContext;
        protected readonly DbSet<T> _entities;

        public Repository(NetbyDbContext netbyDbContext)
        {
            _netbyDbContext = netbyDbContext;
            _entities = _netbyDbContext.Set<T>();
        }
        public int Count()
        {
            return _entities.Count();
        }

        public bool Delete(string id)
        {
            T entity = Get(id);
            if (entity != null)
            {
                _entities.Remove(entity);
                return true;
            }
            return false;
        }

        public T Get(string id)
        {
            return _entities.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.ToList();
        }

        public bool Insert(T entity)
        {
            _entities.Add(entity);
            return true;
        }

        public bool Update(T entity)
        {
            _entities.Update(entity);
            return true;
        }
    }
}
