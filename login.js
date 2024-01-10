"use strict"
$(() => {
    hide();
    let k = 0;
    
    $("#title").html("Benvenuto");
   
    $("#btnLogin").on("click", function () {
        console.log("ciaoao")
        $("#title").html("Benvenuto");
        if ($("#txtUsernameRecupero").val() != "" && $("#txtPasswordLg").val() != "") {
            let logIn = sendRequestNoCallback("api/utente/ricercaLogin/" + $("#txtUsernameRecupero").val() + "/" + $("#txtPasswordLg").val(), "GET", {});
            logIn.fail(function (jqXHR) {
                errore(jqXHR);

            })
            logIn.done(function (serverData) {
                console.log(serverData);

                if (serverData == "login avvenuto con successo") {
                    $("#lErr").show().text(serverData);
                    console.log(serverData);
                  
                    window.location.href = "index.html"
                  
                   
                   
                } else {
                    k++
                    console.log(k)
                    if (k >= 5) {
                        let postData = "{username: '" + $("#txtUsernameRecupero").val() + "', password: '" + $("#txtPasswordLg").val() + "'}";
                        let invioEmail = sendRequestNoCallback("api/utente/invioEmailAmministratore", "POST", postData);
                        invioEmail.fail(function (jqXHR) {
                            errore(jqXHR);

                        })
                        invioEmail.done(function (serverData) {
                            console.log(serverData);

                            if (serverData != null) {
                                $("#lErr").show().text(serverData);

                            } else {
                                $("#lErr").show().text(serverData);
                            }
                        });
                    }
                    $("#lErr").show().text(serverData);
                }
            });
        }

    })
    $("#pwdDimenticata").on("click", function () {

        $("#pwdDimenticata").hide()
        $("#userDimenticata").hide()
        $("#pwdDimenticataInvio").show()
        $("#divPasswordLg").hide()
        $("#divEmailRecupero").show()
        $("#title").html("Inserisci credenziali");


    })
    $("#pwdDimenticataInvio").on("click", function () {


        if ($("#txtEmailRecupero").val() != "" && $("#txtUsernameRecupero").val() != "") {
            let postData = "{username: '" + $("#txtUsernameRecupero").val() + "', email: '" + $("#txtEmailRecupero").val() + "', check: '" + "2" + "'}";
            console.log(postData)
            let invioEmail = sendRequestNoCallback("api/utente/invioEmail", "POST", postData);
            invioEmail.fail(function (jqXHR) {
                errore(jqXHR);

            })
            invioEmail.done(function (serverData) {
                console.log(serverData);

                if (serverData != null) {
                    $("#lErr").show().text(serverData);

                } else {
                    $("#lErr").show().text(serverData);
                }
            });
        }
        else {
            $("#lErr").text("ATTENZIONE! Devi riempire tutti i campi");
        }

    })
    $("#userDimenticata").on("click", function () {
        $("#title").html("Inserisci credenziali");
        $("#userDimenticata").hide()
        $("#pwdDimenticata").hide()
        $("#userDimenticataInvio").show()
        $("#divPasswordLg").hide()
        $("#divEmailRecupero").show()


    })
    $("#userDimenticataInvio").on("click", function () {


        if ($("#txtEmailRecupero").val() != "" && $("#txtUsernameRecupero").val() != "") {
            let postData = "{username: '" + $("#txtUsernameRecupero").val() + "', email: '" + $("#txtEmailRecupero").val() + "', check: '" + "1" + "'}";
            console.log(postData)
            let invioEmail = sendRequestNoCallback("api/utente/invioEmail", "POST", postData);
            invioEmail.fail(function (jqXHR) {
                errore(jqXHR);

            })
            invioEmail.done(function (serverData) {
                console.log(serverData);

                if (serverData != null) {
                    $("#lErr").show().text(serverData);

                } else {
                    $("#lErr").show().text(serverData);
                }
            });
        }
        else {
            $("#lErr").text("ATTENZIONE! Devi riempire tutti i campi");
        }

    })
    $("#btnRegister").on("click", function () {
        $("#title").html("Benvenuto");
        var emailRegex = /\S+@\S+\.\S+/;
        if ($("#txtNome").val() != "" && $("#txtCognome").val() != "" && $("#txtEmail").val() != "" && $("#txtUsername").val() != "" && $("#txtPassword").val() != "" && $("#txtPasswordRp").val() != "") {
            console.log("1")
            if (emailRegex.test($("#txtEmail").val())) {
                let passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*&])[A-Za-z\d@$!%*?&]{8,}$/
                if (passwordRegex.test($("#txtPassword").val())) {
                    if ($("#txtPassword").val() == $("#txtPasswordRp").val()) {
                        console.log("inserito OK")

                        let getId = sendRequestNoCallback("api/utente/getLastId", "POST");
                        getId.fail(function (jqXHR) {
                            console.log(jqXHR.status)
                        })
                        getId.done(function (serverData) {
                            console.log(serverData);
                            if (serverData == null) {
                                console.log("ciao")
                                serverData = 0;
                            }
                            let id = serverData++;
                            console.log(id++)
                            let postData = "{idUtente:'" + serverData+1 + "', nome:'" + $("#txtNome").val() + "', cognome: '" + $("#txtCognome").val() + "', email: '" + $("#txtEmail").val() + "', username: '" + $("#txtUsername").val() + "', password: '" + $("#txtPassword").val() + "'}";
                            console.log(postData)
                            let insert = sendRequestNoCallback("api/utente/insert", "POST", postData);

                            insert.fail(function (jqXHR) {
                                errore(jqXHR);
                                $("#lErr").show().text(serverData);
                            })
                            insert.done(function (serverData) {
                                $("#lErr").show().text(serverData);
                            })
                        })

                    }
                    else
                        $("#lErr").show().text("ATTENZIONE! la 2 password inserite non coincidono");
                }
                else
                    $("#lErr").show().text("ATTENZIONE! formato password errato");
            }
            else
                $("#lErr").show().text("ATTENZIONE! Formato della mail non corretto");
        }
        else {
            console.log("2")
            $("#lErr").text("ATTENZIONE! Devi riempire tutti i campi");
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
    $("#lock3").on("click", function () {
        $("#unlock3").show();
        $("#lock3").hide();
        $("#txtPasswordLg").attr('type', 'text');
    });
    $("#unlock3").on("click", function () {
        $("#unlock3").hide();
        $("#lock3").show();
        $("#txtPasswordLg").attr('type', 'password');
    });
})
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
    $("#userDimenticataInvio").hide()
    $("#divEmailRecupero").hide()
}
function setLocalToken(data) {
    data = data.replace(/"/g, "");
    console.log(data);
    localStorage.setItem("token", data);
}
function getHeaders(data) {
    console.log(data);
    if (data == "false") {
        localStorage.clear();
        $("#myModalAlert").show();
    }
    else {
        let uriWebService = "api/token/setLocalToken";
        send_request(uriWebService, "GET", "", setLocalToken);
    }
}
function insertUtente() {

};
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