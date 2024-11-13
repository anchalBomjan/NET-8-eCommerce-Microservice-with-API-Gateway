
using System.Linq.Expressions;
using Resp = eCommerce.SharedLibrary.Response;
namespace eCommerce.SharedLibrary.Interface
{
    public interface IGenericInterface<T> where T:class
    {
       // Task<Response> CreateAsync(T entity);
        Task<Resp.Response> CreateAsync(T entity);
        Task<Resp.Response> UpdateAsync(T entity);
        Task<Resp.Response> DeleteAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> FindByIdAsync(int id);
        Task<T> GetByAsync(Expression<Func<T, bool>> predicate);

    }
}
