using Data_Layer;
using Microsoft.Ajax.Utilities;
using Model_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation_Layer.Controllers
{
    public class HomeController : Controller
    {
        private Insert _insertproduct;

        public HomeController()
        {
            _insertproduct = new Insert();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sale()
        {
            Sales sale = new Sales();
            List<Venta> ventas = sale.ObtenerTodasLasVentas();

            return View("Sale", ventas);
        }

        public ActionResult Search()
        {
            // Al cargar la página, obtén todos los productos
            Search search = new Search();
            List<Product> productos = search.ObtenerTodosLosProductos();

            // Pasa la lista de productos a la vista
            return View("Search", productos);
        }

        [HttpPost]
        public ActionResult Search(string searchTerm, string searchBy)
        {
            // Verificar si searchBy no tiene un valor válido
            if (string.IsNullOrEmpty(searchBy))
            {
                ViewBag.NoResultsMessage = "Por favor, selecciona un método de búsqueda.";
                return View();  // Regresar a la vista de búsqueda
            }

            Search search = new Search();
            List<Product> productos = search.BuscarProducto(searchTerm, searchBy);

            // Comprobar si hay resultados
            if (productos.Count == 0)
            {
                ViewBag.NoResultsMessage = "No se encontraron resultados.";
            }

            return View("Search", productos); // Devuelve la vista de búsqueda con los resultados
        }

        public ActionResult GetAllProducts()
        {
            Search search = new Search();
            List<Product> productos = search.ObtenerTodosLosProductos();

            return View("Search", productos);
        }

        public ActionResult Update()
        {
            Search search = new Search();
            List<Product> productos = search.ObtenerTodosLosProductos();

            return View("Update", productos);
        }


        public ActionResult Insert()
        {
            Search search = new Search();
            List<Product> productos = search.ObtenerTodosLosProductos();

            return View("Insert", productos);
        }

        [HttpPost]
        public ActionResult Insert(Product product)
        {
            if (ModelState.IsValid)
            {
                // Traduce el valor seleccionado del proveedor al ID correspondiente
                if (product.ProveedorId == 1)
                {
                    product.ProveedorId = 1;
                }
                else
                {
                    product.ProveedorId = 2;
                }

                // Realiza la inserción en la base de datos
                _insertproduct.InsertarProducto(product);
                // Redirige a la vista de éxito o a la misma vista de inserción.
                return RedirectToAction("Insert");
            }
            // Si hay errores de validación, regresa a la vista de inserción con los errores.
            return View(product);
        }
        
        [HttpPost]
        public ActionResult ActualizarProducto(int id, string descripcion, string proveedor, string categoria, string marca, int cantidad, decimal costo, decimal precio)
        {
            try
            {
                // Obtener el producto de la base de datos
                Search search = new Search();
                Product producto = search.ObtenerProductoPorId(id);

                // Actualizar los campos
                producto.Descripcion = descripcion;
                producto.ProveedorNombre = proveedor;
                producto.CategoriaNombre = categoria;
                producto.MarcaNombre = marca;
                producto.Cantidad = cantidad;
                producto.Costo = costo;
                producto.Precio = precio;

                // Guardar los cambios en la base de datos
                _insertproduct.ActualizarProducto(producto);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Manejar errores según tus necesidades
                return Json(new { success = false, message = ex.Message });
            }
        }

    }
}
