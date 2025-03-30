using Microsoft.AspNetCore.Mvc;
using ms_loguearusuario.Controllers.Constans;
using ms_loguearusuario.Controllers.Type;
using ms_loguearusuario.Service.Contract;
using ms_loguearusuario.Utils;

namespace ms_loguearusuario.Controllers.Impl
{
    [Route("v1/" + General.Tipo_Servicio + "/" + General.Nombre_Servicio)]
    [Tags(General.Nombre_Servicio)]
    [ApiController]
    public class ControllerImpl : ControllerBase
    {
        private readonly ILogger<ControllerImpl> _logger;
        private readonly IService Svc;

        public ControllerImpl(IService Servicio, ILogger<ControllerImpl> logger)
        {
            Svc = Servicio;
            _logger = logger;
        }

        #region Método de autenticación
        [HttpPost("login")]
        [ProducesResponseType(typeof(ResponseLoginType), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status502BadGateway)]
        [Consumes(MimeType.JSON)]
        [Produces(MimeType.JSON)]
        public async Task<ActionResult<object>> Login(RequestLoginType requestLoginType)
        {
            try
            {
                _logger.LogInformation($"Inicia capacidad de Login - CONTROLLER");

                ResponseLoginType responseLoginType = await Svc.Login(requestLoginType);
                return StatusCode(StatusCodes.Status200OK, responseLoginType);
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, $"Error ServiceException en capacidad de Login");
                return DataValidator.ValidarResultadoExcepcion(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error Exception en capacidad de Login");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Interno del Servidor");
            }
            finally
            {
                _logger.LogInformation($"Finaliza Capacidad de Login - CONTROLLER");
            }
        }
        #endregion


    }
}
