using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mi_formulario.Models
{
    public class DocenteModel
    {
        public int ID { get; set; }
        /// <summary>
        /// Obtiene el nombre del docente
        /// </summary>
        public string NOMBRE { get; set; }
        /// <summary>
        /// Obtiene el numero de identificacion del docente
        /// </summary>
        public string DOCUMENTO { get; set; }
        /// <summary>
        /// Obtiene el correo electronico del docente
        /// </summary>
        public string CORREO { get; set; }
    }
}
