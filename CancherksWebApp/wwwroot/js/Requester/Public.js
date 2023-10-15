

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

function convertTimeFormat(timeString) {
    var parts = timeString.split(':');
    return `${parts[0]}:${parts[1]}`;
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

function loadPublicInstallation(idSport) {
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
                        checkAviabilityofWeek($(this).data('installation-id'), this);
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
function checkAviabilityofWeek(idInstallation, installationDivCliked) {

    clearInstallationList()
    installationDivCliked.classList.add("installationSelected")
    loadDataScheduleAviable(idInstallation);


}

//------------------------------------------------------------------------------------------//
//                                      Schedule Availability                               //
//------------------------------------------------------------------------------------------//



//Load the schedule and check the aviability of the installation, also call the load reservation card when a button is clicked
function loadDataScheduleAviable(idInstallation) {

    $.ajax({
        url: '/Solicitante/Publicas?handler=LoadSchudeuleAviableInfo',
        type: 'GET',
        data: { idInstallation: idInstallation },
        success: function (data) {

            var scheduleDays = $(".top-info");
            var ulElement = scheduleDays.next('ul');

            //for to clear all ul elements
            for (var i = 0; i < ulElement.length; i++) {
                ulElement[i].innerHTML = "";
            }

            // Loop through the returned data and create cards
            data.forEach(function (item) {
                
                    console.log(item)
                var liElement = $('<li></li>')
                    .addClass('single-event')
                    .attr('data-start', convertTimeFormat(item.startTime))
                    .attr('data-end', convertTimeFormat(item.endTime))
                    .attr('data-content', 'event-rowing-workout')
                    .attr('data-event', 'event-1');

                var anchorElement = $('<a></a>')
                    .attr('href', '#0')
                    .appendTo(liElement);

                var emElement = $('<em></em>')
                    .addClass('event-name')
                    .text('Disponible al Público')
                    .appendTo(anchorElement);

                // Append the liElement to the desired parent element
                // Example: Append to a <ul> with class "events-list"
                ulElement[item.idDay-1].append(liElement[0]);


            });

            // ****************************************************************************************
            //                          Section to load the schedule plan                             
            // ****************************************************************************************

			//should add a loding while the events are organized 

			function SchedulePlan(element) {
				this.element = element;
				this.timeline = this.element.find('.timeline');
				this.timelineItems = this.timeline.find('li');
				this.timelineItemsNumber = this.timelineItems.length;
				this.timelineStart = getScheduleTimestamp(this.timelineItems.eq(0).text());
				//need to store delta (in our case half hour) timestamp
				this.timelineUnitDuration = getScheduleTimestamp(this.timelineItems.eq(1).text()) - getScheduleTimestamp(this.timelineItems.eq(0).text());

				this.eventsWrapper = this.element.find('.events');
				this.eventsGroup = this.eventsWrapper.find('.events-group');
				this.singleEvents = this.eventsGroup.find('.single-event');
				this.eventSlotHeight = this.eventsGroup.eq(0).children('.top-info').outerHeight();

				this.modal = this.element.find('.event-modal');
				this.modalHeader = this.modal.find('.header');
				this.modalHeaderBg = this.modal.find('.header-bg');
				this.modalBody = this.modal.find('.body');
				this.modalBodyBg = this.modal.find('.body-bg');
				this.modalMaxWidth = 800;
				this.modalMaxHeight = 480;

				this.animating = false;

				this.initSchedule();
			}

			SchedulePlan.prototype.initSchedule = function () {

				this.initEvents();
				this.placeEvents();
				
			};


			SchedulePlan.prototype.initEvents = function () {
				var self = this;

				this.singleEvents.each(function () {
					//create the .event-date element for each event
					var durationLabel = '<span class="event-date">' + $(this).data('start') + ' - ' + $(this).data('end') + '</span>';
					$(this).children('a').prepend($(durationLabel));
				});

			};

			SchedulePlan.prototype.placeEvents = function () {
				var self = this;
				this.singleEvents.each(function () {
					//place each event in the grid -> need to set top position and height
					var start = getScheduleTimestamp($(this).attr('data-start')),
						duration = getScheduleTimestamp($(this).attr('data-end')) - start;

					var eventTop = self.eventSlotHeight * (start - self.timelineStart) / self.timelineUnitDuration,
						eventHeight = self.eventSlotHeight * duration / self.timelineUnitDuration;

					$(this).css({
						top: (eventTop - 1) + 'px',
						height: (eventHeight + 1) + 'px'
					});
				});

				this.element.removeClass('loading');
			};

			

			SchedulePlan.prototype.mq = function () {
				//get MQ value ('desktop' or 'mobile') 
				var self = this;
				return window.getComputedStyle(this.element.get(0), '::before').getPropertyValue('content').replace(/["']/g, '');
			};


			var schedules = $('.cd-schedule');
			var objSchedulesPlan = [],
				windowResize = false;

			if (schedules.length > 0) {
				schedules.each(function () {
					//create SchedulePlan objects
					objSchedulesPlan.push(new SchedulePlan($(this)));
				});
			}


			function getScheduleTimestamp(time) {
				//accepts hh:mm format - convert hh:mm to timestamp
				time = time.replace(/ /g, '');
				var timeArray = time.split(':');
				var timeStamp = parseInt(timeArray[0]) * 60 + parseInt(timeArray[1]);
				return timeStamp;
            }
            // ****************************************************************************************
            //                          Section to load the schedule plan                             //
            // ****************************************************************************************
        },
        error: function (error) {
            console.log(error);
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

    loadPublicInstallation(0) //inicialice the load data function
});
