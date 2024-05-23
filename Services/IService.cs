namespace Tour_API.Services
{
    public interface IService
    {
        Task<List<T>> GetAllAsync<T>();
        Task<T> GetByIdAsync<T>(int id);
        Task<T> CreateAsync<T>(T model);
        Task<T> UpdateAsync<T>(int id, T model);
        Task<T> DeleteAsync<T>(int id);
    }
}
