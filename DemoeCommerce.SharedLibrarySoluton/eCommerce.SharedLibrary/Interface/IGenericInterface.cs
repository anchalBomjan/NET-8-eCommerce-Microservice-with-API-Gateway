

using eCommerce.SharedLibrary.Response;
namespace eCommerce.SharedLibrary.Interface
{
    public interface IGenericInterface<T> where T:class
    {
        Task<Response> CreateAsync(T entity);
    }
}
