$(() => {
    $("#modelSerra").hide();
    $("#modelZona").hide();
    $("#messaggioModifica").hide();
    $("#modelFormDatiLavoro").hide();
})
//funzione richiamata da questa riga nell'html:    <script type="text/javascript" src="http://www.bing.com/api/maps/mapcontrol?callback=loadMapScenario" async defer></script>
function loadMapScenario() {
    $("#btnCloseMod").on("click", function () {
        $("#modelAggiungiLavoro").hide()
        $("#caselle").html(" ")
        $("#insertButton").hide()
        location.reload();//ricarica la pagina quando aggiungo una modifica
    })
    $("#btnMenu").on("click", function () {
        $("#modelSerra").hide();
        $("#modelZona").hide();
        if ($("#myMap").is(":visible")) {
            $("#myMap").hide()
        } else {
            $("#myMap").show()
        }
    })
    $("#mappa").on("click", function () {
        $("#modelSerra").hide();
        $("#modelZona").hide();
    })
    $("#logOut").on("click", function () {
        window.location.href = "login.html"
    })
    //creazione mappa
    var map = new Microsoft.Maps.Map('#myMap',
        {
            credentials: 'Am0G-qgPdkuzxoY51ifQZR6pqUESorI8kMRnSVMOB04AVoIZfq1D6HgQT1xrOlYp',
            center: new Microsoft.Maps.Location(44.7310, 7.9092),
            mapTypeId: Microsoft.Maps.MapTypeId.aerial,
            zoom: 18,
            showMapTypeSelector: false,
            showZoomButtons: false,
        });
    var windowWidth = $(window).width();
    if (windowWidth < 768) {
        map.setView({
            center: new Microsoft.Maps.Location(44.7310, 7.9092),
            zoom: 20
        });
        map.setOptions({
            width: windowWidth - 40
        });
    } else { 
        map.setView({
            center: new Microsoft.Maps.Location(44.7310, 7.9092),
            zoom: 18
        });
        map.setOptions({
            width: windowWidth - 80
        });
    }
    //prende le coordinate delle zone per creare le zone sulla mappa
    let logIn = sendRequestNoCallback("api/coordinateZone/getAllCoordinate/", "GET", {});
    logIn.fail(function (jqXHR) {
        errore(jqXHR);
     
    })
    logIn.done(function (serverData) {
        for (let item of serverData) {
            //crezione dei rettangoli
            var rectCoords = [
                new Microsoft.Maps.Location(item.Lat1, item.Long1),
                new Microsoft.Maps.Location(item.Lat4, item.Long4),
                new Microsoft.Maps.Location(item.Lat5, item.Long5),
                new Microsoft.Maps.Location(item.Lat6, item.Long6),
                new Microsoft.Maps.Location(item.Lat3, item.Long3),
                new Microsoft.Maps.Location(item.Lat2, item.Long2),
            ];
            //grafica dei rettangoli
            var rect = new Microsoft.Maps.Polygon(rectCoords, {
                id: item.IdCoordinate,
                strokeColor: 'lime',
                strokeThickness: 2,
                
            });
            rect.metadata = { id: item.IdCoordinate };
            //gestire il click della zona, quando cliccata mi apre il modal per gestire la zona
            Microsoft.Maps.Events.addHandler(rect, 'click', function (r) {
                let postData = "{idZona: '" + r.target.metadata.id + "'}";
                //richiama la funzione dal controller
                let logIn = sendRequestNoCallback("api/zona/getZona/", "POST", postData);
                logIn.fail(function (jqXHR) {
                    errore(jqXHR);
                })
                logIn.done(function (serverData) {
                    //crea e formatta: titolo e la tabella contenente i dati della zona
                    let intest = Object.keys(serverData[0]);
                    let vect = Object.values(serverData[0])
                    $("#tableZona").html(" ");
                    $("#zona").children("div").eq(0).append($("#tableZona"));
                    $("#title").html("ZONA N°:" + vect[0]);
                    let z = 0;
                    for (let item of intest) {
                        let tr = $("<tr>");
                        $("<th>").html(item).appendTo(tr);
                        $("<td>").appendTo(tr).text(vect[z]);
                        z++
                        $("#tableZona").append(tr);
                    }
                    $("#modelBody").children().eq(1).attr('id', vect[0]);
                    $("#modelBody").children().eq(2).attr('id', vect[0]);
                    $("#modelSerra").hide();
                    $("#modelZona").show();
                  
                })
            });
            rect.setOptions({ zIndex: 1 });//permette di inserire il rettangolo sopra alla mappa altrimenti non si vedrebbe
            map.entities.push(rect);//carica il rettangolo sulla mappa
        }
        //fa lo stesso della zona ma qua è per le serre
        let coord = sendRequestNoCallback("api/coordinate/getAllCoordinate/", "GET", {});
        coord.fail(function (jqXHR) {
            errore(jqXHR);
            console.log(serverData);
        })
        coord.done(function (serverData) {
            for (let item of serverData) {
                var rectCoords = [
                    new Microsoft.Maps.Location(item.Lat1, item.Long1),
                    new Microsoft.Maps.Location(item.Lat3, item.Long3),
                    new Microsoft.Maps.Location(item.Lat4, item.Long4),
                    new Microsoft.Maps.Location(item.Lat2, item.Long2)
                ];
                var rect = new Microsoft.Maps.Polygon(rectCoords, {
                    id: item.IdCoordinate,
                    fillColor: 'lime',
                    strokeColor: 'black',
                    strokeThickness: 2,
                    pinPosition: new Microsoft.Maps.Point(0, -20)
                });
                rect.metadata = { id: item.IdCoordinate };
                //creazione modal serra dopo click, per mostrare informazioni
                Microsoft.Maps.Events.addHandler(rect, 'click', function (r) {
                    let postData = "{idSerra: '" + r.target.metadata.id + "'}";
                    let logIn = sendRequestNoCallback("api/serra/getSerra/", "POST", postData);
                    logIn.fail(function (jqXHR) {
                        errore(jqXHR);
                    })
                    logIn.done(function (serverData) {
                        let intest = Object.keys(serverData[0]);
                        let vect = Object.values(serverData[0]);
                        $("#tableSerra").html(" ");
                        $("#serra").children("div").eq(0).append($("#tableSerra"));
                        $("#myModalLabel").html("SERRA N°:" + vect[0]);
                        let z = 0;
                        for (let item of intest) {
                            let tr = $("<tr>");
                            $("<th>").html(item).appendTo(tr);
                            //bloccare la modifica da parte dell'utente
                            if (item == "IdSerra" || item == "KgTotaliRaccolti" || item == "IdZona") {
                                $("<td>").appendTo(tr).text(vect[z]);

                            }
                            else {
                                if (item == "AnnoNylon" || item == "AnnoGomme" || item == "NumPiante") {
                                    //controllo per abilitare l'inserimento di solo numeri all'interno di determinate  celle
                                    $("<td>").appendTo(tr).text(vect[z]).attr('contenteditable', 'true').on('input', function () {
                                        let inputValue = $(this).text();
                                        if (!isNaN(inputValue)) {
                                           
                                        } else {
                                            $(this).text('0');  
                                         }
                                    });
                                }
                                else {
                                    $("<td>").appendTo(tr).text(vect[z]).attr('contenteditable', 'true').on('blur', function () {
                                        let inputValue = $(this).text();  
                                        //questo per abilitare solo l'inserimento di false o di true 
                                       if (inputValue == "true" || inputValue=="false") {
                                            
                                        } else {
                                           $(this).text('false');  
                                      
                                        }
                                    });
                                }
                            }
                           z++
                            $("#tableSerra").append(tr);
                        }
                        $("#modelZona").hide();
                        $("#modelSerra").show();
                    })
                });
                rect.setOptions({ zIndex: 999 });
                map.entities.push(rect);
            }
        });
    })
     $("#btnClose").on("click", function () {
        $("#messaggioModifica").show();
     })
    $("btnNonConfermaInserimento").on("click", function () {
        $("#messaggioInserimentoSbagliato").hide();
    })
    $("btnConfermaModifiche").on("click", function () {
        location.reload();
    })
    //salvare le modifiche apportate alla serra 
    $("#btnSalvaModificheSerra").on("click", function () {
        let chiave = [];
        let valore = [];
        let i = 0;
        //prendere tutti i valori della tabella, li carico poi all'interno di un vettore
        $('#tableSerra tr').each(function () {
            chiave[i] = $(this).find('td:eq(0)').text();
            valore[i] = $(this).find('th:eq(0)').text();
            i++;

        });
        //aggiornamento dati della serra
        let postData = "{idSerra: '" + chiave[0] + "', numPiante: '" + chiave[1] + "', disinfettata: '" + chiave[2] + "', verduraPresentePrima: '" + chiave[3] + "', kgTotaliRaccolti: '" + chiave[4] + "', idZona: '" + chiave[5] + "', annoNylon: '" + chiave[6] + "', annoGomme: '" + chiave[7] + "'}";
        let getID11 = sendRequestNoCallback("api/serra/update", "POST", postData);
        getID11.fail(function (jqXHR) {
            errore(jqXHR);
            console.log(serverData);
            $("#lblMessage").val(serverData).css({
                "color": "white"
            })
        })
        getID11.done(function (serverData) {
            console.log(serverData)
        })
        $("#modelSerra").hide();
        $("#modelZona").hide();
        $("#messaggioModifica").hide();
    })
    $("#btnNonSalvaModificheSerra").on("click", function () {
        $("#modelSerra").hide();
        $("#modelZona").hide();
        $("#messaggioModifica").hide();
    })
    $("#btnCloseZona").on("click", function () {
        $("#modelZona").hide();
    })
    $("#btnCloseAggLavoro").on("click", function () {
        $("#modelFormDatiLavoro").hide();
        location.reload();
    })
    $("#modelBody").children().eq(1).on("click", function () {
        window.location.href = "visualizzazioneAttivita.html?parametro=" + $("#modelBody").children().eq(1).attr('id'); +""
       //ogni paragrafo ha un id($("#modelBody").children().eq(1).attr('id')) quando premo il bottone, lui mi prende l'id della zona, e lo passa 
        //all'url per richiamare la pagina visualizzazioneAttivita
    })
    //stessa cosa di prima ma qua lo facciamo per aggiungere un nuovo lavoro fatto su quella zona
    $("#modelBody").children().eq(2).on("click", function () {
        $("#modelZona").hide();
        $("#modelAggiungiLavoro").show();
      
       
            $("#caselle").html(" ");
            //richiamo la funzione, ma il controller ha il nome che inizia con la lettera minuscola, invece la funzione presente nel controller inizia con la lettera
            //maiuscola
        $('.btnAggLavoro').each(function () {
            $(this).on('click', function () {
                let dati
                if ($(this).text() != "LANCIO INSETTI ") {
                     dati = sendRequestNoCallback("api/" + $(this).text().toLowerCase().trim() + "/getAll" + $(this).attr('id') + "/", "GET", {});
                }
                else {
                   
                    let parola = $(this).text().trim().replace(/\s+/g, '');
                    console.log(parola)
                    let text = parola.substring(0, 6).toLowerCase() + parola[6] + parola.substring(7).toLowerCase();
                    console.log(text)
                    
                    dati = sendRequestNoCallback("api/" +text+ "/getAll" + $(this).attr('id') + "/", "GET", {});
                }
              
                $("#modelFormDatiLavoro").show()
                let selezionato = $(this).text().toLowerCase().trim()
                dati.fail(function (jqXHR) {
                    errore(jqXHR);
                    console.log(serverData);
                })
                //qua inizio a comporre il form modal con gli elementi per l'inserimento
                dati.done(function (serverData) {
                    let intest = Object.keys(serverData[0]);
                    let k = 0;
                    for (let item of intest) {
                        var label = $('<label>').text(item + ": ").css({
                            "display": "block",
                            "margin-bottom": "5px",

                        });

                        if (item.toString() == "Data") {
                            var datePickerId = "datepicker" + ($("input[type='text'][id^='datepicker']").length + 1);
                            $("<input>").attr("type", "text").attr("id", item).attr("name", datePickerId).appendTo("body");
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
                                position: { my: "right top", at: "right top", of: $("#" + datePickerId) }
                            }).val(new Date().toISOString().split('T')[0]);
                            var div = $('<div>').append(label, $("#" + item));
                            $('#caselle').append(div);
                        }
                        else {
                            if (item == "IdRaccoltaFinale") {

                                let getId = sendRequestNoCallback("api/" + "raccoltaFinale" + "/getRaccoltaFinaliNonConcluse", "POST");
                                getId.fail(function (jqXHR) {
                                    console.log(jqXHR.status)
                                })
                                getId.done(function (serverData) {

                                    var comboBox = $('<select></select>').css({
                                        "width": "100%",
                                        "box-sizing": "border-box",
                                        "padding": "10px",
                                        "border": "1px solid #ccc",
                                        "border-radius": "4px",
                                        "margin-bottom": "10px"
                                    });
                                    comboBox.attr('id', "raccoltaFinale");
                                    serverData.forEach(function (item) {

                                        comboBox.append($('<option></option>').attr('value', item.IdRaccoltaFinale).text(item.IdRaccoltaFinale + " " + item.Data.slice(0, -9)));
                                    })
                                    var div = $('<div>').append(label, comboBox);
                                    $('#caselle').append(div);

                                })
                            }
                            else {
                                if (item == "Nome") {

                                }
                                else {
                                    var input = $('<input>').attr({
                                        type: 'text',
                                        name: 'field' + item
                                    }).css({
                                        "width": "100%",
                                        "box-sizing": "border-box",
                                        "padding": "10px",
                                        "border": "1px solid #ccc",
                                        "border-radius": "4px",
                                        "margin-bottom": "10px"
                                    });
                                    input.attr("id", item)

                                    if (k == 0) {
                                    
                                        let getId = sendRequestNoCallback("api/" + selezionato.replace(/\s/g, '') + "/getLastId", "POST");
                                        getId.fail(function (jqXHR) {
                                            console.log(jqXHR.status)
                                        })
                                        getId.done(function (serverData) {
                                            $("#caselle :input:first").val(serverData + 1).prop('disabled', true);

                                        })
                                        var div = $('<div>').append(label, input);
                                        $('#caselle').append(div);
                                    }
                                    else {
                                        if (item == "IdZona") {

                                            input.val($("#modelBody").children().eq(2).attr("id"))
                                            input.attr('disabled', true);
                                            var div = $('<div>').append(label, input);
                                            $('#caselle').append(div);
                                        }
                                        else {

                                            if (item == "Varieta") {
                                                var comboBox = $('<select></select>').css({
                                                    "width": "100%",
                                                    "box-sizing": "border-box",
                                                    "padding": "10px",
                                                    "border": "1px solid #ccc",
                                                    "border-radius": "4px",
                                                    "margin-bottom": "10px"
                                                });
                                                comboBox.attr('id', item);
                                                comboBox.append('<option value="elemento1">Araldino</option>');
                                                comboBox.append('<option value="elemento2">Gigowak</option>');
                                                var div = $('<div>').append(label, comboBox);
                                                $('#caselle').append(div);

                                            }
                                            else {
                                                var div = $('<div>').append(label, input);
                                                $('#caselle').append(div);

                                            }
                                        }
                                    }
                                }


                            }
                        }
                        k++;
                    }

                    if (selezionato == "sfemminellatura" || selezionato == "raccolta") {
                        let getId = sendRequestNoCallback("api/" + "operai" + "/getAllOperai", "GET");
                        getId.fail(function (jqXHR) {
                            console.log(jqXHR.status)
                        })
                        getId.done(function (serverData) {
                            console.log(serverData)
                            let intest = Object.keys(serverData[0]);

                            var label = $('<label>').text(intest[2] + "operario" + ": ").css({
                                "display": "block",
                                "margin-bottom": "5px",

                            });
                            var comboBox = $('<select></select>', { multiple: true }).css({
                                "width": "100%",
                                "box-sizing": "border-box",
                                "padding": "10px",
                                "border": "1px solid #ccc",
                                "border-radius": "4px",
                                "margin-bottom": "10px"
                            });
                            comboBox.attr('id', "operaio");
                            serverData.forEach(function (item) {
                                console.log(item)
                                comboBox.append($('<option></option>').attr('value', item.IdOperaio).text(item.Nome));




                            })
                            var div = $('<div>').append(label, comboBox);
                            $('#caselle').append(div);


                        })

                    }
                    else {
                        if (selezionato == "concimazione") {


                            let getId = sendRequestNoCallback("api/" + "concimi" + "/getAllConcimi", "GET");
                            getId.fail(function (jqXHR) {
                                console.log(jqXHR.status)
                            })
                            getId.done(function (serverData) {
                                console.log(serverData)
                                let intest = Object.keys(serverData[0]);

                                var label = $('<label>').text(intest[1] + "concime" + ": ").css({
                                    "display": "block",
                                    "margin-bottom": "5px",

                                });

                                var comboBox = $('<select></select>', { multiple: true }).css({
                                    "width": "100%",
                                    "box-sizing": "border-box",
                                    "padding": "10px",
                                    "border": "1px solid #ccc",
                                    "border-radius": "4px",
                                    "margin-bottom": "10px"
                                });
                                comboBox.attr('id', 'concimeUtilizzato');
                                serverData.forEach(function (item) {

                                    comboBox.append($('<option></option>').attr('value', item.IdConcime).text(item.Nome));

                                    console.log(item.Nome)


                                })
                                var div = $('<div>').append(label, comboBox);
                                $('#caselle').append(div);
                            })
                        }
                        else {
                            if (selezionato == "trattamenti") {
                        let getId = sendRequestNoCallback("api/" + "fitofarmaci" + "/getAllFitofarmaci", "GET");
                        getId.fail(function (jqXHR) {
                            console.log(jqXHR.status)
                        })
                        getId.done(function (serverData) {
                            console.log(serverData)
                            let intest = Object.keys(serverData[0]);

                            var label = $('<label>').text(intest[1] + "fitofarmaco" + ": ").css({
                                "display": "block",
                                "margin-bottom": "5px",

                            });
                            var label = $("<label>").text("Multi-select").css({
                                "display": "block",
                                "margin-bottom": "5px",

                            });;
                            var comboBox = $('<select></select>', { multiple: true }).css({
                                "width": "100%",
                                "box-sizing": "border-box",
                                "padding": "10px",
                                "border": "1px solid #ccc",
                                "border-radius": "4px",
                                "margin-bottom": "10px"
                            });
                            comboBox.attr('id', 'trattamentoSelezionato');
                            serverData.forEach(function (item) {
                                comboBox.append($('<option></option>').attr('value', item.IdFitofarmaco).text(item.Nome));
                            })
                            var div = $('<div>').append(label, comboBox);
                            $('#caselle').appiend(div);
                            })
                            }

                        }

                    }
                    $("#insert").html(" ")
                    $("#insert").css({
                        "width": "100%",
                        "padding-right": "10px",
                        "padding-left": "10px",

                    })
                    var buttonUpdate = $('<button></button>');
                    buttonUpdate.attr('id', 'insertButton');
                    buttonUpdate.text('INSERISCI');
                    buttonUpdate.css({
                        "width": "100%",
                        "text-align": "center",
                        "padding": "10px",
                        "color": "black",
                        "font-weight":"bold",
                        "margin-bottom": "10px",
                        
                    });
                   
                    //padding - right: 90px
                    buttonUpdate.appendTo('#insert');
                    buttonUpdate.on("click", function () {


                        switch ($('input[type="text"]:first').attr("id")) {
                            case 'IdRaccolta':
                                if ($("#Data").val() != "" && $("#Quantita").val() != "" && $("#operaio option:selected").attr('value') != undefined) {
                                    let postData = "{idRaccolta: '" + $('input[type="text"]:first').val() + "', dataRaccolta: '" + $("#Data").val() + "', quantita: '" + $("#Quantita").val() + "', idZona: '" + $("#IdZona").val() + "', idRaccoltaFinale: '" + $("#raccoltaFinale").val().charAt(0) + "'}";
                                    let getID11 = sendRequestNoCallback("api/" + "raccolta" + "/insert" + "/", "POST", postData);
                                    getID11.fail(function (jqXHR) {
                                        errore(jqXHR);
                                        console.log(serverData);
                                        $("#lblMessage").val(serverData).css({
                                            "color": "white"
                                        })

                                        $("#messaggioInserimentoSbagliato").show();
                                        $("#btnNonConfermaInserimento").on("click", function () {
                                            $("#messaggioInserimentoSbagliato").hide();
                                        })

                                    })
                                    getID11.done(function (serverData) {
                                        $("#lblMessage").text(serverData)
                                        console.log(serverData)
                                        for (let i = 0; i < ($("#operaio").val().length); i++) {
                                            let postData1 = "{idRaccolta: '" + $('input[type="text"]:first').val() + "', idOperaio: '" + $("#operaio").val()[i] + "', dataRaccolta: '" + $("#Data").val() + "'}";
                                            let getID111 = sendRequestNoCallback("api/" + "esecuzioneRaccolta" + "/insert" + "/", "POST", postData1);
                                            getID111.fail(function (jqXHR) {
                                                errore(jqXHR);
                                                console.log(serverData);
                                                $("#lblMessage").val(serverData).css({
                                                    "color": "white"
                                                })
                                                $("#messaggioInserimentoSbagliato").show();

                                            })
                                            getID111.done(function (serverData) {
                                                $("#lblMessage").text(serverData)
                                                console.log(serverData)
                                                $("#messaggioInserimento").show();
                                                $("#btnConfermaModifiche").on("click", function () {
                                                    location.reload();
                                                })
                                            })
                                        }
                                    })
                                }
                                else {
                                    $("#messaggioInserimentoSbagliato").show();
                                    $("#btnNonConfermaInserimento").on("click", function () {
                                        $("#messaggioInserimentoSbagliato").hide();
                                    })
                                }



                                break;

                            case 'IdConcimazione':
                                var selectedTexts = $('#concimeUtilizzato option:selected').map(function () {
                                    return $(this).text();
                                }).get();
                                console.log(selectedTexts[0])
                                if ($("#Data").val() != "" && $("#concimeUtilizzato").val() != undefined) {

                                    let Data = "{idConcimazione: '" + $('input[type="text"]:first').val() + "', dataConcimazione: '" + $("#Data").val() + "', idZona: '" + $("#IdZona").val() + "'}";
                                    let getID12 = sendRequestNoCallback("api/" + "concimazione" + "/insert" + "/", "POST", Data);
                                    getID12.fail(function (jqXHR) {
                                        errore(jqXHR);
                                        console.log(serverData);
                                        $("#lblMessage").text(serverData)
                                        $("#messaggioInserimentoSbagliato").show();
                                        $("#btnNonConfermaInserimento").on("click", function () {
                                            $("#messaggioInserimentoSbagliato").hide();
                                        })
                                    })
                                    getID12.done(function (serverData) {
                                        $("#lblMessage").text(serverData)
                                        for (let i = 0; i < ($("#concimeUtilizzato").val().length); i++) {
                                            let postData1 = "{idConcimazione: '" + $('input[type="text"]:first').val() + "', idConcime: '" + $("#concimeUtilizzato").val()[i] + "', nome: '" + selectedTexts[i] + "'}";
                                            let getID111 = sendRequestNoCallback("api/" + "concimeUtilizzato" + "/insert" + "/", "POST", postData1);
                                            getID111.fail(function (jqXHR) {
                                                errore(jqXHR);
                                                console.log(serverData);
                                                $("#lblMessage").val(serverData).css({
                                                    "color": "white"
                                                })
                                                $("#messaggioInserimentoSbagliato").show();
                                                $("#btnNonConfermaInserimento").on("click", function () {
                                                    $("#messaggioInserimentoSbagliato").hide();
                                                })
                                            })
                                            getID111.done(function (serverData) {
                                                $("#lblMessage").text(serverData)
                                                console.log(serverData)
                                                $("#messaggioInserimento").show();
                                                $("#btnConfermaModifiche").on("click", function () {
                                                    location.reload();
                                                })
                                            })
                                        }
                                    })
                                }
                                else {
                                    $("#messaggioInserimentoSbagliato").show();
                                    $("#btnNonConfermaInserimento").on("click", function () {
                                        $("#messaggioInserimentoSbagliato").hide();
                                    })
                                }
                                break;

                            case 'IdLancioInsetti':
                                if ($("#Data").val() != "") {
                                    let Dat1 = "{id: '" + $('input[type="text"]:first').val() + "', dataLancioInsetti: '" + $("#Data").val() + "', idZona: '" + $("#IdZona").val() + "'}";
                                    let getID13 = sendRequestNoCallback("api/" + "lancioInsetti" + "/insert" + "/", "POST", Dat1);
                                    getID13.fail(function (jqXHR) {
                                        errore(jqXHR);
                                        console.log(serverData);
                                        $("#lblMessage").text(serverData)
                                        $("#messaggioInserimentoSbagliato").show();
                                        $("#btnNonConfermaInserimento").on("click", function () {
                                            $("#messaggioInserimentoSbagliato").hide();
                                        })
                                    })
                                    getID13.done(function (serverData) {
                                        $("#lblMessage").text(serverData)
                                        $("#messaggioInserimento").show();
                                        $("#btnConfermaModifiche").on("click", function () {
                                            location.reload();
                                        })
                                    })
                                }
                                else {
                                    $("#messaggioInserimentoSbagliato").show();
                                    $("#btnNonConfermaInserimento").on("click", function () {
                                        $("#messaggioInserimentoSbagliato").hide();
                                    })
                                }
                                break;
                            case 'IdTrattamento':
                                var selectedTexts = $('#trattamentoSelezionato option:selected').map(function () {
                                    return $(this).text();
                                }).get();
                                console.log(selectedTexts[0])
                                if ($("#Data").val() != "" && $("#trattamentoSelezionato").val() != undefined) {
                                    let Data2 = "{idTrattamento: '" + $('input[type="text"]:first').val() + "', dataTrattamento: '" + $("#Data").val() + "', idZona: '" + $("#IdZona").val() + "'}";
                                    let getID124 = sendRequestNoCallback("api/" + "trattamenti" + "/insert" + "/", "POST", Data2);
                                    getID124.fail(function (jqXHR) {
                                        errore(jqXHR);
                                        console.log(serverData);
                                        $("#lblMessage").text(serverData)
                                        $("#messaggioInserimentoSbagliato").show();
                                        $("#btnNonConfermaInserimento").on("click", function () {
                                            $("#messaggioInserimentoSbagliato").hide();
                                        })
                                    })
                                    getID124.done(function (serverData) {
                                        $("#lblMessage").text(serverData)
                                        for (let i = 0; i < ($("#trattamentoSelezionato").val().length); i++) {
                                            let postData12 = "{idTrattamento: '" + $('input[type="text"]:first').val() + "', idFitofarmaco: '" + $("#trattamentoSelezionato").val()[i] + "', nome: '" + selectedTexts[i] + "'}";
                                            let getID1114 = sendRequestNoCallback("api/" + "fitofarmacoUtilizzato" + "/insert" + "/", "POST", postData12);
                                            getID1114.fail(function (jqXHR) {
                                                errore(jqXHR);
                                                console.log(serverData);
                                                $("#lblMessage").val(serverData).css({
                                                    "color": "white"
                                                })
                                                $("#messaggioInserimentoSbagliato").show();
                                                $("#btnNonConfermaInserimento").on("click", function () {
                                                    $("#messaggioInserimentoSbagliato").hide();
                                                })
                                            })
                                            getID1114.done(function (serverData) {
                                                $("#lblMessage").text(serverData)
                                                console.log(serverData)
                                                $("#messaggioInserimento").show();
                                                $("#btnConfermaModifiche").on("click", function () {
                                                    location.reload();
                                                })
                                            })

                                        }
                                    })
                                }
                                else {
                                    $("#messaggioInserimentoSbagliato").show();
                                    $("#btnNonConfermaInserimento").on("click", function () {
                                        $("#messaggioInserimentoSbagliato").hide();
                                    })
                                }

                                break;
                            case 'IdSfemminellatura':
                                console.log($("#operaio option:selected").attr('value'))
                                if ($("#operaio option:selected").attr('value') != undefined && $("#Data").val() != "") {
                                    console.log("ciao")
                                    let postData4 = "{idSfemminellatura: '" + $('input[type="text"]:first').val() + "', dataSfemminellatura: '" + $("#Data").val() + "', idZona: '" + $("#IdZona").val() + "'}";
                                    let getID114 = sendRequestNoCallback("api/" + "sfemminellatura" + "/insert" + "/", "POST", postData4);
                                    getID114.fail(function (jqXHR) {
                                        console.log(jqXHR)
                                        errore(jqXHR);
                                        console.log(serverData);
                                        $("#lblMessage").val(serverData).css({
                                            "color": "white"
                                        })
                                        $("#messaggioInserimentoSbagliato").show();
                                        $("#btnNonConfermaInserimento").on("click", function () {
                                            $("#messaggioInserimentoSbagliato").hide();
                                        })

                                    })
                                    getID114.done(function (serverData) {

                                        $("#lblMessage").text(serverData)
                                        console.log(serverData)

                                        for (let i = 0; i < ($("#operaio").val().length); i++) {

                                            let postData14 = "{idSfemminellatura: '" + $('input[type="text"]:first').val() + "', idOperaio: '" + $("#operaio").val()[i] + "', dataSfemminellatura: '" + $("#Data").val() + "'}";
                                            let getID1114 = sendRequestNoCallback("api/" + "esecuzioneSfemminellatura" + "/insert" + "/", "POST", postData14);
                                            getID1114.fail(function (jqXHR) {
                                                errore(jqXHR);

                                                $("#lblMessage").val(serverData).css({
                                                    "color": "white"
                                                })
                                                $("#messaggioInserimentoSbagliato").show();
                                                $("#btnNonConfermaInserimento").on("click", function () {
                                                    $("#messaggioInserimentoSbagliato").hide();
                                                })
                                                console.log(serverData);
                                            })
                                            getID1114.done(function (serverData) {
                                                $("#lblMessage").text(serverData)

                                                $("#messaggioInserimento").show();
                                                $("#btnConfermaModifiche").on("click", function () {
                                                    location.reload();
                                                })
                                                console.log(serverData)
                                            })

                                        }


                                    })
                                }
                                else {
                                    $("#messaggioInserimentoSbagliato").show();
                                    $("#btnNonConfermaInserimento").on("click", function () {
                                        $("#messaggioInserimentoSbagliato").hide();
                                    })
                                }
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
                                if ($("#Data").val() != "" && $("#Varieta").val()!="" && $("#NumPiante").val()!="") {
                                    let Dat345 = "{idPiantato: '" + $('input[type="text"]:first').val() + "', numPiante: '" + $("#NumPiante").val() + "', idZona: '" + $("#IdZona").val() + "', dataPiantata: '" + $("#Data").val() + "', varieta: '" + $("#Varieta :selected").text() + "'}";
                                    let getID2654 = sendRequestNoCallback("api/" + "piantato" + "/insertPiantato" + "/", "POST", Dat345);
                                    getID2654.fail(function (jqXHR) {
                                        errore(jqXHR);
                                        $("#lblMessage").text(serverData)
                                        $("#messaggioInserimentoSbagliato").show();
                                        $("btnNonConfermaInserimento").on("click", function () {
                                            $("#messaggioInserimentoSbagliato").hide();
                                        })
                                    })
                                    getID2654.done(function (serverData) {
                                        $("#lblMessage").text(serverData)
                                        $("#messaggioInserimento").show();
                                        $("#btnConfermaModifiche").on("click", function () {
                                            location.reload();
                                        })
                                    })

                                }
                                else {
                                    $("#messaggioInserimentoSbagliato").show();
                                    $("#btnNonConfermaInserimento").on("click", function () {
                                        $("#messaggioInserimentoSbagliato").hide();
                                    })
                                }

                                break;

                            default:
                        }

                    })


                });

            });
        });
        
           
        
    })
}
function confermaModifiche() {
   
}
function confermaNonModifiche() {
  
    
}
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

