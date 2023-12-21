using BattleshipClient.GameLogic.Invokers;
using BattleshipClient.GameLogic.Strategy;
using BattleshipClient.GameLogic.Strategy.Decorator;
using BattleshipClient.GameLogic.Template;
using Microsoft.AspNetCore.SignalR;
using System.Runtime.InteropServices;
using System.Text.Json;

public class ShipHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        string messageType = message.Split(';')[0];
        string messageArgs = message.Split(';')[1];

        // Create commands
        Ready readyCommand = new Ready(messageArgs, user,this);
        SelectWeapon selectWeaponCommand = new SelectWeapon(messageArgs,user, this);
        FireWeapon fireWeaponCommand = new FireWeapon(messageArgs, message, user,this);
        FireGroup fireGroupCommand = new FireGroup(messageArgs, message, user, this);
        CloneShip cloneShipCommand = new CloneShip(user,this);
        NextRound nextRound = new NextRound(messageArgs, user, this);
        Interpret interpret = new Interpret(messageArgs, user, this);

        switch (messageType)
        {
            case "ready":
                readyCommand.execute();
                break;
            case "undoReady":
                readyCommand.undoAsync();
                break;
            case "randomize":
                readyCommand.executeRandomPlayer();
                break;
            case "selectWeapon":
                selectWeaponCommand.execute();
                break;
            case "unselectWeapon":
                selectWeaponCommand.undoAsync();
                break;
            case "fireWeapon":
                fireWeaponCommand.execute();
                break;
            case "fireGroup":
                fireGroupCommand.execute();
                break;
            case "unFireWeapon":
                fireWeaponCommand.undoAsync();
                break;
            case "cloneShip":
                cloneShipCommand.execute();
                break;
            case "unCloneShip":
                cloneShipCommand.undoAsync();
                break;
            // State messages
            case "waitingForPause":
                WaitingForPause waitingForPause = new WaitingForPause(user, int.Parse(messageArgs), this);
                waitingForPause.execute();
                break;
            case "gameWaitingUnpaused":
                WaitingForUnpause waitingForUnpause = new WaitingForUnpause(user, int.Parse(messageArgs), this);
                waitingForUnpause.execute();
                break;
            case "nextRound":
                nextRound.execute();        
                break;
            case "interpret":
                interpret.execute();
                break;
            default:
                break;
        }
    }
}