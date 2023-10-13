
// Asegúrate de que el DOM esté cargado antes de agregar cualquier escuchador de eventos
document.addEventListener("DOMContentLoaded", function () {

    function validateForm() {
        // Ejemplo de validación:
        var name = document.getElementById("Name").value;
        if (!name) {
            Swal.fire({
                icon: 'error',
                title: '¡Error!',
                text: 'El nombre es obligatorio.',
                allowOutsideClick: false
            });
            return false;
        }

        // Agrega más validaciones según sea necesario...
        return true;
    }

    document.getElementById("acept").addEventListener("click", function (event) {
        // Prevenir el envío inmediato del formulario
        event.preventDefault();

        if (validateForm()) {
            Swal.fire({
                icon: 'success',
                title: '¡Todo correcto!',
                text: 'Formulario validado con éxito.',
                allowOutsideClick: false,
                showCancelButton: true,   // Muestra botón de cancelar
                confirmButtonText: 'Agregar',   // Texto del botón de confirmación
                cancelButtonText: 'Cancelar'   // Texto del botón de cancelación
            }).then((result) => {
                // Si el usuario confirma (hace clic en "Enviar"), envía el formulario
                if (result.isConfirmed) {
                    document.getElementById("installationForm").submit();
                }
            });
        }
    });
});

