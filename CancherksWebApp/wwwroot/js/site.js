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



function getSessionStorageValues() {
    invertedColors = sessionStorage.getItem('invertedColors') ? sessionStorage.getItem('invertedColors') : false;
    yellowBlack = sessionStorage.getItem('yellowBlack') ? sessionStorage.getItem('yellowBlack') : false;
    grayBlack = sessionStorage.getItem('grayBlack') ? sessionStorage.getItem('grayBlack') : false;
    invertedColors = JSON.parse(invertedColors)
    yellowBlack = JSON.parse(yellowBlack)
    grayBlack = JSON.parse(grayBlack)
}


var tamaniosOriginales = [];
var tamaniosTipos = [];
var value = sessionStorage.getItem('value') ? parseFloat(sessionStorage.getItem('value')) : 1;
var invertedColors;
var yellowBlack;
var grayBlack;
getSessionStorageValues()


//check if high contrast is enabled
function checkHighContrast() {
    if (invertedColors) {
        toggleInvertedColors();
    }
    if (yellowBlack) {
        toggleYellowBlack();
    }
    if (grayBlack) {
        toggleGrayBlack();
    }
}

window.addEventListener('DOMContentLoaded', function () {
    //check for high contrast value
    checkHighContrast();
    guardarTamaniosOriginales();
    //aplicarZoomActual();
});


function zoomIn() {
    value *= 1.05;
    sessionStorage.setItem('value', value);
    aplicarZoomActual();
}

function zoomOut() {
    value *= 0.95;
    sessionStorage.setItem('value', value);
    aplicarZoomActual();

}

function guardarTamaniosOriginales() {
    var elementos = document.getElementsByTagName('*');
    for (var i = 0; i < elementos.length; i++) {
        var elemento = elementos[i];
        var estilo = window.getComputedStyle(elemento, null);
        var fontSizeStr = estilo.getPropertyValue('font-size');

        var match = fontSizeStr.match(/^([\d.]+)([a-z]*)$/); // This regex will match the numeric part and the unit part
        if (match) {
            tamaniosOriginales.push(parseFloat(match[1]));
            tamaniosTipos.push(match[2]);
        }
    }
}
function aplicarZoomActual() {
    var elementos = document.getElementsByTagName('*');
    for (var i = 0; i < elementos.length; i++) {
        var elemento = elementos[i];
        var fontSizeOriginal = tamaniosOriginales[i];
        var tipoOriginal = tamaniosTipos[i];
        var nuevoFontSize = fontSizeOriginal * value;
        elemento.style.fontSize = nuevoFontSize + tipoOriginal;
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
    getSessionStorageValues()
    if (invertedColors) {
        toggleInvertedColors() // remove the inverted colors
        toggleYellowBlack() // apply yellow black
        sessionStorage.setItem('invertedColors', false);
        sessionStorage.setItem('yellowBlack', true);
    }
    else if (yellowBlack) {
        toggleYellowBlack() // remove the yellow black
        toggleGrayBlack()   // apply gray black
        sessionStorage.setItem('yellowBlack', false);
        sessionStorage.setItem('grayBlack', true);
    }
    else if (grayBlack) {
        toggleGrayBlack() // remove the gray black
        sessionStorage.setItem('grayBlack', false);
    } else {
        toggleInvertedColors()
        sessionStorage.setItem('invertedColors', true);
    }

}