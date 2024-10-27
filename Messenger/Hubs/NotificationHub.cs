using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace Messenger.Hubs;

public class NotificationHub : Hub
{
    public async override Task OnConnectedAsync()
    {
        if (Context.User.Identity.IsAuthenticated)
        {
            // Get the user ID or name from the authenticated user context
            var userId = Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                // Use the userId to uniquely identify the user group
                await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            }
        }
        await base.OnConnectedAsync();
    }

    public async Task SendMessage(string userId,string message)
    {
        await Clients.User(userId).SendAsync("receive message",message);
    }

    public async Task SendMessagesToGroup(string group, string message)
    {
        await Clients.GroupExcept(group,Context.ConnectionId).SendAsync("receive",message);
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        if (Context.User.Identity.IsAuthenticated)
        {
            var userId = Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                // Remove the user from the group on disconnection
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
            }
        }

        await base.OnDisconnectedAsync(exception);
    }
}