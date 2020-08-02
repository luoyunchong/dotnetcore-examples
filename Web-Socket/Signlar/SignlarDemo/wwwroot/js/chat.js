"use strict";

function initconnection() {

    var connection = new signalR.HubConnectionBuilder()
        .withUrl("/chatHub", {
            accessTokenFactory: () => {
                return localStorage.getItem("token")
            }
        })
        .build();

    //Disable send button until connection is established
    document.getElementById("sendButton").disabled = true;

    connection.on("ReceiveMessage", function (user, message) {
        console.log(user)
        console.log(message)
        var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
        var encodedMsg = user + " says " + msg;
        var li = document.createElement("li");
        li.textContent = encodedMsg;
        document.getElementById("messagesList").appendChild(li);
    });

    connection.on("ReceiveMessage2", function (date) {
        console.log(date)
    });

    connection.on("Notify", function (d) {
        console.log("Notify", d)
    });


    connection.start().then(function () {
        document.getElementById("sendButton").disabled = false;
    }).catch(function (err) {
        return console.error(err.toString());
    });
    return connection;
}

document.getElementById("sendButton").addEventListener("click", function (event) {
    var toUser = document.getElementById("toUserInput").value;
    var message = document.getElementById("messageInput").value;
    $.ajax({
        url: '/Home/SendMessage',
        data: {
            toUser: toUser,
            message: message
        },
        headers: {
            "Authorization": "Bearer " + localStorage.getItem("token")
        },
        contentType: "application/json",
        success: function (d) {
            console.log(d)
        }
    })
    //connection.invoke("SendMessage", user, message).catch(function (err) {
    //    return console.error(err.toString());
    //});
    event.preventDefault();
});

