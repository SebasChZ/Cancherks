var listInstallations = []

function loadData(idSport) {
    $.ajax({
        url: '/Solicitante/Reservacion?handler=LoadData',
        type: 'GET',
        data: { idSport: idSport },
        success: function (data) {

            // Clear existing cards
            $('#gridDiv').empty();
            listInstallations = data
            // Loop through the returned data and create cards
            data.forEach(function (item) {
                
                var card = `
                         <div class="col-6" style="max-width: 640px;">
                            <div class="card mb-3">
                                <div class="row g-0">
                                        <div class="col-md-4">
                                                <img src="/img/${item.picture}" class="img-fluid rounded-start" alt="...">
                                        </div>
                                        <div class="col-md-8">
                                            <div class="card-body">
                                                    <h5 class="card-title font-weight-bold ">${item.installationName}</h5>
                                                    <p class="card-text mb-0">${item.description}</p>
                                                    <p class="card-text mb-1 text-primary"><i class="bi bi-geo-fill"></i> ${item.location}</p>
                                                    <button type="button" class="consult btn btn-outline-primary" onclick="checkAviability(${item.idInstallation} )">Consultar Disponibilidad</button>

                                            </div>
                                        </div>
                                </div>
                            </div>
                        </div>
                        `;
                $('#gridDiv').append(card);
            });
        },
        error: function (error) {
            console.log(error);
        }
    });
}
function checkAviability(idInstallation) {
    var dateValue = document.getElementById("datepickerQuery").value;
    console.log(dateValue)
    
    if (dateValue == "") {
        alert("Are you estupid you should select a date");
    } else {
        var dt = new Date(dateValue);
        var day = dt.getDay()

        if (day == 0) {
            day = 7
        }
        console.log(day)
        loadDataReservationInfo(idInstallation, day)
        loadReservation(idInstallation)
    }

}

function loadReservation(idInstallation)
{
    var reservationCard = document.getElementById("reservationCard");
    reservationCard.classList.remove("visible-nd");
    reservationCard.classList.add("visible");

    var listSchedule = document.getElementById("listSchedule");
    listSchedule.classList.remove("visible-nd");
    listSchedule.classList.add("visible");

    listInstallations.forEach(function (item) {
        if (item.idInstallation == idInstallation) {
            console.log(item)
            document.getElementById("installationName").innerHTML = item.installationName;
            document.getElementById("description").innerHTML = item.description;
            document.getElementById("location").innerHTML = item.location;
            document.getElementById("picture").src = "/img/" + item.picture;
        }
    }); 

}



function loadDataReservationInfo(idInstallation, idDay) {
    $.ajax({
        url: '/Solicitante/Reservacion?handler=LoadDataInfo',
        type: 'GET',
        data: { idInstallation: idInstallation, idDay: idDay },
        success: function (data) {
            console.log(data);
            // Clear existing cards


            // Loop through the returned data and create cards
            data.forEach(function (item) {
                console.log(item)
                var card = `
                         <div class="col-6" style="max-width: 640px;">
                            <div class="card mb-3">
                                <div class="row g-0">
                                        <div class="col-md-4">
                                                <img src="/img/${item.picture}" class="img-fluid rounded-start" alt="...">
                                        </div>
                                        <div class="col-md-8">
                                            <div class="card-body">
                                                    <h5 class="card-title font-weight-bold ">${item.installationName}</h5>
                                                <p class="card-text mb-0">${item.description}</p>
                                                <p class="card-text mb-1 text-primary"><i class="bi bi-geo-fill"></i> ${item.location}</p>
                                                   <!-- Hidden Inputs -->

                                                <input type="hidden" id="maxCantPeople_${item.idInstallation}" value="${item.maxCantPeople}">
                                                <input type="hidden" id="timeSplitReservation_${item.idInstallation}" value="${item.timeSplitReservation}">
                                                <input type="hidden" id="idSport_${item.idInstallation}" value="${item.idSport}">

                                                     <form method="post">

                                                               <button type="button" class="consult btn btn-outline-primary" onclick="loadReservationInfo()">Consultar Disponibilidad</button>
                                                    </form>
                                            </div>
                                        </div>
                                </div>
                            </div>
                        </div>
                        `;
                $('#gridDiv').append(card);
            });
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

    loadData(0) //inicialice the load data function

    HTMLInputElementObject.addEventListener('input', function (evt) {
        something(this.value);
    });
});

