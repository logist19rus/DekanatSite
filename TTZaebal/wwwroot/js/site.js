$('#arrow_left').click(function () {
    setDate("-7");
});
$('#arrow_right').click(function () {
    setDate("7");
});
function setDate(str) {
    //13.10.2020 0:00:00
    str = parseInt(str);
    let date = getCookie('time');
    let day = parseInt(date.substring(0, 2));
    let mount = date.substring(3, 5);
    let year = date.substring(6, 10);
    day += str;
    let newDate = new Date(year, mount, day);
    let dd = newDate.getDate();
    if (dd < 10) dd = "0" + dd;
    let mm = newDate.getMonth();
    if (mm < 10) mm = "0" + mm;
    let yy = newDate.getFullYear();
    let strNewDate = dd + "." + mm + "." + yy;
    setCookie('time', strNewDate);
    console.log(date + " " + strNewDate);
    location.reload();
};
function getCookie(name) {
    let matches = document.cookie.match(new RegExp(
        "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
    ));
    return matches ? decodeURIComponent(matches[1]) : undefined;
}
function setCookie(name, value) {
    let updatedCookie = encodeURIComponent(name) + "=" + encodeURIComponent(value);
    document.cookie = updatedCookie;
}

$("#ToformButton").click(function () {
    console.log("asd");
    let year = $("#year").val();
    let ses = $("#session").val();
    if (year != null || ses != null) {
        $.ajax({
            url: '/Home/getPerfomanceTable',
            type: 'Get',
            data: {
                'year': year,
                'session': ses
            },
            success: function (res) {
                if (res == null || res == "" || !res.includes("<td>")) {
                    alert("Данная сессия ещё не сдана");
                }
                else {
                    $('#content').html(res);
                }
            },
            Error: function () {
                console.log("Ошибка получения данных");
            }
        });
    }
    else {
        alert("Выберите дату");
    }
});