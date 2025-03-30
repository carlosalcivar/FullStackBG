using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ms_loguearusuario.Controllers.Type
{
    public class RequestLoginType
    {
        [JsonPropertyName("usuario")]
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_@.-]+$", ErrorMessage = "Usuario contiene caracteres inválidos.")]
        public string Usuario { get; set; } = string.Empty;

        [JsonPropertyName("password")]
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
