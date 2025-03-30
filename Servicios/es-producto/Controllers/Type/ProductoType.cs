using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace es_producto.Controllers.Type
{
    public class ProductoType
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(12)]
        [JsonPropertyName("sku")]
        public string Sku { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(255)]
        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; } = string.Empty;

        [Required]
        [MaxLength(1)]
        [JsonPropertyName("estado")]
        public string Estado { get; set; } = "A";
    }
}
