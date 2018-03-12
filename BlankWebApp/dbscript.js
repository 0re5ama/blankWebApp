function validateForm() {
    var temp1 = document.getElementById("name").value;
    if (temp1 == "") {
        document.getElementById("nameerr").innerHTML = "*Name is required";
        document.getElementById("name").focus();
        return false;
    }
    else if (checkname(temp1) == 0) {
        document.getElementById("nameerr").innerHTML = "*Enter valid name";
        document.getElementById("name").focus();
        return false;
    }
    else {
        document.getElementById("nameerr").innerHTML = "";
    }
    male = document.getElementById("male");
    female = document.getElementById("female");
    if (male.checked == false && female.checked == false) {
        document.getElementById("generr").innerHTML = "*Please select your gender";
        document.getElementById("male").focus();
        return false;
    }
    else {
        document.getElementById("generr").innerHTML = "";
    }
    var temp2 = document.getElementById("dob").value;
    if (temp2 == "") {
        document.getElementById("doberr").innerHTML = "*Date of Birth is required";
        document.getElementById("dob").focus();
        return false;
    }
    else {
        document.getElementById("doberr").innerHTML = "";
    }
    var temp3 = document.getElementById("address").value;
    if (temp3 == "") {
        document.getElementById("adderr").innerHTML = "*Address is required";
        document.getElementById("address").focus();
        return false;
    }
    else {
        document.getElementById("adderr").innerHTML = "";
    }
    if (validateMarks() == false) {
        return false;
    }
}

function validateMarks() {
    marksView();
    var temp1 = document.getElementById("math").value;
    if (temp1 == "") {
        document.getElementById("matherr").innerHTML = "*Enter Math Score";
        document.getElementById("math").focus();
        return false;
    }
    else if (temp1 < 0 || temp1 > 100) {
        document.getElementById("matherr").innerHTML = "*Enter value between 1 and 100";
        document.getElementById("math").focus();
        return false;
    }
    else {
        document.getElementById("matherr").innerHTML = "";
    }
    var temp2 = document.getElementById("science").value;
    if (temp2 == "") {
        document.getElementById("scnerr").innerHTML = "*Enter Science Score";
        document.getElementById("science").focus();
        return false;
    }
    else {
        document.getElementById("scnerr").innerHTML = "";
    }
    var temp3 = document.getElementById("english").value;
    if (temp3 == "") {
        document.getElementById("engerr").innerHTML = "*Enter English Score";
        document.getElementById("english").focus();
        return false;
    }
    else {
        document.getElementById("engerr").innerHTML = "";
    }
    var temp4 = document.getElementById("social").value;
    if (temp4 == "") {
        document.getElementById("socerr").innerHTML = "*Enter Social Score";
        document.getElementById("social").focus();
        return false;
    }
    else {
        document.getElementById("socerr").innerHTML = "";
    }
}

function checkname(name) {
    var regex = /^[a-zA-Z ]*$/;
    if (regex.test(name)) {
        return 1;
    }
    else {
        return 0;
    }
}

function calcP() {
    if (validateMarks() == false) {
        document.getElementById("percentage").innerHTML = "0";
        return;
    }
    var math = parseInt(document.getElementById("math").value);
    var science = parseInt(document.getElementById("science").value);
    var english = parseInt(document.getElementById("english").value);
    var social = parseInt(document.getElementById("social").value);
    var total = math + science + english + social;
    var perc = total * 100 / 400;
    document.getElementById("percentage").value = perc;
}

function save() {
    if (validateForm() == false) {
        return false;
    }
    console.log('Save function running..');
    var req = new XMLHttpRequest();
    var gnder = "";
    var male = document.getElementById("male");
    var female = document.getElementById("female");
    if (male.checked == true) {
        gnder = male.value;
    }
    else if (female.checked == true) {
        gnder = female.value;
    }
    var student = {
        name: document.getElementById("name").value,
        gender: gnder,
        dob: document.getElementById("dob").value,
        address: document.getElementById("address").value,
        percentage: document.getElementById("percentage").value
    };
    var stringified_student = JSON.stringify(student);
    var reqdata = {
        action: "saveStudent",
        data: stringified_student,
        mark: null
    }
    var finaldata = JSON.stringify(reqdata);
    req.open("POST", "dbhandler.ashx", true);
    req.send(finaldata);
    req.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            console.log(req.responseText);
            var res = req.responseText;
            console.log(res);
            var id = parseInt(res);
            var mark = {
                qid: id,
                math: document.getElementById("math").value,
                science: document.getElementById("science").value,
                english: document.getElementById("english").value,
                social: document.getElementById("social").value
            }
            var stringifiedMark = JSON.stringify(mark);
            var markData = {
                action: "saveMark",
                data: stringifiedMark,
                mark: null
            }
            var finalMark = JSON.stringify(markData);
            var req2 = new XMLHttpRequest();
            req2.open("POST", "dbhandler.ashx", true);
            req2.send(finalMark);
            req2.onreadystatechange = function () {
                if (req2.readyState == 4 && req2.status == 200) {
                    var res2 = req2.responseText;
                    if (res2 == "success") {
                        alert("Successfully saved with ID: " + id);
                    }
                }
            }
        }
    };
    updateView();
}

function cleardata() {
    console.log('Clearing');
    document.getElementById("id").value = "";
    document.getElementById("name").value = "";
    document.getElementById("male").checked = false;
    document.getElementById("female").checked = false;
    document.getElementById("dob").value = "";
    document.getElementById("address").value = "";
    document.getElementById("percentage").value = "";
    document.getElementById("qid").value = "";
    document.getElementById("math").value = "";
    document.getElementById("science").value = "";
    document.getElementById("english").value = "";
    document.getElementById("social").value = "";
}

