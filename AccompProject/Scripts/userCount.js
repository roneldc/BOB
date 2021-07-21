$(function () {

    //setScreen(false);



    // Declare a proxy to reference the hub.
    var chatHub = $.connection.chatHub;

    registerClientMethods(chatHub);

    //// Start Hub
    ////$.connection.hub.start().done(function () {

    //    registerEvents(chatHub)

    //});

});


function registerClientMethods(chatHub) {

    // Calls when user successfully logged in
    chatHub.client.onConnected = function (id, userName, allUsers, messages) {

        //setScreen(true);

        //$('#hdId').val(id);
        //$('#hdUserName').val(userName);
        //$('#spanUser').html(userName);

        // Add All Users
        for (i = 0; i < allUsers.length; i++) {

            AddUser(chatHub, allUsers[i].ConnectionId, allUsers[i].UserName);
        }

        // Add Existing Messages
        //for (i = 0; i < messages.length; i++) {

        //    AddMessage(messages[i].UserName, messages[i].Message);
        //}

        $('.badge').text(allUsers.length);

    }
    
   }

