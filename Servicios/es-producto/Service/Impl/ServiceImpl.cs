using es_producto.Controllers.Constans;
using es_producto.Controllers.Type;
using es_producto.Repository.Contract;
using es_producto.Repository.Model;
using es_producto.Service.Contract;
using es_producto.Utils;

namespace es_producto.Service.Impl
{
    public class ServiceImpl : IService
    {
        private readonly ILogger<ServiceImpl> _logger;
        private IRepository _repository;

        public ServiceImpl(ILogger<ServiceImpl> logger, IRepository repositorio)
        {
            _logger = logger;
            _repository = repositorio;
        }

        public async Task<ProductoType> Guardar(ProductoType productoType)
        {
            try
            {
                _logger.LogInformation($"Inicia metodo de Guardar - SERVICE");

                bool ExisteCodigo = await _repository.ValidarExistencia(productoType.Sku);

                if (ExisteCodigo)
                    throw new ServiceException(Mensajes.CodigoExistente) { Codigo = StatusCodes.Status400BadRequest };

                ProductoDTO productoDTO = Converts.ConvertirTypeAModel(productoType);
                productoDTO = await _repository.Guardar(productoDTO);
                return Converts.ConvertirModelAType(productoDTO);
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, $"Error ServiceException en metodo de Guardar - SERVICE {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error metodo de Guardar - SERVICE {ex.Message}");
                throw;
            }
            finally
            {
                _logger.LogInformation($"Finaliza metodo de Guardar - SERVICE");
            }

        }

        public async Task<ProductoType> Actualizar(ProductoType productoType)
        {
            try
            {
                _logger.LogInformation($"Inicia metodo de Actualizar - SERVICE");

                bool Existe = await _repository.ValidarExistencia(productoType.Id);
                if (!Existe)
                    throw new ServiceException(Mensajes.RegistroNoExistente) { Codigo = StatusCodes.Status400BadRequest };

                ProductoDTO productoDTO = Converts.ConvertirTypeAModel(productoType);
                productoDTO = await _repository.Actualizar(productoDTO);
                return Converts.ConvertirModelAType(productoDTO);
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, $"Error ServiceException en metodo de Actualizar - SERVICE {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error metodo de Actualizar - SERVICE {ex.Message}");
                throw;
            }
            finally
            {
                _logger.LogInformation($"Finaliza metodo de Actualizar - SERVICE");
            }
        }

        public async Task<string> Eliminar(int id)
        {
            try
            {
                _logger.LogInformation($"Inicia metodo de Eliminar - SERVICE");
                ProductoDTO? productoDTO = await _repository.ConsultarPorId(id);

                if (productoDTO == null)
                    throw new ServiceException(Mensajes.RegistroNoExistente) { Codigo = StatusCodes.Status404NotFound };

                if (await _repository.Eliminar(productoDTO) == 0)
                    throw new ServiceException(Mensajes.RegistroNoEliminado) { Codigo = StatusCodes.Status400BadRequest };

                return Mensajes.RegistroEliminado;
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, $"Error ServiceException en metodo de Eliminar - SERVICE {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error metodo de Eliminar - SERVICE {ex.Message}");
                throw;
            }
            finally
            {
                _logger.LogInformation($"Finaliza metodo de Eliminar - SERVICE");
            }
        }

        public async Task<List<ProductoType>> Consultar()
        {
            try
            {
                _logger.LogInformation($"Inicia metodo de Consultar - SERVICE");
                List<ProductoDTO> ListadoModel = await _repository.Consultar();
                List<ProductoType> ListadoType = Converts.ConvertirListModelToListType(ListadoModel);
                return ListadoType;
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, $"Error ServiceException en metodo de Consultar - SERVICE {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error metodo de Consultar - SERVICE {ex.Message}");
                throw;
            }
            finally
            {
                _logger.LogInformation($"Finaliza metodo de Consultar - SERVICE");
            }
        }

        public async Task<ProductoType> ConsultarPorId(int id)
        {
            try
            {
                _logger.LogInformation($"Inicia metodo de ConsultarPorId - SERVICE");
                ProductoDTO? productoDTO = await _repository.ConsultarPorId(id);

                if (productoDTO == null)
                    throw new ServiceException(Mensajes.RegistroNoExistente) { Codigo = StatusCodes.Status404NotFound };

                return Converts.ConvertirModelAType(productoDTO);
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, $"Error ServiceException en metodo de ConsultarPorId - SERVICE {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error metodo de ConsultarPorId - SERVICE {ex.Message}");
                throw;
            }
            finally
            {
                _logger.LogInformation($"Finaliza metodo de ConsultarPorId - SERVICE");
            }
        }

        public async Task<List<ProductoType>> ConsultarPorEstado(string Estado)
        {
            try
            {
                _logger.LogInformation($"Inicia metodo de ConsultarPorEstado - SERVICE");
                List<ProductoDTO> Listado = await _repository.ConsultarPorEstado(Estado);

                return Converts.ConvertirListModelToListType(Listado);
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, $"Error ServiceException en metodo de ConsultarPorEstado - SERVICE {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error metodo de ConsultarPorEstado - SERVICE {ex.Message}");
                throw;
            }
            finally
            {
                _logger.LogInformation($"Finaliza metodo de ConsultarPorEstado - SERVICE");
            }
        }


    }
}
