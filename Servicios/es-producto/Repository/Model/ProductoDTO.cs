using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace es_producto.Repository.Model
{
    [Table("tbl_producto")]
    public class ProductoDTO
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("sku", TypeName = "varchar(12)")]
        public string Sku { get; set; }

        [Column("nombre", TypeName = "varchar(200)")]
        public string Nombre { get; set; }

        [Column("descripcion", TypeName = "varchar(255)")]
        public string Descripcion { get; set; }

        [Column("estado", TypeName = "varchar(1)")]
        public string Estado { get; set; }

        [Column("usuario_creacion_id")]
        public int UsuarioCreacionId { get; set; }

        [Column("fecha_creacion", TypeName = "datetime")]
        public DateTime FechaCreacion { get; set; }

        [Column("usuario_modificacion_id")]
        public int? UsuarioModificacionId { get; set; }

        [Column("fecha_modificacion", TypeName = "datetime")]
        public DateTime? FechaModificacion { get; set; }

        public ProductoDTO()
        {
            Sku = string.Empty;
            Nombre = string.Empty;
            Descripcion = string.Empty;
            Estado = "A";
            UsuarioCreacionId = 0;
            FechaCreacion = DateTime.Now;
            UsuarioModificacionId = null;
            FechaModificacion = null;
        }
    }
}
