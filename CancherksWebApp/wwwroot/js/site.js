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


var tamaniosOriginales = {};
var value = sessionStorage.getItem('value') ? parseFloat(sessionStorage.getItem('value')) : 1;
window.addEventListener('DOMContentLoaded', function () {
    guardarTamaniosOriginales();
    aplicarZoomActual();
});

// Get icon zoom by class
var zoomIcon = document.getElementById('zoom-icon');

// Add event to zoom in icon
zoomIcon.addEventListener('click', function () {
    value *= 1.05;
    sessionStorage.setItem('value', value);
    aplicarZoomActual();
});

// Get icon zoom out by class
var zoomOutIcon = document.querySelector('.bi-zoom-out');

// Add event to zoom out in icon
zoomOutIcon.addEventListener('click', function () {
    value *= 0.95;
    sessionStorage.setItem('value', value);
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
        var nuevoFontSize = fontSizeOriginal * value;
        elemento.style.fontSize = nuevoFontSize + 'px';
    }
}

function toggleYellowBlack() {
    var body = document.body;

    if (body.classList.contains('yellow-black')) {
        body.classList.remove('yellow-black');
    } else {
        body.classList.add('yellow-black');
    }
}

function toggleInvertedColors() {
    var body = document.body;

    if (body.classList.contains('inverted-colors')) {
        body.classList.remove('inverted-colors');
    } else {
        body.classList.add('inverted-colors');
    }
}
function toggleGrayBlack() {
    var body = document.body;

    if (body.classList.contains('gray-black')) {
        body.classList.remove('gray-black');
    } else {
        body.classList.add('gray-black');
    }
}


function applyHighContrast() {
    toggleInvertedColors()
}


