var isOperationInprogress = false;

$(function () {

});

function showSnackbar(msg) {
    if (msg != '' && msg != null && msg != undefined) {
        $('#snackbar').html(msg);
        $('#snackbar').addClass('show');

        setTimeout(function () {
            $('#snackbar').removeClass('show');
        }, 3000);
    }
}

//Global method to perform ajax calls
function makeAjaxCall(url, data, loaderId, type) {
    return $.ajax({
        url: url,
        type: type ? type : "GET",
        data: data,
        contentType: "application/json",
        beforeSend: function () { showLoader(loaderId); },
        complete: function () { hideLoader(loaderId); }
    });
}

function showLoader(id) {
    if (!id)
        $("#fullLoader").addClass("loader");
}

function hideLoader(id) {
    if (!id)
        $('#fullLoader').removeClass('loader');
}

function showMultiThreadingProgressBar() {
    $("#myProgress").removeClass("nodisplay");
    $("#myProgress #myProgressBar").width("1%");
    $("#myProgress #emptyProgress").width("99%");
}

function blockCurrentProcessing() {
    isOperationInprogress = true;
    showMultiThreadingProgressBar();
    getMultiProcessStatus();
}

function updateProgress(percentageBar) {
    $("#myProgress").removeClass("nodisplay");
    //Show progress

    $("#myProgress #myProgressBar").width((percentageBar) + "%");
    $("#myProgress #emptyProgress").width((100 - percentageBar) + "%");
    isOperationInprogress = true;

    if (percentageBar == 100) {
        //Process complete message
        isOperationInprogress = false;
        setTimeout(function () {
            $("#myProgress").addClass("nodisplay");
        }, 800);
    }
}

function getMultiProcessStatus(isOnLoad) {
    var obj = { UserId: userId, Module: moduleName }
    makeAjaxCall(multiProcessStatusGetUrl, JSON.stringify(obj), "nsnn", 'POST')
        .done(function (response) {
            if (!response.IsSuccess) {
                if (isOnLoad != true) {
                    setTimeout(function () { getMultiProcessStatus(); }, 1000);
                }
                return;
            }
            if (response.Data != null) {
                if (!response.Data.IsCompleted) {
                    setTimeout(function () { getMultiProcessStatus(); }, 1000);
                }
                else {
                    if (isOperationInprogress) {
                        var msg = "Total Records : ";
                        msg += response.Data.TotalRecords;
                        msg += ",<br>";
                        msg += "Total Success : ";
                        msg += response.Data.SuccessRecords;
                        msg += ",<br>";
                        msg += "Total fail : ";
                        msg += response.Data.FailedRecords;
                        showSnackbar(msg);
                    }
                }
                isOperationInprogress = false;
                if (response.Data.IsCompleted && isOnLoad == true) {

                }
                else
                    updateProgress(parseInt(response.Data.Percentage));
            }
        });
}