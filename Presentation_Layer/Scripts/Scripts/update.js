function editarProducto(button) {
    // Desplaza la página hacia arriba
    window.scrollTo({ top: 0, behavior: 'smooth' });

    // Obtén los valores del atributo data-
    var descripcion = button.getAttribute("data-descripcion");
    var proveedor = button.getAttribute("data-proveedor");
    var categoria = button.getAttribute("data-categoria");
    var marca = button.getAttribute("data-marca");
    var cantidad = button.getAttribute("data-cantidad");
    var costo = button.getAttribute("data-costo");
    var precio = button.getAttribute("data-precio");

    // Rellena los campos del formulario con los valores obtenidos
    document.getElementById('name').value = descripcion;

    // Cambia el valor seleccionado de los elementos select
    document.getElementById('proveedor').value = proveedor;
    document.getElementById('categoria').value = categoria;
    document.getElementById('marca').value = marca;

    document.getElementById('cantidad').value = cantidad;
    document.getElementById('costo').value = costo;
    document.getElementById('precio').value = precio;

    // Calcula el total y actualiza el campo correspondiente
    var total = parseFloat(cantidad) * parseFloat(precio);
    document.getElementById('total').value = total;

    // Muestra el formulario de edición
    $('#editarProductoForm').modal('show');
}