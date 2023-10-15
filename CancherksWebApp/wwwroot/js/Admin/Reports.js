var selectedInstallationId = "0";
var selectedStartDate = null;
var selectedEndDate = null;

document.querySelectorAll('.custom-dropdown-item').forEach(item => {
    item.addEventListener('click', function (event) {
        event.preventDefault();
        const selectedText = this.textContent || this.innerText; // get the text of the selected item
        selectedInstallationId = this.getAttribute('data-id');
        document.getElementById('dropdownInstallations').textContent = selectedText; // change the text of the dropdown button
        var inputHidden = document.querySelector('#installationSelected');
        console.log(inputHidden)
        console.log("selectedInstallationId", selectedInstallationId);
        inputHidden.value = selectedInstallationId;
        combinedFilter();
    });
});
function combinedFilter() {
    var tableRows = document.querySelectorAll('.table tbody tr');
    tableRows.forEach(row => {
        var rowInstallationId = row.getAttribute('data-installation-id');
        var rowDateText = row.querySelector('td:nth-child(6)').innerText;

        // Filter by installation ID
        var matchInstallation = (selectedInstallationId === "0" || rowInstallationId === selectedInstallationId);
        var rowDate = new Date(rowDateText);


        // If both dates are established, filter by dates too
        var matchDate = true;
        if (selectedStartDate && selectedEndDate) {
            var starDate = new Date(selectedStartDate);
            var endDate = new Date(selectedEndDate);
            matchDate = (rowDate >= starDate && rowDate <= endDate);
        }
        else if (selectedStartDate && selectedEndDate == null) {
            var starDate = new Date(selectedStartDate);
            matchDate = (rowDate >= starDate);
            console.log("Solo fecha inicio", matchDate);
        }
        else if (selectedStartDate == null && selectedEndDate) {
            var starDate = new Date(selectedStartDate);
            matchDate = (rowDate <= endDate);
        }

        if (matchInstallation && matchDate) {
            row.style.display = '';
        } else {
            row.style.display = 'none';
        }
    });
}

function convertToDbDateFormat(dateString) {
    var parts = dateString.split('/');
    return `${parts[2]}/${parts[1]}/${parts[0]}`;
}


$(document).ready(function () {
    $('#datepickerInicio-input').datepicker().on('change', function () {
        var originalDate = $(this).val();
        selectedStartDate = convertToDbDateFormat(originalDate);
        combinedFilter();
    });


    $('#datepickerFinal-input').datepicker().on('change', function () {
        var originalDate = $(this).val();
        selectedEndDate = convertToDbDateFormat(originalDate);
        combinedFilter();
    });
});