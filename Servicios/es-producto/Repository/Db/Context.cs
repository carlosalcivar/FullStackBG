using es_producto.Repository.Model;
using Microsoft.EntityFrameworkCore;

namespace es_producto.Repository.Db
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<ProductoDTO> Productos => Set<ProductoDTO>();
    }
}
