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
        private Get _getproduct;
        private Update _updateproduct;
        private Insert _insertproduct;

        public HomeController()
        {
            _getproduct = new Get();
            _updateproduct = new Update();
            _insertproduct = new Insert();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sale()
        {
            //Get obtener = new Get();
            List<Venta> ventas = _getproduct.ObtenerTodasLasVentas();
//            Sales sale = new Sales();
//          List<Venta> ventas = sale.ObtenerTodasLasVentas();

            return View("Sale", ventas);
        }

        public ActionResult Search()
        {
            List<Product> productos = _getproduct.ObtenerTodosLosProductos();
            //CargarItems();
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

        [HttpPost]
        public ActionResult ActualizarProducto(int id, string descripcion, string proveedor, string categoria, string marca, int cantidad, decimal costo, decimal precio)
        {
            try
            {
                string errorMessage = _updateproduct.ActualizarProductoSearch(id, descripcion, proveedor, categoria, marca, cantidad, costo, precio);

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    // Si hay un error, mostrarlo en la vista
                    ViewBag.Error = "Proveedor, marca o categoría no existe en la base de datos.";
                    return View("Search");
                }

                // Obtener IDs de proveedor, marca y categoría por nombre
                int proveedorId = _updateproduct.ObtenerProveedorIdPorNombre(proveedor);
                int marcaId = _updateproduct.ObtenerMarcaIdPorNombre(marca);
                int categoriaId = _updateproduct.ObtenerCategoriaIdPorNombre(categoria);

                // Validar existencia de proveedor, marca y categoría antes de continuar
                if (proveedorId == 0 || marcaId == 0 || categoriaId == 0)
                {
                    ViewBag.Error = "Proveedor, marca o categoría no existe en la base de datos.";
                    return View("Error");
                }

                // Llamar a la función ActualizarProductoSearch con los parámetros necesarios
                _updateproduct.ActualizarProductoSearch(id, descripcion, proveedor, categoria, marca, cantidad, costo, precio);

                // Obtener los productos actualizados después de la actualización
                List<Product> productosActualizados = _getproduct.ObtenerTodosLosProductos();

                return View("Search", productosActualizados); // Devuelve la vista de búsqueda con los resultados actualizados
            }
            catch (Exception ex)
            {
                // Log y manejo de excepciones generales
                ViewBag.ErrorMessage = "Error al actualizar el producto.";
                ViewBag.ErrorDetails = ex.Message;
                return View("Error");
            }
        }

        /*public ActionResult CargarItems()
        {
            // Obtener datos para llenar los selects
            var categorias = _getproduct.ObtenerCategorias();
            var marcas = _getproduct.ObtenerMarcas();
            var proveedores = _getproduct.ObtenerProveedores();

            // Enviar datos a la vista
            ViewBag.Categorias = new SelectList(categorias, "Id", "Nombre");
            ViewBag.Marcas = new SelectList(marcas, "Id", "Nombre");
            ViewBag.Proveedores = new SelectList(proveedores, "Id", "Nombre");

            // Otras lógicas y devolución de la vista...
            return View();
        }*/


        public ActionResult GetAllProducts()
        {
            //Search search = new Search();
            //Get obtener = new Get();
            List<Product> productos = _getproduct.ObtenerTodosLosProductos();

            return View("Search", productos);
        }

        public ActionResult Update()
        {
            //Search search = new Search();
            List<Product> productos = _getproduct.ObtenerTodosLosProductos();

            return View("Search", productos);
        }
        
        /*[HttpPost]
        public ActionResult UpdateProduct(Product product)
        {
            _updateproduct = new Update();

            if (ModelState.IsValid)
            {
                _updateproduct.ActualizarProducto(product);
                return Json(new { success = true });
            }

            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
        }*/

        public ActionResult Insert()
        {
            //Search search = new Search();
            //Get obtener = new Get();
            List<Product> productos = _getproduct.ObtenerTodosLosProductos();

            return View("Insert", productos);
        }
        [HttpPost]
        public ActionResult Insert(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ProveedorId == 1)
                {
                    product.ProveedorId = 1;
                }
                else
                {
                    product.ProveedorId = 2;
                }
                _insertproduct.InsertarProducto(product);
                return RedirectToAction("Insert");
            }
            return View(product);
        }

    }
}
