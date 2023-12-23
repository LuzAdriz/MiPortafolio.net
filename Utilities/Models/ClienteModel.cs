
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Utilities.Models
{
    public class ClienteModel
    {  
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("IdCliente")]
        [Key]
        public int? ID_CLIENTE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("Nit")]
        public int? NIT { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("RazonSocial")]
        public string RAZON_SOCIAL { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("Telefono")]
        public string TELEFONO { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("Email")]
        public string EMAIL { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("Direccion")]
        public string DIRECCION { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("Usuario")]
        public string USUARIO { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("FechaLog")]
        public DateTime? FECHA_LOG { get; set; }

    }
}
