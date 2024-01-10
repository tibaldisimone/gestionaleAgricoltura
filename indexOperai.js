$(() =>{
    $("#titleOperaio").hide();
    $("#titleOreOperai").hide();
    $("#btnAggiungiOre").hide();
    let logIn = sendRequestNoCallback("api/operai/getAllOperai/", "GET", {});
    logIn.fail(function (jqXHR) {
        errore(jqXHR);

    })
    logIn.done(function (serverData) {
        console.log(serverData)
       
            let intest = Object.keys(serverData[0]);
            let vect = Object.values(serverData[0])
            let i = 0;
        let dat = [];
        let u = 0;
            $("#tableOperai").html(" ");
            let tr = $("<tr>");
        $("#tableOperai").append(tr);
        $("#titleOperaio").show();
            $("#operai").children("div").eq(0).append($("#tableOperai"));
            for (let item of intest) {
                $("<th>").html(item).appendTo(tr);
            }
        
            for (let item of serverData) {
                let trDati = $("<tr>");
                $("#tableOperai").append(trDati);
              
                for (let voice of intest) {
                 
                    $("<td>").appendTo(trDati).html(item[voice]).attr("id",item.IdOperaio);
                    trDati.attr("id",i)
                
                    dat[i] = item[voice];
                    i++;
                    console.log(i)
                    
                  
                   
                }
                trDati.on("click", function () {

                    $("#modelOperai").show();


                    dat[i] = item[0];

                    console.log(i)
                    $("#txtIdOperaio").val(" ").css({ "margin": "5px" });
                    $("#txtCognome").val(" ").css({ "margin": "5px" });
                    $("#txtNome").val(" ").css({ "margin": "5px" });
                    $("#txtidUtente").val(" ").css({ "margin": "5px" });
                    console.log()
                    $("#txtIdOperaio").val(dat[$(this).attr('id') - 3]).css({ "margin": "5px" });
                   
                    $("#txtCognome").val(dat[$(this).attr('id') - 2]).css({ "margin": "5px" });
                    $("#txtNome").val(dat[$(this).attr('id') - 1]).css({ "margin": "5px" });
                    $("#txtidUtente").val(dat[$(this).attr('id')]).css({ "margin": "5px" });
                    u++
                })
                

            }
            
        
    })

    $("#btnModifica").on("click", function () {
        let postData = "{idOperaio: '" + $("#txtIdOperaio").val() + "', cognome: '" + $("#txtCognome").val() + "', nome: '" + $("#txtNome").val() + "', idUtente: '" + $("#txtidUtente").val() + "'}";
        let getID11 = sendRequestNoCallback("api/" + "operai" + "/update" + "/", "POST", postData);
        getID11.fail(function (jqXHR) {
            errore(jqXHR);
            console.log(serverData);
            $("#lblMessage").val(serverData).css({
                "color": "white"
            })
        })
        getID11.done(function (serverData) {
            $("#lblMessage").text(serverData)
            console.log(serverData)
        })
    })
    $("#btnDelete").on("click", function () {
        let postData = "{idOperaio: '" + $("#txtIdOperaio").val() + "'}";
        let getID1 = sendRequestNoCallback("api/" + "operai" + "/delete" + "/", "POST", postData);
        getID1.fail(function (jqXHR) {
            errore(jqXHR);
            $("#lblMessage").text(serverData)
        })
        getID1.done(function (serverData) {
            $("#lblMessage").text(serverData)
        })
    })
    $("#btnShowOre").on("click", function () {
        $("#modelOperai").hide();
        let postData = "{idOperaio: '" + $("#txtIdOperaio").val() + "'}";
        let getID1 = sendRequestNoCallback("api/" + "oreOperai" + "/getAllOreOperaiIdOperaio" + "/", "POST", postData);
        getID1.fail(function (jqXHR) {
            errore(jqXHR);
            $("#lblMessage").text(serverData)
        })
        getID1.done(function (serverData) {
            let intest = Object.keys(serverData[0]);
            let vect = Object.values(serverData[0])
            let i = 0;
            let dat = [];
            $("#tableOreOperai").html(" ");
            $("#titleOreOperai").show()
            $("#btnAggiungiOre").show();
            let tr = $("<tr>");
            $("#tableOreOperai").append(tr);
            $("#oreOperai").children("div").eq(0).append($("#tableOreOperai"));
            for (let item of intest) {
                $("<th>").html(item).appendTo(tr);
            }
            for (let item of serverData) {
                let trDati = $("<tr>");
                $("#tableOreOperai").append(trDati);
           
                for (let voice of intest) {
              
                    $("<td>").appendTo(trDati).html(item[voice]);
                    trDati.on("click", function () {
                        $("#modelOreOperai").show();
                        console.log(item.Data)
                        dat[i] = item[voice];
                        i++;
                        console.log(dat[i])
                        console.log(i)
                       
                        $("#txtIdOre").val(dat[0]).css({ "margin": "5px" });
                        $("#txtData").val(dat[2]).css({ "margin": "5px" });
                        $("#txtNumeroLavorate").val(dat[1]).css({ "margin": "5px" });
                        $("#txtidOperaio").val(dat[3]).css({ "margin": "5px" });
                       
                    })
                }

            }
        })
    })
    $("#btnModificaOre").on("click", function () {
        let postData = "{idOre: '" + $("#txtIdOre").val() + "', dataOraOperaio: '" + $("#txtData").val() + "', numOre: '" + $("#txtNumeroLavorate").val() + "', idOperaio: '" + $("#txtidOperaio").val() + "'}";
        let getID11 = sendRequestNoCallback("api/" + "oreOperai" + "/update" + "/", "POST", postData);
        getID11.fail(function (jqXHR) {
            errore(jqXHR);
            console.log(serverData);
            $("#lblMessage").val(serverData).css({
                "color": "white"
            })
        })
        getID11.done(function (serverData) {
            $("#lblMessage").text(serverData)
            console.log(serverData)
        })
    })
    $("#btnInserisciOre").on("click", function () {
      
        let postData = "{idOre: '" + $("#txtidOre").val() + "', dataOraOperaio: '" + $("#txtdata").val() + "', numOreLavorate: '" + $("#txtnumeroLavorate").val() + "', idOperaio: '" + $("#txtiddOperaio").val() + "'}";
        let getID11 = sendRequestNoCallback("api/" + "oreOperai" + "/insert" + "/", "POST", postData);
        getID11.fail(function (jqXHR) {
            errore(jqXHR);
            console.log(serverData);
            $("#lblMessage").val(serverData).css({
                "color": "white"
            })
        })
        getID11.done(function (serverData) {
            $("#lblMessage").text(serverData)
            console.log(serverData)
        })
    })
    $("#btnDeleteOre").on("click", function () {
        let postData = "{idOperaio: '" + $("#txtIdOperaio").val() + "'}";
        let getID1 = sendRequestNoCallback("api/" + "operai" + "/delete" + "/", "POST", postData);
        getID1.fail(function (jqXHR) {
            errore(jqXHR);
            $("#lblMessage").text(serverData)
        })
        getID1.done(function (serverData) {
            $("#lblMessage").text(serverData)
        })
    })
    $("#btnAggiungiOre").on("click", function () {
       
        $("#modeloreOperai").show();
        let getId = sendRequestNoCallback("api/" + "oreOperai" + "/getLastId", "POST");
        getId.fail(function (jqXHR) {
            console.log(jqXHR.status)
        })
        getId.done(function (serverData) {
            $("#txtidOre").val(serverData + 1).prop('disabled', true);

        })
        $("#txtdata").val(new Date().toISOString().split('T')[0])
        $("#txtiddOperaio").val($("txtIdOperaio").val())
    })

    $("#btnClose").on("click", function () {
        $("#modelOperai").hide();
        location.reload();
    })
    $("#closeOre").on("click", function () {
        $("#modeloOreOperai").hide();
        location.reload();
    })
    $("#closeeOre").on("click", function () {
        $("#modeloreOperai").hide();
        location.reload();
    })
   
})

function errore(jqXHR) {
    console.log("Errore esecuzione web service ASP.Net");
    let code = jqXHR.status;
    let message = jqXHR.responseText;
    $("#lErr").show();
    if (code == 0)
        $("#lErr").html("Server Timeout!");
    else
        console.log("Codice errore: " + code + " - " + message)
}