// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {

    function showLoading (on) {
        var _loading = $("#loading-animation")
        var _1stblock = $("#first-block")
        var _2ndblock = $("#second-block")

        if (on) {
            _loading.show();
            _1stblock.hide();
            _2ndblock.hide();
            return;
        }
        _loading.hide("slow");
        _1stblock.show("fast");
        _2ndblock.show("fast");
    }

    $("input").on("click", function () {
        $("#return-message").hide();
    });


    jQueryCheckPost = form => {
        try {
            console.log(form);
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    if (!res.isValid) {
                        $("#return-message").show();
                        $("#return-message").html(res.message);
                        return;
                    }
                    window.location.href = './Resultado';
                },
                error: function (err) {
                    console.log(err)
                }
            })
            return false;
        } catch (ex) {
            console.log(ex)
        }
    }




    jQueryResultPost = form => {
        try {
            console.log(form)


            showLoading(true);
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    showLoading(false);
                    if (!res.isValid) {
                        // exibe mensagem de erro 
                        $("#return-message").show();
                        $("#return-message").html(res.message);
                        return;
                    }
                    $("#resultView-1").html(res.htmlView1);
                    $("#resultView-2").html(res.htmlView2);

                },
                error: function (err) {
                    console.log(err)
                }
            })
            return false;
        } catch (ex) {
            console.log(ex)
        }
    }

});