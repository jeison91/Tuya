using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tuya.Application.Port;
using Tuya.Transversal.ResponseModel;

namespace Tuya.Api.Controllers
{
    /// <summary>
    /// Clase encargada de las funciones para interactuar con los datos de productos.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService _productService) : ControllerBase
    {
        /// <summary>
        /// Metodo encargado de obtener todos los productos activos.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            var products = await _productService.GetAll(pageNumber, pageSize);
            return Ok(new MessageResponse() { Status = StatusCodes.Status200OK, Message = "Consulta finalizada.", Data = products });
        }

        /// <summary>
        /// Metodo encargado de buscar un producto por su Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var products = await _productService.GetById(id);
            return Ok(new MessageResponse() { Status = StatusCodes.Status200OK, Message = "Consulta finalizada.", Data = products });
        }
    }
}
