// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function convertToDbDateFormat(dateString) {
    var parts = dateString.split('/');
    return `${parts[1]}/${parts[0]}/${parts[2]}`;
}

$(document).ready(function () {
    $('#datepickerInicio-input').datepicker().on('change', function () {
        var originalDate = $(this).val();
        var convertedDate = convertToDbDateFormat(originalDate);
        console.log("agarra el inicio"); 
        console.log(convertedDate);  // Esto imprimirá la fecha en el formato "yyyy-mm-dd"
    });

    // Desencadenar el datepicker al hacer clic en el ícono
    $('#datepickerInicio-icon').click(function () {
        $('#datepickerInicio-input').focus();
    });
});


$(document).ready(function () {
    $('#datepickerFinal-input').datepicker().on('change', function () {
        var originalDate = $(this).val();
        var convertedDate = convertToDbDateFormat(originalDate);
        console.log("agarra el fin");
        console.log(convertedDate);  // Esto imprimirá la fecha en el formato "yyyy-mm-dd"
    });

    // Desencadenar el datepicker al hacer clic en el ícono
    $('#datepickerFinal-icon').click(function () {
        $('#datepickerFinal-input').focus();
    });
});

function filterTableIfBothDatesSet() {
    var startDate = $('#datepickerInicio-input').data('converted');
    var endDate = $('#datepickerFinal-input').data('converted');

    // Verifica si ambas fechas están establecidas
    if (startDate && endDate) {
        filterTableByDates(startDate, endDate);
    }
}

$(document).ready(function () {
    $('#datepickerInicio-input').datepicker().on('change', function () {
        var originalDate = $(this).val();
        var convertedDate = convertToDbDateFormat(originalDate);

        // Guarda la fecha convertida usando jQuery data
        $(this).data('converted', convertedDate);


        // Filtra la tabla si ambas fechas están establecidas
        filterTableIfBothDatesSet();
    });

    $('#datepickerFinal-input').datepicker().on('change', function () {
        var originalDate = $(this).val();
        var convertedDate = convertToDbDateFormat(originalDate);

        // Guarda la fecha convertida usando jQuery data
        $(this).data('converted', convertedDate);

        // Filtra la tabla si ambas fechas están establecidas
        filterTableIfBothDatesSet();
    });
});

function filterTableByDates(startDate, endDate) {
    var tableRows = document.querySelectorAll('.table tbody tr');
    
    // Convertimos las fechas a un formato comparable
    var comparableStartDate = convertToDbDateFormat(startDate);
    var comparableEndDate = convertToDbDateFormat(endDate);

    tableRows.forEach(row => {
        var rowDateText = row.querySelector('td:nth-child(3)').innerText;
        var comparableRowDate = convertToDbDateFormat(rowDateText);
        
        console.log("fechas tabla:", comparableRowDate)
        
        if (comparableRowDate >= comparableStartDate && comparableRowDate <= comparableEndDate) {
            row.style.display = '';
        } else {
            row.style.display = 'none';
        }
    });
}

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


