using es_producto.Controllers.Constans;
using es_producto.Controllers.Type;
using es_producto.Service.Contract;
using es_producto.Utils;
using Microsoft.AspNetCore.Mvc;

#if !DEBUG
using Microsoft.AspNetCore.Authorization;
#endif

namespace es_producto.Controllers.Impl
{
    [Route("v1/" + General.Tipo_Servicio + "/" + General.Nombre_Servicio)]
    [Tags(General.Nombre_Servicio)]
    [ApiController]

#if !DEBUG
    [Authorize]
#endif
    public class ControllerImpl : ControllerBase
    {
        private readonly ILogger<ControllerImpl> _logger;
        private readonly IService Svc;

        public ControllerImpl(IService Servicio, ILogger<ControllerImpl> logger)
        {
            Svc = Servicio;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProductoType), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Consumes(MimeType.JSON)]
        [Produces(MimeType.JSON)]
        public async Task<ActionResult<object>> Guardar(ProductoType productoType)
        {
            try
            {
                _logger.LogInformation($"Inicia capacidad de Guardar - CONTROLLER");

                ProductoType resultado = await Svc.Guardar(productoType);
                return StatusCode(StatusCodes.Status201Created, resultado);
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, $"Error ServiceException en capacidad de Guardar");
                return DataValidator.ValidarResultadoExcepcion(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Exception en capacidad de Guardar");
                return StatusCode(StatusCodes.Status500InternalServerError, Mensajes.ErrorGeneral);
            }
            finally
            {
                _logger.LogInformation($"Finaliza Capacidad de Guardar - CONTROLLER");
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(ProductoType), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Consumes(MimeType.JSON)]
        [Produces(MimeType.JSON)]
        public async Task<ActionResult<object>> Actualizar(ProductoType productoType)
        {
            try
            {
                _logger.LogInformation($"Inicia capacidad de Actualizar - CONTROLLER");

                ProductoType resultado = await Svc.Actualizar(productoType);
                return StatusCode(StatusCodes.Status200OK, resultado);
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, $"Error ServiceException en capacidad de Actualizar");
                return DataValidator.ValidarResultadoExcepcion(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Exception en capacidad de Actualizar");
                return StatusCode(StatusCodes.Status500InternalServerError, Mensajes.ErrorGeneral);
            }
            finally
            {
                _logger.LogInformation($"Finaliza Capacidad de Actualizar - CONTROLLER");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Produces(MimeType.JSON)]
        public async Task<ActionResult<object>> Eliminar(int id)
        {
            try
            {
                _logger.LogInformation($"Inicia capacidad de Eliminar - CONTROLLER");

                string resultado = await Svc.Eliminar(id);
                return StatusCode(StatusCodes.Status200OK, resultado);
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, $"Error ServiceException en capacidad de Eliminar");
                return DataValidator.ValidarResultadoExcepcion(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Exception en capacidad de Eliminar");
                return StatusCode(StatusCodes.Status500InternalServerError, Mensajes.ErrorGeneral);
            }
            finally
            {
                _logger.LogInformation($"Finaliza Capacidad de Eliminar - CONTROLLER");
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ProductoType>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Produces(MimeType.JSON)]
        public async Task<ActionResult<object>> Consultar()
        {
            try
            {
                _logger.LogInformation($"Inicia capacidad de Eliminar - Consultar");

                List<ProductoType> resultado = await Svc.Consultar();
                return StatusCode(StatusCodes.Status200OK, resultado);
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, $"Error ServiceException en capacidad de Consultar");
                return DataValidator.ValidarResultadoExcepcion(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Exception en capacidad de Consultar");
                return StatusCode(StatusCodes.Status500InternalServerError, Mensajes.ErrorGeneral);
            }
            finally
            {
                _logger.LogInformation($"Finaliza Capacidad de Consultar - CONTROLLER");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductoType), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Produces(MimeType.JSON)]
        public async Task<ActionResult<object>> ConsultarPorId(int id)
        {
            try
            {
                _logger.LogInformation($"Inicia capacidad de ConsultarPorId - Consultar");

                ProductoType resultado = await Svc.ConsultarPorId(id);
                return StatusCode(StatusCodes.Status200OK, resultado);
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, $"Error ServiceException en capacidad de ConsultarPorId");
                return DataValidator.ValidarResultadoExcepcion(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Exception en capacidad de ConsultarPorId");
                return StatusCode(StatusCodes.Status500InternalServerError, Mensajes.ErrorGeneral);
            }
            finally
            {
                _logger.LogInformation($"Finaliza Capacidad de ConsultarPorId - CONTROLLER");
            }
        }

        [HttpGet("estado/{estado}")]
        [ProducesResponseType(typeof(List<ProductoType>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Produces(MimeType.JSON)]
        public async Task<ActionResult<object>> ConsultarPorEstado(string estado)
        {
            try
            {

                _logger.LogInformation($"Inicia capacidad de ConsultarPorEstado - Consultar");
                List<ProductoType> resultado = await Svc.ConsultarPorEstado(estado);
                return StatusCode(StatusCodes.Status200OK, resultado);
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, $"Error ServiceException en capacidad de ConsultarPorEstado");
                return DataValidator.ValidarResultadoExcepcion(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Exception en capacidad de ConsultarPorEstado");
                return StatusCode(StatusCodes.Status500InternalServerError, Mensajes.ErrorGeneral);
            }
            finally
            {
                _logger.LogInformation($"Finaliza Capacidad de ConsultarPorEstado - CONTROLLER");
            }
        }
    }
}
