function save() {
    var xhr = new XMLHttpRequest();
    var sendData = {
        name: document.getElementById('fullname').value,
        age: document.getElementById('age').value
    };
    xhr.open('GET', 'Handler.ashx?name='+sendData.name+'&age='+sendData.age, true);
    xhr.send(JSON.stringify(sendData));
    xhr.onreadystatechange = function () {
        if (xhr.readyState == XMLHttpRequest.DONE && xhr.status == 200) {
            var data = JSON.parse(xhr.responseText);
            data = data[0];
            console.log(data);
            document.getElementById('result').innerHTML = data.id + date.name + date.age;
        } else {
            document.getElementById('result').innerHTML = 'not found';
        }
    };
}
function show() {
    var req = new XMLHttpRequest();
    req.open("GET", "Handler2.ashx", true);
    req.onreadystatechange = function () {
        if (req.readyState == XMLHttpRequest.DONE && req.status == 200) {
            var detail = JSON.parse(req.responseText);
            detail = detail[0];
            console.log(detail);
            document.getElementById("showdb").innerHTML =
        }
    }
}