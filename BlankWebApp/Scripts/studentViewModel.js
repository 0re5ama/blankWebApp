var studentViewModel = function () {
    var self = this;
    self.koid = ko.observable();
    self.koname = ko.observable();
    self.kogender = ko.observable('M');
    self.kodob = ko.observable();
    self.koaddress = ko.observable();
    self.kopercentage = ko.observable();
    self.showForm = ko.observable(false);
    self.saveData = ko.observable(true);
    self.updateData = ko.observable(false);
    self.selectedSubject = ko.observable();
    self.selectedMark = ko.observable();
    self.markButton = ko.observable('Add');
    self.marks = ko.observableArray([]);
    self.subjects = ko.observableArray([]);
    self.allStudent = ko.observableArray([]);
    self.loadrec = function () {
        console.log('loading data');
        var req = new XMLHttpRequest();
        req.open('GET', 'dbhandler.ashx?action=showAllStudent&id=', true);
        req.send();
        req.onreadystatechange = function () {
            if (req.readyState == 4 && req.status == 200) {
                var jsonResponse = req.responseText;
                var stringResponse = JSON.parse(jsonResponse);
            }
            self.allStudent(stringResponse);
        };
    };

    self.addData = function () {
        console.log('form displayed');
        self.showForm(true);
        self.clear();
        self.saveData(true);
    };

    self.clear = function () {
        self.koid('');
        self.koname('');
        self.kogender('M');
        self.kodob('');
        self.koaddress('');
        self.kopercentage('');
        self.saveData(true);
        self.updateData(false);
    };

    self.getMark = function () {
        self.selectedMark(self.selectedSubject().mark);
    };

    self.rowClicked = function (id) {
        self.showForm(true);
        self.saveData(false);
        self.updateData(true);
        console.log(id);
        $.ajax({
            type: 'GET',
            datatype: 'JSON',
            url: 'dbhandler.ashx',
            data: { 'action': 'getOneStudent', 'id': id },
            success: function (res) {
                self.koid(res.id);
                self.koname(res.name);
                self.kogender(res.gender);
                var tempdate = new Date(res.dob);
                var htmldate = tempdate.getFullYear() + '-' + ('0' + (tempdate.getMonth() + 1)).slice(-2) + '-' + ('0' + (tempdate.getDate() + 1)).slice(-2);
                self.kodob(htmldate);
                self.koaddress(res.address);
                self.kopercentage(res.percentage);
                var keyList = Object.keys(res.marks);
                keyList.shift();
                var markObj = keyList.map(x => {
                    var tempObj = {};
                    tempObj.subject = x;
                    tempObj.mark = res.marks[x];
                    return tempObj;
                });
                self.marks(markObj);
            },
            error: function () {
                alert('Failed to load data.\nPlease try again later.');
            }
        });
    };

    self.addUpdate = function () {

    };

    self.delet = function (id) {
        if (confirm('Delete all details associated with id ' + id)) {
            $.ajax({
                type: 'GET',
                datatype: 'JSON',
                url: 'dbhandler.ashx',
                data: { 'action': 'deleteOne', 'id': id },
                success: function (res) {
                    alert(res);
                },
                error: function () {
                    alert('Error occured while deleting.\nPlease try again later.');
                }
            });
        }
    };

    self.calcP = function () {

    };

    self.save = function () {
        var saveDetails = {
            name: self.koname(),
            gender: self.kogender(),
            dob: self.kodob(),
            address: self.koaddress(),
            percentage: self.kopercentage()
        };
        $.ajax({
            type: 'POST',
            datatype: 'JSON',
            url: 'dbhandler.ashx',
            data: { 'action': 'saveStudent', 'data': JSON.stringify(saveDetails) },
            success: function (res) {
                alert(res);
            }
        });
    };

    self.update = function () {

    };

    self.loadrec();
};
$(document).ready(function () {
    var view = new studentViewModel();
    ko.applyBindings(view);
});