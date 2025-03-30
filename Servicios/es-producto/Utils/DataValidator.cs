using Microsoft.AspNetCore.Mvc;

namespace es_producto.Utils
{
    public class DataValidator
    {
        public static ObjectResult ValidarResultadoExcepcion(ServiceException ServiceException)
        {
            string mensaje;

            switch (ServiceException.Codigo)
            {
                case StatusCodes.Status400BadRequest:
                case StatusCodes.Status404NotFound:
                    mensaje = ServiceException.Message;
                    break;

                case StatusCodes.Status502BadGateway:
                    mensaje = "El servicio externo no está disponible actualmente.";
                    break;

                case StatusCodes.Status500InternalServerError:
                    mensaje = "Error interno del servidor.";
                    break;

                default:
                    mensaje = "Ocurrió un error inesperado.";
                    break;
            }

            return new ObjectResult(mensaje)
            {
                StatusCode = ServiceException.Codigo
            };
        }
    }
}
