using Colegios.Api.Interfaces;
using Colegios.Api.Models;
using Colegios.Api.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Colegios.Api.Controllers
{
    [Route("api/[controller]")]


    public class DocenteController : BaseApiController
    {
        private readonly IDocente _docente;
        private readonly IConfiguration _configuration;

        public DocenteController(
            IDocente docente,
            IConfiguration configuration)
        {
            _docente = docente;
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string cnx = GetCnx(_configuration);
            var list = await _docente.FindAll(cnx);
            return Ok(list);
        }
        [HttpGet]
        [Route("FindById")]
        public async Task<IActionResult> FindById([FromQuery] int id)
        {
            string cnx = GetCnx(_configuration);
            var list = await _docente.FindById(cnx, id);
            return Ok(list.FirstOrDefault());
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DocenteModel model)
        {
            string cnx = GetCnx(_configuration);
            var inserted = await _docente.CreateDocente(cnx, model);
            return Ok(new
            {
                Message = "Creado exitosamente",
                Data = inserted
            });
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] DocenteModel model)
        {
            string cnx = GetCnx(_configuration);
            var updated = await _docente.UpdateDocente(cnx, model);
            return Ok(new
            {
                Message = "Actualizado exitosamente",
                Data = updated
            });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            string cnx = GetCnx(_configuration);
            await _docente.DeleteDocente(cnx, id);
            return Ok(new
            {
                Message = "Eliminado exitosamente",
            });
        }
        [HttpPatch]
        public async Task<IActionResult> Patch([FromBody] DocenteModel model)
        {
            string cnx = GetCnx(_configuration);
            var updated = await _docente.UpdateDocente(cnx, model);
            return Ok(new
            {
                Message = "Actualizado exitosamente",
                Data = updated
            });
        }
    }
}
