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

    // Asignar el ID del producto al campo oculto
    $('#productoId').val(id);

    // Mostrar el modal de edición
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

    // Obtener los datos del formulario
    var id = $('#productoId').val();
    var descripcion = $('#inputDescripcion').val();
    var proveedor = $('#inputProveedor').val();
    var categoria = $('#inputCategoria').val();
    var marca = $('#inputMarca').val();
    var cantidad = $('#inputCantidad').val();
    var costo = $('#inputCosto').val();
    var precio = $('#inputPrecio').val();

    // Realizar la llamada AJAX para actualizar el producto
    $.ajax({
        url: '@Url.Action("ActualizarProducto", "Home")',
        type: 'POST',
        data: {
            id: id,
            descripcion: descripcion,
            proveedor: proveedor,
            categoria: categoria,
            marca: marca,
            cantidad: cantidad,
            costo: costo,
            precio: precio
        },
        success: function (result) {
            if (result.success) {
                // Actualización exitosa
                alert('Producto actualizado correctamente.');
                // Cerrar el modal de edición
                $('#modalEditarProducto').modal('hide');
                // Puedes recargar la página o actualizar solo la fila modificada según tus necesidades
                // window.location.reload();
            } else {
                // Manejar el caso en que la actualización no fue exitosa
                alert('Error al actualizar el producto: ' + result.message);
            }
        },
        error: function () {
            alert('Error al actualizar el producto.');
        }
    });

    // Cerrar la ventana emergente
    $('#modalEditarProducto').modal('hide');
});
