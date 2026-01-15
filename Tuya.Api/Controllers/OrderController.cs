using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tuya.Application.Dto.Request;
using Tuya.Application.Port;
using Tuya.Transversal.ResponseModel;

namespace Tuya.Api.Controllers
{
    /// <summary>
    /// Clase encargada de las funciones para interactuar con las Ordenes.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrderService _orderService) : ControllerBase
    {
        /// <summary>
        /// Metodo encargado de obtener todos las ordenes.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            var Orders = await _orderService.GetAll(pageNumber, pageSize);
            return Ok(new MessageResponse() { Status = StatusCodes.Status200OK, Message = "Consulta finalizada.", Data = Orders });
        }

        /// <summary>
        /// Metodo encargado de buscar una orden por su Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var Order = await _orderService.GetById(id);
            return Ok(new MessageResponse() { Status = StatusCodes.Status200OK, Message = "Consulta finalizada.", Data = Order });
        }

        /// <summary>
        /// Metodo encargo de registrar una orden en el sistema.
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderRequest orderRequest)
        {
            OrderValidator validator = new();
            var validatorResult = await validator.ValidateAsync(orderRequest);
            if (!validatorResult.IsValid)
                InvalidModel.Response(validatorResult);

            await _orderService.Add(orderRequest);
            return Ok(new MessageResponse() { Status = StatusCodes.Status201Created, Message = "Registro Exitoso" });
        }

        /// <summary>
        /// Finalizar orden en Proceso.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("FinishOrder")]
        public async Task<IActionResult> FinishOrder(int id)
        {
            await _orderService.FinishOrder(id);
            return Ok(new MessageResponse() { Status = StatusCodes.Status200OK, Message = "Registro actualizado" });
        }

        /// <summary>
        /// Cancelar orden en Proceso.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("CancelOrder")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            await _orderService.CancelOrder(id);
            return Ok(new MessageResponse() { Status = StatusCodes.Status200OK, Message = "Registro actualizado" });
        }
    }
}
