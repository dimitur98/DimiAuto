var cloudinaryPath = "http://res.cloudinary.com/dimitur98/image/upload/";
var defaultCarImg = cloudinaryPath + "v1586939183/thfnuz2coeraduttwb54.jpg"
var defaultAvatarImg = cloudinaryPath + "v1586940693/a2lj8vldxdpmmpyjjamz.jpg"

function readURL1(input, carId) {
    console.log(carId);
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $("#img1")
                .attr('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
        $("#btn1").show();
        $("#input1").hide();
    } else {

        deleteImg($("#img1").attr("src"), carId);
        $("#input1").val(null);
        $("#img1").attr("src", defaultCarImg);
        $("#btn1").hide();
        console.log("as");
        $("#input1").show();
    }
}


function readURL2(input, carId) {

    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $("#img2")
                .attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
        $("#btn2").show();
        $("#input2").hide();

    } else {
        deleteImg($("#img2").attr("src"), carId);
        $("#input2").val(null);
        $("#img2").attr("src", defaultCarImg);
        $("#btn2").hide();
        $("#input2").show();

    }

}
function readURL3(input, carId) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $("#img3")
                .attr('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
        $("#btn3").show();
        $("#input3").hide();

    } else {
        deleteImg($("#img3").attr("src"), carId);
        $("#input3").val(null);
        $("#img3").attr("src", defaultCarImg);
        $("#btn3").hide();
        $("#input3").show();

    }
}

function readURL4(input, carId) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $("#img4")
                .attr('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
        $("#btn4").show();
        $("#input4").hide();

    } else {
        deleteImg($("#img4").attr("src"), carId);
        $("#input4").val(null);
        $("#img4").attr("src", defaultCarImg);
        $("#btn4").hide();
        $("#input4").show();

    }

}
function readURL5(input, carId) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $("#img5")
                .attr('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
        $("#btn5").show();
        $("#input5").hide();

    } else {
        deleteImg($("#img5").attr("src"), carId);
        $("#input5").val(null);
        $("#img5").attr("src", defaultCarImg);
        $("#btn5").hide();
        $("#input5").show();

    }
}

function readURL6(input, carId) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $("#img6")
                .attr('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
        $("#btn6").show();
        $("#input6").hide();

    } else {
        deleteImg($("#img6").attr("src"), carId);
        $("#input6").val(null);
        $("#img6").attr("src", defaultCarImg);
        $("#btn6").hide();
        $("#input6").show();

    }
}
function readURL7(input, carId) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $("#img7")
                .attr('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
        $("#btn7").show();
        $("#input7").hide();

    } else {
        deleteImg($("#img7").attr("src"), carId);
        $("#input7").val(null);
        $("#img7").attr("src", defaultCarImg);
        $("#btn7").hide();
        $("#input7").show();
    }
}

function readURL8(input, carId) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $("#img8")
                .attr('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
        $("#btn8").show();
        $("#input8").hide();

    } else {
        deleteImg($("#img8").attr("src"), carId);
        $("#input8").val(null);
        $("#img8").attr("src", defaultCarImg);
        $("#btn8").hide();
        $("#input8").show();
    }
}
function readURL9(input, carId) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $("#img9")
                .attr('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
        $("#btn9").show();
        $("#input9").hide();

    } else {
        deleteImg($("#img9").attr("src"), carId);
        $("#input9").val(null);
        $("#img9").attr("src", defaultCarImg);
        $("#btn9").hide();
        $("#input9").show();
    }
}
function readURL10(input, carId) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $("#img10")
                .attr('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
        $("#btn10").show();
        $("#input10").hide();

    } else {
        deleteImg($("#img10").attr("src"), carId);
        $("#input10").val(null);
        $("#img10").attr("src", defaultCarImg);
        $("#btn10").hide();
        $("#input10").show();
    }

}
function checkImgs() {
    for (var i = 1; i <= 10; i++) {
        var img = $("#img" + i);
        if (img.attr("src") == defaultCarImg) {
            console.log("1");
            $("#input" + i).show();
        } else if (img.attr("src") == cloudinaryPath) {
            img.attr("src", defaultCarImg)
            $("#input" + i).show();
        }
        else {
            console.log("2");

            $("#btn" + i).show();
            $("#input" + i).hide();
        }
    }
}
function deleteImg(input, carId) {
    console.log(carId);
    var token = $("#form input[name=__RequestVerificationToken]").val();
    var json = { imgToDel: input, carId: carId };
    console.log(json);
    $.ajax({
        url: "/api/imgDelete/deleteCarImg",
        type: "POST",
        data: JSON.stringify(json),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: { 'X-CSRF-TOKEN': token },
        success: function (data) {
            if (data) {
                $("#imgRemove").show().delay(2000).fadeOut();
            } else {
                $("#imgNotRemoved").show().delay(2000).fadeOut();
            }
        }
    });
}

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $("#img")
                .attr('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
        $("#btn").show();
        $("#btnSubmit").show();
        $("#input").hide();
    } else {

        deleteAvatar($("#img").attr("src"));
        $("#input").val(null);
        $("#img").attr("src", defaultAvatarImg);
        $("#btn").hide();
        $("#btnSubmit").hide();
        $("#input").show();


    }
}

function loadImg() {
    var img = $("#img");
    console.log(img.attr("src"))
    if (img.attr("src") != defaultAvatarImg) {
        $("#btn").show();
        $("#btnSubmit").hide();
        $("#input").hide();
    } else {
        $("#btn").hide();
        $("#btnSubmit").hide();
        $("#input").show();
    }
}

function deleteAvatar(input) {
    console.log("asd");
    var token = $("#form input[name=__RequestVerificationToken]").val();
    var json = { imgToDel: input, carId: null };
    console.log(json);
    $.ajax({
        url: "/api/imgDelete/deleteAvatarImg",
        type: "POST",
        data: JSON.stringify(json),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: { 'X-CSRF-TOKEN': token },
        success: function (data) {
            if (data) {
                $("#imgRemove").show().delay(2000).fadeOut();
            } else {
                $("#imgNotRemoved").show().delay(2000).fadeOut();
            }
            
        }
    });
}
