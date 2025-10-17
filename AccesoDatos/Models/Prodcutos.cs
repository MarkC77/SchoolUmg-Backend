using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Models;
public class Productos
{
    public int idproducto { get; set; }
    public string descripcion { get; set; }
    public int stock { get; set; }
    public decimal? precioventa { get; set; }
    public int? idcategoria { get; set; }
    public DateTime? fechaingreso { get; set; }
    public DateTime? fechacaducidad { get; set; }
}

