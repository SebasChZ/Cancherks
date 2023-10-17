var listInstallations = []
var insllationSelected = 0
var day
var intemScheduleSelected = 0
var starTime
var endTime
var date

//------------------------------------------------------------------------------------------//
//                                      functionalities                                     //
//------------------------------------------------------------------------------------------//
function convertFromESToDBFormat(dateString) {
    var parts = dateString.split('/');

    return `${parts[2]}/${parts[1]}/${parts[0]}`;
}

function convertToEnglishFormat(dateString) {
    var parts = dateString.split('/');
    return `${parts[1]}/${parts[0]}/${parts[2]}`;
}



//------------------------------------------------------------------------------------------//
//                                      InstallationGrid                                    //
//------------------------------------------------------------------------------------------//
//clear installation list
function clearInstallationList() {
    var gridDiv = document.getElementById("gridDiv");
    var installations = gridDiv.getElementsByClassName("installation-view");

    for (var i = 0; i < installations.length; i++) {
        //remove the property to the clicked element
        installations[i].classList.remove("installationSelected")
    }
}
function convertToParagraphs(inputStr) {
    let parts = inputStr.split('\n');  // Split the string at each newline character
    let result = '';
    for (let part of parts) {
        result += '<p>' + part + '</p>';
    }
    return result;
}


function loadInstallation(idSport) {

    
    $.ajax({
        url: '/Solicitante/Reservacion?handler=LoadInstallationbySport',
        type: 'GET',
        data: { idSport: idSport },
        success: function (data) {

            // Clear existing cards
            $('#gridDiv').empty();
            listInstallations = data
            // Loop through the returned data and create cards
            data.forEach(function (item) {
                if (item.isPublic == 0) {

                    let outputHtml = convertToParagraphs(item.description);
                    console.log(outputHtml);

                    var card = $('<button></button>')
                        .addClass('col-6 p-0 consult installation-view div-button align-items-center')
                        .css('max-width', '640px')
                        .data('installation-id', item.idInstallation) // Store the idInstallation in the element's data
                        .append(
                            $('<div></div>').append(
                                $('<div></div>').addClass('row g-0').append(
                                    $('<div></div>').addClass('col-md-4').append(
                                        $('<img>')
                                            .attr('src', '/img/' + item.picture)
                                            .attr('alt', '...')
                                            .addClass('img-fluid rounded-start')
                                    ),
                                    $('<div></div>').addClass('col-md-8').append(
                                        $('<div></div>').addClass('card-body').append(
                                            $('<h5></h5>')
                                                .addClass('card-title font-weight-bold')
                                                .text(item.installationName),
                                            $('<p></p>')
                                                .addClass('card-text mb-0')
                                                .text(item.description),
                                            $('<p></p>')
                                                .addClass('card-text mb-1 text-primary')
                                                .html('<i class="bi bi-geo-fill"></i> ' + item.location)
                                        )
                                    )
                                )
                            )
                        );

                    card.on('click', function () {
                        checkAviability($(this).data('installation-id'), this);
                    });
                }
                

                $('#gridDiv').append(card);
            });


        },
        error: function (error) {
            console.log(error);
        }
    });
}

//Checks and load the schudele aviability of the installation, it's the list of time aviable
function checkAviability(idInstallation, installationDivCliked) {
    var dateValue = document.getElementById("datepickerQuery").value;
    var dateValueDB = convertFromESToDBFormat(dateValue);

    insllationSelected = idInstallation
    clearReservationCard() //clear the reservation card sumary
    //view the installation selected
    clearInstallationList()
    installationDivCliked.classList.add("installationSelected")

    if (dateValue != "") {

        //conver to mm-dd-yyyy
        var dateEnglish = convertToEnglishFormat(dateValue)
        var dt = new Date(dateEnglish);
        var day = dt.getDay()

        if (day == 0) {
            day = 7
        }

        loadDataScheduleAviable(idInstallation, day, dateValueDB)
    }

}

//------------------------------------------------------------------------------------------//
//                                      Schedule Availability                               //
//------------------------------------------------------------------------------------------//

//Clear the list of  schedures aviability
function clearListAviability() {
    var listSchedule = document.getElementById("listSchedule");
    var buttons = listSchedule.getElementsByTagName("button");

    for (var i = 0; i < buttons.length; i++) {
        //remove the property to the clicked element
        buttons[i].classList.remove("active")
        buttons[i].removeAttribute("aria-current")
    }
}