function loadrec() {
    console.log("loading data");
    var i = 0;
    var req = new XMLHttpRequest();
    req.open("GET", "dbhandler.ashx?action=showAllStudent&id=", true);
    req.send();
    req.onreadystatechange = function () {
        if (req.readyState == 4 && req.status == 200) {
            var jsonResMsg = req.responseText;
            var resMsg = JSON.parse(jsonResMsg);
            var showData = resMsg.map(function (obj) {
                var arrObj = {};
                arrObj["SN"] = ++i;
                arrObj["ID"] = obj.id;
                arrObj["Name"] = obj.name;
                return arrObj;
            })
            var table = document.getElementById("table");
            var tableHeader = table.createTHead();
            var headerRow = tableHeader.insertRow();
            for (key in showData[0]) {
                var headCell = headerRow.insertCell();
                headCell.innerHTML = key;
            }
            var tableBody = table.createTBody();
            tableBody.setAttribute("style", "cursor: pointer");
            showData.forEach(function (x) {
                var bodyRow = tableBody.insertRow();
                bodyRow.setAttribute("onclick", "rowclicked(this)");
                for (key in showData[0]) {
                    var bodyCell = bodyRow.insertCell();
                    bodyCell.innerHTML = x[key];
                }
                var delCell = bodyRow.insertCell();
                delCell.innerHTML = "<a href=# onclick='deleteRow(this)'><span class='fas fa-trash-alt'></span></a>";
            });
        };
    }
}

document.addEventListener("DOMContentLoaded", loadrec());

function rowclicked(row) {
    var id = row.firstElementChild.nextElementSibling.innerHTML;
    console.log(id);
    row.id = id;
    var req = new XMLHttpRequest();
    req.open("GET", "dbhandler.ashx?action=getOne&id=" + id, true);
    req.send();
    req.onreadystatechange = function () {
        if (req.readyState == 4 && req.status == 200) {
            var resData = req.responseText;
            console.log(resData);
            var detail = JSON.parse(resData);
            document.getElementById("id").value = id;
            document.getElementById("name").value = detail.name;
            var genderVal = detail.gender;
            if (genderVal == 'M' || genderVal == 'm') {
                document.getElementById("male").checked = true;
            }
            else if (genderVal == 'F' || genderVal == 'f') {
                document.getElementById("female").checked = true;
            }
            var dateOB = new Date(detail.dob);
            var month = dateOB.getMonth() + 1;
            if (month < 10) {
                month = "0" + month;
            }
            var day = dateOB.getDate();
            if (day < 10) {
                day = "0" + day;
            }
            var strDate = dateOB.getFullYear() + "-" + month + "-" + day;
            document.getElementById("dob").value = strDate;
            document.getElementById("address").value = detail.address;
            marksView();
            document.getElementById("qid").value = detail.qid;
            document.getElementById("math").value = detail.math;
            document.getElementById("science").value = detail.science;
            document.getElementById("english").value = detail.english;
            document.getElementById("social").value = detail.social;
            document.getElementById("percentage").value = detail.percentage;
        }
    }
}

function deleteRow(element) {
    var cell = element.parentElement;
    var row = cell.parentElement;
    var id = row.firstElementChild.nextElementSibling.innerHTML;
    var req = new XMLHttpRequest();
    if (confirm("Delete details with id " + id)) {
        req.open("GET", "dbhandler.ashx?action=deleteOne&id=" + id, true);
        req.send();
        req.onreadystatechange = function () {
            if (req.readyState == 4 && req.status == 200) {
                row.innerHTML = "";
                delete row;
                alert("Delete status: " + req.responseText);
            }
        }
    }
    updateView();
}

function update() {
    if (validateForm() == false) {
        return false;
    }
    console.log('Update function running..');
    var sn = document.getElementById("id").value;
    console.log(sn);
    var req = new XMLHttpRequest();
    var gnder = "";
    var male = document.getElementById("male");
    var female = document.getElementById("female");
    if (male.checked == true) {
        gnder = male.value;
    }
    else if (female.checked == true) {
        gnder = female.value;
    }
    var student = {
        id: sn,
        name: document.getElementById("name").value,
        gender: gnder,
        dob: document.getElementById("dob").value,
        address: document.getElementById("address").value,
        percentage: document.getElementById("percentage").value
    };
    var stringified_student = JSON.stringify(student);
    var mark = {
        qid: sn,
        math: document.getElementById("math").value,
        science: document.getElementById("science").value,
        english: document.getElementById("english").value,
        social: document.getElementById("social").value
    }
    var stringified_mark = JSON.stringify(mark);
    var reqdata = {
        action: "updateStudent",
        data: stringified_student,
        mark: stringified_mark
    }
    var finaldata = JSON.stringify(reqdata);
    req.open("POST", "dbhandler.ashx", true);
    req.send(finaldata);
    req.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var res = req.responseText;
            alert(res);
        }
    };
    updateView();
}

function updateView() {
    var tble = document.getElementById("table");
    tble.innerHTML = "";
    loadrec();
}

function marksView() {
    var marks = document.getElementById("marks");
    var link = document.getElementById("markLink");
    var collapse = document.getElementById("collapse");
    marks.style.display = "block";
    link.style.display = "none";
    collapse.style.display = "block";
}

function hide() {
    document.getElementById("marks").style.display = "none";
    document.getElementById("markLink").style.display = "block";
    document.getElementById("collapse").style.display = "none";
}