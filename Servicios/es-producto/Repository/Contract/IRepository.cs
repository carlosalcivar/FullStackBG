using es_producto.Repository.Model;

namespace es_producto.Repository.Contract
{
    public interface IRepository
    {
        public Task<ProductoDTO> Guardar(ProductoDTO procesoDTO);

        public Task<ProductoDTO> Actualizar(ProductoDTO procesoDTO);

        public Task<int> Eliminar(ProductoDTO procesoDTO);

        public Task<List<ProductoDTO>> Consultar();

        public Task<ProductoDTO?> ConsultarPorId(int id);

        public Task<List<ProductoDTO>> ConsultarPorEstado(string estado);

        public Task<bool> ValidarExistencia(int id);

        public Task<bool> ValidarExistencia(string sku);
    }
}
