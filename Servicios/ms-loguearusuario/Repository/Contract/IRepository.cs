using ms_loguearusuario.Repository.Model;

namespace ms_loguearusuario.Repository.Contract
{
    public interface IRepository
    {

        public Task<UsuarioDTO?> ConsultarPorUsuario(string usuario);

    }
}
