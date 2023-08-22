/* Default Values */
var lang = "";
var _datePickerDefault = {};

var _optionTooltip = {
    placement: 'top',
    fontSize: '10px'
};
var _optionModal = {
    keyboard: false
};
var _localeDate = "es-AR";

/* Commons Functions */

$(document).ready(function () {
    lang = $("#language").val();
    _datePickerDefault = (lang=='es')?{
            monthsShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
            weekdaysShort: ['Dom', 'Lun', 'Mar', 'Mie', 'Jue', 'Vie', 'Sab'],
            weekdaysFull: ['Domingo', 'Lunes', 'Martes', 'Miercoles', 'Jueves', 'Viernes', 'Sábado'],
            showMonthsShort: true,
            labelMonthNext: 'Próximo mes',
            labelMonthPrev: 'Mes anterior',
            labelMonthSelect: 'Seleccione un mes',
            labelYearSelect: 'Seleccione un año',
            selectMonths: true,
            format: 'dd/mm/yyyy',
            formatSubmit: 'dd/mm/yyyy',
            today: '',
            clear: '',
            close: '',
            closeOnSelect: true,
            showWeekdaysFull: false,
            selectYears: 100
    } : {
            monthsShort: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'June', 'July', 'Aug', 'Sept', 'Oct', 'Nov', 'Dec'],
            weekdaysShort: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'],
            weekdaysFull: ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'],
            showMonthsShort: true,
            labelMonthNext: 'Next motnh',
            labelMonthPrev: 'Prev. month',
            labelMonthSelect: 'Select a month',
            labelYearSelect: 'Select a year',
            selectMonths: true,
            format: 'mm/dd/yyyy',
            formatSubmit: 'mm/dd/yyyy',
            today: '',
            clear: '',
            close: '',
            closeOnSelect: true,
            showWeekdaysFull: false,
            selectYears: 100
    };
});

function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

function getQueryStringValue(key) {
    return unescape(window.location.search.replace(new RegExp("^(?:.*[&\\?]" + escape(key).replace(/[\.\+\*]/g, "\\$&") + "(?:\\=([^&]*))?)?.*$", "i"), "$1"));
}

function setHash(value) {
    window.location.hash = '';
    //window.history.pushState(null, null, (value === '') ? window.location.pathname + window.location.search : value);
    window.history.pushState(null, null, value);
}

function getHash() {
    var hash = '';
    if (window.location.hash.length > 0) hash = window.location.hash.split('?')[0].substring(1).toLowerCase().replace('/', '');
    return hash;
}

function getAction() {
    var arrRoute = window.location.pathname.split('/');
    return arrRoute[arrRoute.length-1];
}

function executeActionMensage(action) {
    if (action === undefined) return false;
    if ($.isNumeric(action)) {
        var lan = $('#language').val();
        var locHref = null;
        switch (action) {
            case 0: break;
            case 1: locHref = "/account/login"; break;
            case 3: locHref = "/account/signup"; break;
            case 4: locHref = "/account/forgot"; break;
            case 2: locHref = "/dashboard/home"; break;
            case 5: locHref = pathSite; break;
            case 6: locHref = "/account/login?endSession=1"; break;
            case 7: locHref = "/account/login?endToken=1"; break;
        }
        if (locHref != null) location.href = "/" + lan + locHref;
    }
    else location.href = action;
}

function DateTimeToUnixTimestamp(DATETIME) {
    if (DATETIME == null) return null;
    var parts = DATETIME.split("/");
    var d1 = new Date(Number(parts[2]), Number(parts[1]) - 1, Number(parts[0]));
    return d1.getTime() / 1000;
}

function UnixTimeStampToDateTime(UNIX_timestamp, format = false) {
    var a = new Date(UNIX_timestamp * 1000);
    var months = ['01', '02', '03', '04', '05', '06', '07', '08', '09', '10', '11', '12'];
    var monthsStr_es = ['enero', 'febrero', 'marzo', 'abril', 'mayo', 'junio', 'julio', 'agosto', 'septiembre', 'octubre', 'noviembre', 'diciembre'];
    var monthsStr_en = ['january', 'febrery', 'march', 'april', 'may', 'june', 'july', 'august', 'september', 'october', 'november', 'december'];
    var year = a.getFullYear();
    var date = a.getDate();
    var dateReturn = null;
    switch (lang) {
        case 'en':
            dateReturn = (!format) ? months[a.getMonth()] + '/' + ((date < 10) ? '0' + date : date) + '/' + year : monthsStr_en[a.getMonth()] + ' ' + date + ', ' + year;
            break;
        case 'es':
        default:
            dateReturn = (!format) ? ((date<10)?'0'+date:date) + '/' + months[a.getMonth()] + '/' + year : date + ' de ' + monthsStr_es[a.getMonth()] + ' de ' + year;
            break;
    }
    
    return dateReturn;
}

function imageExists(url, callback) {
    var img = new Image();
    img.onload = function () {
        callback(true);
    };
    img.onerror = function () {
        callback(false);
    };
    img.src = url;
}

String.prototype.format = function () {
    a = this;
    for (k in arguments) {
        a = a.replace("{" + k + "}", arguments[k])
    }
    return a
}