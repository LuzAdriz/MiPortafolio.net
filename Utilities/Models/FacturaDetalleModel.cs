
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Utilities.Models
{
    public class FacturaDetalleModel
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("IdDetalleFactura")]
        [Key]
        public int? ID_DETALLE_FACTURA { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("IdFactura")]
        public int? ID_FACTURA { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("IdProducto")]
        public int? ID_PRODUCTO { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("Cantidad")]
        public int? CANTIDAD { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("ValorUnitario")]
        public decimal? VALOR_UNITARIO { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("ValorBase")]
        public decimal? VALOR_BASE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("Iva")]
        public int? IVA { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("ValorIva")]
        public decimal? VALOR_IVA { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("ValorTotal")]
        public decimal? VALOR_TOTAL { get; set; }
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
