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
                            ship.Cannon = new SingleShot();
                            break;
                        case "Destroyer":
                            ship.Cannon = new HorizontalShot();
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
                    Random random = new Random();
                    int random_number = random.Next(Players.Count());
                    Console.WriteLine(random_number);
                    Player luckyPlayer = Players.ElementAt(random_number);
                    luckyPlayer.SetState(true);
                    ShipPlayers.UpdatePlayer(luckyPlayer.Name, luckyPlayer);

                    foreach (var online in Players)
                    {
                        await Clients.All.SendAsync("StartGame",online.Name, online.Name + ";" + online.GetShipsBoard().ToString());
                    }

                }
                break;
            case "selectWeapon":
                int chosenWeaponNumber = int.Parse(messageArgs);
                //Console.WriteLine("Selected weapon ");
                //Console.WriteLine(chosenWeaponNumber);
                Player player1 = ShipPlayers.GetPlayer(user);
                player1.SetSelectedShip(chosenWeaponNumber);
                ShipPlayers.UpdatePlayer(user, player1);
                //Console.WriteLine("trying fire method:");
                //player1.GetSelectedShip().FireWeapon();
                break;
            case "fireWeapon":

                int x_cord = int.Parse(messageArgs);
                int y_cord = int.Parse(message.Split(';')[2]);

                Player current_player = ShipPlayers.GetPlayer(user);
                if(!current_player.GetState())
                {
                    Console.WriteLine("Illegal player turn");
                } else {
                    Player opponent_player = ShipPlayers.GetPlayerOpponent(user);
                    
                    //Console.WriteLine(String.Format(current_player.Name + " is firing against " + opponent_player));
                    Console.WriteLine(String.Format(x_cord + " " + y_cord));
                    current_player.GetSelectedShip().FireWeapon(opponent_player, x_cord, y_cord);
                    current_player.SetState(!current_player.GetState());
                    opponent_player.SetState(!opponent_player.GetState());
                    current_player.GetSelectedShip().FireWeapon(opponent_player, x_cord, y_cord);
                    ShipPlayers.UpdatePlayer(current_player.Name, current_player);
                    ShipPlayers.UpdatePlayer(opponent_player.Name, opponent_player);
                    await Clients.All.SendAsync("FireShot", current_player.Name, opponent_player.Name + ";" + opponent_player.GetShipsBoard().ToString());
                }

                break;

            default:
                break;
        }
    }
}