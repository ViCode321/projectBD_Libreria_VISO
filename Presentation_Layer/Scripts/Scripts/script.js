function validateSearchForm() {
    var searchTerm = document.getElementById("searchTerm").value;
    var searchBy = document.querySelector('input[name="searchBy"]:checked');

    if (!searchTerm || !searchBy) {
        showAlert("Por favor, ingrese un término de búsqueda y seleccione un criterio.");
        return false; // Evita que se envíe el formulario si falta información.
    }

    return true; // Permite que el formulario se envíe si todo está bien.
}

function showAlert(message) {
    var alertDiv = document.getElementById("alertMessages");
    alertDiv.innerHTML = '<div class="alert alert-warning alert-dismissible fade show" role="alert">' +
        message +
        '<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button></div>';
}
