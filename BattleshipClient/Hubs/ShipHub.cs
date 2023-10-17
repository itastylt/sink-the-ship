using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

public class ShipHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        string messageType = message.Split(';')[0];
        string messageArgs = message.Split(';')[1];

        switch (messageType)
        {
            case "ready":
                ShipsBoard userBoard = new ShipsBoard();

                List<PlacedShip> ships = JsonSerializer.Deserialize<List<PlacedShip>>(messageArgs);

                foreach (PlacedShip ship in ships)
                {
                    userBoard.PlaceShip(ship);
                }


                Player player = new Player(user);
                player.SetShipsBoard(userBoard);

                List<Player> Players = ShipPlayers.AddPlayer(player);

                foreach (var online in Players)
                {
                    Console.WriteLine(online.Name);
                }

                if (Players.Count % 2 == 0)
                {
                    foreach (var online in Players)
                    {
                        await Clients.All.SendAsync("StartGame",online.Name, online.Name + ";" + online.GetShipsBoard().ToString());
                    }

                }
                break;

            default:
                break;
        }
    }
}