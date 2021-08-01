var countryListElement = document.getElementById("stateId");
var resultDiv2 = document.getElementById("result2");

countryListElement.addEventListener("change", GetcountryCities);


function GetcountryCities(event) {
    $.get("AjaxPeople/GetcountryCities",
        {
            id: event.target.value
        },
        function (data, status) {
            console.log("Data: " + data + "\nStatus: " + status);
            resultDiv2.innerHTML = data;
        });

    var id = event.target.value;
    console.log("country ID ", id);
}