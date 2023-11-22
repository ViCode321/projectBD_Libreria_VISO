function editarProducto(id) {
    // Obtén los datos del producto
    var descripcion = document.querySelector(`td[data-id='${id}'][data-columna='Descripcion']`).innerText;
    var proveedor = document.querySelector(`td[data-id='${id}'][data-columna='Proveedor']`).innerText;
    var categoria = document.querySelector(`td[data-id='${id}'][data-columna='Categoria']`).innerText;
    var marca = document.querySelector(`td[data-id='${id}'][data-columna='Marca']`).innerText;
    var cantidad = document.querySelector(`td[data-id='${id}'][data-columna='Cantidad']`).innerText;
    var costo = document.querySelector(`td[data-id='${id}'][data-columna='Costo']`).innerText;
    var precio = document.querySelector(`td[data-id='${id}'][data-columna='Precio']`).innerText;
    // Rellena los campos de entrada en la ventana emergente con los datos del producto
    $('#inputDescripcion').val(descripcion);
    $('#inputProveedor').val(proveedor);
    $('#inputCategoria').val(categoria);
    $('#inputMarca').val(marca);
    $('#inputCantidad').val(cantidad);
    $('#inputCosto').val(costo);
    $('#inputPrecio').val(precio);
    // Muestra la ventana emergente
    $('#modalEditarProducto').modal('show');
}

// Asigna la función a los elementos correspondientes
$('#modalEditarProducto .close, #modalEditarProducto .btn-secondary').on('click', function () {
    // Oculta la ventana modal
    $('#modalEditarProducto').modal('hide');
});

// Manejar el envío del formulario
$('#editarProductoForm').on('submit', function (event) {
    event.preventDefault(); // Evitar el envío del formulario por defecto
    // Puedes agregar aquí la lógica para enviar los datos al servidor usando AJAX si es necesario
    // ...

    // Cerrar la ventana emergente
    $('#modalEditarProducto').modal('hide');
});