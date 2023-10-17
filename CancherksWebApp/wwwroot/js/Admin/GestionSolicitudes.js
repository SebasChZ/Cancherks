
// Asegúrate de que el DOM esté cargado antes de agregar cualquier escuchador de eventos

// Obtener horas

//Alert before acepting request

document.addEventListener('DOMContentLoaded', function () {
    var successIcons = document.querySelectorAll('.custom-success');
    successIcons.forEach(function (icon) {
        icon.addEventListener('click', function () {
            var requestId = this.getAttribute('data-id');
            document.getElementById('idSolicitud').value = requestId;
            var aceptarModal = new bootstrap.Modal(document.getElementById('acept'));
            aceptarModal.show();
        });
    });
});


//Alert before rejecting  request

document.addEventListener('DOMContentLoaded', function () {
    var rejectIcons = document.querySelectorAll('.custom-danger');
    rejectIcons.forEach(function (icon) {
        icon.addEventListener('click', function () {
            var requestId = this.getAttribute('data-id');
            document.getElementById('idSolicitudReject').value = requestId;
            var rejectModal = new bootstrap.Modal(document.getElementById('reject'));
            rejectModal.show();
        });
    });
});



//Filter Data
document.querySelectorAll('.custom-dropdown-item').forEach(item => {
    item.addEventListener('click', function () {
        var installationId = this.getAttribute('data-id');
        filterTable(installationId);
    });
});

function filterTable(installationId) {
    var tableRows = document.querySelectorAll('.table tbody tr');
    tableRows.forEach(row => {
        var rowInstallationId = row.getAttribute('data-installation-id');
        if (installationId === "0" || rowInstallationId === installationId) {
            row.style.display = '';  // Mostrar todas las filas si se selecciona "Todas las instalaciones" o si coincide con la instalación seleccionada
        } else {
            row.style.display = 'none';
        }
    });
}

//Change placeholder when user selects option
document.addEventListener('DOMContentLoaded', () => { // Asegura que el DOM está completamente cargado
    const dropdownItems = document.querySelectorAll('.custom-dropdown-item');

    dropdownItems.forEach(item => {
        item.addEventListener('click', function () {
            const selectedText = this.textContent || this.innerText; // Obtiene el texto del item seleccionado
            document.getElementById('dropdownMenuButton').textContent = selectedText; // Cambia el texto del botón
            const installationId = this.getAttribute('data-id'); // Obtiene el ID de la instalación
            filterTable(installationId); // Llama a la función para filtrar la tabla
        });
    });
});
 


