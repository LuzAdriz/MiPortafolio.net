
namespace Utilities.Interfaces
{
    public interface IRepositorio<T>
    {
        Task<bool> UpdateAsync(string strConx, string spName, T model);
        Task<bool> InsertAsync(string strConx, string spName, T model);
        Task<bool> DeleteAsync<P>(string strConx, string spName, P parameters);
        Task<IEnumerable<T>> FindAsync(string strConx, string spName);
        Task<IEnumerable<T>> FindAsync<P>(string strConx, string spName, P parameters);
    }
}
