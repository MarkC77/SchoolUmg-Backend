using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Models
{
    public class Proveedores
    {
        public int idproveedor { get; set; }
        public string descripcion { get; set; }
        public int nit { get; set; }
        public string? direccion { get; set; }
        public string? estado { get; set; }
    }
}
