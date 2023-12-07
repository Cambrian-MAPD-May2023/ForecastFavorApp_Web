using Microsoft.AspNetCore.SignalR;

public class NotificationHub : Hub
{
    // Define methods that clients can call
    public async Task SendNotification(string message)
    {
        await Clients.All.SendAsync("ReceiveNotification", message);
    }
}
