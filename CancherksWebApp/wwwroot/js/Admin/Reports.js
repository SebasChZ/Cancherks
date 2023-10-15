var selectedInstallationId = "0";
var selectedStartDate = null;
var selectedEndDate = null;

document.querySelectorAll('.custom-dropdown-item').forEach(item => {
    item.addEventListener('click', function (event) {
        event.preventDefault();
        selectedInstallationId = this.getAttribute('data-id');
        combinedFilter();
    });
});
function combinedFilter() {
    var tableRows = document.querySelectorAll('.table tbody tr');
    tableRows.forEach(row => {
        var rowInstallationId = row.getAttribute('data-installation-id');
        var rowDateText = row.querySelector('td:nth-child(3)').innerText;

        // Filter by installation ID
        var matchInstallation = (selectedInstallationId === "0" || rowInstallationId === selectedInstallationId);

        // If both dates are established, filter by dates too
        var matchDate = true;
        if (selectedStartDate && selectedEndDate) {
            matchDate = (rowDateText >= selectedStartDate && rowDateText <= selectedEndDate);
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
    return `${parts[1]}/${parts[0]}/${parts[2]}`;
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