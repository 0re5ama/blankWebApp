function save() {
    console.log('asdf');
    var req = new XMLHttpRequest();
    var student = {
        name: document.getElementById("name").value,
        gender: document.getElementById("gender").value,
        dob: document.getElementById("dob").value,
        address: document.getElementById("address").value,
        percentage: document.getElementById("perc").value
    };
    var stringified_student = JSON.stringify(student);
    var reqdata = {
        action: "saveStudent",
        data: stringified_student
    }
    var finaldata = JSON.stringify(reqdata);
    req.open("POST", "dbhandler.ashx", true);
    req.send(finaldata);
    req.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var res = JSON.parse(req.responseText);
            alert(res);
        }
    };
}

function cleardata() {
    console.log('asdf');
    document.getElementById("name").value = "";
    document.getElementById("gender").value = "";
    document.getElementById("dob").value = "";
    document.getElementById("address").value = "";
    document.getElementById("perc").value = "";
}

function loadrec() {
    console.log("loading data");
    var req = new XMLHttpRequest();
    var data = { "action": "showAllStudent", "data": null };
    dataToSend = JSON.stringify(data);
    req.open("POST", "dbhandler.ashx", true);
    req.send(dataToSend);
    req.onreadystatechange = function () {
        if (req.readyState == 4 && req.status == 200) {
            var jsonResMsg = req.responseText;
            var resMsg = JSON.parse(jsonResMsg);
            console.log(resMsg.length);
            var table = document.getElementById("table");
            var tableHeader = table.createTHead();
            var headerRow = tableHeader.insertRow();
            for (key in resMsg[0]) {
                var headCell = headerRow.insertCell();
                var head = key;
                var formated_head = head.charAt(0).toUpperCase() + head.slice(1);
                headCell.innerHTML = formated_head;
            }
            var tableBody = table.createTBody();
            resMsg.forEach(function (x) {
                var bodyRow = tableBody.insertRow();
                for (key in resMsg[0]) {
                    var bodyCell = bodyRow.insertCell();
                    bodyCell.innerHTML = x[key];
                }
            });
        };
    }
}

document.addEventListener("DOMContentLoaded", loadrec());