using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
public class GameHub : Hub
{
    public async Task MakeMove(string player, int x, int y)
    {
        // Notify all clients about the move
        await Clients.All.SendAsync("ReceiveMove", player, x, y);
    }
}