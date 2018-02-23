function validateForm() {
    var temp1 = document.getElementById("fullname").value;
    if (temp1 == "") {
        document.getElementById("error").innerHTML = "name is required";
        return false;
    }
    var temp2 = document.getElementById("age").value;
    if (temp2 == "") {
        document.getElementById("error").innerHTML = "age is required";
        return false;
    }
    var temp3 = document.getElementById("dob").value;
    if (temp3 == "") {
        document.getElementById("error").innerHTML = "please enter date of birth";
        return false;
    }
    document.getElementById("error").innerHTML = "";
    return true;
}
function saveData() {
    if (validateForm() == false) {
        return false;
    }
    var var1 = document.getElementById("fullname").value;
    var var2 = document.getElementById("age").value;
    var var3 = document.getElementById("dob").value;
    var person = {
        name: var1,
        age: var2,
        dob: var3
    };
    $.post("handler.ashx", JSON.stringify(person), function (result) {
        result = JSON.parse(result);
        document.getElementById("result").innerHTML = result;
    });

}