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
        Ready readyCommand = new Ready(messageArgs, user,this);
        SelectWeapon selectWeaponCommand = new SelectWeapon(messageArgs,user);
        FireWeapon fireWeaponCommand = new FireWeapon(messageArgs, message, user,this);
        CloneShip cloneShipCommand = new CloneShip(user,this);


        switch (messageType)
        {
            case "ready":
                readyCommand.execute();
                break;
            case "undoReady":
                readyCommand.undo();
                break;
            case "selectWeapon":
                selectWeaponCommand.execute();
                break;
            case "unselectWeapon":
                selectWeaponCommand.undo();
                break;
            case "fireWeapon":
                fireWeaponCommand.execute();
                break;
            //Daryt
            case "unFireWeapon":
                fireWeaponCommand.undo();
                break;
            case "cloneShip":
                cloneShipCommand.execute();
                break;
            //Daryt
            case "unCloneShip":
                cloneShipCommand.undo();
                break;
            default:
                break;
        }
    }
}