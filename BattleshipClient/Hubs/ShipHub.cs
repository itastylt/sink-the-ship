using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace BattleshipClient.Hubs
{
    public class ShipHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            string messageType = message.Split(';')[0];
            string messageArgs = message.Split(';')[1];

            switch (messageType)
            {
                case "ready":
                    ShipsBoard userBoard = new ShipsBoard(user);

                    List<PlacedShip> ships = JsonSerializer.Deserialize<List<PlacedShip>>(messageArgs);
                    foreach(PlacedShip ship in ships)
                    {
                        userBoard.PlaceShip(ship);
                    }
                    userBoard.PrintBoard();
                    await Clients.All.SendAsync("EnemyBoard", user, messageArgs);
                    break;
                default:
                    Console.WriteLine("testas2");
                    break;
            }
        }
    }
}
