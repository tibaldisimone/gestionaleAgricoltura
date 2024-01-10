$(() => {
    $("#titleElencoProduzione").hide();
    let logIn = sendRequestNoCallback("api/produzioneFinale/getAllProduzioneFinale/", "GET", {});
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
        $("#tableElencoProduzioni").html(" ");
        let tr = $("<tr>");
        $("#tableElencoProduzioni").append(tr);
        $("#titleElencoProduzione").show();
        $("#elencoProduzioni").children("div").eq(0).append($("#tableElencoProduzioni"));
        for (let item of intest) {
            $("<th>").html(item).appendTo(tr);
        }

        for (let item of serverData) {
            let trDati = $("<tr>");
            $("#tableElencoProduzioni").append(trDati);

            for (let voice of intest) {

                $("<td>").appendTo(trDati).html(item[voice]).attr("id", item.IdProduzioneFinale);
                trDati.attr("id", i)

               
          
                trDati.on("click", function () {
                    $("#modelElencoProduzioni").show();
                    dat[i] = item[voice];
                    console.log(dat[i])
                    i++;
                    $("#txtIdProduzioneFinale").val(dat[0]).css({ "margin": "5px" });
                    $("#txtKgFinaliVerdi").val(dat[1]).css({ "margin": "5px" });
                    $("#txtColliFinaliVerdi").val(dat[2]).css({ "margin": "5px" });
                    $("#txtKgFinaliRosse").val(dat[3]).css({ "margin": "5px" });
                    $("#txtColliFinaliRosse").val(dat[4]).css({ "margin": "5px" });
                    $("#txtKgSeconda").val(dat[5]).css({ "margin": "5px" });
                    $("#txtColliFinaliSeconda").val(dat[6]).css({ "margin": "5px" });
                    $("#txtData").val(dat[7]).css({ "margin": "5px" });
                    $("#txtIdRaccoltaFinale").val(dat[8]).css({ "margin": "5px" });
                })
            }
        


        }


    })
})
    
    