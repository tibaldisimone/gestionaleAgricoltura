"use strict"

function cambioPassword() {
    controlloPagina()
    const urlParams = new URLSearchParams(window.location.search);
    const var1 = urlParams.get('var1');
    $("#btnChange").on("click", function (){
     
        if ($("#txtPassword").val() != "" && $("#txtPasswordRp").val() != "") {
            let passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*&])[A-Za-z\d@$!%*?&]{8,}$/
            if (passwordRegex.test($("#txtPassword").val())) {
                if ($("#txtPassword").val() == $("#txtPasswordRp").val()) {
                  
                    let update = sendRequestNoCallback("api/utente/aggiornaPassword/" + $("#txtPassword").val() + "/" + urlParams.get('var1'), "GET", {});
                    update.fail(function (jqXHR) {
                        errore(jqXHR);
                    })
                    update.done(function (serverData) {
                        $("#lErr").html(serverData);

                    })
                }
            }
        }
       
    })

    hide()

    $("#lock").on("click", function () {
        $("#unlock").show();
        $("#lock").hide();
        $("#txtPassword").attr('type', 'text');
    });
    $("#unlock").on("click", function () {
        $("#unlock").hide();
        $("#lock").show();
        $("#txtPassword").attr('type', 'password');
    });
    $("#lock2").on("click", function () {
        $("#unlock2").show();
        $("#lock2").hide();
        $("#txtPasswordRp").attr('type', 'text');
    });
    $("#unlock2").on("click", function () {
        $("#unlock2").hide();
        $("#lock2").show();
        $("#txtPasswordRp").attr('type', 'password');
    });
}

function cambioUsername() {
    controlloPagina()
    hide()
    const urlParams = new URLSearchParams(window.location.search);
    const var1 = urlParams.get('var1');
    $("#btnCambia").on("click", function () {

        if ($("#txtPassword").val() != "" && $("#txtPasswordRp").val() != "") {
            let passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*&])[A-Za-z\d@$!%*?&]{8,}$/
            if (passwordRegex.test($("#txtPassword").val())) {
                if ($("#txtPassword").val() == $("#txtPasswordRp").val()) {

                   
                    let postData = "{password:'" + $("#txtPassword").val() + "', var1:'" + urlParams.get('var1') + "', username: '" + $("#txtUsername").val() +"'}";
                    console.log(postData)
                    let update = sendRequestNoCallback("api/utente/aggiornaCredenziali", "POST", postData);
                    update.fail(function (jqXHR) {
                        errore(jqXHR);
                    })
                    update.done(function (serverData) {
                        $("#lErr").html(serverData);

                    })
                }
            }
        }

    })

    $("#lock").on("click", function () {
        $("#unlock").show();
        $("#lock").hide();
        $("#txtPassword").attr('type', 'text');
    });
    $("#unlock").on("click", function () {
        $("#unlock").hide();
        $("#lock").show();
        $("#txtPassword").attr('type', 'password');
    });
    $("#lock2").on("click", function () {
        $("#unlock2").show();
        $("#lock2").hide();
        $("#txtPasswordRp").attr('type', 'text');
    });
    $("#unlock2").on("click", function () {
        $("#unlock2").hide();
        $("#lock2").show();
        $("#txtPasswordRp").attr('type', 'password');
    });
}

function controlloPagina() {
    const urlParams = new URLSearchParams(window.location.search);
    const var1 = urlParams.get('var1');
    const var2 = urlParams.get('var2');
    let logIn = sendRequestNoCallback("api/utente/controlloPagina/" + var1 + "/" + var2, "GET", {});
    logIn.fail(function (jqXHR) {
        errore(jqXHR);
        console.log(serverData);
    })
    logIn.done(function (serverData) {
        if (serverData != "ok") {
            window.location.href = "http://simonetibaldi-001-site1.dtempurl.com/login.html"
        }
    });
}

function mostraIcona() {
    document.getElementById("finestra").style.display = "block";
}
function nascondiFinestra() {
    document.getElementById("finestra").style.display = "none";
}
function hide() {
    $("#unlock").hide();
    $("#unlock2").hide();
    $("#unlock3").hide();
    $("#pwdDimenticataInvio").hide()
    $("#divEmailRecupero").hide()
}

function errore(jqXHR) {
    console.log("Errore esecuzione web service ASP.Net");
    let code = jqXHR.status;
    let message = jqXHR.responseText;
    $("#lErr").show();
    if (code == 0)
        $("#lErr").html("Server Timeout!");
    else
        $("#lErr").html("Codice errore: " + code + " - " + message);
}