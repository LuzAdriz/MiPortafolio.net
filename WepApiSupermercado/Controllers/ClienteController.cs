
using Microsoft.AspNetCore.Mvc;
using Utilities.Interfaces;
using Utilities.Models;

namespace WepApiSupermercado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ICliente _clienteService;
        public ClienteController(
            IConfiguration config,
            ICliente clienteService)
        {
            _config = config;
            _clienteService = clienteService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClienteModel>))]
        public async Task<IActionResult> Get()
        {
            var strConx = _config.GetConnectionString("myDb1");

            var list = await _clienteService.FindAll(strConx);

            return Ok(list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("FindById")]
        public async Task<IActionResult> FindById([FromQuery] int id)
        {
            var strConx = _config.GetConnectionString("myDb1");
            var list = await _clienteService.FindById(strConx, id);
            return Ok(list.FirstOrDefault());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClienteModel model)
        {
            var strConx = _config.GetConnectionString("myDb1");
            var inserted = await _clienteService.CreateCliente(strConx, model);
            return Ok(new
            {
                Message = "Creado exitosamente",
                Data = inserted
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ClienteModel model)
        {
            var strConx = _config.GetConnectionString("myDb1");
            var updated = await _clienteService.UpdateCliente(strConx, model);
            return Ok(new
            {
                Message = "Actualizado exitosamente",
                Data = updated
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var strConx = _config.GetConnectionString("myDb1");
            await _clienteService.DeleteCliente(strConx, id);
            return Ok(new
            {
                Message = "Eliminado exitosamente",
            });
        }

    }
}
