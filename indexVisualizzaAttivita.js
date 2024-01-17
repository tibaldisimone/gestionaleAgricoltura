$(window).on('load', function () {
    $(".line").hide();
    let vect = new Array("lancioInsetti", "trattamenti", "sfemminellatura", "concimazione", "piantato", "raccolta");
   
    var parametro = new URLSearchParams(window.location.search).get("parametro");
    for (let i = 0; i < vect.length; i++) {
        let postData = "{idZona: '" + parametro + "'}";
        let dati = sendRequestNoCallback("api/" + vect[i] + "/getAll" + vect[i] + "Zone", "POST", postData);

        dati.fail(function (jqXHR) {
            errore(jqXHR);
            console.log(serverData);
        })
        dati.done(function (serverData) {
            let intest = Object.keys(serverData[0]);
            var table = $('<table>');
            var thead = $('<thead>');
            var tbody = $('<tbody>');
            var dynamicText = "Dettagli " +vect[i];
            var dynamicH1 = $("<h1></h1>");
            dynamicH1.html(dynamicText)
            thead.append(dynamicH1)
            dynamicH1.css({

               
                "margin-top": "25px",
                "margin-bottom":"25px"


            })
            var headerRow = $('<tr>');
            intest.forEach(function (headerText) {
                var headerCell = $('<th>').text(headerText);
               headerRow.append(headerCell);
              
            });
            thead.append(headerRow);
            thead.attr("id", "thead" + vect[i])
            table.append(thead);
            dynamicH1.attr("id", vect[i])
            let id = 0;
            let sfemminellatura;
            let concimazione;
            let raccolta;
            let trattamento;
            serverData.forEach(function (item) {

                if (item.IdSfemminellatura != undefined) {
                    let nomeOperaio = " ";
                    if (sfemminellatura != item.IdSfemminellatura) {
                        sfemminellatura = item.IdSfemminellatura;
                        for (let l = 0; l < serverData.length; l++) {
                            let descrizioneOperaio = Object.values(serverData[l])
                            if (sfemminellatura == descrizioneOperaio[0]) {
                               
                                nomeOperaio += "- " + descrizioneOperaio[3] + " <br>"
                            }
                        }
                        
                        var row = $('<tr>');
                        row.append($('<td>').text(item.IdSfemminellatura).attr("id", "idSfemminellatura" + "Sfemminellatura" + item.IdSfemminellatura));
                        row.append($('<td>').text(moment(item.Data).format("DD/MM/YYYY"))).attr("id", "data" + "Sfemminellatura" + item.IdSfemminellatura);
                        row.append($('<td>').text(item.IdZona).attr("id", "idZona" + "Sfemminellatura" + item.IdSfemminellatura));
                        row.append($('<td>').html(nomeOperaio).attr("id", "nome" + "Sfemminellatura" + item.IdSfemminellatura));
                        nomeOperaio = " ";
                        row.attr("id", id + item.IdSfemminellatura)
                        id = 0;
                        tbody.append(row);
                        row.on("click", function () {
                            $("#tableModifica").show()
                            var label = $('<label>').text("data" + ": ").css({
                                "display": "block",
                                "margin-bottom": "5px"
                            });
                                   
                            var datePickerId = "datepicker" + ($("input[type='text'][id^='datepicker']").length + 1);
                            $("<label>").attr("for", datePickerId).text("Seleziona la data:").appendTo("body");
                            $("<input>").attr("type", "text").attr("id", "Data").appendTo("body");
                            $("#" + "Data").datepicker().css({
                                "width": "100%",
                                "box-sizing": "border-box",
                                "padding": "10px",
                                "border": "1px solid #ccc",
                                "border-radius": "4px",
                                "margin-bottom": "10px",


                            });;

                            $("#" + "Data").datepicker({
                                appendTo: ".datepicker-container",
                                position: { my: "right top", at: "right top", of: $("#" + "Data") }
                            })
                            var div = $('<div>').append(label, $("#" + "Data"));
                            $('#bodyModifica').append(div);
                                 
                            var label = $('<label>').text("idZona" + ": ").css({
                                "display": "block",
                                "margin-bottom": "5px"
                            });   

                            $("#" + "Data").val($("#dataSfemminellatura" + $(this).attr('id')).text())

                            var numeri = [1, 2, 3, 4, 5, 6];
                            var comboBox = $('<select></select>').css({
                                "width": "100%",
                                "box-sizing": "border-box",
                                "padding": "10px",
                                "border": "1px solid #ccc",
                                "border-radius": "4px",
                                "margin-bottom": "10px"
                            });;
                            for (var z = 0; z < numeri.length; z++) {
                                comboBox.append($('<option></option>').attr('value', numeri[z]).text(numeri[z]));
                            }
                            comboBox.attr("id", "idZona")
                            var div = $('<div>').append(label, comboBox);
                            $('#bodyModifica').append(div);
                            let idSelezionati=[];
                                      
                            let getId = sendRequestNoCallback("api/" + "operai" + "/getAllOperai", "GET");
                            getId.fail(function (jqXHR) {
                                console.log(jqXHR.status)
                            })
                            getId.done(function (serverData) {
                               
                                var values = $("#nomeSfemminellatura").text().split(" ");
                                
                              
                                var div = $('<div>').append(label, comboBox);
                                $('#bodyModifica').append(div);
                                let valori = Object.values(serverData)
                                let w = 0;
                              
                                for (let p = 0; p < values.length; p++) {

                                    serverData.forEach(function (item) {
                                        if (item.Nome == values[p]) {
                                            idSelezionati[w] = item.IdOperaio;
                                            w++;

                                        }
                                      
                                        
                                    })
                                 
                                        
                                 }

                              
                                let vettore = [];
                                let intest2 = Object.keys(serverData[0]);
                                serverData.forEach(function (item) {
                                    Object.values(item).forEach(function (value) {
                                        if (intest2[i] == "Data") {
                                            $("#" + intest2[i]).val(value.slice(0, -9))
                                        }
                                        else {
                                            $("#" + intest2[i]).val(value)
                                        }
                                        if (i == 0) {
                                            $("#" + intest2[i]).prop('disabled', true);

                                        }
                                        vettore[i] = intest2[i];
                                        i++;

                                    });
                                })
                                var buttonUpdate = $('<button></button>');
                                buttonUpdate.attr('id', 'updateButton');
                                buttonUpdate.text('UPDATE');
                                buttonUpdate.css({
                                    "width": "100%",
                                    "box-sizing": "border-box",
                                    "padding": "10px",
                                    "border": "1px solid #ccc",
                                    "border-radius": "4px",
                                    "margin-bottom": "10px",
                                    'background-color': 'grey'
                                });
                                buttonUpdate.appendTo('#bodyModifica');
                                var button = $('<button></button>');
                                button.attr('id', row.attr("id").replace(/\D/g, ''));
                                button.text('DELETE');
                                button.css({
                                    "width": "100%",
                                    "box-sizing": "border-box",
                                    "padding": "10px",
                                    "border": "1px solid #ccc",
                                    "border-radius": "4px",
                                    "margin-bottom": "10px",
                                    'background-color': 'red'
                                });
                                button.appendTo('#bodyModifica');
                                button.on("click", function () {

                                    let postData4 = "{id: '" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "'}";
                                  
                                    let getID5 = sendRequestNoCallback("api/" + "esecuzioneSfemminellatura" + "/delete" + "/", "POST", postData4);
                                            getID5.fail(function (jqXHR) {
                                                errore(jqXHR);
                                                $("#lblMessage").text(serverData)
                                            })
                                            getID5.done(function (serverData) {
                                                $("#lblMessage").text(serverData)
                                                for (let i = 0; i < idSelezionati.length; i++) {
                                                    
                                                    let postData14 = "{idSfemminellatura: '" + row.attr("id").replace(/\D/g, '') + "', idOperaio: '" + idSelezionati[i]  + "'}";
                                                    let getID1114 = sendRequestNoCallback("api/" + "esecuzioneSfemminellatura" + "/delete" + "/", "POST", postData14);
                                                    getID1114.fail(function (jqXHR) {
                                                        errore(jqXHR);
                                                   
                                                        $("#lblMessage").val(serverData).css({
                                                            "color": "white"
                                                        })
                                                    })
                                                    getID1114.done(function (serverData) {
                                                        $("#lblMessage").text(serverData)
                                                      
                                                    })
                                                }
                                            })
                                      
                                      

                                    
                                })
                                buttonUpdate.on("click", function () {
                                    let Dat3 = "{id: '" + row.attr("id").replace(/\D/g, '') + "', dataSfemminellatura: '" + $("#Data").val() + "', idZona: '" + $("#idZona").val() + "'}";
                                            let getID26 = sendRequestNoCallback("api/" + "sfemminellatura" + "/update" + "/", "POST", Dat3);
                                            getID26.fail(function (jqXHR) {
                                                errore(jqXHR);
                                                $("#lblMessage").text(serverData)
                                            })
                                            getID26.done(function (serverData) {
                                                $("#lblMessage").text(serverData)
                                                for (let i = 0; i < idSelezionati.length; i++) {
                                                    let postData14 = "{idSfemminellatura: '" + row.attr("id").replace(/\D/g, '') + "', idOperaio: '" + idSelezionati[i] + "', dataSfemminellatura: '" + $("#Data").val() + "'}";
                                                    let getID1114 = sendRequestNoCallback("api/" + "esecuzioneSfemminellatura" + "/update" + "/", "POST", postData14);
                                                    getID1114.fail(function (jqXHR) {
                                                        errore(jqXHR);
                                                      
                                                        $("#lblMessage").val(serverData).css({
                                                            "color": "white"
                                                        })
                                                    })
                                                    getID1114.done(function (serverData) {
                                                        $("#lblMessage").text(serverData)
                                                     
                                                    })
                                                }
                                            })
                                })
                                $("#btnClose").on("click", function () {
                                    $("#tableModifica").hide()
                                    $("#bodyModifica").html(" ")
                                    location.reload();
                                })
                            })
                         });
                    }
                    
                }
                else {
                    if (item.IdConcimazione != undefined) {
                        
                        let nomeConcime = " ";
                        if (concimazione != item.IdConcimazione) {
                            concimazione = item.IdConcimazione;
                            for (let l = 0; l < serverData.length; l++) {
                                let descrizioneConcime = Object.values(serverData[l])
                                if (concimazione == descrizioneConcime[0]) {

                                    nomeConcime += "- " + descrizioneConcime[3] + " <br>"
                                }
                            }
                            var row = $('<tr>');
                            row.append($('<td>').text(item.IdConcimazione));
                            row.append($('<td>').text(moment(item.Data).format("DD/MM/YYYY")));
                            row.append($('<td>').text(item.IdZona));
                            row.append($('<td>').html(nomeConcime));
                            nomeConcime = " ";
                            row.attr("id", id + vect[i])
                            id = 0;
                            tbody.append(row);
                            row.on("click", function () {
                                $("#tableModifica").show()
                                let postData = "{id: '" + row.attr("id").replace(/\D/g, '') + "'}";
                                let getID = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/get" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/", "POST", postData);
                                getID.fail(function (jqXHR) {
                                    errore(jqXHR);
                                    console.log(serverData);
                                })
                                getID.done(function (serverData) {
                              
                                    let intest = Object.keys(serverData[0]);
                                    for (let item of intest) {

                                        var label = $('<label>').text(item + ": ").css({
                                            "display": "block",
                                            "margin-bottom": "5px"
                                        });
                                        if (item.toString() == "Data") {
                                            var datePickerId = "datepicker" + ($("input[type='text'][id^='datepicker']").length + 1);
                                            $("<label>").attr("for", datePickerId).text("Seleziona la data:").appendTo("body");
                                            $("<input>").attr("type", "text").attr("id", item).appendTo("body");
                                            $("#" + item).datepicker().css({
                                                "width": "100%",
                                                "box-sizing": "border-box",
                                                "padding": "10px",
                                                "border": "1px solid #ccc",
                                                "border-radius": "4px",
                                                "margin-bottom": "10px"
                                            });;
                                            $("#" + item).datepicker({
                                                appendTo: ".datepicker-container",
                                                position: { my: "right top", at: "right top", of: $("#" + item) }
                                            });
                                            var div = $('<div>').append(label, $("#" + item));
                                            $('#bodyModifica').append(div);
                                        }
                                        else {
                                            if (item.toString() == "IdZona") {
                                                $("#" + item).val()
                                                var numeri = [1, 2, 3, 4, 5, 6];
                                                var comboBox = $('<select></select>').css({
                                                    "width": "100%",
                                                    "box-sizing": "border-box",
                                                    "padding": "10px",
                                                    "border": "1px solid #ccc",
                                                    "border-radius": "4px",
                                                    "margin-bottom": "10px"
                                                });;
                                                for (var z = 0; z < numeri.length; z++) {
                                                    comboBox.append($('<option></option>').attr('value', numeri[z]).text(numeri[z]));
                                                }
                                                var div = $('<div>').append(label, comboBox);
                                            }
                                            else {
                                                $("#" + item).val()
                                                var input = $('<input>').attr({
                                                    type: 'text',
                                                    id: item
                                                }).css({
                                                    "width": "100%",
                                                    "box-sizing": "border-box",
                                                    "padding": "10px",
                                                    "border": "1px solid #ccc",
                                                    "border-radius": "4px",
                                                    "margin-bottom": "10px"
                                                });
                                                var div = $('<div>').append(label, input);
                                            }
                                        }
                                        $('#bodyModifica').append(div);
                                    }
                                    let i = 0;
                                    let vettore = [];
                                    let intest2 = Object.keys(serverData[0]);
                                    serverData.forEach(function (item) {
                                        Object.values(item).forEach(function (value) {
                                            if (intest2[i] == "Data") {
                                                $("#" + intest2[i]).val(value.slice(0, -9))
                                            }
                                            else {
                                                $("#" + intest2[i]).val(value)
                                            }
                                            if (i == 0) {
                                                $("#" + intest2[i]).prop('disabled', true);

                                            }
                                            vettore[i] = intest2[i];
                                            i++;

                                        });
                                    })
                                    var buttonUpdate = $('<button></button>');
                                    buttonUpdate.attr('id', 'updateButton');
                                    buttonUpdate.text('UPDATE');
                                    buttonUpdate.css({
                                        "width": "100%",
                                        "box-sizing": "border-box",
                                        "padding": "10px",
                                        "border": "1px solid #ccc",
                                        "border-radius": "4px",
                                        "margin-bottom": "10px",
                                        'background-color': 'grey'
                                    });
                                    buttonUpdate.appendTo('#bodyModifica');
                                    var button = $('<button></button>');
                                    button.attr('id', row.attr("id").replace(/\D/g, ''));
                                    button.text('DELETE');
                                    button.css({
                                        "width": "100%",
                                        "box-sizing": "border-box",
                                        "padding": "10px",
                                        "border": "1px solid #ccc",
                                        "border-radius": "4px",
                                        "margin-bottom": "10px",
                                        'background-color': 'red'
                                    });
                                    button.appendTo('#bodyModifica');
                                    button.on("click", function () {
                                        switch (vettore[0]) {
                                            case 'IdRaccolta':
                                                let postData = "{id: '" + row.attr("id").replace(/\D/g, '') + "'}";
                                                let getID1 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/delete" + "/", "POST", postData);
                                                getID1.fail(function (jqXHR) {
                                                    errore(jqXHR);
                                                    $("#lblMessage").text(serverData)
                                                })
                                                getID1.done(function (serverData) {
                                                    $("#lblMessage").text(serverData)
                                                })
                                                break;

                                            case 'idConcimazione':
                                                let postData1 = "{id: '" + row.attr("id").replace(/\D/g, '') + "'}";
                                                let getID2 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/delete" + "/", "POST", postData1);
                                                getID2.fail(function (jqXHR) {
                                                    errore(jqXHR);
                                                    $("#lblMessage").text(serverData)
                                                })
                                                getID2.done(function (serverData) {
                                                    $("#lblMessage").text(serverData)
                                                })
                                                break;

                                            case 'idLancioInsetti':
                                                let postData2 = "{id: '" + row.attr("id").replace(/\D/g, '') + "'}";
                                                let getID3 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/delete" + "/", "POST", postData2);
                                                getID3.fail(function (jqXHR) {
                                                    errore(jqXHR);
                                                    $("#lblMessage").text(serverData)
                                                })
                                                getID3.done(function (serverData) {
                                                    $("#lblMessage").text(serverData)
                                                })
                                                break;
                                            case 'idTrattamenti':
                                                let postData3 = "{id: '" + row.attr("id").replace(/\D/g, '') + "'}";
                                                let getID4 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/delete" + "/", "POST", postData3);
                                                getID4.fail(function (jqXHR) {
                                                    errore(jqXHR);
                                                    $("#lblMessage").text(serverData)
                                                })
                                                getID4.done(function (serverData) {
                                                    $("#lblMessage").text(serverData)
                                                })
                                                break;
                                            case 'idSfemminellatura':
                                                let postData4 = "{id: '" + row.attr("id").replace(/\D/g, '') + "'}";
                                                let getID5 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/delete" + "/", "POST", postData4);
                                                getID5.fail(function (jqXHR) {
                                                    errore(jqXHR);
                                                    $("#lblMessage").text(serverData)
                                                })
                                                getID5.done(function (serverData) {
                                                    $("#lblMessage").text(serverData)
                                                })
                                                break;
                                            case 'idRilevamentiUmitidita':
                                                let postData5 = "{id: '" + row.attr("id").replace(/\D/g, '') + "'}";
                                                let getID6 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/delete" + "/", "POST", postData5);
                                                getID6.fail(function (jqXHR) {
                                                    errore(jqXHR);
                                                    $("#lblMessage").text(serverData)
                                                })
                                                getID6.done(function (serverData) {
                                                    $("#lblMessage").text(serverData)
                                                })
                                                break;
                                            case 'idPiantato':
                                                let postData6 = "{id: '" + row.attr("id").replace(/\D/g, '') + "'}";
                                                let getID7 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/delete" + "/", "POST", postData6);
                                                getID7.fail(function (jqXHR) {
                                                    errore(jqXHR);
                                                    $("#lblMessage").text(serverData)
                                                })
                                                getID7.done(function (serverData) {
                                                    $("#lblMessage").text(serverData)
                                                })
                                                break;

                                            default:

                                        }
                                    })
                                    buttonUpdate.on("click", function () {
                                     
                                        switch (vettore[0]) {
                                            case 'IdRaccolta':

                                                let postData = "{id: '" + row.attr("id").replace(/\D/g, '') + "', dataRaccolta: '" + $("#Data").val() + "', quantita: '" + $("#Quantita").val() + "', idZona: '" + $("#IdZona").val() + "', idRaccoltaFinale: '" + $("#IdRaccoltaFinale").val() + "'}";
                                                let getID11 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/update" + "/", "POST", postData);
                                                getID11.fail(function (jqXHR) {
                                                    errore(jqXHR);
                                                    console.log(serverData);
                                                    $("#lblMessage").val(serverData).css({
                                                        "color": "white"
                                                    })
                                                })
                                                getID11.done(function (serverData) {
                                                    $("#lblMessage").text(serverData)
                                                 
                                                })
                                                break;

                                            case 'IdConcimazione':
                                                let Data = "{id: '" + row.attr("id").replace(/\D/g, '') + "', dataConcimazione: '" + $("#Data").val() + "', quantita: '" + $("#Quantità").val() + "', idZona: '" + $("#IdZona").val() + "'}";
                                                let getID12 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/update" + "/", "POST", Data);
                                                getID12.fail(function (jqXHR) {
                                                    errore(jqXHR);
                                                    
                                                    $("#lblMessage").text(serverData)
                                                })
                                                getID12.done(function (serverData) {
                                                    $("#lblMessage").text(serverData)
                                                })
                                                break;

                                            case 'IdLancioInsetti':
                                                let Dat1 = "{id: '" + row.attr("id").replace(/\D/g, '') + "', dataLancioInsetti: '" + $("#Data").val() + "', quantita: '" + $("#Quantita").val() + "', idZona: '" + $("#IdZona").val() + "'}";
                                                let getID13 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/update" + "/", "POST", Dat1);
                                                getID13.fail(function (jqXHR) {
                                                    errore(jqXHR);
                                                   
                                                    $("#lblMessage").text(serverData)
                                                })
                                                getID13.done(function (serverData) {
                                                    $("#lblMessage").text(serverData)
                                                })
                                                break;
                                            case 'IdTrattamento':
                                                let Dat2 = "{id: '" + row.attr("id").replace(/\D/g, '') + "', dataTrattamento: '" + $("#Data").val() + "', quantità: '" + $("#Quantita").val() + "', idZona: '" + $("#IdZona").val() + "'}";
                                                let getID16 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/update" + "/", "POST", Dat2);
                                                getID16.fail(function (jqXHR) {
                                                    errore(jqXHR);
                                                
                                                    $("#lblMessage").text(serverData)
                                                })
                                                getID16.done(function (serverData) {
                                                    $("#lblMessage").text(serverData)
                                                })
                                                break;
                                            case 'IdSfemminellatura':
                                                let Dat3 = "{id: '" + row.attr("id").replace(/\D/g, '') + "', dataSfemminellatura: '" + $("#Data").val() + "', idZona: '" + $("#IdZona").val() + "'}";
                                                let getID26 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/update" + "/", "POST", Dat3);
                                                getID26.fail(function (jqXHR) {
                                                    errore(jqXHR);
                                                    $("#lblMessage").text(serverData)
                                                })
                                                getID26.done(function (serverData) {
                                                    $("#lblMessage").text(serverData)
                                                })
                                                break;
                                            case 'IdRilevamentoUmidita':

                                                let Dat33 = "{id: '" + row.attr("id").replace(/\D/g, '') + "', valoreUmidita: '" + $("#ValoreUmidita").val() + "', idZona: '" + $("#IdZona").val() + "', dataOra: '" + $("#DataOra").val() + "'}";
                                                let getID263 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/update" + "/", "POST", Dat33);
                                                getID263.fail(function (jqXHR) {
                                                    errore(jqXHR);
                                                    $("#lblMessage").text(serverData)
                                                })
                                                getID263.done(function (serverData) {
                                                    $("#lblMessage").text(serverData)
                                                })
                                                break;
                                            case 'IdPiantato':
                                                let Dat34 = "{id: '" + row.attr("id").replace(/\D/g, '') + "', numPiante: '" + $("#NumPiante").val() + "', idZona: '" + $("#IdZona").val() + "', dataPiantata: '" + $("#Data").val() + "', varieta: '" + $("#Varieta").val() + "'}";
                                                let getID264 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/update" + "/", "POST", Dat34);
                                                getID264.fail(function (jqXHR) {
                                                    errore(jqXHR);
                                                    $("#lblMessage").text(serverData)
                                                })
                                                getID264.done(function (serverData) {
                                                    $("#lblMessage").text(serverData)
                                                })
                                                break;

                                            default:

                                        }

                                    })

                                })

                                $("#btnClose").on("click", function () {
                                    $("#tableModifica").hide()
                                    $("#bodyModifica").html(" ")
                                    location.reload();
                                })


                            });
                        }
                        
                    }
                    else {

                        if (item.IdRaccolta != undefined) {
                            
                            let cognomeOperaio = " ";
                            if (raccolta != item.IdRaccolta) {
                                raccolta = item.IdRaccolta;
                                for (let l = 0; l < serverData.length; l++) {
                                    let descrizioneOperaio = Object.values(serverData[l])
                                   
                                    if (raccolta == descrizioneOperaio[0]) {
                                        cognomeOperaio += "- " + descrizioneOperaio[5] + "<br>"
                                    }
                                }
                                var row = $('<tr>');
                                row.append($('<td>').text(item.IdRaccolta));
                                row.append($('<td>').text(moment(item.Data).format("DD/MM/YYYY")));
                                row.append($('<td>').text(item.Quantita));
                                row.append($('<td>').text(item.IdZona));
                                row.append($('<td>').text(item.IdRaccoltaFinale));
                                row.append($('<td>').html(cognomeOperaio));
                                nome = " ";
                                row.attr("id", id + vect[i])
                                id = 0;
                                tbody.append(row);
                          
                                
                                row.on("click", function () {
                                    $("#tableModifica").show()
                                    let postData = "{id: '" + row.attr("id").replace(/\D/g, '') + "'}";
                                    let getID = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/get" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/", "POST", postData);
                                    getID.fail(function (jqXHR) {
                                        errore(jqXHR);
                                        console.log(serverData);
                                    })
                                    getID.done(function (serverData) {
                                    
                                        let intest = Object.keys(serverData[0]);
                                        for (let item of intest) {

                                            var label = $('<label>').text(item + ": ").css({
                                                "display": "block",
                                                "margin-bottom": "5px"
                                            });
                                            if (item.toString() == "Data") {
                                                var datePickerId = "datepicker" + ($("input[type='text'][id^='datepicker']").length + 1);
                                                $("<label>").attr("for", datePickerId).text("Seleziona la data:").appendTo("body");
                                                $("<input>").attr("type", "text").attr("id", item).appendTo("body");
                                                $("#" + item).datepicker().css({
                                                    "width": "100%",
                                                    "box-sizing": "border-box",
                                                    "padding": "10px",
                                                    "border": "1px solid #ccc",
                                                    "border-radius": "4px",
                                                    "margin-bottom": "10px"
                                                });;
                                                $("#" + item).datepicker({
                                                    appendTo: ".datepicker-container",
                                                    position: { my: "right top", at: "right top", of: $("#" + item) }
                                                });
                                                var div = $('<div>').append(label, $("#" + item));
                                                $('#bodyModifica').append(div);
                                            }
                                            else {
                                                if (item.toString() == "IdZona") {
                                                    $("#" + item).val()
                                                    var numeri = [1, 2, 3, 4, 5, 6];
                                                    var comboBox = $('<select></select>').css({
                                                        "width": "100%",
                                                        "box-sizing": "border-box",
                                                        "padding": "10px",
                                                        "border": "1px solid #ccc",
                                                        "border-radius": "4px",
                                                        "margin-bottom": "10px"
                                                    });;
                                                    for (var z = 0; z < numeri.length; z++) {
                                                        comboBox.append($('<option></option>').attr('value', numeri[z]).text(numeri[z]));
                                                    }
                                                    var div = $('<div>').append(label, comboBox);
                                                }
                                                else {
                                                    $("#" + item).val()
                                                    var input = $('<input>').attr({
                                                        type: 'text',
                                                        id: item
                                                    }).css({
                                                        "width": "100%",
                                                        "box-sizing": "border-box",
                                                        "padding": "10px",
                                                        "border": "1px solid #ccc",
                                                        "border-radius": "4px",
                                                        "margin-bottom": "10px"
                                                    });
                                                    var div = $('<div>').append(label, input);
                                                }
                                            }
                                            $('#bodyModifica').append(div);
                                        }
                                        let i = 0;
                                        let vettore = [];
                                        let intest2 = Object.keys(serverData[0]);
                                        serverData.forEach(function (item) {
                                            Object.values(item).forEach(function (value) {
                                                if (intest2[i] == "Data") {
                                                    $("#" + intest2[i]).val(value.slice(0, -9))
                                                }
                                                else {
                                                    $("#" + intest2[i]).val(value)
                                                }
                                                if (i == 0) {
                                                    $("#" + intest2[i]).prop('disabled', true);

                                                }
                                                vettore[i] = intest2[i];
                                                i++;

                                            });
                                        })
                                        var buttonUpdate = $('<button></button>');
                                        buttonUpdate.attr('id', 'updateButton');
                                        buttonUpdate.text('UPDATE');
                                        buttonUpdate.css({
                                            "width": "100%",
                                            "box-sizing": "border-box",
                                            "padding": "10px",
                                            "border": "1px solid #ccc",
                                            "border-radius": "4px",
                                            "margin-bottom": "10px",
                                            'background-color': 'grey'
                                        });
                                        buttonUpdate.appendTo('#bodyModifica');
                                        var button = $('<button></button>');
                                        button.attr('id', row.attr("id").replace(/\D/g, ''));
                                        button.text('DELETE');
                                        button.css({
                                            "width": "100%",
                                            "box-sizing": "border-box",
                                            "padding": "10px",
                                            "border": "1px solid #ccc",
                                            "border-radius": "4px",
                                            "margin-bottom": "10px",
                                            'background-color': 'red'
                                        });
                                        button.appendTo('#bodyModifica');
                                        button.on("click", function () {
                                            switch (vettore[0]) {
                                                case 'IdRaccolta':
                                                    let postData = "{id: '" + row.attr("id").replace(/\D/g, '') + "'}";
                                                    let getID1 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/delete" + "/", "POST", postData);
                                                    getID1.fail(function (jqXHR) {
                                                        errore(jqXHR);
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    getID1.done(function (serverData) {
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    break;

                                                case 'idConcimazione':
                                                    let postData1 = "{id: '" + row.attr("id").replace(/\D/g, '') + "'}";
                                                    let getID2 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/delete" + "/", "POST", postData1);
                                                    getID2.fail(function (jqXHR) {
                                                        errore(jqXHR);
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    getID2.done(function (serverData) {
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    break;

                                                case 'idLancioInsetti':
                                                    let postData2 = "{id: '" + row.attr("id").replace(/\D/g, '') + "'}";
                                                    let getID3 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/delete" + "/", "POST", postData2);
                                                    getID3.fail(function (jqXHR) {
                                                        errore(jqXHR);
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    getID3.done(function (serverData) {
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    break;
                                                case 'idTrattamenti':
                                                    let postData3 = "{id: '" + row.attr("id").replace(/\D/g, '') + "'}";
                                                    let getID4 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/delete" + "/", "POST", postData3);
                                                    getID4.fail(function (jqXHR) {
                                                        errore(jqXHR);
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    getID4.done(function (serverData) {
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    break;
                                                case 'idSfemminellatura':
                                                    let postData4 = "{id: '" + row.attr("id").replace(/\D/g, '') + "'}";
                                                    let getID5 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/delete" + "/", "POST", postData4);
                                                    getID5.fail(function (jqXHR) {
                                                        errore(jqXHR);
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    getID5.done(function (serverData) {
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    break;
                                                case 'idRilevamentiUmitidita':
                                                    let postData5 = "{id: '" + row.attr("id").replace(/\D/g, '') + "'}";
                                                    let getID6 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/delete" + "/", "POST", postData5);
                                                    getID6.fail(function (jqXHR) {
                                                        errore(jqXHR);
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    getID6.done(function (serverData) {
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    break;
                                                case 'idPiantato':
                                                    let postData6 = "{id: '" + row.attr("id").replace(/\D/g, '') + "'}";
                                                    let getID7 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/delete" + "/", "POST", postData6);
                                                    getID7.fail(function (jqXHR) {
                                                        errore(jqXHR);
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    getID7.done(function (serverData) {
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    break;

                                                default:

                                            }
                                        })
                                        buttonUpdate.on("click", function () {
                                         
                                            switch (vettore[0]) {
                                                case 'IdRaccolta':

                                                    let postData = "{id: '" + row.attr("id").replace(/\D/g, '') + "', dataRaccolta: '" + $("#Data").val() + "', quantita: '" + $("#Quantita").val() + "', idZona: '" + $("#IdZona").val() + "', idRaccoltaFinale: '" + $("#IdRaccoltaFinale").val() + "'}";
                                                    let getID11 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/update" + "/", "POST", postData);
                                                    getID11.fail(function (jqXHR) {
                                                        errore(jqXHR);
                                                        console.log(serverData);
                                                        $("#lblMessage").val(serverData).css({
                                                            "color": "white"
                                                        })
                                                    })
                                                    getID11.done(function (serverData) {
                                                        $("#lblMessage").text(serverData)
                                                      
                                                    })
                                                    break;

                                                case 'IdConcimazione':
                                                    let Data = "{id: '" + row.attr("id").replace(/\D/g, '') + "', dataConcimazione: '" + $("#Data").val() + "', quantita: '" + $("#Quantità").val() + "', idZona: '" + $("#IdZona").val() + "'}";
                                                    let getID12 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/update" + "/", "POST", Data);
                                                    getID12.fail(function (jqXHR) {
                                                        errore(jqXHR);
                                                        console.log(serverData);
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    getID12.done(function (serverData) {
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    break;

                                                case 'IdLancioInsetti':
                                                    let Dat1 = "{id: '" + row.attr("id").replace(/\D/g, '') + "', dataLancioInsetti: '" + $("#Data").val() + "', quantita: '" + $("#Quantita").val() + "', idZona: '" + $("#IdZona").val() + "'}";
                                                    let getID13 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/update" + "/", "POST", Dat1);
                                                    getID13.fail(function (jqXHR) {
                                                        errore(jqXHR);
                                                        console.log(serverData);
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    getID13.done(function (serverData) {
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    break;
                                                case 'IdTrattamento':
                                                    let Dat2 = "{id: '" + row.attr("id").replace(/\D/g, '') + "', dataTrattamento: '" + $("#Data").val() + "', quantità: '" + $("#Quantita").val() + "', idZona: '" + $("#IdZona").val() + "'}";
                                                    let getID16 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/update" + "/", "POST", Dat2);
                                                    getID16.fail(function (jqXHR) {
                                                        errore(jqXHR);
                                                        console.log(serverData);
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    getID16.done(function (serverData) {
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    break;
                                                case 'IdSfemminellatura':
                                                    let Dat3 = "{id: '" + row.attr("id").replace(/\D/g, '') + "', dataSfemminellatura: '" + $("#Data").val() + "', idZona: '" + $("#IdZona").val() + "'}";
                                                    let getID26 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/update" + "/", "POST", Dat3);
                                                    getID26.fail(function (jqXHR) {
                                                        errore(jqXHR);
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    getID26.done(function (serverData) {
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    break;
                                                case 'IdRilevamentoUmidita':

                                                    let Dat33 = "{id: '" + row.attr("id").replace(/\D/g, '') + "', valoreUmidita: '" + $("#ValoreUmidita").val() + "', idZona: '" + $("#IdZona").val() + "', dataOra: '" + $("#DataOra").val() + "'}";
                                                    let getID263 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/update" + "/", "POST", Dat33);
                                                    getID263.fail(function (jqXHR) {
                                                        errore(jqXHR);
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    getID263.done(function (serverData) {
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    break;
                                                case 'IdPiantato':
                                                    let Dat34 = "{id: '" + row.attr("id").replace(/\D/g, '') + "', numPiante: '" + $("#NumPiante").val() + "', idZona: '" + $("#IdZona").val() + "', dataPiantata: '" + $("#Data").val() + "', varieta: '" + $("#Varieta").val() + "'}";
                                                    let getID264 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/update" + "/", "POST", Dat34);
                                                    getID264.fail(function (jqXHR) {
                                                        errore(jqXHR);
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    getID264.done(function (serverData) {
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    break;

                                                default:

                                            }

                                        })

                                    })

                                    $("#btnClose").on("click", function () {
                                        $("#tableModifica").hide()
                                        $("#bodyModifica").html(" ")
                                        location.reload();
                                    })


                                });
                            }
                            
                        }
                        else {
                            if (item.IdTrattamento != undefined) {

                                let nomeTrattamento = " ";
                                if (trattamento != item.IdTrattamento) {
                                    trattamento = item.IdTrattamento;
                                    for (let l = 0; l < serverData.length; l++) {
                                        let descrizioneTrattamento = Object.values(serverData[l])
                                        if (trattamento == descrizioneTrattamento[0]) {
                                            nomeTrattamento += "- "+descrizioneTrattamento[3] + "<br>"
                                        }
                                    }
                                    var row = $('<tr>');
                                    row.append($('<td>').text(item.IdTrattamento));
                                    row.append($('<td>').text(moment(item.Data).format("DD/MM/YYYY")));
                                    row.append($('<td>').text(item.IdZona));
                                    row.append($('<td>').html(nomeTrattamento));
                                    nomeTrattamento = " ";
                                    row.attr("id", id + vect[i])
                                    id = 0;
                                    tbody.append(row);
                                    row.on("click", function () {
                                        $("#tableModifica").show()
                                        let postData = "{id: '" + row.attr("id").replace(/\D/g, '') + "'}";
                                        let getID = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/get" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/", "POST", postData);
                                        getID.fail(function (jqXHR) {
                                            errore(jqXHR);
                                            console.log(serverData);
                                        })
                                        getID.done(function (serverData) {
                                        
                                            let intest = Object.keys(serverData[0]);
                                            for (let item of intest) {

                                                var label = $('<label>').text(item + ": ").css({
                                                    "display": "block",
                                                    "margin-bottom": "5px"
                                                });
                                                if (item.toString() == "Data") {
                                                    var datePickerId = "datepicker" + ($("input[type='text'][id^='datepicker']").length + 1);
                                                    $("<label>").attr("for", datePickerId).text("Seleziona la data:").appendTo("body");
                                                    $("<input>").attr("type", "text").attr("id", item).appendTo("body");
                                                    $("#" + item).datepicker().css({
                                                        "width": "100%",
                                                        "box-sizing": "border-box",
                                                        "padding": "10px",
                                                        "border": "1px solid #ccc",
                                                        "border-radius": "4px",
                                                        "margin-bottom": "10px"
                                                    });;
                                                    $("#" + item).datepicker({
                                                        appendTo: ".datepicker-container",
                                                        position: { my: "right top", at: "right top", of: $("#" + item) }
                                                    });
                                                    var div = $('<div>').append(label, $("#" + item));
                                                    $('#bodyModifica').append(div);
                                                }
                                                else {
                                                    if (item.toString() == "IdZona") {
                                                        $("#" + item).val()
                                                        var numeri = [1, 2, 3, 4, 5, 6];
                                                        var comboBox = $('<select></select>').css({
                                                            "width": "100%",
                                                            "box-sizing": "border-box",
                                                            "padding": "10px",
                                                            "border": "1px solid #ccc",
                                                            "border-radius": "4px",
                                                            "margin-bottom": "10px"
                                                        });;
                                                        for (var z = 0; z < numeri.length; z++) {
                                                            comboBox.append($('<option></option>').attr('value', numeri[z]).text(numeri[z]));
                                                        }
                                                        var div = $('<div>').append(label, comboBox);
                                                    }
                                                    else {
                                                        $("#" + item).val()
                                                        var input = $('<input>').attr({
                                                            type: 'text',
                                                            id: item
                                                        }).css({
                                                            "width": "100%",
                                                            "box-sizing": "border-box",
                                                            "padding": "10px",
                                                            "border": "1px solid #ccc",
                                                            "border-radius": "4px",
                                                            "margin-bottom": "10px"
                                                        });
                                                        var div = $('<div>').append(label, input);
                                                    }
                                                }
                                                $('#bodyModifica').append(div);
                                            }
                                            let i = 0;
                                            let vettore = [];
                                            let intest2 = Object.keys(serverData[0]);
                                            serverData.forEach(function (item) {
                                                Object.values(item).forEach(function (value) {
                                                    if (intest2[i] == "Data") {
                                                        $("#" + intest2[i]).val(value.slice(0, -9))
                                                    }
                                                    else {
                                                        $("#" + intest2[i]).val(value)
                                                    }
                                                    if (i == 0) {
                                                        $("#" + intest2[i]).prop('disabled', true);

                                                    }
                                                    vettore[i] = intest2[i];
                                                    i++;

                                                });
                                            })
                                            var buttonUpdate = $('<button></button>');
                                            buttonUpdate.attr('id', 'updateButton');
                                            buttonUpdate.text('UPDATE');
                                            buttonUpdate.css({
                                                "width": "100%",
                                                "box-sizing": "border-box",
                                                "padding": "10px",
                                                "border": "1px solid #ccc",
                                                "border-radius": "4px",
                                                "margin-bottom": "10px",
                                                'background-color': 'grey'
                                            });
                                            buttonUpdate.appendTo('#bodyModifica');
                                            var button = $('<button></button>');
                                            button.attr('id', row.attr("id").replace(/\D/g, ''));
                                            button.text('DELETE');
                                            button.css({
                                                "width": "100%",
                                                "box-sizing": "border-box",
                                                "padding": "10px",
                                                "border": "1px solid #ccc",
                                                "border-radius": "4px",
                                                "margin-bottom": "10px",
                                                'background-color': 'red'
                                            });
                                            button.appendTo('#bodyModifica');
                                            button.on("click", function () {
                                                switch (vettore[0]) {
                                                    case 'IdRaccolta':
                                                        let postData = "{id: '" + row.attr("id").replace(/\D/g, '') + "'}";
                                                        let getID1 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/delete" + "/", "POST", postData);
                                                        getID1.fail(function (jqXHR) {
                                                            errore(jqXHR);
                                                            $("#lblMessage").text(serverData)
                                                        })
                                                        getID1.done(function (serverData) {
                                                            $("#lblMessage").text(serverData)
                                                        })
                                                        break;

                                                    case 'idConcimazione':
                                                        let postData1 = "{id: '" + row.attr("id").replace(/\D/g, '') + "'}";
                                                        let getID2 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/delete" + "/", "POST", postData1);
                                                        getID2.fail(function (jqXHR) {
                                                            errore(jqXHR);
                                                            $("#lblMessage").text(serverData)
                                                        })
                                                        getID2.done(function (serverData) {
                                                            $("#lblMessage").text(serverData)
                                                        })
                                                        break;

                                                    case 'idLancioInsetti':
                                                        let postData2 = "{id: '" + row.attr("id").replace(/\D/g, '') + "'}";
                                                        let getID3 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/delete" + "/", "POST", postData2);
                                                        getID3.fail(function (jqXHR) {
                                                            errore(jqXHR);
                                                            $("#lblMessage").text(serverData)
                                                        })
                                                        getID3.done(function (serverData) {
                                                            $("#lblMessage").text(serverData)
                                                        })
                                                        break;
                                                    case 'idTrattamenti':
                                                        let postData3 = "{id: '" + row.attr("id").replace(/\D/g, '') + "'}";
                                                        let getID4 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/delete" + "/", "POST", postData3);
                                                        getID4.fail(function (jqXHR) {
                                                            errore(jqXHR);
                                                            $("#lblMessage").text(serverData)
                                                        })
                                                        getID4.done(function (serverData) {
                                                            $("#lblMessage").text(serverData)
                                                        })
                                                        break;
                                                    case 'idSfemminellatura':
                                                        let postData4 = "{id: '" + row.attr("id").replace(/\D/g, '') + "'}";
                                                        let getID5 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/delete" + "/", "POST", postData4);
                                                        getID5.fail(function (jqXHR) {
                                                            errore(jqXHR);
                                                            $("#lblMessage").text(serverData)
                                                        })
                                                        getID5.done(function (serverData) {
                                                            $("#lblMessage").text(serverData)
                                                        })
                                                        break;
                                                    case 'idRilevamentiUmitidita':
                                                        let postData5 = "{id: '" + row.attr("id").replace(/\D/g, '') + "'}";
                                                        let getID6 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/delete" + "/", "POST", postData5);
                                                        getID6.fail(function (jqXHR) {
                                                            errore(jqXHR);
                                                            $("#lblMessage").text(serverData)
                                                        })
                                                        getID6.done(function (serverData) {
                                                            $("#lblMessage").text(serverData)
                                                        })
                                                        break;
                                                    case 'idPiantato':
                                                        let postData6 = "{id: '" + row.attr("id").replace(/\D/g, '') + "'}";
                                                        let getID7 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/delete" + "/", "POST", postData6);
                                                        getID7.fail(function (jqXHR) {
                                                            errore(jqXHR);
                                                            $("#lblMessage").text(serverData)
                                                        })
                                                        getID7.done(function (serverData) {
                                                            $("#lblMessage").text(serverData)
                                                        })
                                                        break;

                                                    default:

                                                }
                                            })
                                            buttonUpdate.on("click", function () {
                                                console.log(vettore[0])
                                                switch (vettore[0]) {
                                                    case 'IdRaccolta':

                                                        let postData = "{id: '" + row.attr("id").replace(/\D/g, '') + "', dataRaccolta: '" + $("#Data").val() + "', quantita: '" + $("#Quantita").val() + "', idZona: '" + $("#IdZona").val() + "', idRaccoltaFinale: '" + $("#IdRaccoltaFinale").val() + "'}";
                                                        let getID11 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/update" + "/", "POST", postData);
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
                                                        break;

                                                    case 'IdConcimazione':
                                                        let Data = "{id: '" + row.attr("id").replace(/\D/g, '') + "', dataConcimazione: '" + $("#Data").val() + "', quantita: '" + $("#Quantità").val() + "', idZona: '" + $("#IdZona").val() + "'}";
                                                        let getID12 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/update" + "/", "POST", Data);
                                                        getID12.fail(function (jqXHR) {
                                                            errore(jqXHR);
                                                            console.log(serverData);
                                                            $("#lblMessage").text(serverData)
                                                        })
                                                        getID12.done(function (serverData) {
                                                            $("#lblMessage").text(serverData)
                                                        })
                                                        break;

                                                    case 'IdLancioInsetti':
                                                        let Dat1 = "{id: '" + row.attr("id").replace(/\D/g, '') + "', dataLancioInsetti: '" + $("#Data").val() + "', quantita: '" + $("#Quantita").val() + "', idZona: '" + $("#IdZona").val() + "'}";
                                                        let getID13 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/update" + "/", "POST", Dat1);
                                                        getID13.fail(function (jqXHR) {
                                                            errore(jqXHR);
                                                            console.log(serverData);
                                                            $("#lblMessage").text(serverData)
                                                        })
                                                        getID13.done(function (serverData) {
                                                            $("#lblMessage").text(serverData)
                                                        })
                                                        break;
                                                    case 'IdTrattamento':
                                                        let Dat2 = "{id: '" + row.attr("id").replace(/\D/g, '') + "', dataTrattamento: '" + $("#Data").val() + "', quantità: '" + $("#Quantita").val() + "', idZona: '" + $("#IdZona").val() + "'}";
                                                        let getID16 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/update" + "/", "POST", Dat2);
                                                        getID16.fail(function (jqXHR) {
                                                            errore(jqXHR);
                                                            console.log(serverData);
                                                            $("#lblMessage").text(serverData)
                                                        })
                                                        getID16.done(function (serverData) {
                                                            $("#lblMessage").text(serverData)
                                                        })
                                                        break;
                                                    case 'IdSfemminellatura':
                                                        let Dat3 = "{id: '" + row.attr("id").replace(/\D/g, '') + "', dataSfemminellatura: '" + $("#Data").val() + "', idZona: '" + $("#IdZona").val() + "'}";
                                                        let getID26 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/update" + "/", "POST", Dat3);
                                                        getID26.fail(function (jqXHR) {
                                                            errore(jqXHR);
                                                            $("#lblMessage").text(serverData)
                                                        })
                                                        getID26.done(function (serverData) {
                                                            $("#lblMessage").text(serverData)
                                                        })
                                                        break;
                                                    case 'IdRilevamentoUmidita':

                                                        let Dat33 = "{id: '" + row.attr("id").replace(/\D/g, '') + "', valoreUmidita: '" + $("#ValoreUmidita").val() + "', idZona: '" + $("#IdZona").val() + "', dataOra: '" + $("#DataOra").val() + "'}";
                                                        let getID263 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/update" + "/", "POST", Dat33);
                                                        getID263.fail(function (jqXHR) {
                                                            errore(jqXHR);
                                                            $("#lblMessage").text(serverData)
                                                        })
                                                        getID263.done(function (serverData) {
                                                            $("#lblMessage").text(serverData)
                                                        })
                                                        break;
                                                    case 'IdPiantato':
                                                        let Dat34 = "{id: '" + row.attr("id").replace(/\D/g, '') + "', numPiante: '" + $("#NumPiante").val() + "', idZona: '" + $("#IdZona").val() + "', dataPiantata: '" + $("#Data").val() + "', varieta: '" + $("#Varieta").val() + "'}";
                                                        let getID264 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/update" + "/", "POST", Dat34);
                                                        getID264.fail(function (jqXHR) {
                                                            errore(jqXHR);
                                                            $("#lblMessage").text(serverData)
                                                        })
                                                        getID264.done(function (serverData) {
                                                            $("#lblMessage").text(serverData)
                                                        })
                                                        break;

                                                    default:

                                                }

                                            })

                                        })

                                        $("#btnClose").on("click", function () {
                                            $("#tableModifica").hide()
                                            $("#bodyModifica").html(" ")
                                            location.reload();
                                        })


                                    });
                                }
                                
                            }
                            else {
                                var row = $('<tr>');
                                Object.values(item).forEach(function (value) {
                                    if (id == 0) {
                                        id = value;
                                    }


                                    if (item.IdSfemminellatura != undefined) {


                                    }
                                    else {
                                        
                                        if (moment(value, moment.ISO_8601, true).isValid()) {
                                            
                                            row.append($('<td>').text(moment(value).format("DD/MM/YYYY")));
                                        }
                                        else {
                                           
                                            row.append($('<td>').text(value));
                                        }
                                        
                                    }
                                });

                                row.attr("id", id + vect[i])
                                id = 0;
                                tbody.append(row);
                                row.on("click", function () {
                                    $("#tableModifica").show()
                                    let postData = "{id: '" + row.attr("id").replace(/\D/g, '') + "'}";
                                    let getID = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/get" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/", "POST", postData);
                                    getID.fail(function (jqXHR) {
                                        errore(jqXHR);
                                        console.log(serverData);
                                    })
                                    getID.done(function (serverData) {
                                        console.log(serverData)
                                        let intest = Object.keys(serverData[0]);
                                        for (let item of intest) {

                                            var label = $('<label>').text(item + ": ").css({
                                                "display": "block",
                                                "margin-bottom": "5px"
                                            });
                                            if (item.toString() == "Data") {
                                                var datePickerId = "datepicker" + ($("input[type='text'][id^='datepicker']").length + 1);
                                                $("<label>").attr("for", datePickerId).text("Seleziona la data:").appendTo("body");
                                                $("<input>").attr("type", "text").attr("id", item).appendTo("body");
                                                $("#" + item).datepicker().css({
                                                    "width": "100%",
                                                    "box-sizing": "border-box",
                                                    "padding": "10px",
                                                    "border": "1px solid #ccc",
                                                    "border-radius": "4px",
                                                    "margin-bottom": "10px"
                                                });;
                                                $("#" + item).datepicker({
                                                    appendTo: ".datepicker-container",
                                                    position: { my: "right top", at: "right top", of: $("#" + item) }
                                                });
                                                var div = $('<div>').append(label, $("#" + item));
                                                $('#bodyModifica').append(div);
                                            }
                                            else {
                                                if (item.toString() == "IdZona") {
                                                    $("#" + item).val()
                                                    var numeri = [1, 2, 3, 4, 5, 6];
                                                    var comboBox = $('<select></select>').css({
                                                        "width": "100%",
                                                        "box-sizing": "border-box",
                                                        "padding": "10px",
                                                        "border": "1px solid #ccc",
                                                        "border-radius": "4px",
                                                        "margin-bottom": "10px"
                                                    });;
                                                    for (var z = 0; z < numeri.length; z++) {
                                                        comboBox.append($('<option></option>').attr('value', numeri[z]).text(numeri[z]));
                                                    }
                                                    var div = $('<div>').append(label, comboBox);
                                                }
                                                else {
                                                    $("#" + item).val()
                                                    var input = $('<input>').attr({
                                                        type: 'text',
                                                        id: item
                                                    }).css({
                                                        "width": "100%",
                                                        "box-sizing": "border-box",
                                                        "padding": "10px",
                                                        "border": "1px solid #ccc",
                                                        "border-radius": "4px",
                                                        "margin-bottom": "10px"
                                                    });
                                                    var div = $('<div>').append(label, input);
                                                }
                                            }
                                            $('#bodyModifica').append(div);
                                        }
                                        let i = 0;
                                        let vettore = [];
                                        let intest2 = Object.keys(serverData[0]);
                                        serverData.forEach(function (item) {
                                            Object.values(item).forEach(function (value) {
                                                if (intest2[i] == "Data") {
                                                    $("#" + intest2[i]).val(value.slice(0, -9))
                                                }
                                                else {
                                                    $("#" + intest2[i]).val(value)
                                                }
                                                if (i == 0) {
                                                    $("#" + intest2[i]).prop('disabled', true);

                                                }
                                                vettore[i] = intest2[i];
                                                i++;

                                            });
                                        })
                                        var buttonUpdate = $('<button></button>');
                                        buttonUpdate.attr('id', 'updateButton');
                                        buttonUpdate.text('UPDATE');
                                        buttonUpdate.css({
                                            "width": "100%",
                                            "box-sizing": "border-box",
                                            "padding": "10px",
                                            "border": "1px solid #ccc",
                                            "border-radius": "4px",
                                            "margin-bottom": "10px",
                                            'background-color': 'grey'
                                        });
                                        buttonUpdate.appendTo('#bodyModifica');
                                        var button = $('<button></button>');
                                        button.attr('id', row.attr("id").replace(/\D/g, ''));
                                        button.text('DELETE');
                                        button.css({
                                            "width": "100%",
                                            "box-sizing": "border-box",
                                            "padding": "10px",
                                            "border": "1px solid #ccc",
                                            "border-radius": "4px",
                                            "margin-bottom": "10px",
                                            'background-color': 'red'
                                        });
                                        button.appendTo('#bodyModifica');
                                        button.on("click", function () {
                                            switch (vettore[0]) {
                                                case 'IdRaccolta':
                                                    let postData = "{id: '" + row.attr("id").replace(/\D/g, '') + "'}";
                                                    let getID1 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/delete" + "/", "POST", postData);
                                                    getID1.fail(function (jqXHR) {
                                                        errore(jqXHR);
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    getID1.done(function (serverData) {
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    break;

                                                case 'idConcimazione':
                                                    let postData1 = "{id: '" + row.attr("id").replace(/\D/g, '') + "'}";
                                                    let getID2 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/delete" + "/", "POST", postData1);
                                                    getID2.fail(function (jqXHR) {
                                                        errore(jqXHR);
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    getID2.done(function (serverData) {
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    break;

                                                case 'idLancioInsetti':
                                                    let postData2 = "{id: '" + row.attr("id").replace(/\D/g, '') + "'}";
                                                    let getID3 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/delete" + "/", "POST", postData2);
                                                    getID3.fail(function (jqXHR) {
                                                        errore(jqXHR);
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    getID3.done(function (serverData) {
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    break;
                                                case 'idTrattamenti':
                                                    let postData3 = "{id: '" + row.attr("id").replace(/\D/g, '') + "'}";
                                                    let getID4 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/delete" + "/", "POST", postData3);
                                                    getID4.fail(function (jqXHR) {
                                                        errore(jqXHR);
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    getID4.done(function (serverData) {
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    break;
                                                case 'idSfemminellatura':
                                                    let postData4 = "{id: '" + row.attr("id").replace(/\D/g, '') + "'}";
                                                    let getID5 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/delete" + "/", "POST", postData4);
                                                    getID5.fail(function (jqXHR) {
                                                        errore(jqXHR);
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    getID5.done(function (serverData) {
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    break;
                                                case 'idRilevamentiUmitidita':
                                                    let postData5 = "{id: '" + row.attr("id").replace(/\D/g, '') + "'}";
                                                    let getID6 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/delete" + "/", "POST", postData5);
                                                    getID6.fail(function (jqXHR) {
                                                        errore(jqXHR);
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    getID6.done(function (serverData) {
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    break;
                                                case 'idPiantato':
                                                    let postData6 = "{id: '" + row.attr("id").replace(/\D/g, '') + "'}";
                                                    let getID7 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/delete" + "/", "POST", postData6);
                                                    getID7.fail(function (jqXHR) {
                                                        errore(jqXHR);
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    getID7.done(function (serverData) {
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    break;

                                                default:

                                            }
                                        })
                                        buttonUpdate.on("click", function () {
                                            console.log(vettore[0])
                                            switch (vettore[0]) {
                                                case 'IdRaccolta':

                                                    let postData = "{id: '" + row.attr("id").replace(/\D/g, '') + "', dataRaccolta: '" + $("#Data").val() + "', quantita: '" + $("#Quantita").val() + "', idZona: '" + $("#IdZona").val() + "', idRaccoltaFinale: '" + $("#IdRaccoltaFinale").val() + "'}";
                                                    let getID11 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/update" + "/", "POST", postData);
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
                                                    break;

                                                case 'IdConcimazione':
                                                    let Data = "{id: '" + row.attr("id").replace(/\D/g, '') + "', dataConcimazione: '" + $("#Data").val() + "', quantita: '" + $("#Quantità").val() + "', idZona: '" + $("#IdZona").val() + "'}";
                                                    let getID12 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/update" + "/", "POST", Data);
                                                    getID12.fail(function (jqXHR) {
                                                        errore(jqXHR);
                                                        console.log(serverData);
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    getID12.done(function (serverData) {
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    break;

                                                case 'IdLancioInsetti':
                                                    let Dat1 = "{id: '" + row.attr("id").replace(/\D/g, '') + "', dataLancioInsetti: '" + $("#Data").val() + "', quantita: '" + $("#Quantita").val() + "', idZona: '" + $("#IdZona").val() + "'}";
                                                    let getID13 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/update" + "/", "POST", Dat1);
                                                    getID13.fail(function (jqXHR) {
                                                        errore(jqXHR);
                                                        console.log(serverData);
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    getID13.done(function (serverData) {
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    break;
                                                case 'IdTrattamento':
                                                    let Dat2 = "{id: '" + row.attr("id").replace(/\D/g, '') + "', dataTrattamento: '" + $("#Data").val() + "', quantità: '" + $("#Quantita").val() + "', idZona: '" + $("#IdZona").val() + "'}";
                                                    let getID16 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/update" + "/", "POST", Dat2);
                                                    getID16.fail(function (jqXHR) {
                                                        errore(jqXHR);
                                                        console.log(serverData);
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    getID16.done(function (serverData) {
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    break;
                                                case 'IdSfemminellatura':
                                                    let Dat3 = "{id: '" + row.attr("id").replace(/\D/g, '') + "', dataSfemminellatura: '" + $("#Data").val() + "', idZona: '" + $("#IdZona").val() + "'}";
                                                    let getID26 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/update" + "/", "POST", Dat3);
                                                    getID26.fail(function (jqXHR) {
                                                        errore(jqXHR);
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    getID26.done(function (serverData) {
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    break;
                                                case 'IdRilevamentoUmidita':

                                                    let Dat33 = "{id: '" + row.attr("id").replace(/\D/g, '') + "', valoreUmidita: '" + $("#ValoreUmidita").val() + "', idZona: '" + $("#IdZona").val() + "', dataOra: '" + $("#DataOra").val() + "'}";
                                                    let getID263 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/update" + "/", "POST", Dat33);
                                                    getID263.fail(function (jqXHR) {
                                                        errore(jqXHR);
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    getID263.done(function (serverData) {
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    break;
                                                case 'IdPiantato':
                                                    let Dat34 = "{id: '" + row.attr("id").replace(/\D/g, '') + "', numPiante: '" + $("#NumPiante").val() + "', idZona: '" + $("#IdZona").val() + "', dataPiantata: '" + $("#Data").val() + "', varieta: '" + $("#Varieta").val() + "'}";
                                                    let getID264 = sendRequestNoCallback("api/" + row.attr("id").replace(/[^a-zA-Z]/g, '') + "/update" + "/", "POST", Dat34);
                                                    getID264.fail(function (jqXHR) {
                                                        errore(jqXHR);
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    getID264.done(function (serverData) {
                                                        $("#lblMessage").text(serverData)
                                                    })
                                                    break;

                                                default:

                                            }

                                        })

                                    })

                                    $("#btnClose").on("click", function () {
                                        $("#tableModifica").hide()
                                        $("#bodyModifica").html(" ")
                                        location.reload();
                                    })


                                });
                                
                            }
                        }
                       
                    }
                    
                }
                table.append(tbody);
                tbody.attr("id", "tbody" + vect[i])
                table.hide()
                $("#divTabelle").append(table);
                table.attr("id", vect[i]);
                $('[id="raccolta"]').show()
                $("#raccoltaLine").show()
                $("#pRaccolta").on("click", function () {
                    $(".line").hide();
                    table.hide();
                    $('[id="raccolta"]').show()
                    $("#raccoltaLine").show()
                    $("#divGraficoRaccolta").show()
                })
                $("#pTrattamenti").on("click", function () {
                    $(".line").hide();
                    table.hide();
                    $('[id="trattamenti"]').show()
                    $("#trattamentiLine").show()
                    $("#divGraficoRaccolta").hide()
                })
                $("#pSfemminellatura").on("click", function () {
                    $(".line").hide();
                    table.hide();
                    $('[id="sfemminellatura"]').show()
                    $("#sfemminellaturaLine").show()
                    $("#divGraficoRaccolta").hide()
                })
                $("#pConcimazione").on("click", function () {
                    $(".line").hide();
                    table.hide();
                    $('[id="concimazione"]').show()
                    $("#concimazioneLine").show()
                    $("#divGraficoRaccolta").hide()
                })
                $("#pPiantato").on("click", function () {
                    $(".line").hide();
                    table.hide();
                    $('[id="piantato"]').show()
                    $("#piantatoLine").show()
                    $("#divGraficoRaccolta").hide()
                })
                $("#pLancioInsetti").on("click", function () {
                    $(".line").hide();
                    table.hide();
                    $('[id="lancioInsetti"]').show()
                    $("#lancioInsettiLine").show()
                    $("#divGraficoRaccolta").hide()
                })
            })
        })
    }
})
   
    

   

    
        


    
   






