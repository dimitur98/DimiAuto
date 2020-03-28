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
        $("#img1").attr("src", "http://placehold.it/180");
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

    } else {
        deleteImg($("#img2").attr("src"), carId);
        $("#input2").val(null);
        $("#img2").attr("src", "http://placehold.it/180");
        $("#btn2").hide();

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

    } else {
        deleteImg($("#img3").attr("src"), carId);
        $("#input3").val(null);
        $("#img3").attr("src", "http://placehold.it/180");
        $("#btn3").hide();

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

    } else {
        deleteImg($("#img4").attr("src"), carId);
        $("#input4").val(null);
        $("#img4").attr("src", "http://placehold.it/180");
        $("#btn4").hide();

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

    } else {
        deleteImg($("#img5").attr("src"), carId);
        $("#input5").val(null);
        $("#img5").attr("src", "http://placehold.it/180");
        $("#btn5").hide();

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

    } else {
        deleteImg($("#img6").attr("src"), carId);
        $("#input6").val(null);
        $("#img6").attr("src", "http://placehold.it/180");
        $("#btn6").hide();


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

    } else {
        deleteImg($("#img7").attr("src"), carId);
        $("#input7").val(null);
        $("#img7").attr("src", "http://placehold.it/180");
        $("#btn7").hide();
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

    } else {
        deleteImg($("#img8").attr("src"), carId);
        $("#input8").val(null);
        $("#img8").attr("src", "http://placehold.it/180");
        $("#btn8").hide();
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

    } else {
        deleteImg($("#img9").attr("src"), carId);
        $("#input9").val(null);
        $("#img9").attr("src", "http://placehold.it/180");
        $("#btn9").hide();
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

    } else {
        deleteImg($("#img10").attr("src"), carId);
        $("#input10").val(null);
        $("#img10").attr("src", "http://placehold.it/180");
        $("#btn10").hide();
    }

}
function checkImgs() {
    for (var i = 1; i <= 10; i++) {
        var img = $("#img" + i);
        if (img.attr("src") == "https://via.placeholder.com/180") {
            console.log("1");

            $("#input" + i).show();
        } else if (img.attr("src") == undefined) {
            img.attr("src", "http://placehold.it/180")
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
        url: "/api/imgDelete",
        type: "POST",
        data: JSON.stringify(json),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: { 'X-CSRF-TOKEN': token },
        success: function (data) {
            console.log(data);
        }
    });
}