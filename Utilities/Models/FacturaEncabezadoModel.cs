
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Utilities.Models
{
    public class FacturaEncabezadoModel
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("IdFactura")]
        [Key]
        public int? ID_FACTURA { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("NumeroFactura")]
        public string NUMERO_FACTURA { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("Idcliente")]
        public int? ID_CLIENTE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("FechaFactura")]
        public DateTime? FECHA_FACTURA { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("FormaPago")]
        public string? FORMA_PAGO { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("ValorBase")]
        public decimal? VALOR_BASE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("IMPUESTO")]
        public decimal? IMPUESTO { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("Estado")]
        public string ESTADO { get; set; }
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
