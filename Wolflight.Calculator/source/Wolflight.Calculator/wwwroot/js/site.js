const uriBase = "/api/Calculator/";
const uriTotal = uriBase + "Total";
const uriAdd = uriBase + "Add";

let GetTotalValuePromise = function () {
    return new Promise(function (myResolve, myReject) {
        let total = fetch(uriTotal)
            .then(response => response.text())
            .catch(error => myReject('Unable to get total.', error));

        myResolve(total);
    })
};

let PutAddValuePromise = function (addValue) {
    return new Promise(function (myResolve, myReject) {
        fetch(uriAdd + '?value=' + addValue, { method: 'PUT' })
            .catch(error => myReject('Unable to add value.', error));

        myResolve();
    }
    )
};

function DisplayTotal(total) {
    const tBody = document.getElementById('totalDisplay');
    tBody.innerHTML = total;
}

function GetTotal() {
    UpdateDisplay();
}

function AddValue() {
    let value = document.getElementById('addValue').value;

    PutAddValuePromise(value)
        .then(function () {
            UpdateDisplay();
        });
}

function UpdateDisplay() {
    GetTotalValuePromise()
        .then(
            function (total) { DisplayTotal(total); },
            function (message, error) { console.error(message, error); }
        )
}