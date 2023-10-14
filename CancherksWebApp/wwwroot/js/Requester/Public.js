

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

function loadInstallation(idSport) {
    $.ajax({
        url: '/Solicitante/Publicas?handler=LoadInstallationbySport',
        type: 'GET',
        data: { idSport: idSport },
        success: function (data) {

            // Clear existing cards
            $('#gridDiv').empty();

            listInstallations = data
            // Loop through the returned data and create cards
            data.forEach(function (item) {
                if (item.isPublic == 1) {
                    var card = $('<div></div>')
                        .addClass('col-6 p-0 consult installation-view')
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
