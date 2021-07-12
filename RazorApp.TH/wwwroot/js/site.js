// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {

    console.log("uhul")

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
                    if (res.isValid) {
                        $('#viewAll').html(res.html)
                        $('#form-modal').modal('hide');
                    }
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

    jQueryModalGet = (url, reload) => {
        try {
            $.ajax({
                type: 'GET',
                url: url,
                contentType: false,
                processData: false,
                success: function (res) {
                    console.log("sucesso:", res);
                    var urlAtual = window.location.href;
                    $("#" + reload).load(urlAtual + ' #' + reload, (e) => {
                        console.log('realiza o reload', e);
                    });
                },
                error: function (err) {
                    console.log("erro:", err)
                }
            })
            //to prevent default form submit event
            return false;
        } catch (ex) {
            console.log("throw:", ex)
        }
    }
});