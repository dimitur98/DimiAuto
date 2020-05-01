$(function () {
    moment.locale("en");
    $("time").each(function (i, e) {
        const dateTimeValue = $(e).attr("datetime");
        if (!dateTimeValue) {
            return;
        }

        const time = moment.utc(dateTimeValue).local();
        $(e).html(time.format("llll"));
        $(e).attr("title", $(e).attr("datetime"));
    });
});

function admin(input, action) {
    var token = $("#form input[name=__RequestVerificationToken]").val();
    var json = { id: input };
    $.ajax({
        url: "/api/administration/administrationControl/" + action,
        type: "POST",
        data: JSON.stringify(json),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: { 'X-CSRF-TOKEN': token },
        success: function (data) {

            if (data["action"] == "Undelete") {
                document.getElementById("statuse").innerHTML = "Approved, Not deleted";
                document.getElementById("delete").style.display = "block";
                document.getElementById("undelete").style.display = "none";
            } else if (data["action"] == "Approve") {
                document.getElementById("statuse").innerHTML = "Approved, Not deleted";
                document.getElementById("approve").style.display = "none";
                document.getElementById("delete").style.display = "block";
            } else if (data["action"] == "Delete") {
                document.getElementById("statuse").innerHTML = "Approved, Deleted";
                document.getElementById("delete").style.display = "none";
                document.getElementById("undelete").style.display = "block";

            }
        }
    });
}

function hideComments() {
    document.getElementById("commentsSection").style.display = "none";
    document.getElementById("commentTitle").style.display = "block";
}

function showComments() {
    document.getElementById("commentsSection").style.display = "block";
    document.getElementById("commentTitle").style.display = "none";
}

function checkBtns() {
    var approve = document.getElementById("statuse").innerHTML.split(", ")[0];
    var deleteStatuse = document.getElementById("statuse").innerHTML.split(", ")[1];
    console.log(approve);
    if (approve == "Statuse: Approved") {
        document.getElementById("approve").style.display = "none";
        if (deleteStatuse == "Deleted") {
            document.getElementById("delete").style.display = "none";
            document.getElementById("undelete").style.display = "block";

        } else {
            document.getElementById("delete").style.display = "block";
            document.getElementById("undelete").style.display = "none";
        }
    } else {
        document.getElementById("approve").style.display = "block";
        document.getElementById("delete").style.display = "none";
        document.getElementById("undelete").style.display = "none";
    }
}


function stopDropdown() {
    console.log($('#orderByPriceSelect').val());
    if ($('#orderByYearSelect').val() != 0) {
        $("#orderByPriceSelect").attr("disabled", "disabled");
    } else if ($('#orderByYearSelect').val() == 0) {
        $("#orderByPriceSelect").attr("disabled", null);
    }
    if ($('#orderByPriceSelect').val() != 0) {
        console.log("a");
        $("#orderByYearSelect").attr("disabled", "disabled");
    } else if ($('#orderByPriceSelect').val() == 0) {
        $("#orderByYearSelect").attr("disabled", null);
    }

}

function loadFirstCar() {
    $("#myHeader").show();
    $("#firstCar").show();
    $("#firstCarImg").attr("src", sessionStorage.getItem("firstCarImg"));
    $("#firstCarMakeModel").text(sessionStorage.getItem("firstCarMakeModel"));
    $("#firstCarInput").val(sessionStorage.getItem("firstCarId"));
}

function loadSecondCar() {
    $("#secondCar").show();
    $("#secondCarImg").attr("src", sessionStorage.getItem("secondCarImg"));
    $("#secondCarMakeModel").text(sessionStorage.getItem("secondCarMakeModel"));
    $("#secondCarInput").val(sessionStorage.getItem("secondCarId"));
    $("#compareBtns").show();
}

