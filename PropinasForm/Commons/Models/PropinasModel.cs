using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.Models
{
    public class PropinasModel
    {
        public int? Id { get; set; }
        public int? IdMesero { get; set; }
        public int? IdMesa { get; set; }
        public double Valor { get; set; }
        public DateTime? Fecha { get; set; }
    }
}
