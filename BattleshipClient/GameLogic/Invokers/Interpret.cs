using BattleshipClient.GameLogic.Command;
using BattleshipClient.GameLogic.Composite;
using BattleshipClient.GameLogic.Factory;
using BattleshipClient.GameLogic.Visitor;
using Microsoft.AspNetCore.SignalR;
using System.Reflection;

namespace BattleshipClient.GameLogic.Invokers
{
    public class Interpret : ICommand
    {
        string _message;
        string _user;
        string _messageArgs;

        ShipHub _hub;

        public Interpret(string messageArgs, string user, ShipHub hub)
        {
            _messageArgs = messageArgs;
            _user = user;
            _hub = hub;
        }


        public async void execute()
        {
            ExpressionParser expressionParser = new ExpressionParser(this._hub, this._user);
            Context context = new Context(_messageArgs, "");
            expressionParser.Interpret(context);
        }

        public async void undoAsync()
        {
            throw new NotImplementedException();
        }
    }
}
