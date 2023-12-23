using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Colegios.Api.Models
{
    public class DocenteModel
    {   /// <summary>
        /// Establece un codigo unico de identificacion por docente
        /// </summary>
        [JsonPropertyName("Id")]
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// Obtiene el nombre del docente
        /// </summary>
        [JsonPropertyName("Nombre")]
        public string NOMBRE { get; set; }
        /// <summary>
        /// Obtiene el numero de identificacion del docente
        /// </summary>
        [JsonPropertyName("Documento")]
        public string DOCUMENTO { get; set; }
        /// <summary>
        /// Obtiene el correo electronico del docente
        /// </summary>
        [JsonPropertyName("Correo")]
        public string CORREO { get; set; }
      

    }
}
