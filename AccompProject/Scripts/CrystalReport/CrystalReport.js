$(document).ready(function () {

    $('#btnLoadReport').click(function () {

        ReportManager.LoadReport();



    });




});


var ReportManager = {

    LoadReport: function () {

        var jsonParams = "";
        var serviceUrl = "../CrystalReport/GenerateCrystalReport/";
        alert('');
        ReportManager.GetReport(serviceUrl, jsonParams, onFailed);
        function onFailed(error) {

            alert("Found Error");
        }
    },


    GetReport: function (serviceUrl, jsonParams, errorCallback) {

        jQuery.ajax({
            url: serviceUrl,
            async: false,
            type: "POST",
            data: "{" + jsonParams + "}",
            contentType: "application/json; charset=utf-8",
            success: function () {
                alert('success');
                window.open('../Report/ReportViewer.aspx', '_newtab');

            },



            error: errorCallback


        });

    }
}