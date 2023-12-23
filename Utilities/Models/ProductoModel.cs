
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Utilities.Models
{
    public class ProductoModel
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("Id")]
        [Key]

        public int? ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("Productos")]
        public string PRODUCTOS { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("Marca")]
        public string MARCA { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("Proveedor")]
        public string PROVEEDOR { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("Precio")]
        public decimal? PRECIO { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("Iva")]
        public int? IVA { get; set; }
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
