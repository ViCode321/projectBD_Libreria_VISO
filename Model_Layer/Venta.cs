using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Layer
{
    public class Venta
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Cliente es obligatorio.")]
        public string Cliente { get; set; }

        [Required(ErrorMessage = "El campo Fecha es obligatorio.")]
        public string Fecha { get; set; }

        [Required(ErrorMessage = "El campo Producto es obligatorio.")]
        public string Producto { get; set; }

        [Required(ErrorMessage = "El campo Cantidad es obligatorio.")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El campo Moneda es obligatorio.")]
        public string TipoMoneda { get; set; }

        [Required(ErrorMessage = "El campo Monto es obligatorio.")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "El campo Total es obligatorio.")]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "El campo Final es obligatorio.")]
        public decimal Final { get; set; }


    }
}
