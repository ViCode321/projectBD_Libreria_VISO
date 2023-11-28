using Data_Layer;
using Microsoft.Ajax.Utilities;
using Model_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Presentation_Layer.Controllers
{
    public class HomeController : Controller
    {
        private Get _getproduct;
        private Update _updateproduct;
        private Insert _insertproduct;
        private Sale _insertsale;

        public HomeController()
        {
            _getproduct = new Get();
            _updateproduct = new Update();
            _insertproduct = new Insert();
            _insertsale = new Sale();
        }

        public ActionResult Index()
        {
            return View();
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
                // Obtener IDs de proveedor, marca y categoría por nombre
                int proveedorId = _getproduct.ObtenerProveedorIdPorNombre(proveedor);
                int marcaId = _getproduct.ObtenerMarcaIdPorNombre(marca);
                int categoriaId = _getproduct.ObtenerCategoriaIdPorNombre(categoria);

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
                ViewBag.SuccessMessage = "Producto actualizado con éxito.";

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

            return View("Update", productos);
        }

        [HttpPost]
        public ActionResult UpdateProduct(int id, string name, string proveedor, string categoria, string marca, int cantidad, decimal costo, decimal precio)
        {
            try
            {
                string errorMessage = _updateproduct.ActualizarProductoSearch(id, name, proveedor, categoria, marca, cantidad, costo, precio);

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    // Si hay un error, mostrarlo en la vista
                    ViewBag.Error = "Proveedor, marca o categoría no existe en la base de datos.";
                    return View("Update");
                }
                // Obtener IDs de proveedor, marca y categoría por nombre
                int proveedorId = _getproduct.ObtenerProveedorIdPorNombre(proveedor);
                int marcaId = _getproduct.ObtenerMarcaIdPorNombre(marca);
                int categoriaId = _getproduct.ObtenerCategoriaIdPorNombre(categoria);

                // Validar existencia de proveedor, marca y categoría antes de continuar
                if (proveedorId == 0 || marcaId == 0 || categoriaId == 0)
                {
                    ViewBag.Error = "Proveedor, marca o categoría no existe en la base de datos.";
                    return View("Error");
                }

                // Llamar a la función ActualizarProductoSearch con los parámetros necesarios
                _updateproduct.ActualizarProductoSearch(id, name, proveedor, categoria, marca, cantidad, costo, precio);

                // Obtener los productos actualizados después de la actualización
                List<Product> productosActualizados = _getproduct.ObtenerTodosLosProductos();
                ViewBag.SuccessMessage = "Producto actualizado con éxito.";

                return View("Update", productosActualizados); // Devuelve la vista de búsqueda con los resultados actualizados
            }
            catch (Exception ex)
            {
                // Log y manejo de excepciones generales
                ViewBag.ErrorMessage = "Error al actualizar el producto.";
                ViewBag.ErrorDetails = ex.Message;
                return View("Error");
            }
        }

        public ActionResult Insert()
        {
            List<Product> productos = _getproduct.ObtenerTodosLosProductos();

            return View("Insert", productos);
        }

        [HttpPost]
        public ActionResult InsertProduct(int? id, string name, string proveedor, string categoria, string marca, int? cantidad, decimal? costo, decimal? precio)
        {
            try
            {
                // Verificar si el producto ya existe
                bool productoExistente = _getproduct.ActualizarProducto(name);

                if (productoExistente)
                {
                    int productoid = _getproduct.ObtenerProductoIdPorDescripcion(name);
                    // El producto ya existe, realizar la actualización
                    _updateproduct.ActualizarProductoSearch(productoid, name, proveedor, categoria, marca, (int)cantidad, (decimal)costo, (decimal)precio);

                    _updateproduct.ActualizarProductoSearch((int)id, name, proveedor, categoria, marca, (int)cantidad, (decimal)costo, (decimal)precio);
                    ViewBag.SuccessMessage = "Producto actualizado con éxito.";
                }
                else
                {
                    // El producto no existe, realizar la inserción
                    string errorMessage = _insertproduct.InsertarProducto((int)id, name, proveedor, categoria, marca, (int)cantidad, (decimal)costo, (decimal)precio);

                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        // Si hay un error, mostrarlo en la vista
                        ViewBag.Error = "Proveedor, marca o categoría no existe en la base de datos.";
                        return View("Insert");
                    }

                    // Obtener IDs de proveedor, marca y categoría por nombre
                    int proveedorId = _getproduct.ObtenerProveedorIdPorNombre(proveedor);
                    int marcaId = _getproduct.ObtenerMarcaIdPorNombre(marca);
                    int categoriaId = _getproduct.ObtenerCategoriaIdPorNombre(categoria);

                    // Validar existencia de proveedor, marca y categoría antes de continuar
                    if (proveedorId == 0 || marcaId == 0 || categoriaId == 0)
                    {
                        ViewBag.Error = "Proveedor, marca o categoría no existe en la base de datos.";
                        return View("Error");
                    }

                    ViewBag.SuccessMessage = "Producto insertado con éxito.";
                }

                // Obtener los productos actualizados después de la inserción o actualización
                List<Product> productosActualizados = _getproduct.ObtenerTodosLosProductos();

                return View("Insert", productosActualizados); // Devuelve la vista de inserción con los resultados actualizados
            }
            catch (Exception ex)
            {
                // Log y manejo de excepciones generales
                ViewBag.ErrorMessage = "Error al insertar o actualizar el producto.";
                ViewBag.ErrorDetails = ex.Message;
                return View("Error");
            }
        }

        public ActionResult CargarProductos()
        {
            // Obtener datos para llenar los selects
            var productos = _getproduct.ObtenerTodosNombresDeLosProductos();

            // Convierte la lista de productos a SelectListItems
            var productosSelectList = productos.Select(p => new SelectListItem { Value = p.Producto, Text = p.Producto }).ToList();

            // Enviar datos a la vista
            ViewBag.Producto = new SelectList(productosSelectList, "Value", "Text");

            return View();
        }

        public ActionResult ObtenerPrecioDeProducto(string name)
        {
            try
            {
                // Llamada a tu función para obtener el precio del producto
                int precio = _getproduct.ObtenerPrescioDeProducto(name);

                // Devolver el precio como un resultado JSON con JsonRequestBehavior.AllowGet
                return Json(new { precio }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Manejar excepciones y devolver un mensaje de error
                return Json(new { error = "Error al obtener el precio del producto. Detalles: " + ex.ToString() });
            }
        }

        public ActionResult Sale()
        {
            List<Venta> ventas = _getproduct.ObtenerTodasLasVentas();
            CargarProductos();
            return View("Sale", ventas);
        }

        [HttpPost]
        public ActionResult InsertSale(Venta venta)
        {
            try
            {
                if (string.IsNullOrEmpty(venta.Producto))
                {
                    // Manejar el caso en que venta.Producto sea nulo o vacío
                    ViewBag.Error = "La descripción del producto no puede estar vacía.";
                    return View("Sale", _getproduct.ObtenerTodasLasVentas());
                }

                int productoId = _getproduct.ObtenerProductoIdPorDescripcion(venta.Producto);
                // Validar la existencia del producto
                if (!_getproduct.ExisteProducto(productoId))
                {
                    ViewBag.Error = "El producto especificado no existe.";
                    return View("Sale", _getproduct.ObtenerTodasLasVentas());
                }
                                

                // Ahora puedes llamar a la función InsertarVenta
                _insertsale.InsertarVenta(venta);
                // Después de la inserción, redireccionar a la vista de ventas actualizada
                List<Venta> ventas = _getproduct.ObtenerTodasLasVentas();
                ViewBag.SuccessMessage = "Venta insertada con éxito.";
                return View("Sale", ventas);
            }
            catch (Exception ex)
            {
                // Manejar el error y mostrar un mensaje detallado
                ViewBag.Error = "Error al insertar la venta. Detalles: " + ex.ToString();
                return View("Sale", _getproduct.ObtenerTodasLasVentas());
            }
        }
    
    }
}