
using Utilities.Models;

namespace Utilities.Interfaces
{
    public interface ICliente
    {
        Task<IEnumerable<ClienteModel>> FindAll(string strConx);
        Task<IEnumerable<ClienteModel>> FindById(string strConx, int id);
        Task<ClienteModel> CreateCliente(string strConx, ClienteModel model);
        Task<ClienteModel> UpdateCliente(string strConx, ClienteModel model);
        Task<bool> DeleteCliente(string strConx, int id);
        Task<IEnumerable<ClienteModel>> FindByNit(string strConx, string nit);

    }
}
