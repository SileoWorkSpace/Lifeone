$(window).on('load', function () {
    
    var timeout = $("#TimeOut").val();
    var time = timeout * 1000 * 60;
    var timeout;
    var isLogout = false;
    timeout = setTimeout(function () {
        var counter = 300;
        var interval = setInterval(function () {
            counter--;
            if (counter <= 0) {
                clearInterval(interval);
                $(".SessionNotifaction > p").remove();
                $(".SessionNotifaction").append("<p><b>Your session has expired!</b></p>");
                $(".session-footer-btn > button").remove();
                $(".session-footer-btn").append("<a href='/Home/LogOut' class='btn btn-info btn-sm id='SBtn'>Ok</a>");
                return;
            } else {
                var s = counter;
                var h = Math.floor(s / 3600);
                s -= h * 3600;
                var m = Math.floor(s / 60);
                s -= m * 60;
                var r_t = (m < 10 ? '0' + m : m) + ":" + (s < 10 ? '0' + s : s)
                $('#SessionModalExpire').text(r_t);
            }
        }, 1000);
        $('#SessionModalExpireModal').modal({ backdrop: 'static', keyboard: false });
        $("#SessionModalExpireModal").modal("show");
        isLogout = true;
    }, time);
});