function validate() {
    var params = '{userName : "' + name.value + '", userPwd : "' + pwd.value + '"}';
    $.ajax({
        type: 'POST',
        url: '/Login/Index',
        contentType: 'application/json',
        dataType: 'json',
        data: params,
        success: function (result) {
            location.href = "/Home/Index";
        },
        error: function (result) {
            location.href = "/Login/Index";
        }
    });
}