using BattleshipClient.GameLogic.Strategy;
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

                foreach (PlacedShip ship in ships) //Adding different cannon strategies to different weapons
                {
                    switch (ship.Type)
                    {
                        case "Boat":
                            ship.Cannon = new SingleShot();
                            break;
                        case "Lavantier":
                            ship.Cannon = new HorizontalShot();
                            break;
                        case "Submarine":
                            ship.Cannon = new VerticalShot();
                            break;
                        case "Destroyer":
                            ship.Cannon = new DiagonalShot();
                            break;
                    }
                }

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

            case "selectWeapon":
                int chosenWeaponNumber = int.Parse(messageArgs);
                Player player1 = ShipPlayers.GetPlayer(user);
                player1.SetSelectedShip(chosenWeaponNumber);
                break;

            case "fireWeapon":
                int x_cord = int.Parse(messageArgs);
                int y_cord = int.Parse(message.Split(';')[2]);

                Player current_player = ShipPlayers.GetPlayer(user);
                Player opponent_player = ShipPlayers.GetPlayerOpponent(user);

                current_player.GetSelectedShip().FireWeapon(opponent_player, x_cord, y_cord);
                break;

            default:
                break;
        }
    }
}