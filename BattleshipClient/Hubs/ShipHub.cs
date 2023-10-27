using BattleshipClient.GameLogic.Invokers;
using BattleshipClient.GameLogic.Strategy;
using BattleshipClient.GameLogic.Strategy.Decorator;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

public class ShipHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        string messageType = message.Split(';')[0];
        string messageArgs = message.Split(';')[1];

        // Create commands
        Ready readyCommand = new Ready(user, messageArgs,this);
        SelectWeapon selectWeaponCommand = new SelectWeapon(messageArgs,user);
        FireWeapon fireWeaponCommand = new FireWeapon(messageArgs, message, user,this);
        CloneShip cloneShipCommand = new CloneShip(user,this);

        switch (messageType)
        {
            case "ready":
                readyCommand.execute();
                break;
            case "selectWeapon":
                selectWeaponCommand.execute();
                break;
            case "fireWeapon":
                fireWeaponCommand.execute();
                break;
            case "cloneShip":
                cloneShipCommand.execute();
                break;
            default:
                break;
        }
    }
}