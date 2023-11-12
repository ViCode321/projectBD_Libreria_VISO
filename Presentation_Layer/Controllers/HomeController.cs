using Data_Layer;
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

        public ActionResult Search()
        {
            return View();
        }

        public ActionResult Sale()
        {
            return View();
        }

        public ActionResult Update()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Insert()
        {
            // Puedes necesitar cargar datos adicionales, como las listas de proveedores, marcas y categorías.
            return View();
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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}
