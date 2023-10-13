// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// START For filtering by dates and installation in reports

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

function convertToDbDateFormat(dateString) {
    var parts = dateString.split('/');
    return `${parts[1]}/${parts[0]}/${parts[2]}`;
}

$(document).ready(function () {
    $('#datepickerInicio-input').datepicker().on('change', function () {
        var originalDate = $(this).val();
        var convertedDate = convertToDbDateFormat(originalDate);
    });

    $('#datepickerInicio-icon').click(function () {
        $('#datepickerInicio-input').focus();
    });  
});


$(document).ready(function () {
    $('#datepickerFinal-input').datepicker().on('change', function () {
        var originalDate = $(this).val();
        var convertedDate = convertToDbDateFormat(originalDate);
    });

    
    $('#datepickerFinal-icon').click(function () {
        $('#datepickerFinal-input').focus();
    });
});
// END For filtering by dates and installation in reports

function readURL(input) {
    if (input.files && input.files[0]) {

        var reader = new FileReader();

        reader.onload = function (e) {
            $('.image-upload-wrap').hide();

            $('.file-upload-image').attr('src', e.target.result);
            $('.file-upload-content').show();

            $('.image-title').html(input.files[0].name);
        };

        reader.readAsDataURL(input.files[0]);

    } else {
        removeUpload();
    }
}

function removeUpload() {
    $('.file-upload-input').replaceWith($('.file-upload-input').clone());
    $('.file-upload-content').hide();
    $('.image-upload-wrap').show();
}
$('.image-upload-wrap').bind('dragover', function () {
    $('.image-upload-wrap').addClass('image-dropping');
});
$('.image-upload-wrap').bind('dragleave', function () {
    $('.image-upload-wrap').removeClass('image-dropping');
});
