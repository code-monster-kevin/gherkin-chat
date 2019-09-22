const apiserverurl = "https://localhost:44386";

const createMessageElement = (message) => {
    const currentchannel = document.getElementById("currentchannel");
    const currentuser = document.getElementById("currentuser");

    var listitemclass = "list-group-item list-group-item-info";
    if (message.user === currentuser.value) {
        listitemclass = "list-group-item list-group-item-success";
    }

    if (message.channel === currentchannel.value) {
        const messageListDiv = document.getElementById("messagesList");
        const li = document.createElement("li");
        li.className = listitemclass;
        li.textContent = message.user + " (" + message.timeStamp + "): " + message.value;
        messageListDiv.appendChild(li);
    }
};

const setupConnection = () => {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("/messagehub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("NewMessage", function (message) {
        createMessageElement(message)
    });
    
    connection.start()
        .catch(err => {
            console.error(err.toString());
        });
};

const getChannels = () => {
    const channelselect = document.getElementById("channelselect");

    var request = new XMLHttpRequest()

    request.open("GET", apiserverurl + "/api/channels", true);
    request.onload = function () {
        var data = JSON.parse(this.response)

        if (request.status >= 200 && request.status < 400) {
            data.forEach(item => {
                let option = document.createElement("option");
                option.text = item.name;
                option.value = item.name;
                channelselect.appendChild(option)
            })
        } else {
            console.log('error')
        }
    }
    request.send();
};

const getMessages = (channel) => {

    var request = new XMLHttpRequest()

    var url = apiserverurl + "/api/messages?channel=" + encodeURI(channel);

    request.open("GET", url, true);
    request.onload = function () {
        var data = JSON.parse(this.response)

        if (request.status >= 200 && request.status < 400) {
            data.forEach(item => {
                createMessageElement(item);
            })
        } else {
            console.log('error')
        }
    }
    request.send();
};

const sendMessage = (channel, user, message) => {
    const jsonData = `{'value':'${message}','channel':'${channel}','user':'${user}'}`;

    var request = new XMLHttpRequest();
    request.open("POST", apiserverurl + "/api/messages", true);
    request.setRequestHeader("Content-type", "application/json");
    request.send(jsonData);
};

$(function () {
    $("#channelselect").change(function () {
        const channelinput = document.getElementById("channel");
        channelinput.value = $(this).val();
    });

    $("#joinchannel").click(function () {
        const username = document.getElementById("username");
        const channel = document.getElementById("channel");
        const currentuser = document.getElementById("currentuser");
        const currentchannel = document.getElementById("currentchannel");

        if (username.value === "") {
            alert("Please enter a user name");
            return;
        };
        if (channel.value === "") {
            alert("Please select or enter a channel");
            return;
        };

        currentuser.value = username.value;
        currentchannel.value = channel.value;

        const messageListDiv = document.getElementById("messagesList");
        $(messageListDiv).empty();

        getMessages(channel.value);
    });

    $("#sendmessage").click(function () {
        const currentuser = document.getElementById("currentuser");
        const currentchannel = document.getElementById("currentchannel");
        const message = document.getElementById("message");

        if (currentuser.value === "") {
            alert("Please enter a user name");
            return;
        };
        if (channel.value === "") {
            alert("Please select or enter a channel");
            return;
        };
        if (message.value === "") {
            alert("Please enter a message to send");
            return;
        };

        sendMessage(currentchannel.value.replace(/'/g, "\\'"), currentuser.value.replace(/'/g, "\\'"), message.value.replace(/'/g,"\\'"));

        message.value = "";
    });
});

setupConnection();

$(document).ready(function () {
    getChannels();
});