// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {

    //$("#formCheck").on("submit", function (e, f, x) {
    //    e.preventDefault();
    //    console.log(e, f, x);
    //});

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
                        // exibe mensagem de erro 
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
            console.log(form);
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    if (!res.isValid) {
                        // exibe mensagem de erro 
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