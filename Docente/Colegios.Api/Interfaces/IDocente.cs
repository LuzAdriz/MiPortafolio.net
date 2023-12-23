using Colegios.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Colegios.Api.Interfaces
{
    public interface IDocente
    {
        Task<IEnumerable<DocenteModel>> FindAll(string cnx);
        Task<IEnumerable<DocenteModel>> FindById(string cnx, int id);
        Task<DocenteModel> CreateDocente(string cnx, DocenteModel model);
        Task<DocenteModel> UpdateDocente(string cnx, DocenteModel model);
        Task<bool> DeleteDocente(string cnx, int id);
        Task<IEnumerable<DocenteModel>> FindByDocumento(string cnx, string documento);


    }
}