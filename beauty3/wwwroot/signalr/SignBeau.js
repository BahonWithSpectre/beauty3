
var siteurl = 'https://localhost:44356';


var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user) {
  //  var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
  //  var encodedMsg = user + " says " + msg;
  //  var li = document.createElement("li");
  //  li.textContent = encodedMsg;
  //  document.getElementById("messagesList").appendChild(li);

    var pageuser = document.getElementById("username").innerHTML;
    if (user === pageuser) {


        const urlFetch = siteurl + '/Account/UserClose'
        fetch(urlFetch, {
            method: 'POST',
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                "Id": user,
            }),
        })
            .then(function (res) { return res.json(); })
            .then(function (data) {
                if (data === 'false') {
                    HopUser(user);
                }
            })
            .catch((error) => { console.log(error) });




        
    }

});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {

    var user = document.getElementById("phone").value;

    const urlFetch = siteurl + '/Account/UserOpen'
    fetch(urlFetch, {
        method: 'POST',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            "Id": user,
        }),
    })
        .then(function (res) { return res.json(); })
        .then(function (data) {
            if (data === 'true') {

                connection.invoke("SendMessage", user).catch(function (err) {
                    return console.error(err.toString());
                });

            }
        })
        .catch((error) => { console.log(error) });


    
    
    event.preventDefault();
});







connection.on("ReceiveUser", function (user) {

    var pageuser = document.getElementById("phone").value;
    if (user === pageuser) {
        alert("Аккаунт в онлайне!");

        document.getElementById("loginform").submit();
    }

});

function HopUser(e) {
    var user = e;
    connection.invoke("SendUser", user).catch(function (err) {
        return console.error(err.toString());
    });
}




function UserOpen(e) {
    
    var user = e;

    const urlFetch = siteurl + '/Account/UserOpen'
    fetch(urlFetch, {
        method: 'POST',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            "Id": user,
        }),
    })
        .then(function (res) { return res.json(); })
        .then(function (data) {
            if (data === 'true') {
                alert("True");
            }
        })
        .catch((error) => { console.log(error) });
}



function UserClose(e) {

    var user = e;

    const urlFetch = siteurl + '/Account/UserClose'
    fetch(urlFetch, {
        method: 'POST',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            "Id": user,
        }),
    })
        .then(function (res) { return res.json(); })
        .then(function (data) {

        })
        .catch((error) => { console.log(error) });
}