function parseJsonDate(jsonDate) {
    if (jsonDate != null) {
        //if the variable is not null you can use substr with no problems

        var fullDate = new Date(parseInt(jsonDate.substr(6)));
        var twoDigitMonth = (fullDate.getMonth() + 1) + ""; if (twoDigitMonth.length == 1) twoDigitMonth = "0" + twoDigitMonth;

        var twoDigitDate = fullDate.getDate() + ""; if (twoDigitDate.length == 1) twoDigitDate = "0" + twoDigitDate;
        var currentDate = fullDate.getFullYear() + "-" + twoDigitMonth + "-" + twoDigitDate;

        return currentDate;
    }
}

function getDateNow() {
    var now = new Date()
    var day = ("0" + now.getDate()).slice(-2)
    var month = ("0" + (now.getMonth() + 1)).slice(-2)
    var today = now.getFullYear() + "-" + (month) + "-" + (day)
    $('#tanggal').val(today)
}