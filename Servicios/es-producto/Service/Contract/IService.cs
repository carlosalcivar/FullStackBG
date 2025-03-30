using es_producto.Controllers.Type;

namespace es_producto.Service.Contract
{
    public interface IService
    {
        public Task<ProductoType> Guardar(ProductoType productoType);

        public Task<ProductoType> Actualizar(ProductoType productoType);

        public Task<string> Eliminar(int id);

        public Task<List<ProductoType>> Consultar();

        public Task<ProductoType> ConsultarPorId(int id);

        public Task<List<ProductoType>> ConsultarPorEstado(string estado);
    }
}
