$(() => {
    let vect = new Array("lancioInsetti", "trattamenti", "sfemminellatura", "concimazione", "piantato", "raccolta");
    let p = 0;
    let tipo = "";
    var parametro = new URLSearchParams(window.location.search).get("parametro");
    for (let i = 0; i<vect.length; i++) {
        let postData = "{idZona: '" + parametro + "'}";
        let dati = sendRequestNoCallback("api/" + vect[i] + "/getAll" + vect[i] + "Zone", "POST", postData);
        dati.fail(function (jqXHR) {
            errore(jqXHR);
            console.log(serverData);
        })
        dati.done(function (serverData) {
            console.log(serverData)
            let intest = Object.keys(serverData[0]);
            console.log(Object.values(serverData[1]))
            var table = $('<table>');
            var thead = $('<thead>');
            var tbody = $('<tbody>');
            var dynamicText = vect[i] + '<i class="fa fa-angle-right" style="font-size:45px" id="iconMaggiore"></i>';
            var dynamicH1 = $("<h1></h1>");
            dynamicH1.html(dynamicText)
            thead.append(dynamicH1)
            dynamicH1.css({

                "padding-left": "10px",

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
            let cosa="";
            serverData.forEach(function (item) {
               
                var row = $('<tr>');
                if (item.IdSfemminellatura != undefined) {
                    
                    if (cosa != item.IdSfemminellatura) {
                        
                        cosa = item.IdSfemminellatura;
                        console.log(tipo)
                    }
                }
               
                Object.values(item).forEach(function (value) {
                    if (id == 0) {
                        id = value;
                    }
                    row.append($('<td>').text(value));
                    
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
                        let vettore= [];
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
                                        $("#lblMessage").text(serverData)                                    })
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
                                            "color":"white"
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
               
                
                table.append(tbody);
                tbody.attr("id", "tbody" + vect[i])
                tbody.hide()
               
                $('.table__body' + vect[i] + '').append(table);
                table.attr("id", vect[i]);


                $('thead tr').hide()
                /*
                const id = vect[i];
                nodeListArray.push({ id: id, nodeList: table_rows });
               
                
                nodeListArrayHeight.push({ id: id, nodeList: table_headings });
                console.log(table_headings)
                */
                /*
                $("#thead" + vect[i]).on("click", function () {
                    var table_rows = document.querySelectorAll("#tbody" + $(this).attr("id").replace("thead", "") + " tr")
                    var table_headings = document.querySelectorAll("#thead" + $(this).attr("id").replace("thead", "") + " th");
                    console.log($(this).attr("id"))
                    console.log(table_rows)
                    table_headings.forEach((head, i) => {
                        console.log(table_headings)
                        let sort_asc = true;
    
                        head.onclick = () => {
                            table_headings.forEach(head => head.classList.remove('active'));
                            head.classList.add('active');
                            document.querySelectorAll('td').forEach(td => td.classList.remove('active'));
                            table_rows.forEach(row => {
                                row.querySelectorAll('td')[i].classList.add('active');
                            })
                            head.classList.toggle('asc', sort_asc);
                            sort_asc = head.classList.contains('asc') ? false : true;
                            sortTable(i, sort_asc);
                            console.log($(this).attr("id"))
                        }
                    })
                    function sortTable(column, sort_asc) {
    
                        [...table_rows].sort((a, b) => {
                            let first_row = a.querySelectorAll('td')[column].textContent.toLowerCase(),
                                second_row = b.querySelectorAll('td')[column].textContent.toLowerCase();
    
                            return sort_asc ? (first_row < second_row ? 1 : -1) : (first_row < second_row ? -1 : 1);
                        })
                        console.log($(this).attr("id"))
                            .map(sorted_row => document.querySelector("#tbody" + $(this).attr("id").replace("thead", "")).appendChild(sorted_row));
                        
                    }
                })
    
                */

                $('h1').on("click", function () {
                
                   
                    if (tipo != $(this).attr('id')) {
                        
                        tipo = $(this).attr('id');
                        if ($("#tbody" + $(this).attr('id')).css("display") == "none") {

                            //dynamicH1.html(vect[i])
                            console.log($(this).attr('id'))
                            $("#tbody" + $(this).attr('id')).show()
                            //headerRow.show()
                            $("#thead" + $(this).attr('id') + " tr").show()


                        }
                        else {
                            console.log("ciuso")
                            $("#thead" + $(this).attr('id') + " tr").hide()
                            $("#tbody" + $(this).attr('id')).hide()
                            dynamicH1.html(dynamicText);
                        }
                        
                    }
                     // $("#tbody" + dynamicH1.attr("id")).fadeToggle(500);
                });
            
            })
    
          

        })

        
    }
   
    

})






