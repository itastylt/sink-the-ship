﻿using BattleshipClient.GameLogic.Command;
using BattleshipClient.GameLogic.Composite;
using BattleshipClient.GameLogic.Factory;
using BattleshipClient.GameLogic.Visitor;
using Microsoft.AspNetCore.SignalR;
using System.Reflection;

namespace BattleshipClient.GameLogic.Invokers
{
    public class FireWeapon : ICommand
    {
        string _message;
        string _user;
        string _messageArgs;

        ShipHub _hub;

        public FireWeapon(string messageArgs, string message, string user, ShipHub hub) 
        { 
            _message = message;
            _messageArgs = messageArgs;
            _user = user;
            _hub = hub;
        }

        public async void execute()
        {
            int x_cord = int.Parse(_messageArgs);
            int y_cord = int.Parse(_message.Split(';')[2]);

            Player current_player = ShipPlayers.GetPlayer(_user);
            if (!current_player.GetState())
            {
                Console.WriteLine("Illegal player turn");
            }
            else
            {
                Player opponent_player = ShipPlayers.GetPlayerOpponent(_user);
                Console.WriteLine(String.Format(x_cord + " " + y_cord));
                current_player.GetSelectedShip().FireWeapon(opponent_player, x_cord, y_cord, 0);
                current_player.setLastShot(new List<int>() { x_cord,y_cord});
                current_player.setLastShotType((int)(Ship)Enum.Parse(typeof(Ship), current_player.GetSelectedShip().Type));
                int temp = current_player.getLastShotType();
                List<int> test = current_player.getLastShot(); 
                current_player.SetState(!current_player.GetState());
                opponent_player.SetState(!opponent_player.GetState());
                ShipPlayers.UpdatePlayer(current_player.Name, current_player);
                ShipPlayers.UpdatePlayer(opponent_player.Name, opponent_player);

                var damageVisitor = new DamageAssessmentVisitor(opponent_player.GetShipsBoard()); //Count damage
                ShipGroup group = opponent_player.GetShipsBoard().getShipGroup();
                group.Accept(damageVisitor);
                int totalDamage = damageVisitor.totalDamage;

                if (opponent_player.GetShipsBoard().BoardEnd())
                {
                    await _hub.Clients.All.SendAsync("RoundEnd", current_player.Name, current_player.Name + ";" + opponent_player.Name + ";");
                    ShipPlayers.UpdateCurrentRoundChain();
                }
                else
                {
                    await _hub.Clients.All.SendAsync("FireShot", current_player.Name, opponent_player.Name + ";" + opponent_player.GetShipsBoard().ToString() + ";" + opponent_player.Name + ";" + totalDamage + ";");
                }



            }
            
        }

        public async void undoAsync()
        {

            Player current_player = ShipPlayers.GetPlayer(_user);

            Player opponent_player = ShipPlayers.GetPlayerOpponent(_user);

            List<int> coordinates = current_player.getLastShot();
            int lastShotType = current_player.getLastShotType();

            current_player.GetSelectedShip().FireWeapon(opponent_player, coordinates[0], coordinates[1], 1);
            opponent_player.GetShipsBoard().PrintBoard();
            current_player.SetState(!current_player.GetState());
            opponent_player.SetState(!opponent_player.GetState());
            ShipPlayers.UpdatePlayer(current_player.Name, current_player);
            ShipPlayers.UpdatePlayer(opponent_player.Name, opponent_player);
            await _hub.Clients.All.SendAsync("FireShot", current_player.Name, opponent_player.Name + ";" + opponent_player.GetShipsBoard().ToString() + ";" + opponent_player.Name);


        }
    }
}
