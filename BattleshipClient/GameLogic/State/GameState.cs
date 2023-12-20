namespace BattleshipClient.GameLogic.State
{
    public abstract class GameState
    {
        private GameState nextState;

        public void setNextState(GameState nextState) { this.nextState = nextState; }

        public void getNextState(Game context)
        { 
            context.setState(nextState);
        }

        public abstract void stateOperation();
    }
}
