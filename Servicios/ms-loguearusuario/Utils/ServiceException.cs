namespace ms_loguearusuario.Utils
{
    public class ServiceException : Exception
    {
        public int Codigo { get; set; } = StatusCodes.Status400BadRequest;

        public ServiceException(string mensaje)
            : base(mensaje)
        {
        }

        public ServiceException(string mensaje, Exception excepcionInterna)
            : base(mensaje, excepcionInterna)
        {
        }
    }
}
