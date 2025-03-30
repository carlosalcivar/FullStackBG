using Microsoft.EntityFrameworkCore;
using ms_loguearusuario.Repository.Model;

namespace ms_loguearusuario.Repository.Db
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<UsuarioDTO> Usuarios => Set<UsuarioDTO>();
    }
}
