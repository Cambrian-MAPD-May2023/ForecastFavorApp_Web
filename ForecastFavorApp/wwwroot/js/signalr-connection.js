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
    // Code to execute when a notification is received
    alert(message); // Display the notification message using an alert (for demonstration purposes)
});
