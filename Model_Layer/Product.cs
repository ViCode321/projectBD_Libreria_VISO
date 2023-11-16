using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Layer
{
    // En el Model Layer
    public class Product
    {        
        //Códigos
        public int Id { get; set; }
        public int ProveedorId { get; set; }
        public int CategoriaId { get; set; }
        public int MarcaId { get; set; }

        [Required(ErrorMessage = "El campo Proveedor es obligatorio.")]
        public string ProveedorNombre { get; set; }

        [Required(ErrorMessage = "El campo Descripción es obligatorio")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo Categoria es obligatorio")]
        public string CategoriaNombre { get; set; }

        [Required(ErrorMessage = "El campo Marca es obligatorio")]
        public string MarcaNombre { get; set; }

        [Required(ErrorMessage = "El campo Cantidad es obligatorio.")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El campo Costo es obligatorio.")]
        public decimal Costo { get; set; }

        [Required(ErrorMessage = "El campo Precio es obligatorio.")]
        public decimal Precio { get; set; }
    }       
}
