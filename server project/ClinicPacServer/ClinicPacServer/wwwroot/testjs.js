$().ready(function () {
    $.ajax({

        url: "https://clinicpac.squaresgrow.com/api/Users/Login?Email=asd&Pass=asd",

        type:"post",

        error: function (error) {
            console.log(error);
        },
        success: function (res) {
            console.log(res); 
        }
    });
});