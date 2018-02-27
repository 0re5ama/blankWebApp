function get() {
    console.log("GET function running");
    var req = new XMLHttpRequest();
    req.open("GET", "testHandler.ashx?action=showAll", true);
    req.send();
    req.onreadystatechange = function () {
        if (req.readyState == 4 && req.status == 200) {
            var resMsg = req.responseText;
            alert(resMsg);
        }
    }
}

function post() {
    console.log("POST function running");
    var req = new XMLHttpRequest();
    var data = { "id": "123", "name": "person123" };
    var jsonData = JSON.stringify(data);
    var dataToSend = { "action": "save", "data": jsonData };
    var jsonToSend = JSON.stringify(dataToSend);
    req.open("POST", "testHandler.ashx", true);
    req.send(jsonToSend);
    req.onreadystatechange = function () {
        if (req.readyState == 4 && req.status == 200) {
            var resMsg = req.responseText;
            alert(resMsg);
        }
    }
}