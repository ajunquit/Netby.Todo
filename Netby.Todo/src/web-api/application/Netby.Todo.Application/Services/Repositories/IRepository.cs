namespace Netby.Todo.Application.Services.Repositories
{
    public interface IRepository<T> where T : class
    {
        #region Métodos Síncronos
        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(string id);
        T Get(string id);
        IEnumerable<T> GetAll();
        //IEnumerable<T> GetAllWithPagination(int pageNumber, int pageSize);
        int Count();
        #endregion
    }
}
