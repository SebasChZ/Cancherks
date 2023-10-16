
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
function loadInstallation(idSport) {
    console.log("Estoy en la funcion")
    $.ajax({
        url: '/Admin/BuscarInstalacion?handler=LoadInstallationbySport',
        type: 'GET',
        data: { idSport: idSport },
        success: function (data) {
            var tbody = $(".table tbody");

            $(".table tbody").empty();

            /*
            
             @foreach (var installation in Model.Installations)
            {
                <tr data-installation-id="@installation.Id">
                    <td>@installation.Name</td>
                    <td>@installation.Location</td>
                    <td>@installation.Description</td>
                    <td>
                        <img src="/img/@installation.Picture" alt="Imagen" width="50" height="50">
                    </td>
                    <td>@installation.MaxCantPeople</td>
                    <td>@installation.TimeSplitReservation</td>
                    <td>@(installation.isPublic ? "Sí" : "No")</td>
                    <td style="text-align: center;">
                        <a asp-page="/Admin/ModificarInstalacion" asp-route-id="@installation.Id">
                            <i class="bi bi-pencil-square custom-edit" style="cursor:pointer;"></i>
                        </a>
                    </td>
                </tr>
            }
            */
            var rowsIntallations = "";
            data.forEach(function (installation) {
                var row = "<tr data-installation-id='" + installation.idInstallation + "'>";
                row += "<td>" + installation.installationName + "</td>";
                row += "<td>" + installation.location + "</td>";
                row += "<td>" + installation.description + "</td>";
                row += "<td><img src='/img/" + installation.picture + "' alt='Imagen' width='50' height='50'></td>";
                row += "<td>" + installation.maxCantPeople + "</td>";
                row += "<td>" + installation.timeSplitReservation + "</td>";
                row += "<td>" + (installation.isPublic ? "Sí" : "No") + "</td>";
                row += "<td style='text-align: center;'><a href='/Admin/ModificarInstalacion/" + installation.idInstallation + "'><i class='bi bi-pencil-square custom-edit' style='cursor:pointer;'></i></a></td>";
                row += "</tr>";
                rowsIntallations = rowsIntallations + row;
                
            });
            tbody.html(rowsIntallations);


        },
        error: function (error) {
            console.log(error);
        }
    });
}

document.addEventListener('DOMContentLoaded', () => { // sure DOM is loaded
    const dropdownItems = document.querySelectorAll('.custom-dropdown-item');

    //refresing the dropdown button text
    dropdownItems.forEach(item => {
        item.addEventListener('click', function () {
            const selectedText = this.textContent || this.innerText; // get the text of the selected item
            document.getElementById('dropdownInstallations').textContent = selectedText; // change the text of the dropdown button

        });
    });

});