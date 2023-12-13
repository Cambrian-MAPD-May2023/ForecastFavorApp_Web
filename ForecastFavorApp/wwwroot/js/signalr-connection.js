// Create a new connection using the SignalR HubConnectionBuilder
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/notificationHub") // Set the URL for the SignalR hub
    .configureLogging(signalR.LogLevel.Information) // Configure logging level
    .build(); // Build the connection instance

// Start the connection and add error handling
connection.start().catch(function (err) {
    return console.error(err.toString()); // Log any errors that occur during connection start
});

// Set up an event listener for receiving notifications from the server
connection.on("ReceiveNotification", function (message) {
    // Check if browser notifications are supported
    if (!("Notification" in window)) {
        alert("This browser does not support desktop notification");
    }
    // Check whether notification permissions have already been granted
    else if (Notification.permission === "granted") {
        // If it's okay, create a notification
        new Notification("ForecastFavor App", {
            body: message,
            icon: "/images/forecastfavor_icon.png"  
        });
    }
    // Otherwise, ask the user for permission
    else if (Notification.permission !== "denied") {
        Notification.requestPermission().then(function (permission) {
            // If the user accepts, let's create a notification
            if (permission === "granted") {
                new Notification("ForecastFavor App", {
                    body: message,
                    icon: "/images/forecastfavor_icon.png" 
                });
            }
        });
    }
    
});
