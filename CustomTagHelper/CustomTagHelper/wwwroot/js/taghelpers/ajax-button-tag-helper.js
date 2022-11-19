/// A custom AJAX button tag helper javascript
///
///
$(document).ready(function () {
    $("button[class='my-ajax']").each(function () {
        $(this).addEventListener("click", function (idx, item) {
            // get button attribue value
            let targetId = $(item).data("target-id");
            let url = $(item).data("url");
            let method = $(item).data("method");
            let successCall = $(item).data("call-success");
            let failureCall = $(item).data("call-failure");
            // get ajax data 
            const ajaxData = {};
            $("#" + targetId).find("input, textarea, select").each(function (idx, node) {
                ajaxData[node.name] = node.value;
            });

            ajaxCall(url, method, ajaxData, successCall, failureCall);

        })
    });
})

const ajaxCall = function(url, method, data, successCall, failureCall) {
    $.ajax({
        url: url,
        type: method,
        dataType: "json",
        //contentType: "application/json; charset=utf-8",
        data: data,
        success: function (result) {
            if (successCall !== "") {
                window[successCall]();
            }
            // when call is sucessfull
        },
        error: function (err) {
            // check the err for error details
            if (failureCall !== "") {
                window[failureCall]();
            }
        }
    });
}

// register ajax button
//var elems = document.getElementByClass("my-ajax");



