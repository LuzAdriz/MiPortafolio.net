using System.Collections.Generic;
using System.Threading.Tasks;

namespace Colegios.Api.Interfaces
{
    /// <summary>
    /// Interfaz generica para ejecutar proocedimientos almacenados
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepositorio<T>
    {
        Task<bool> UpdateAsync(string cnx, string spName, T model);
        Task<bool> InsertAsync(string cnx, string spName, T model);
        Task<bool> DeleteAsync<P>(string cnx, string spName, P parameters);
        Task<IEnumerable<T>> FindAsync(string cnx, string spName);
        Task<IEnumerable<T>> FindAsync<P>(string cnx, string spName, P parameters);
    }
}
