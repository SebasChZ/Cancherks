var selectedInstallationId = "0";
var selectedStartDate = null;
var selectedEndDate = null;

document.querySelectorAll('.custom-dropdown-item').forEach(item => {
    item.addEventListener('click', function (event) {
        event.preventDefault();
        const selectedText = this.textContent || this.innerText;
        selectedInstallationId = this.getAttribute('data-id');
        document.getElementById('dropdownInstallations').textContent = selectedText;
        document.querySelector('#installationSelected').value = selectedInstallationId;
        combinedFilter();
    });
});


function normalizeDate(date) {
    date.setHours(0, 0, 0, 0);
    return date;
}

function combinedFilter() {
    var tableRows = document.querySelectorAll('.table tbody tr');
    tableRows.forEach(row => {
        var rowInstallationId = row.getAttribute('data-installation-id');
        var rowDateText = row.querySelector('td:nth-child(6)').innerText;
        // Filter by installation ID
        var matchInstallation = (selectedInstallationId === "0" || rowInstallationId === selectedInstallationId);
        // Filter by dates
        var matchDate = true;
        if (selectedStartDate) {
            matchDate = (rowDateText >= selectedStartDate);
        }
        if (selectedEndDate) {
            matchDate = matchDate && (rowDateText <= selectedEndDate);
        }
        if (matchInstallation && matchDate) {
            row.style.display = '';
        } else {
            row.style.display = 'none';
        }
    });
}

$(document).ready(function () {
    $('#datepickerInicio-input').datepicker().on('change', function () {
        selectedStartDate = $(this).val();
        combinedFilter();
    });

    $('#datepickerFinal-input').datepicker().on('change', function () {
        selectedEndDate = $(this).val();
        console.log(selectedEndDate)
        combinedFilter();
    });
});
