// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(document).ready(function () {
    $('#datepickerInicio-input').datepicker();

    // Desencadenar el datepicker al hacer clic en el ícono
    $('#datepickerInicio-icon').click(function () {
        $('#datepickerInicio-input').focus();
    });
});

$(document).ready(function () {
    $('#datepickerFinal-input').datepicker();

    // Desencadenar el datepicker al hacer clic en el ícono
    $('#datepickerFinal-icon').click(function () {
        $('#datepickerFinal-input').focus();
    });
});

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


document.getElementById('datepickerInicio-input').addEventListener('changeDate', filterByDate);
document.getElementById('datepickerFinal-input').addEventListener('changeDate', filterByDate);

function convertDatepickerToDate(dateStr) {
    // Formato del datepicker: mm/dd/yyyy
    const [month, day, year] = dateStr.split('/').map(Number);
    return new Date(year, month - 1, day); // month - 1 porque los meses en JavaScript son 0-indexados
}

function convertTableDateToDate(dateStr) {
    // Formato en la tabla: dd-mm-yyyy
    const [day, month, year] = dateStr.split('-').map(Number);
    return new Date(year, month - 1, day);
}





