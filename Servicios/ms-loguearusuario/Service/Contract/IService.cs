using ms_loguearusuario.Controllers.Type;

namespace ms_loguearusuario.Service.Contract
{
    public interface IService
    {
        public Task<ResponseLoginType> Login(RequestLoginType requestLoginType);
    }
}
