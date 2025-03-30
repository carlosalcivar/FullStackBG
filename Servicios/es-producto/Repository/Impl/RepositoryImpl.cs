using es_producto.Repository.Contract;
using es_producto.Repository.Db;
using es_producto.Repository.Model;
using es_producto.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace es_producto.Repository.Impl
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

        public async Task<ProductoDTO> Guardar(ProductoDTO productoDTO)
        {
            try
            {
                _logger.LogInformation($"Inicia metodo de Guardar - REPOSITORY");

                EntityEntry<ProductoDTO> item = db.Productos.Add(productoDTO);
                await db.SaveChangesAsync();
                return item.Entity;
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, $"Error en metodo de Guardar - REPOSITORY {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error metodo de Guardar - REPOSITORY {ex.Message}");
                throw;
            }
            finally
            {
                _logger.LogInformation($"Finaliza metodo de Guardar - REPOSITORY");
            }
        }

        public async Task<ProductoDTO> Actualizar(ProductoDTO productoDTO)
        {
            try
            {
                _logger.LogInformation($"Inicia metodo de Actualizar - REPOSITORY");
                db.Entry(productoDTO).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return productoDTO;
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, $"Error en metodo de Actualizar - REPOSITORY {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error metodo de Actualizar - REPOSITORY {ex.Message}");
                throw;
            }
            finally
            {
                _logger.LogInformation($"Finaliza metodo de Actualizar - REPOSITORY");
            }
        }

        public async Task<int> Eliminar(ProductoDTO productoDTO)
        {
            try
            {
                _logger.LogInformation($"Inicia metodo de Eliminar - REPOSITORY");
                productoDTO.Estado = "E";
                return await db.SaveChangesAsync();
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, $"Error en metodo de Eliminar - REPOSITORY {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error metodo de Eliminar - REPOSITORY {ex.Message}");
                throw;
            }
            finally
            {
                _logger.LogInformation($"Finaliza metodo de Eliminar - REPOSITORY");
            }
        }

        public async Task<List<ProductoDTO>> Consultar()
        {
            try
            {
                _logger.LogInformation($"Inicia metodo de Consultar - REPOSITORY");
                return await db.Productos.Where(x => x.Estado != "E").ToListAsync();
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, $"Error en metodo de Consultar - REPOSITORY {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error metodo de Consultar - REPOSITORY {ex.Message}");
                throw;
            }
            finally
            {
                _logger.LogInformation($"Finaliza metodo de Consultar - REPOSITORY");
            }
        }

        public async Task<ProductoDTO?> ConsultarPorId(int id)
        {
            try
            {
                _logger.LogInformation($"Inicia metodo de ConsultarPorId - REPOSITORY");
                return await db.Productos.FindAsync(id);
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, $"Error en metodo de ConsultarPorId - REPOSITORY {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error metodo de ConsultarPorId - REPOSITORY {ex.Message}");
                throw;
            }
            finally
            {
                _logger.LogInformation($"Finaliza metodo de ConsultarPorId - REPOSITORY");
            }
        }

        public async Task<List<ProductoDTO>> ConsultarPorEstado(string estado)
        {
            try
            {
                _logger.LogInformation($"Inicia metodo de ConsultarPorEstado - REPOSITORY");
                return await db.Productos.Where(x => x.Estado.Equals(estado)).ToListAsync();
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, $"Error en metodo de ConsultarPorEstado - REPOSITORY {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error metodo de ConsultarPorEstado - REPOSITORY {ex.Message}");
                throw;
            }
            finally
            {
                _logger.LogInformation($"Finaliza metodo de ConsultarPorEstado - REPOSITORY");
            }
        }

        public async Task<bool> ValidarExistencia(int id)
        {
            try
            {
                _logger.LogInformation($"Inicia metodo de ValidarExistencia - REPOSITORY");
                return await db.Productos.AnyAsync(item => item.Id == id);
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, $"Error en metodo de ValidarExistencia - REPOSITORY {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error metodo de ValidarExistencia - REPOSITORY {ex.Message}");
                throw;
            }
            finally
            {
                _logger.LogInformation($"Finaliza metodo de ValidarExistencia - REPOSITORY");
            }
        }

        public async Task<bool> ValidarExistencia(string sku)
        {
            try
            {
                _logger.LogInformation($"Inicia metodo de ValidarExistencia sku - REPOSITORY");
                return await db.Productos.AnyAsync(item => item.Estado != "E" && item.Sku == sku);
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, $"Error en metodo de ValidarExistencia sku - REPOSITORY {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error metodo de ValidarExistencia sku - REPOSITORY {ex.Message}");
                throw;
            }
            finally
            {
                _logger.LogInformation($"Finaliza metodo de ValidarExistencia sku - REPOSITORY");
            }
        }
    }
}
