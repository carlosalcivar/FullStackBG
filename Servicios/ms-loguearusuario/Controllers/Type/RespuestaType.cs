using System.Text.Json.Serialization;

namespace ms_loguearusuario.Controllers.Type
{
    public class RespuestaType
    {
        [JsonPropertyName("codigo")]
        public int Codigo { get; set; }

        [JsonPropertyName("descripcion")]
        public string? Descripcion { get; set; }
    }
}
