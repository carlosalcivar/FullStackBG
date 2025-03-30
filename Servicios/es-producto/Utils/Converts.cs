using es_producto.Controllers.Type;
using es_producto.Repository.Model;

namespace es_producto.Utils
{
    public class Converts
    {

        public static ProductoType ConvertirModelAType(ProductoDTO model)
        {
            return new ProductoType
            {
                Id = model.Id,
                Sku = model.Sku,
                Nombre = model.Nombre,
                Descripcion = model.Descripcion,
                Estado = model.Estado
            };
        }

        public static ProductoDTO ConvertirTypeAModel(ProductoType type)
        {
            return new ProductoDTO
            {
                Id = type.Id,
                Sku = type.Sku,
                Nombre = type.Nombre,
                Descripcion = type.Descripcion,
                Estado = type.Estado,
                UsuarioCreacionId = 0, // deberías setearlo desde el contexto (ej: usuario autenticado)
                FechaCreacion = DateTime.Now
            };
        }

        public static List<ProductoType> ConvertirListModelToListType(List<ProductoDTO> listadoModel)
        {
            var listadoType = new List<ProductoType>();

            if (listadoModel != null)
            {
                foreach (var item in listadoModel)
                {
                    var tipo = ConvertirModelAType(item);
                    if (tipo != null)
                        listadoType.Add(tipo);
                }
            }

            return listadoType;
        }


    }
}
