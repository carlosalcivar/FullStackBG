using Microsoft.AspNetCore.Mvc;

namespace ms_loguearusuario.Utils
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

        public static bool EsBase64Valido(string base64)
        {
            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out _);
        }

    }
}
