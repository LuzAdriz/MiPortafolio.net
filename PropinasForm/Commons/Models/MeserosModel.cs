using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.Models
{
    public class MeserosModel
    {
        public int? Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Cedula { get; set; }
        public string Correo { get; set; }
        public string NombresCompletos { get { return $"{this.Apellidos} {this.Nombres}"; } }
    }
}
