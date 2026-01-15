using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tuya.Application.Dto.Request;
using Tuya.Application.Port;
using Tuya.Transversal.ResponseModel;

namespace Tuya.Api.Controllers
{
    /// <summary>
    /// Clase encargada de las funciones para interactuar con los datos del cliente.
    /// </summary>
    /// <param name="_customerService"></param>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController(ICustomerService _customerService) : ControllerBase
    {
        /// <summary>
        /// Metodo encargado de obtener todos los clientes.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            var customers = await _customerService.GetAll(pageNumber, pageSize);
            return Ok(new MessageResponse() { Status = StatusCodes.Status200OK, Message = "Consulta finalizada.", Data = customers });
        }

        /// <summary>
        /// Metodo encargado de buscar un cliente por su Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var customers = await _customerService.GetById(id);
            return Ok(new MessageResponse() { Status = StatusCodes.Status200OK, Message = "Consulta finalizada.", Data = customers });
        }

        /// <summary>
        /// Metodo encargo de registrar un cliente en el sistema.
        /// </summary>
        /// <param name="customerRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerRequest customerRequest)
        {
            CustomerValidator validator = new();
            var validatorResult = await validator.ValidateAsync(customerRequest);
            if (!validatorResult.IsValid)
                InvalidModel.Response(validatorResult);

            await _customerService.Add(customerRequest);
            return Ok(new MessageResponse() { Status = StatusCodes.Status201Created, Message = "Registro Exitoso" });
        }

        /// <summary>
        /// Metodo para actualizar la información del cliente que se especifique.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customerRequest"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CustomerRequest customerRequest)
        {
            customerRequest.Id = id;
            CustomerValidator validator = new();
            var validatorResult = await validator.ValidateAsync(customerRequest);
            if (!validatorResult.IsValid)
                InvalidModel.Response(validatorResult);

            await _customerService.Update(customerRequest);
            return Ok(new MessageResponse() { Status = StatusCodes.Status200OK, Message = "Registro actualizado" });
        }
    }
}
