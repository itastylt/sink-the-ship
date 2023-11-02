using BattleshipClient.GameLogic.Factory;
using BattleshipClient.GameLogic.Strategy;
using System.Runtime.ConstrainedExecution;

namespace BattleshipClient.GameLogic.Adapter
{
    public class ShipAdapter : ICannonStrategy
    {

        private SplashStrategy splashStrategy;

        public void Fire(Player opponent, int x, int y, int flag)
        {
            splashStrategy.Splash();
        }

    }
}
