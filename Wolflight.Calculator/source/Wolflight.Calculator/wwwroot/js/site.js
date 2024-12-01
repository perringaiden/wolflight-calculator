const uriBase = "/api/Calculator/";
const uriTotal = uriBase + "Total";
const uriAdd = uriBase + "Add";
const uriSubtract = uriBase + "Subtract";

// #region Promises

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
        fetch(GetPutUri(uriAdd, addValue))
            .catch(error => myReject('Unable to add value.', error));

        myResolve();
    }
    )
};

let PutSubtractValuePromise = function (subtractValue) {
    return new Promise(function (myResolve, myReject) {
        fetch(GetPutUri(uriSubtract, subtractValue))
            .catch(error => myReject('Unable to add value.', error));

        myResolve();
    }
    )
};

// #endregion

// #region Display Functions

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

function SubtractValue() {
    let value = document.getElementById('subrtractValue').value;

    PutSubtractValuePromise(value)
        .then(function () {
            UpdateDisplay();
        });
}

// #endregion

// #region Helpers

function UpdateDisplay() {
    GetTotalValuePromise()
        .then(
            function (total) { DisplayTotal(total); },
            function (message, error) { console.error(message, error); }
        )
}
function DisplayTotal(total) {
    const tBody = document.getElementById('totalDisplay');
    tBody.innerHTML = total;
}

function GetPutUri(baseUri, value) {
    return baseUri + '?value=' + value, { method: 'PUT' };
}

// #endregion