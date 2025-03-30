using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ms_loguearusuario.Repository.Model
{
    [Table("tbl_usuario")]
    public class UsuarioDTO
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("usuario", TypeName = "varchar(50)")]
        public string Usuario { get; set; }

        [Column("contrasena", TypeName = "varchar(255)")]
        public string Contrasena { get; set; }

        [Column("nombre", TypeName = "varchar(100)")]
        public string Nombre { get; set; }

        [Column("apellido", TypeName = "varchar(100)")]
        public string Apellido { get; set; }

        [Column("email", TypeName = "varchar(255)")]
        public string Email { get; set; }

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

        public UsuarioDTO()
        {
            Usuario = string.Empty;
            Contrasena = string.Empty;
            Nombre = string.Empty;
            Apellido = string.Empty;
            Email = string.Empty;
            Estado = "A";
            UsuarioCreacionId = 0;
            FechaCreacion = DateTime.Now;
            UsuarioModificacionId = null;
            FechaModificacion = null;
        }
    }
}
