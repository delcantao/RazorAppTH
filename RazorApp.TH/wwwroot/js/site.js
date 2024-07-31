// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {


    $(".info-click").on("click", (e) => {
        const dataOpen = e.currentTarget.attributes.getNamedItem("data-open")
        const companyId = dataOpen.value;
        const companyElement = $(`#${companyId}`);
        companyElement.toggleClass("hidden-ps");

        if (companyElement.hasClass("hidden-ps")) {
            $(`#info-${companyId}`).text("+ Informações")
        } else {
            $(`#info-${companyId}`).text("- Informações")
        }
    })


    $(document).on("click", ".info-click", function(e) {
        console.log(e)

        const dataOpen = e.currentTarget.attributes.getNamedItem("data-open")
        const companyId = dataOpen.value;
        const companyElement = $(`#${companyId}`);
        companyElement.toggleClass("hidden-ps");

        if (companyElement.hasClass("hidden-ps")) {
            $(`#info-${companyId}`).text("+ Informações")
        } else {
            $(`#info-${companyId}`).text("- Informações")
        }
    });
    
    
    $(document).on("click", "#showjson", function() {
        const mainContainer = $("#container-ps-main");
        const jsonContainer = $("#json-container");

        mainContainer.toggleClass("hidden-ps");
        jsonContainer.toggleClass("hidden-ps");
        if (mainContainer.hasClass("hidden-ps")) {
            $("#showjson").text("Ocultar JSON");
        } else {
            $("#showjson").text("Exibir JSON");
        }
    });
    
    $("#form-result").on("submit", function(a1, a2, a3) {
        console.log(a1, a2, a3)
        jQueryResultPost(document.getElementById('form-result'));
        const _2ndblock = $("#second-block")
        _2ndblock.hide()
        setTimeout(()=>{
            _2ndblock.hide()
        }, 1800)
        return false;
    });
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
    $(".input-open-gateway").on("keydown", function (event) {
        if (event.key == "Enter") {
            event.preventDefault();
        }
        
    });

    $("input[type='text'").on("keydown", function (e, f, g) {
        if (e.keyCode == 13) {
            jQueryResultPost(document.getElementById('form-result'))
        }
    })
    function showLoading(on) {
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
    $("#check_send").on("click", (e) => {
        e.preventDefault();

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
                    
                    window.location.href = res.redirectTo ? res.redirectTo : './Resultado';
                },
                error: function (err, a, b, c, d) {
                    console.log(err, a, b, c, d)
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
    jQueryResultPost = (form) => {
        try {
                       
            var _2ndblock = $("#second-block")
            const hideSecondBlock = form.action.indexOf("ConsultaParticipacaoResultado") != -1
            
            
            
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
                        $("#resultView-1").html("");
                        $("#resultView-2").html("");
                        return;
                    }
                    $("#resultView-1").html(res.htmlView1);
                    $("#resultView-2").html(res.htmlView2);
                    if (hideSecondBlock) _2ndblock.hide()
                },
                error: function (err) {
                    console.log(err)
                    if (hideSecondBlock) _2ndblock.hide()
                }
            })
            return false;
        } catch (ex) {
            if (hideSecondBlock) _2ndblock.hide()            
            console.log(ex)
        }
    }

});


