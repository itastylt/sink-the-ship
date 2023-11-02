using BattleshipClient.GameLogic.Strategy.Decorator;
using BattleshipClient.GameLogic.Strategy;
using Microsoft.AspNetCore.SignalR;

public class ReadyFacade
{
    private ShipsBoard Board;
    private ShipHub Hub;
    private Player Player;

    public ReadyFacade (ShipHub hub)
    {
        this.Board = new ShipsBoard();
        this.Hub = hub;
    }

    public void FormBoard(List<PlacedShip> shipList)
    {
        foreach (PlacedShip ship in shipList) //Adding different cannon strategies to different weapons
        {
            switch (ship.Type)
            {
                case "Boat":
                    ship.Cannon = new SingleShot(); //Applying Strategy
                    ship.Cannon = new EnhancedSingleShot(ship.Cannon); //Applying Decorator
                    this.Board.PlaceShip(ship);
                    break;
                case "Lavantier":
                    ship.Cannon = new HorizontalShot();
                    this.Board.PlaceShip(ship);
                    break;
                case "Submarine":
                    ship.Cannon = new VerticalShot();
                    this.Board.PlaceShip(ship);
                    break;
                case "Destroyer":
                    ship.Cannon = new DiagonalShot();
                    this.Board.PlaceShip(ship);
                    break;
                default:
                    throw new Exception("Invalid Ship type!");
            }
        }
    }

    public List<Player> CreatePlayer(string name)
    {
        this.Player = new Player(name);
        this.Player.SetShipsBoard(this.Board);

        List<Player> Players = ShipPlayers.AddPlayer(this.Player);
        return Players;
    }

    public async void StartPlayers(List<Player> players)
    {
        if(players.Count % 2 == 0)
        {
            Random random = new Random();
            int random_number = random.Next(players.Count());
            Player luckyPlayer = players.ElementAt(random_number);
            luckyPlayer.SetState(true);
            ShipPlayers.UpdatePlayer(luckyPlayer.Name, luckyPlayer);
            foreach (var online in players)
            {
                await this.Hub.Clients.All.SendAsync("StartGame", luckyPlayer.Name, online.Name + ";" + online.GetShipsBoard().ToString() + ";" + luckyPlayer.Name);
            }
        }
    }

    public void UnreadyPlayer(string player)
    {
        ShipPlayers.RemovePlayer(player);
    }
    public void RestorePlayer()
    {
        throw new Exception("todo");
    }
}