function loadComparerList() {

    if ($("#myHeader").is(":hidden")) {
        if (sessionStorage.getItem("firstCarId") != null) {
            loadFirstCar();
        }
        if (sessionStorage.getItem("secondCarId") != null) {
            loadSecondCar();
        }
    } else {
        $("#myHeader").hide();
        $("#firstCar").hide();
        $("#secondCar").hide();
        $("#compareBtns").hide();
    }
}

function addToCompare(carId, make, model, imgPath) {
    if (sessionStorage.getItem("firstCarId") == null) {
        $("#myHeader").show();
        $("#firstCar").show();
        $("#firstCarImg").attr("src", imgPath);
        $("#firstCarMakeModel").text(make + ' ' + model);
        $("#firstCarInput").val(carId);
        sessionStorage.setItem("firstCarId", carId);
        sessionStorage.setItem("firstCarImg", imgPath);
        sessionStorage.setItem("firstCarMakeModel", make + " " + model);
    } else if (sessionStorage.getItem("secondCarId") == null && carId != sessionStorage.getItem("firstCarId")) {
        console.log(carId);
        console.log(sessionStorage.getItem("firstCarId"));
        $("#secondCarImg").attr("src", imgPath);
        $("#secondCarMakeModel").text(make + ' ' + model);
        $("#secondCarInput").val(carId);
        sessionStorage.setItem("secondCarId", carId);
        sessionStorage.setItem("secondCarImg", imgPath);
        sessionStorage.setItem("secondCarMakeModel", make + " " + model);
        if ($("#firstCar").is(":visible")) {
            loadSecondCar();
        } else {
            loadFirstCar();
            loadSecondCar();
        }
    }
}

function clearComparerList() {
    $("#firstCarImg").attr("src", "");
    $("#firstCarMakeModel").text(null);
    $("#firstCarInput").val(null);
    $("#secondCarImg").attr("src", "");
    $("#secondCarMakeModel").text(null);
    $("#secondCarInput").val(null);
    $("#firstCar").hide();
    $("#secondCar").hide();
    $("#compareBtns").hide();
    $("#myHeader").hide();
    sessionStorage.clear();
}

function addToFav(carId) {
    console.log(carId);
    var token = $("#form input[name=__RequestVerificationToken]").val();
    var json = { carId: carId };
    console.log(json);
    $.ajax({
        url: "/api/apiAd/addToFav",
        type: "POST",
        data: JSON.stringify(json),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: { 'X-CSRF-TOKEN': token },
        success: function (data) {
            console.log(data["output"]);
            if (data["output"] == "already added") {
                console.log(data);

                $("#" + carId).show().delay(2000).fadeOut();
            } else if (data["output"] == "added") {
                $("#add_" + carId).show().delay(2000).fadeOut();
            }
        }
    });
}


function getValue() {
    var orderByPrice = document.getElementById("orderByPriceSelect");
    var orderByYear = document.getElementById("orderByYearSelect");
    var value = orderByPrice.options[orderByPrice.selectedIndex].value;
    var secValue = orderByYear.options[orderByYear.selectedIndex].value;

    document.getElementById("orderByPrice").value = value;
    document.getElementById("orderByYear").value = secValue;
}

window.onscroll = function () { myFunction() };

var header = document.getElementById("myHeader");

var sticky = header.offsetTop;

function myFunction() {
    if (window.pageYOffset > sticky) {
        header.classList.add("sticky");
    } else {
        header.classList.remove("sticky");
    }
}

function removeFavAd(carId) {
    console.log(carId);
    var token = $("#form input[name=__RequestVerificationToken]").val();
    var json = { carId: carId };
    console.log(json);
    $.ajax({
        url: "/api/apiAd/removeFavAd",
        type: "POST",
        data: JSON.stringify(json),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: { 'X-CSRF-TOKEN': token },
        success: function (data) {
            console.log("succes");
            if (data["output"] == "removed") {
                $("#remove").show().delay(3000).fadeOut();
                $("#" + data["carId"]).remove();
            }
        }
    });
}
