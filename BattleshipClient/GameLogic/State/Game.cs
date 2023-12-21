namespace BattleshipClient.GameLogic.State
{
    public class Game
    {
        private GameState currentState;

        public Game() 
        { 
            StateController ctrl = new StateController();
            GameState defaultState = ctrl.getInitialState();
            currentState = defaultState;
        }

        public GameState operate()
        { 
            currentState.stateOperation();
            currentState.getNextState(this);
            
            return currentState;
        }

        public void setState(GameState nextState) { this.currentState = nextState; }
        public GameState getState() { return this.currentState; }
    }
}
