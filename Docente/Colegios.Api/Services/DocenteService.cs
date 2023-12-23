using Colegios.Api.Extensions;
using Colegios.Api.Interfaces;
using Colegios.Api.Models;
using Colegios.Api.Services.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colegios.Api.Services
{

    public class DocenteService : IDocente
    {
        private readonly IRepositorio<DocenteModel> _repositorio;

        public DocenteService(IRepositorio<DocenteModel> repositorio)
        {
            _repositorio = repositorio;
        }
        public async Task<IEnumerable<DocenteModel>> FindAll(string cnx)
        {
            return await _repositorio.FindAsync(cnx, "SP_DOCENTE_SELECT");
        }
        public async Task<IEnumerable<DocenteModel>> FindById(string cnx, int id)
        {
            return await _repositorio.FindAsync(cnx, "SP_DOCENTE_SELECT", new { ID = id });
        }

        public async Task<DocenteModel> CreateDocente(string cnx, DocenteModel model)
        {

            model.DataToUpper();
            var validatorResult = new DocenteValidator(this, cnx, forUpdate: false).Validate(model);
            if (!validatorResult.IsValid)
                throw new Exception(string.Join(",", validatorResult.Errors.Select(x => x.ErrorMessage)));

            var inserted = await _repositorio.FindAsync(cnx, "SP_DOCENTE_INSERT", model);

            return inserted.FirstOrDefault();
        }
        public async Task<DocenteModel> UpdateDocente(string cnx, DocenteModel model)
        {
            model.DataToUpper();

            var validatorResult = new DocenteValidator(this, cnx, forUpdate: true).Validate(model);
            if (!validatorResult.IsValid)
                throw new Exception(string.Join(", ", validatorResult.Errors.Select(x => x.ErrorMessage)));


            await _repositorio.UpdateAsync(cnx, "SP_DOCENTE_UPDATE", model);

            return model;
        }
        public async Task<bool> DeleteDocente(string cnx, int id)
        {

            var deleted = await _repositorio.DeleteAsync(cnx, "SP_DOCENTE_DELETE", new { ID = id });

            return deleted;
        }
        public async Task<IEnumerable<DocenteModel>> FindByDocumento(string cnx, string documento)
        {
            return await _repositorio.FindAsync(cnx, "SP_DOCENTE_SELECT", new { DOCUMENTO = documento });
        }

    }
}
