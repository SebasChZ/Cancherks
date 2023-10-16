// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// START For filtering by dates and installation in reports


function convertToSpanishFormat(dateString) {
    var parts = dateString.split('/');
    return `${parts[1]}/${parts[0]}/${parts[2]}`;
}

//Code for the datepicker interaction and the date format
$(document).ready(function () {

    $('.datepickerInput').datepicker({ dateFormat: 'dd/mm/yy' });

    $('.datepickerIcon').each(function () {
        $(this).on('click', function () {

            $(this).prev('.datepickerInput').focus();
        });
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

// Guarda los tamaños de fuente originales
var tamaniosOriginales = {};

// Obtén el factor acumulado de sessionStorage, si no existe usa 1 como valor predeterminado.
var factorAcumulado = sessionStorage.getItem('factorAcumulado') ? parseFloat(sessionStorage.getItem('factorAcumulado')) : 1;

// Al cargar la página, guarda los tamaños de fuente originales
window.addEventListener('DOMContentLoaded', function () {
    guardarTamaniosOriginales();
    aplicarZoomActual();
});

// Obtén el ícono de zoom por su ID
var zoomIcon = document.getElementById('zoom-icon');

// Agrega un evento click al ícono de zoom
zoomIcon.addEventListener('click', function () {
    factorAcumulado *= 1.05;
    sessionStorage.setItem('factorAcumulado', factorAcumulado);
    aplicarZoomActual();
});

// Obtén el ícono de alejar por su clase
var zoomOutIcon = document.querySelector('.bi-zoom-out');

// Agrega un evento click al ícono de alejar
zoomOutIcon.addEventListener('click', function () {
    factorAcumulado *= 0.95;
    sessionStorage.setItem('factorAcumulado', factorAcumulado);
    aplicarZoomActual();
});

function guardarTamaniosOriginales() {
    var elementos = document.getElementsByTagName('*');
    for (var i = 0; i < elementos.length; i++) {
        var elemento = elementos[i];
        var estilo = window.getComputedStyle(elemento, null);
        var fontSize = parseFloat(estilo.getPropertyValue('font-size'));
        tamaniosOriginales[elemento] = fontSize;
    }
}

function aplicarZoomActual() {
    var elementos = document.getElementsByTagName('*');
    for (var i = 0; i < elementos.length; i++) {
        var elemento = elementos[i];
        var fontSizeOriginal = tamaniosOriginales[elemento];
        var nuevoFontSize = fontSizeOriginal * factorAcumulado;
        elemento.style.fontSize = nuevoFontSize + 'px';
    }
}