//Load the schedule and check the aviability of the installation, also call the load reservation card when a button is clicked
function loadDataScheduleAviable(idInstallation, idDay, date) {
    clearReservationCard()
    

    console.log("esta en la primera vez que entra")
    $.ajax({
        url: '/Solicitante/Reservacion?handler=LoadSchudeuleAviableInfo',
        type: 'GET',
        data: { idInstallation: idInstallation, idDay: idDay, date: date },
        success: function (data) {
            console.log("esta en la segunda vez que entra")
            // Clear existing cards
            $('#listSchedule').empty();
            // Loop through the returned data and create cards
            data.forEach(function (item) {

                var button = $('<button></button>')
                    .attr('type', 'button')
                    .addClass('list-group-item list-group-item-action')
                    .html(item.startTime + " - " + item.endTime);
                //validate is the item is available
                if (!item.isAvailable) {
                    button.addClass('disable')
                    button.attr('disabled', 'disabled')
                }

                //Add the click evento to the button
                button.on('click', function () {
                    loadReservation(idInstallation, this, item.startTime, item.endTime, date);
                });
                $('#listSchedule').append(button);
            });
        },
        error: function (error) {
            console.log(error);
        }
    });

    //load the list of aviability visibility
    var listSchedule = document.getElementById("listSchedule");
    listSchedule.classList.remove("visible-nd");
    listSchedule.classList.add("visible");
}


//------------------------------------------------------------------------------------------//
//                                      Reservation                                         //
//------------------------------------------------------------------------------------------//
//clear the sumary reservation card
function clearReservationCard() {
    var reservationCard = document.getElementById("reservationCard");
    reservationCard.classList.remove("visible");
    reservationCard.classList.add("visible-nd");
}

//load the reservation card
function loadReservationCard() {
    var reservationCard = document.getElementById("reservationCard");
    reservationCard.classList.remove("visible-nd");
    reservationCard.classList.add("visible");
}
function loadReservation(idInstallation, clickedElement, startTime, endTime) {
    clearListAviability()
    loadReservationCard()
    //add the property to the clicked element
    clickedElement.classList.add('active');
    clickedElement.setAttribute("aria-current", "true")

    listInstallations.forEach(function (item) {
        if (item.idInstallation == idInstallation) {
            document.getElementById("installationName").innerHTML = item.installationName;
            document.getElementById("description").innerHTML = item.description;
            document.getElementById("location").innerHTML = item.location;
            document.getElementById("picture").src = "/img/" + item.picture;
            document.getElementById("date").innerHTML = document.getElementById('datepickerQuery').value;
            document.getElementById("time").innerHTML = startTime + " - " + endTime;


            //set the values to the asp hidden fields
            document.getElementById("installationId").value = item.idInstallation;
            document.getElementById("dateReservation").value = convertFromESToDBFormat(document.getElementById('datepickerQuery').value);
            document.getElementById("startTimeReservation").value = startTime;
            document.getElementById("endTimeReservation").value = endTime;

        }
    });

}


//------------------------------------------------------------------------------------------//
//                                      ContentLoad                                         //
//------------------------------------------------------------------------------------------//

document.addEventListener('DOMContentLoaded', () => { // sure DOM is loaded
    const dropdownItems = document.querySelectorAll('.custom-dropdown-item');

    //refresing the dropdown button text
    dropdownItems.forEach(item => {
        item.addEventListener('click', function () {
            const selectedText = this.textContent || this.innerText; // get the text of the selected item
            document.getElementById('dropdownInstallations').textContent = selectedText; // change the text of the dropdown button

        });
    });

    loadInstallation(0) //inicialice the load data function
});
//detech when the 
$(document).ready(function () {
    $('#datepickerQuery').datepicker().on('change', function () {
        var date = $(this).val();
        console.log("id installatio asdfn: " + insllationSelected)
        if (insllationSelected != 0) {

            var dateValueDB = convertFromESToDBFormat(date);

            //conver to mm-dd-yyyy
            var dateEnglish = convertToEnglishFormat(date)
            var dt = new Date(dateEnglish);
            var day = dt.getDay()

            if (day == 0) {
                day = 7
            }
            loadDataScheduleAviable(insllationSelected, day, dateValueDB);
        }
    });
});
