using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Layer
{
    public class Productinfo
    {
    public int Id { get; set; }
    public int ProveedorId { get; set; }
    public string Nombre { get; set; }
    public string ProveedorNombre { get; set; }
    public string CategoriaNombre { get; set; }    
    }
}
