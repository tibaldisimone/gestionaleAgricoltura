function sendRequest(url, method, parameters, callback) {
	$.ajax({
		url: url,
		type: method,
		contentType: "application/x-www-form-urlencoded; charset=UTF-8",
		dataType: "text",
		data: parameters,
		timeout: 6000000,
		success: callback,
		error: function (jqXHR, test_status, str_error) {
			//console.log("No connection to " + link);
			//console.log("Test_status: " + test_status);
			alert("Error: " + str_error);
		}
	});
}

function send_request(url, method, parameters, callback) {
	//alert(parameters);
	$.ajax({
		url: url,
		type: method,
		contentType: 'application/json',
		dataType: 'text',
		data: parameters,
		timeout: 5000000,
		headers: { 'token': 'Bearer ' + localStorage.getItem("token") },
		success: callback,
		error: function (jqXHR, test_status, str_error) {
			alert("Error: " + str_error + test_status + jqXHR);
		}
	});
}

function sendRequestNoCallback(url, method, parameters) {
	return $.ajax({
		url: url,
		//contentType: "application/x-www-form-urlencoded; charset=UTF-8",
		contentType: "application/json",
		type: method,
		dataType: "json",
		data: parameters,
		timeout: 15000
	});
}