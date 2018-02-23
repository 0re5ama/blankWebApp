function save() {
    
    console.log('asdf');
    var req = new XMLHttpRequest();
    var student = {
        id: document.getElementById("id").value,
        name: document.getElementById("name").value,
        gender: document.getElementById("gender").value,
        dob: document.getElementById("dob").value,
        address: document.getElementById("address").value,
        percentage: document.getElementById("perc").value
    };
    var data = JSON.stringify(student);
    req.open("POST", "dbhandler.ashx", true);
    req.send(data);
    req.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var res = JSON.parse(req.responseText);
            alert(res);
        }
    };
}