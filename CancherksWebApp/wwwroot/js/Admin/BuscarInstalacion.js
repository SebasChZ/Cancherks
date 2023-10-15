
// Asegúrate de que el DOM esté cargado antes de agregar cualquier escuchador de eventos

// Obtener horas

function updateTimes() {
    // Obtener los valores de los inputs
    var startTime = document.getElementById('startTime').value;
    var endTime = document.getElementById('endTime').value;

    // Ya que los timepickers retornan el tiempo en formato HH:MM (que es en sí mismo un formato de 24 horas),
    // no necesitas hacer una conversión adicional. Solo imprime o utiliza los valores:
    console.log("Hora Inicio:", startTime);
    console.log("Hora Final:", endTime);

    // Si necesitas hacer algo adicional con estos valores, puedes hacerlo aquí.
}
$(document).ready(function () {
    $('.custom-dropdown-item').click(function (e) {
        e.preventDefault();

        let sportId = $(this).data('id');
        loadInstallationsBySport(sportId);
    });
});