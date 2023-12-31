﻿using BattleshipClient.GameLogic.Strategy.Decorator;
using BattleshipClient.GameLogic.Strategy;
using Microsoft.AspNetCore.SignalR;
using BattleshipClient.GameLogic.Factory;
using System.Collections;
using BattleshipClient.GameLogic.Mediator;

public class ReadyFacade
{
    private ShipsBoard Board;
    private ShipHub? Hub;
    private Player? Player;

    public ReadyFacade (ShipHub hub)
    {
        this.Board = new ShipsBoard();
        this.Hub = hub;
    }
    public ReadyFacade()
    {
        this.Board = new ShipsBoard();
    }

    public Player GetPlayer ()
    {
        return this.Player;
    }

    public ShipsBoard GetBoard()
    {
        return this.Board;
    }
    public void FormBoard(IEnumerable<PlacedShip> shipList)
    {
        ITeamFactory iTeamFactory = new ITeamFactory();

        ShipFactory teamFactory = null;

        if (ShipPlayers.PlayerCount() == 0)
        {
            teamFactory = iTeamFactory.GetTeam("B").GetFactory();

        }
        else
        {
            teamFactory = iTeamFactory.GetTeam("R").GetFactory();
        }
        if (teamFactory == null){ throw new Exception("Shipfactory team problem "); }

        foreach (PlacedShip ship in shipList) //Adding different cannon strategies to different weapons
        {
            switch (ship.Type)
            {
                case "Boat":
                    IShip newShipB = teamFactory.CreateBoat(ship.X, ship.Y);
                    this.Board.PlaceShip(newShipB);
                    break;
                case "Lavantier":

                    IShip newShipL = teamFactory.CreateLavantier(ship.X, ship.Y);
                    this.Board.PlaceShip(newShipL);
                    break;
                case "Submarine":

                    IShip newShipS = teamFactory.CreateSubmarine(ship.X, ship.Y);
                    this.Board.PlaceShip(newShipS);
                    break;
                case "Destroyer":

                    IShip newShipD = teamFactory.CreateDestroyer(ship.X, ship.Y);
                    this.Board.PlaceShip(newShipD);
                    break;
                default:
                    throw new Exception("Invalid Ship type!");
            }
        }
        this.Board.PrintBoard();
    }

    public List<Player> CreatePlayer(string name)
    {
        MediatorImpl mediator = new MediatorImpl();
        this.Player = new Player(mediator, name);
        this.Player.SetShipsBoard(this.Board);

        List<Player> Players = ShipPlayers.AddPlayer(this.Player);
        return Players;
    }
    public List<Player> CreateRandomPlayer(Player player)
    {
        this.Player = player;
        this.Player.SetShipsBoard(this.Board);

        List<Player> Players = ShipPlayers.AddPlayer(this.Player);
        return Players;
    }
    public void SetBoard(ShipsBoard board)
    {
        this.Board = board;
    }

    public async void StartPlayers(List<Player> players)
    {
        if (players.Count % 2 == 0)
        {
            Random random = new Random();
            int random_number = random.Next(players.Count());
            Player luckyPlayer = players.ElementAt(random_number);
            luckyPlayer.SetState(true);
            ShipPlayers.UpdatePlayer(luckyPlayer.Name, luckyPlayer);
            
            ShipPlayers.UpdateCurrentRoundChain();

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


