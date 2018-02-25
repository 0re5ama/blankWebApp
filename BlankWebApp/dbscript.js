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
    var reqdata = {
        action: "saveStudent",
        data: student
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

}

document.addEventListener(onload, loadrec());