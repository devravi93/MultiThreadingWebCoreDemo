/// <reference path="../site.js" />


$(function () {

    getMultiProcessStatus(true);

    $('#btnStart').click(function () {
        if (isOperationInprogress) {
            alert('process already going on');
            return;
        }
        makeAjaxCall(url).
            done(function (response) {
                if (response.isSuccess) {
                    showSnackbar(response.returnMessage);
                    blockCurrentProcessing();
                }
            }).
            fail(function (error) { });
    });
});