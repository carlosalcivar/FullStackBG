using Microsoft.EntityFrameworkCore;
using ms_loguearusuario.Repository.Contract;
using ms_loguearusuario.Repository.Db;
using ms_loguearusuario.Repository.Model;
using ms_loguearusuario.Utils;


namespace ms_loguearusuario.Repository.Impl
{
    public class RepositoryImpl : IRepository
    {
        private readonly ILogger<RepositoryImpl> _logger;
        private readonly Context db;

        public RepositoryImpl(Context dbContex, ILogger<RepositoryImpl> logger)
        {
            this.db = dbContex;
            _logger = logger;
        }

        public async Task<UsuarioDTO?> ConsultarPorUsuario(string usuario)
        {
            try
            {
                _logger.LogInformation($"Inicia metodo de ConsultarPorUsuario - REPOSITORY");
                return await db.Usuarios.Where(x => x.Usuario.ToLower() == usuario.ToLower()).FirstOrDefaultAsync();
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, $"Error en metodo de ConsultarPorUsuario - REPOSITORY {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error metodo de ConsultarPorUsuario - REPOSITORY {ex.Message}");
                throw;
            }
            finally
            {
                _logger.LogInformation($"Finaliza metodo de ConsultarPorUsuario - REPOSITORY");
            }
        }
    }
}
