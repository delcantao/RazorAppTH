﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {

    // Begin - Tooltip result page
    var timer;
    $(".helper-tooltip").on("mouseover", function () {
        let text = $(this).attr('text-helper');
        if (text) {
            timer = setTimeout(() => {
                let position = $(this).offset();
                position.top += 20;
                position.left += 20;
                $(".helpers").css(position);
                $(".helpers").show('fast');
                text = text.replaceAll(";", '<br />');
                $(".helpers").html(text);
            }, 300);
        }
    });
    $(".helper-tooltip").on("mouseout", function () {
        clearTimeout(timer);
        $(".helpers").html("");
        $(".helpers").hide();

    })
    // End - Tooltip result page


    $("input[type='text'").on("keydown", function (e, f, g) {
        if (e.keyCode == 13) {
            jQueryResultPost(document.getElementById('form-result'))
        }
    })

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

    jQueryCheckLimpar = () => {

        let inputs = $("input[type='checkbox']")
        console.log(inputs);
        inputs.each((idx, obj) => {
            if ($(obj).prop("checked"))
                $(obj).click()
        })
    }
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


    jQueryLimparPost = form => {
        $("input[type='text']").val("");
        $("#resultView-1").empty();
        $("#resultView-2").empty();
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                /*
                showLoading(false);
                if (!res.isValid) {
                    // exibe mensagem de erro 
                    $("#return-message").show();
                    $("#return-message").html(res.message);
                    return;
                }
                $("#resultView-1").html(res.htmlView1);
                $("#resultView-2").html(res.htmlView2);
                */
            },
            error: function (err) {
                console.log(err)
            }
        })
    }

    jQueryResultPost = form => {
        try {
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