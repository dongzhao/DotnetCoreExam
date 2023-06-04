using Microsoft.AspNetCore.SignalR;

namespace Web.Hubs
{
    public class DomainEventHub : Hub
    {
        //public async Task SendMessage(string entityName, string message)
        //{
        //    await Clients.All.SendAsync("EntityUpdated", entityName, message);
        //}
    }
}
