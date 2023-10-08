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

